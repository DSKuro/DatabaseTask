using DatabaseTask.Services.Operations.Utils.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.IO;
using System.Linq;

namespace DatabaseTask.Services.Operations.Utils
{
    public class NameGenerator : INameGenerator
    {
        public string GenerateUniqueCopyName(INode parent, string baseName)
        {
            int copyNumber = 1;

            string nameWithoutExtension = Path.GetFileNameWithoutExtension(baseName);
            string extension = Path.GetExtension(baseName);
            bool hasExtension = !string.IsNullOrEmpty(extension);

            string newName = hasExtension
                ? $"{nameWithoutExtension} - копия{extension}"
                : $"{baseName} - копия";

            while (parent.Children.Any(x => x.Name == newName))
            {
                newName = hasExtension
                    ? $"{nameWithoutExtension} - копия ({copyNumber}){extension}"
                    : $"{baseName} - копия ({copyNumber})";
                copyNumber++;
            }

            return newName;
        }

        public string GenerateUniqueName(INode parent, string baseName)
        {
            string newName = baseName;
            int copyNumber = 1;

            string nameWithoutExtension = Path.GetFileNameWithoutExtension(baseName);
            string extension = Path.GetExtension(baseName);
            bool hasExtension = !string.IsNullOrEmpty(extension);

            while (parent.Children.Any(x => x.Name == newName))
            {
                newName = hasExtension
                    ? $"{nameWithoutExtension} ({copyNumber}){extension}"
                    : $"{baseName} ({copyNumber})";
                copyNumber++;
            }

            return newName;
        }
    }
}
