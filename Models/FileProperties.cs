using DatabaseTask.ViewModels.Nodes;

namespace DatabaseTask.Models
{
    public class FileProperties
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Modificated { get; set; }

        public string IconPath { get; set; }

        public INode Node { get; set; }

        public FileProperties(string name, 
            string size, 
            string modificated,
            string iconPath,
            INode node)
        {
            Name = name;
            Size = size;
            Modificated = modificated;
            IconPath = iconPath;
            Node = node;
        }
    }
}
