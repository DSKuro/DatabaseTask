using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models.AppData;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.AnalyseServices.Interfaces;
using DatabaseTask.Services.Database;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using DatabaseTask.Views.Analyse;
using MsBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels
{
    public class DatabaseInteractionViewModel : ViewModelMessageBox, IDatabaseInteractionViewModel
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly IFindDuplicatesService _findDuplicatesService;
        private readonly IFindUnusedFilesServices _findUnusedFilesServices;
        private readonly ConnectionStringData _connectionStringData;

        public DatabaseInteractionViewModel(
            IMessageBoxService messageBoxService,
            ITreeViewFunctionality treeViewFunctionality,
            IFindDuplicatesService findDuplicatesService,
            IFindUnusedFilesServices findUnusedFilesServices,
            ConnectionStringData connectionStringData)
            : base(messageBoxService)
        {
            _treeViewFunctionality = treeViewFunctionality;
            _findDuplicatesService = findDuplicatesService;
            _findUnusedFilesServices = findUnusedFilesServices;
            _connectionStringData = connectionStringData;
        }

        public async Task FindDuplicates()
        {
            //if (!await ValidateCatalogAndDatabaseAsync())
            //{
            //    return;
            //}

            var duplicates = _findDuplicatesService.FindDuplicatesByNameAndSize();

            //foreach (var group in duplicatesByHash)
            //{
            //    Console.WriteLine($"Хэш: {group.Hash}");

            //    foreach (var file in group.Files)
            //    {
            //        Console.WriteLine($"  - Путь: {file.FullName}");
            //    }
            //    Console.WriteLine(new string('-', 30));
            //}
            //foreach (var group in duplicates)
            //{
            //    Console.WriteLine($"Файл: {group.key}");

            //    foreach (var file in group.files)
            //    {
            //        Console.WriteLine($"  - Путь: {file.FullName}");
            //    }
            //    Console.WriteLine(new string('-', 30));
            //}

            var result = await WeakReferenceMessenger.Default.Send<MainWindowDuplicatesFilesMessage>();
        }

        public async Task FindUnusedFiles()
        {
            //if (!await ValidateCatalogAndDatabaseAsync())
            //{
            //    return;
            //}

            //var unusedFiles = _findUnusedFilesServices.FindUnusedFiles();
            var result = await WeakReferenceMessenger.Default.Send<MainWindowUnusedFilesMessage>();
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
