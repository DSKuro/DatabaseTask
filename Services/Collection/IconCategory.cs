namespace DatabaseTask.Services.Collection
{
    public class IconCategory
    {
        public string Value { get; private set; }

        private IconCategory(string value)
        {
            Value = value;
        }

        public static IconCategory Folder { get { return new IconCategory("/Assets/ButtonIcons/folder-open.svg"); } }
        public static IconCategory File { get { return new IconCategory("/Assets/ButtonIcons/file-copy.svg");  } }
        public static IconCategory OpenedFolder { get { return new IconCategory("/Assets/ButtonIcon/folder-add.svg"); } }
    }
}
