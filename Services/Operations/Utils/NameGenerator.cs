using DatabaseTask.Services.Operations.Utils.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DatabaseTask.Services.Operations.Utils
{
    public class NameGenerator : INameGenerator
    {
        private List<string> _copyNames = new List<string>()
        {
            "{0} - копия{1}",
            "{0} - копия",
            "{0} - копия ({1}){2}",
            "{0} - копия ({1})"
        };

        private List<string> _moveNames = new List<string>()
        {
            "{0}{1}",
            "{0}",
            "{0} ({1}){2}",
            "{0} ({1})"
        };


        public string GenerateUniqueCopyName(INode parent, string baseName)
        {
            return GenerateName(parent, baseName, _copyNames);
        }

        public string GenerateUniqueName(INode parent, string baseName)
        {
            return GenerateName(parent, baseName, _moveNames);
        }

        private string GenerateName(INode parent, string baseName, List<string> names)
        {
            int copyNumber = 1;
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(baseName);
            string extension = Path.GetExtension(baseName);
            bool hasExtension = !string.IsNullOrEmpty(extension);

            string newName = hasExtension ? string.Format(names[0], nameWithoutExtension, extension)
                : string.Format(names[1], nameWithoutExtension);

            while (parent.Children.Any(x => x.Name == newName))
            {
                newName = hasExtension ? string.Format(names[2], nameWithoutExtension, copyNumber, extension)
                : string.Format(names[3], baseName, copyNumber);
                copyNumber++;
            }

            return newName;
        }
    }
}
