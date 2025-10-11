using DatabaseTask.Services.Operations.Utils.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Linq;

namespace DatabaseTask.Services.Operations.Utils
{
    public class NameGenerator : INameGenerator
    {
        public string GenerateUniqueName(INode parent, string baseName)
        {
            string newName = baseName;
            int copyNumber = 1;
            string[] parts = baseName.Split('.');

            while (parent.Children.Any(x => x.Name == newName))
            {
                newName = $"{parts[0]} ({copyNumber}).{parts[1]}";
                copyNumber++;
            }

            return newName;
        }
    }
}
