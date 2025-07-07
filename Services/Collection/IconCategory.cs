namespace DatabaseTask.Services._serviceCollection
{
    public class IconCategory
    {
        public string Value { get; private set; }

        private IconCategory(string value)
        {
            Value = value;
        }

        public static IconCategory Folder { get { return new IconCategory("/Assets/ButtonIcons/folder.svg"); } }
        public static IconCategory File { get { return new IconCategory("/Assets/ButtonIcons/file.svg");  } }
        public static IconCategory OpenedFolder { get { return new IconCategory("/Assets/ButtonIcons/folder-selected.svg"); } }
    }
}
