using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Remote.Protocol.Input;
using DatabaseTask.ViewModels;
using System;

namespace DatabaseTask.Services.TreeView
{
    public interface ITreeViewItem : ITreeViewItemLogic, IDragDrop
    {
        public bool Pressed { get; set; }
        public Visual _mainWindow { get; set; }
        public Visual _treeViewControl { get; set; }
        public ScrollViewer _scrollViewer { get; set; }
        public MainWindowViewModel MainWindowViewModel { get; set; }
        public EventHandler<PointerPressedEventArgs> PressedEvent { get; }
        public EventHandler<PointerEventArgs> PointerMovedEvent { get; }
        public EventHandler<PointerReleasedEventArgs> ReleasedEvent { get; }
    }
}
