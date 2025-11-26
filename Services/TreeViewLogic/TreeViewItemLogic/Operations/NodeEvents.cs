using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations
{
    public class NodeEvents : INodeEvents
    {
        public Func<List<INode>, Task> OnDrop { get; set; } = _ => Task.CompletedTask;
    }
}
