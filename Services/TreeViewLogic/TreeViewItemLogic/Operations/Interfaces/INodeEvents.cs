using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations.Interfaces
{
    public interface INodeEvents
    {
        public Func<List<INode>, bool, bool, Task> OnDrop { get; set; }
    }
}
