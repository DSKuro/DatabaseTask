using DatabaseTask.Services.Comparer.Interfaces;
using System;
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
            public bool HasCopyText;
            public bool HasNumber;
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

            int baseCompare = CompareWithStringEx(xInfo.BaseName, yInfo.BaseName);
            if (baseCompare != 0)
                return baseCompare;

            return CompareCopyRules(xInfo, yInfo);
        }

        private FileInfoEx ExtractFileInfo(string path)
        {
            string fullName = Path.GetFileName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);

            var (hasNumber, copyNum, hasCopy, baseName) = ExtractFileMetadata(name);

            return new FileInfoEx
            {
                BaseName = baseName,
                FullName = fullName,
                Extension = ext,
                IsCopy = hasCopy || hasNumber,
                CopyNumber = copyNum,
                HasCopyText = hasCopy,
                HasNumber = hasNumber
            };
        }

        private (bool hasNumber, int copyNum, bool hasCopy, string baseName) ExtractFileMetadata(string name)
        {
            var numberMatch = NumberRegex.Match(name);
            bool hasNumber = numberMatch.Success;
            int copyNum = hasNumber ? int.Parse(numberMatch.Groups[1].Value) : 0;

            bool hasCopy = CopyRegex.IsMatch(name);

            string baseName = name;

            if (hasCopy)
                baseName = CopyRegex.Replace(baseName, "").Trim();

            if (hasNumber)
                baseName = NumberRegex.Replace(baseName, "").Trim();

            if (hasCopy && !hasNumber)
            {
                copyNum = 1;
            }

            return (hasNumber, copyNum, hasCopy, baseName.Trim());
        }

        private int CompareCopyRules(FileInfoEx x, FileInfoEx y)
        {
            if (!x.IsCopy && !y.IsCopy)
                return 0;

            if (!x.IsCopy && y.IsCopy) return -1;
            if (x.IsCopy && !y.IsCopy) return 1;

            if (x.CopyNumber != y.CopyNumber)
                return x.CopyNumber.CompareTo(y.CopyNumber);

            if (x.HasCopyText != y.HasCopyText)
                return x.HasCopyText ? 1 : -1;

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
