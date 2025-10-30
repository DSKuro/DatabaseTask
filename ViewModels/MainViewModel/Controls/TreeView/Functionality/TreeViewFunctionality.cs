using DatabaseTask.Models;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using ExCSS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Functionality
{
    public class TreeViewFunctionality : ITreeViewFunctionality
    {
        private readonly ITreeView _treeView;

        private readonly StringComparer _stringComparer;

        public TreeViewFunctionality(ITreeView treeView)
        {
            _treeView = treeView;
            CultureInfo culture = new CultureInfo("en-EN");
            _stringComparer = StringComparer.Create(culture, false);
        }

        public bool IsNodeExist(INode parent, string name)
        {
            if (parent == null)
            {
                return false;
            }

            return parent.Children.Any(x => x.Name == name);
        }

        public bool IsParentHasNodeWithName(INode node, string name)
        {
            if (node != null && node.Parent != null)
            {
                return node.Parent.Children
                    .Any(x => x.Name == name);
            }

            return false;
        }

        public bool TryInsertNode(INode parent, INode node, out int index)
        {
            index = GetNodePositionIndex(parent, node);
            if (index == -1)
            {
                return false;
            }
            parent.Children.Insert(index, node);
            return true;
        }

        public void AddSelectedNodeByIndex(int index)
        {
            if (index < 0 || index > _treeView.Nodes.Count)
            {
                return;
            }

            _treeView.SelectedNodes.Add(_treeView.Nodes[index]);
        }

        public INode? CreateNode(INode template, INode parent)
        {
            if (template is NodeViewModel nodeTemplate)
            {
                return new NodeViewModel()
                {
                    Name = nodeTemplate.Name,
                    IsExpanded = nodeTemplate.IsExpanded,
                    IsFolder = nodeTemplate.IsFolder,
                    IconPath = nodeTemplate.IconPath,
                    Parent = parent,
                    Children = new SmartCollection<INode>()
                };
            }
            return null;
        }

        public int GetNodePositionIndex(INode target, INode node)
        {
            NodeViewModel? nodeModel = node as NodeViewModel;
            if (nodeModel == null)
            {
                return -1;
            }

            int relativeIndex = GetRelativeIndex(target, nodeModel);
            if (relativeIndex == -1 || nodeModel.IsFolder)
            {
                return relativeIndex;
            }

            int lastFolderIndex = GetLastFolderIndex(target);

            if (lastFolderIndex == -1)
            {
                return relativeIndex;
            }

            return relativeIndex + lastFolderIndex;
        }

        private int GetRelativeIndex(INode parent, NodeViewModel node)
        {
            var comparer = new AdvancedExplorerComparer();

            var sameTypeChildren = parent.Children
                .OfType<NodeViewModel>()
                .Where(x => x.IsFolder == node.IsFolder)
                .OrderBy(f => f.Name, comparer)
                .ToList();

            // Найти индекс первого элемента, который должен быть после нового узла
            int insertIndexInSameType = sameTypeChildren
                .FindIndex(x => comparer.Compare(node.Name, x.Name) < 0);

            int insertIndexInParent;

            if (insertIndexInSameType == -1)
            {
                // Вставляем в конец среди того же типа
                var lastSameType = sameTypeChildren.LastOrDefault();
                insertIndexInParent = lastSameType != null
                    ? parent.Children.IndexOf(lastSameType) + 1
                    : 0; // если нет ни одного — просто в начало
            }
            else
            {
                // Вставляем перед найденным элементом
                var targetNode = sameTypeChildren[insertIndexInSameType];
                insertIndexInParent = parent.Children.IndexOf(targetNode);
            }

            // Если индекс вне диапазона — вставляем в конец
            if (insertIndexInParent < 0 || insertIndexInParent > parent.Children.Count)
                insertIndexInParent = parent.Children.Count;

            return insertIndexInParent;

        }

        private int GetLastFolderIndex(INode target)
        {
            return target.Children
                .Select((x, index) => new { Node = x as NodeViewModel, Index = index })
                .Where(x => x.Node != null && x.Node.IsFolder)
                .LastOrDefault()?.Index ?? -1;
        }

        public INode? GetFirstSelectedNode()
        {
            return _treeView.SelectedNodes[0] ?? null;
        }

        public List<INode> GetAllSelectedNodes()
        {
            return _treeView.SelectedNodes.ToList();
        }

        public void UpdateSelectedNodes(INode node)
        {
            INode? selectedNode = GetFirstSelectedNode();
            if (selectedNode != null)
            {
                selectedNode.IsExpanded = true;
            }
            _treeView.SelectedNodes.Clear();
            _treeView.SelectedNodes.Add(node);
        }

        public INode? GetChildrenByName(INode node, string name)
        {
            return node.Children.FirstOrDefault(x => x.Name == name);
        }

        public void RemoveNode(INode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Children.Remove(node);
            }
        }

        public void RemoveSelectedNodes(INode node)
        {
            _treeView.SelectedNodes.Remove(node);
        }
    }

    public class NaturalFileComparer : IComparer<string>
    {
        private static readonly Regex CopyPattern = new Regex(@"^(.*?)(?:\s-\sкопия)?(?:\s\((\d+)\))?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex TokenRegex = new Regex(@"\d+|\D+", RegexOptions.Compiled);

        public int Compare(string? x, string? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x is null) return -1;
            if (y is null) return 1;

            // сравниваем полные имена (включая расширение)
            string xFull = x;
            string yFull = y;

            // базовые имена без расширений
            string xBase = Path.GetFileNameWithoutExtension(xFull);
            string yBase = Path.GetFileNameWithoutExtension(yFull);

            // обработка " - копия" и "(N)"
            var mx = CopyPattern.Match(xBase);
            var my = CopyPattern.Match(yBase);

            if (mx.Success && my.Success)
            {
                string xr = mx.Groups[1].Value.Trim();
                string yr = my.Groups[1].Value.Trim();

                int cmp = string.Compare(xr, yr, StringComparison.CurrentCultureIgnoreCase);
                if (cmp != 0) return cmp;

                bool xHasCopy = xBase.IndexOf("копия", StringComparison.OrdinalIgnoreCase) >= 0;
                bool yHasCopy = yBase.IndexOf("копия", StringComparison.OrdinalIgnoreCase) >= 0;
                if (xHasCopy != yHasCopy)
                    return xHasCopy ? 1 : -1; // файл без "копия" идёт раньше

                // сравнение номера в скобках, если есть
                var xNumGroup = mx.Groups[2];
                var yNumGroup = my.Groups[2];
                if (xNumGroup.Success && yNumGroup.Success)
                {
                    if (long.TryParse(xNumGroup.Value, out long xi) && long.TryParse(yNumGroup.Value, out long yi))
                    {
                        int numCmp = xi.CompareTo(yi);
                        if (numCmp != 0) return numCmp;
                    }
                }
                else if (xNumGroup.Success) return 1; // без номера будет раньше
                else if (yNumGroup.Success) return -1;
            }

            // fallback: токенизация по цифрам и строкам
            var xt = TokenRegex.Matches(xBase);
            var yt = TokenRegex.Matches(yBase);
            int n = Math.Min(xt.Count, yt.Count);
            for (int i = 0; i < n; i++)
            {
                string a = xt[i].Value;
                string b = yt[i].Value;
                bool aNum = char.IsDigit(a[0]);
                bool bNum = char.IsDigit(b[0]);

                if (aNum && bNum)
                {
                    string at = a.TrimStart('0');
                    string bt = b.TrimStart('0');
                    if (at.Length != bt.Length) return at.Length.CompareTo(bt.Length);
                    int c = string.CompareOrdinal(at, bt);
                    if (c != 0) return c;
                }
                else if (!aNum && !bNum)
                {
                    int c = string.Compare(a, b, StringComparison.CurrentCultureIgnoreCase);
                    if (c != 0) return c;
                }
                else
                {
                    return aNum ? -1 : 1;
                }
            }

            int tcmp = xt.Count.CompareTo(yt.Count);
            if (tcmp != 0) return tcmp;

            // последний этап: сравнение полных имён с расширением
            return string.Compare(xFull, yFull, StringComparison.CurrentCultureIgnoreCase);
        }
    }

    public class WindowsExplorerComparer : IComparer<string>
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string psz1, string psz2);

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
        private const uint SORT_DIGITSASNUMBERS = 0x00000008; // включает "natural sort"

        private readonly bool forceEnglishLocale;

        public WindowsExplorerComparer(bool forceEnglishLocale = true)
        {
            this.forceEnglishLocale = forceEnglishLocale;
        }

        public int Compare(string? x, string? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            if (forceEnglishLocale)
            {
                // Используем CompareStringEx с en-US
                int result = CompareStringEx(
                    "en-US",
                    SORT_DIGITSASNUMBERS,
                    x, -1,
                    y, -1,
                    IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

                return result switch
                {
                    CSTR_LESS_THAN => -1,
                    CSTR_EQUAL => 0,
                    CSTR_GREATER_THAN => 1,
                    _ => StringComparer.CurrentCultureIgnoreCase.Compare(x, y)
                };
            }
            else
            {
                // Обычное поведение как у Проводника с локалью системы
                return StrCmpLogicalW(x, y);
            }
        }
    }

    public class AdvancedExplorerComparer : IComparer<string>
    {
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

            var xInfo = ExtractFileInfo(x);
            var yInfo = ExtractFileInfo(y);

            bool involvesCopies = xInfo.IsCopy || yInfo.IsCopy;

            if (involvesCopies)
            {
                // ⚙️ 1. Если есть "копия" или "(2)" — используем кастомные правила
                int result = CompareCopyRules(xInfo, yInfo);
                if (result != 0)
                    return result;
            }

            // ⚙️ 2. Обычное сравнение с английской локалью (как в Windows Explorer)
            int cmp = CompareStringEx(
                "en-US",
                SORT_DIGITSASNUMBERS,
                x, -1,
                y, -1,
                IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            return cmp switch
            {
                CSTR_LESS_THAN => -1,
                CSTR_EQUAL => 0,
                CSTR_GREATER_THAN => 1,
                _ => StringComparer.OrdinalIgnoreCase.Compare(x, y)
            };
        }

        private int CompareCopyRules(FileInfoEx x, FileInfoEx y)
        {
            // 🔹 Оригиналы всегда перед копиями
            if (!x.IsCopy && y.IsCopy) return -1;
            if (x.IsCopy && !y.IsCopy) return 1;

            // 🔹 Если оба копии — сравниваем базовое имя
            int baseCmp = CompareStringEx(
                "en-US",
                SORT_DIGITSASNUMBERS,
                x.BaseName, -1,
                y.BaseName, -1,
                IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            if (baseCmp != CSTR_EQUAL)
                return baseCmp == CSTR_LESS_THAN ? -1 : 1;

            // 🔹 Затем по номеру (1), (2), (10)
            if (x.CopyNumber != y.CopyNumber)
                return x.CopyNumber.CompareTo(y.CopyNumber);

            // 🔹 Если всё одинаково — по полному имени
            int fullCmp = CompareStringEx(
                "en-US",
                SORT_DIGITSASNUMBERS,
                x.FullName, -1,
                y.FullName, -1,
                IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            return fullCmp == CSTR_LESS_THAN ? -1 :
                   fullCmp == CSTR_GREATER_THAN ? 1 : 0;
        }

        private FileInfoEx ExtractFileInfo(string path)
        {
            string fullName = Path.GetFileName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);

            var matchNum = NumberRegex.Match(name);
            bool hasNumber = matchNum.Success;
            int copyNum = hasNumber ? int.Parse(matchNum.Groups[1].Value) : 0;

            bool hasCopy = CopyRegex.IsMatch(name);

            string baseName = name;
            if (hasCopy)
                baseName = CopyRegex.Replace(baseName, "").Trim();
            if (hasNumber)
                baseName = NumberRegex.Replace(baseName, "").Trim();

            return new FileInfoEx
            {
                BaseName = baseName,
                FullName = fullName,
                Extension = ext,
                IsCopy = hasCopy || hasNumber,
                CopyNumber = copyNum
            };
        }

        private struct FileInfoEx
        {
            public string BaseName;
            public string FullName;
            public string Extension;
            public bool IsCopy;
            public int CopyNumber;
        }
    }

    public class NativeExplorerComparer : IComparer<string>
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int StrCmpLogicalW(string x, string y);

        public int Compare(string x, string y)
        {
            return StrCmpLogicalW(x ?? "", y ?? "");
        }
    }

    public class ExplorerLogicalComparer : IComparer<string>
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int StrCmpLogicalW(string psz1, string psz2);

        public int Compare(string? x, string? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x is null) return -1;
            if (y is null) return 1;
            return StrCmpLogicalW(x, y);
        }
    }


}
