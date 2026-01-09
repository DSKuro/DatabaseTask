using DatabaseTask.Models.AppData;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Database;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FilesOperations;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using MsBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels
{
    public class DatabaseInteractionViewModel : ViewModelMessageBox, IDatabaseInteractionViewModel
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly IFullPath _fullPath;
        private readonly ConnectionStringData _connectionStringData;

        public DatabaseInteractionViewModel(
            IMessageBoxService messageBoxService,
            ITreeViewFunctionality treeViewFunctionality,
            IFullPath fullPath,
            ConnectionStringData connectionStringData)
            : base(messageBoxService)
        {
            _treeViewFunctionality = treeViewFunctionality;
            _fullPath = fullPath;
            _connectionStringData = connectionStringData;
        }

        public async Task FindDuplicates()
        {
            if (!await ValidateCatalogAndDatabaseAsync())
            {
                return;
            }

            string? path = _fullPath.PathToCoreFolder;
            if (path == null)
            {
                return;
            }
            DirectoryInfo dir = new DirectoryInfo(path);
            var duplicatesByNameAndSize = dir.GetFiles("*.*", SearchOption.AllDirectories)
                .GroupBy(file => new { file.Name, file.Length })
                .Where(group => group.Count() > 1)             
                .Select(group => new {
                    Key = group.Key,
                    Files = group.ToList()
                });

            string GetHash(string filePath)
            {
                using (var sha256 = SHA256.Create())
                using (var stream = File.OpenRead(filePath))
                {
                    return BitConverter.ToString(sha256.ComputeHash(stream));
                }
            }

            var duplicatesByHash = dir.GetFiles("*.*", SearchOption.AllDirectories)
                .GroupBy(file => file.Length)
                .Where(sizeGroup => sizeGroup.Count() > 1)
                .SelectMany(sizeGroup => sizeGroup)
                .GroupBy(file => GetHash(file.FullName))
                .Where(hashGroup => hashGroup.Count() > 1)
                .Select(hashGroup => new
                {
                    Hash = hashGroup.Key,
                    Files = hashGroup.ToList()
                });

            foreach (var group in duplicatesByHash)
            {
                Console.WriteLine($"Хэш: {group.Hash}");

                foreach (var file in group.Files)
                {
                    Console.WriteLine($"  - Путь: {file.FullName}");
                }
                Console.WriteLine(new string('-', 30));
            }
            //foreach (var group in duplicatesByNameAndSize)
            //{
            //    Console.WriteLine($"Файл: {group.Key.Name} | Размер: {group.Key.Length} байт");

            //    foreach (var file in group.Files)
            //    {
            //        Console.WriteLine($"  - Путь: {file.FullName}");
            //    }
            //    Console.WriteLine(new string('-', 30));
            //}
        }

        public async Task FindUnusedFiles()
        {
            if (!await ValidateCatalogAndDatabaseAsync())
            {
                return;
            }

            using var context = new DataContext(_connectionStringData.ConnectionString);
            var drawingContents = context.TblDrawingContents
                .Where(dc => dc.ContentDevice != null &&
                             context.TblDevices.Any(d => d.DeviceId == dc.ContentDevice))
                .Select(t => t.ContentDocument)
                .ToList();
            string? path = _fullPath.PathToCoreFolder;
            if (path == null)
            {
                return;
            }
            DirectoryInfo dir = new DirectoryInfo(path);
            string[] files = Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
            List<string> filesList = new List<string>();
            List<long> sizes = new List<long>();

            Uri baseUri = new Uri(path);

            foreach (string file in files)
            {
                string te = Path.GetRelativePath(path, file);
                string result = Path.Combine(".", te);
                filesList.Add(result);
            }

            List<string> additional = new List<string>();
            foreach (string file in filesList)
            {
                if (!drawingContents.Contains(file))
                {
                    additional.Add(file);
                    FileInfo fileInfo = new FileInfo(path + file);
                    sizes.Add(fileInfo.Length);
                }
            }
        }

        private async Task<bool> ValidateCatalogAndDatabaseAsync()
        {
            if (IsCatalogAndDatabaseNotChosen())
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "База данных или каталог не выбраны",
                    ButtonEnum.Ok));
                return false;
            }
            return true;
        }

        private bool IsCatalogAndDatabaseNotChosen()
        {
            return string.IsNullOrWhiteSpace(_connectionStringData.ConnectionString)
                || _treeViewFunctionality.GetCoreNode() == null;
        }
    }
}
