using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models.AppData;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Models.StorageOptions;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using DatabaseTask.Services.Database.Utils.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Dialogues.Storage;
using DatabaseTask.Services.Messages;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.Logger.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.FileManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using Microsoft.Data.SqlClient;
using MsBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels
{
    public class OpenDataViewModel : ViewModelMessageBox, IOpenDataViewModel
    {
        private readonly IStorageService _storageService;
        private readonly IFileManager _fileManager;
        private readonly IFullPath _fullPath;
        private readonly ILogger _logger;
        private readonly ICommandsHistory _commandsHistory;
        private readonly IDatabaseUtils _databaseUtils;
        private readonly ITblDrawingContentsRepository _drawingRepository;
        private readonly ConnectionStringData _stringData;

        public OpenDataViewModel(IMessageBoxService messageBoxService,
            IStorageService storageService,
            IFileManager fileManager,
            IFullPath fullPath,
            ILogger logger,
            ICommandsHistory commandsHistory,
            IDatabaseUtils databaseUtils,
            ITblDrawingContentsRepository drawingRepository,
            ConnectionStringData stringData)
            : base(messageBoxService)
        {
            _storageService = storageService;
            _fileManager = fileManager;
            _fullPath = fullPath;
            _logger = logger;
            _commandsHistory = commandsHistory;
            _databaseUtils = databaseUtils;
            _drawingRepository = drawingRepository;
            _stringData = stringData;
        }

        public async Task<IEnumerable<IStorageFile>?> ChooseDbFile()
        {
            try
            {
                await ChooseDbFileImplementation();
            }
            catch (ArgumentNullException)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Аргумент не найден",
                    ButtonEnum.Ok));
            }
            catch (ArgumentException)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Неверный аргумент",
                    ButtonEnum.Ok));
            }
            catch (InvalidOperationException)
            {
                await MessageBoxHelper("MainDialogueWindow",new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Запрещённая операция",
                    ButtonEnum.Ok));
            }
            catch (SqlException)
            {
                _stringData.ConnectionString = string.Empty;
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Невозможно установить соединение с базой данных",
                    ButtonEnum.Ok));
            }
            return null;
        }

        private async Task ChooseDbFileImplementation()
        {
            IEnumerable<IStorageFile> files = await _storageService.OpenFilesAsync("MainDialogueWindow",
                   new FilePickerOptions(StorageConstants.BaseStorage.Value,
                   StorageConstants.BaseStorage.Type));
            IStorageFile? file = files.FirstOrDefault();
            if (file != null)
            {
                string connectionString = _databaseUtils.BuildConnectionString(file.Path.LocalPath);
                _stringData.ConnectionString = connectionString;
                OpenDatabase();
            }
        }

        private void OpenDatabase()
        {    
            if (_databaseUtils.IsDatabaseExist())
            {
                _databaseUtils.DetachDatabase();
            }
            _drawingRepository.GetFirstItem();
        }

        public async Task OpenFolder()
        {
            IEnumerable<IStorageFolder>? folders = await ChooseMainFolder();

            if (folders == null || folders.Count() == 0)
            {
                if (_fileManager.TreeView.Nodes.Count != 0)
                {
                    WeakReferenceMessenger.Default.Send(new MainWindowToggleManagerButtons(true));
                }
                return;
            }

            if (folders.Count() != 0)
            {
                _logger.LogOperations.Clear();
            }

            foreach (IStorageFolder folder in folders)
            {
                _fullPath.PathToCoreFolder = folder.Path.LocalPath;
            }
            await _fileManager.GetCollectionFromFolders(folders);
            _fileManager.TreeViewFunctionality.AddSelectedNodeByIndex(0);
            WeakReferenceMessenger.Default.Send(new MainWindowToggleManagerButtons(true));
            _commandsHistory.ClearAll();
        }

        private async Task<IEnumerable<IStorageFolder>?> ChooseMainFolder()
        {
            try
            {
                return await ChooseMainFolderImpl();
            }
            catch (ArgumentNullException)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Аргумент не найден",
                    ButtonEnum.Ok));
            }
            catch (ArgumentException)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Неверный аргумент",
                    ButtonEnum.Ok));
            }
            catch (InvalidOperationException)
            {
                await MessageBoxHelper("MainDialogueWindow",new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Запрещённая операция",
                    ButtonEnum.Ok));
            }
            return null;
        }

        private async Task<IEnumerable<IStorageFolder>> ChooseMainFolderImpl()
        {
            WeakReferenceMessenger.Default.Send(new MainWindowToggleManagerButtons(false));
            IEnumerable<IStorageFolder> folders = await _storageService.OpenFoldersAsync("MainDialogueWindow",
                    new FolderPickerOptions(false));
            return folders;
        }
    }
}
