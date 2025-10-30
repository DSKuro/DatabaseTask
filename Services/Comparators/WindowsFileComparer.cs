using DatabaseTask.Services.Comparer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace DatabaseTask.Services.Comparer
{
    public class WindowsFileComparer : IWindowsFileComparer
    {
        private struct FileInfoEx
        {
            public string BaseName;
            public string FullName;
            public string Extension;
            public bool IsCopy;
            public int CopyNumber;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int CompareStringEx(
            string lpLocaleName,
            uint dwCmpFlags,
            string lpString1,
            int cchCount1,
            string lpString2,
            int cchCount2,
            IntPtr lpVersionInformation,
            IntPtr lpReserved,
            IntPtr lParam);

        private const int CSTR_LESS_THAN = 1;
        private const int CSTR_EQUAL = 2;
        private const int CSTR_GREATER_THAN = 3;
        private const uint SORT_DIGITSASNUMBERS = 0x00000008;

        private static readonly Regex NumberRegex = new(@"\((\d+)\)", RegexOptions.Compiled);
        private static readonly Regex CopyRegex = new(@"[-–—]\s*(копия|copy)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public int Compare(string? x, string? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            FileInfoEx xInfo = ExtractFileInfo(x);
            FileInfoEx yInfo = ExtractFileInfo(y);

            if (IsCopies(xInfo, yInfo))
            {
                int result = CompareCopyRules(xInfo, yInfo);
                if (result != 0)
                    return result;
            }

            return CompareWithStringEx(x, y);
        }

        private FileInfoEx ExtractFileInfo(string path)
        {
            string fullName = Path.GetFileName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);

            (bool hasNumber, int copyNum) = ExtractNumberInfo(name);
            (bool hasCopy, string baseName) = ExtractCopyInfo(name, hasNumber);

            return new FileInfoEx
            {
                BaseName = baseName,
                FullName = fullName,
                Extension = ext,
                IsCopy = hasCopy || hasNumber,
                CopyNumber = copyNum
            };
        }

        private (bool hasNumber, int copyNum) ExtractNumberInfo(string name)
        {
            Match? matchNum = NumberRegex.Match(name);
            bool hasNumber = matchNum.Success;
            int copyNum = hasNumber ? int.Parse(matchNum.Groups[1].Value) : 0;
            return (hasNumber, copyNum);
        }

        private (bool hasCopy, string baseName) ExtractCopyInfo(string name, bool hasNumber)
        {
            string baseName = name;
            bool hasCopy = CopyRegex.IsMatch(name);

            if (hasCopy)
                baseName = CopyRegex.Replace(baseName, "").Trim();
            if (hasNumber)
                baseName = NumberRegex.Replace(baseName, "").Trim();

            return (hasCopy, baseName);
        }

        private bool IsCopies(FileInfoEx x, FileInfoEx y)
        {
            return x.IsCopy || y.IsCopy;
        }

        private int CompareCopyRules(FileInfoEx x, FileInfoEx y)
        {
            if (!x.IsCopy && y.IsCopy) return -1;
            if (x.IsCopy && !y.IsCopy) return 1;

            int baseCmp = CompareWithStringEx(x.BaseName, y.BaseName);
            if (baseCmp != CSTR_EQUAL)
                return baseCmp == CSTR_LESS_THAN ? -1 : 1;

            if (x.CopyNumber != y.CopyNumber)
                return x.CopyNumber.CompareTo(y.CopyNumber);

            return CompareWithStringEx(x.FullName, y.FullName);
        }

        private int CompareWithStringEx(string x, string y)
        {
            int cmp = CompareStringEx(
                "en-US",
                SORT_DIGITSASNUMBERS,
                x, -1,
                y, -1,
                IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            return ConvertStringExResult(cmp, x, y);
        }

        private int ConvertStringExResult(int stringExResult, string x, string y)
        {
            return stringExResult switch
            {
                CSTR_LESS_THAN => -1,
                CSTR_EQUAL => 0,
                CSTR_GREATER_THAN => 1,
                _ => StringComparer.OrdinalIgnoreCase.Compare(x, y)
            };
        }
    }
}
