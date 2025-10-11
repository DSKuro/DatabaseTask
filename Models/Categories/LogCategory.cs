using DatabaseTask.Services.ParametrizedStringImplementation;
using DatabaseTask.Services.ParametrizedStringImplementation.Interfaces;

namespace DatabaseTask.Models.Categories
{
    public class LogCategory
    {
        public IParametrizedString Value { get; private set; }

        public LogCategory(IParametrizedString value)
        {
            Value = value;
        }

        public static LogCategory CreateFolderCategory
        {
            get
            {
                return new LogCategory(new ParametrizedString("CREATE DIRECTORY \"{?}\""));
            }
        }
        public static LogCategory RenameFolderCategory
        {
            get
            {
                return new LogCategory(new ParametrizedString("RENAME DIRECTORY \"{?}\" TO \"{?}\""));
            }
        }
        public static LogCategory DeleteFolderCategory
        {
            get
            {
                return new LogCategory(new ParametrizedString("DELETE DIRECTORY \"{?}\""));
            }
        }
        public static LogCategory CopyFolderCategory
        {
            get
            {
                return new LogCategory(new ParametrizedString("COPY DIRECTORY \"{?}\" TO \"{?}\""));
            }
        }
        public static LogCategory MoveFileCategory
        {
            get
            {
                return new LogCategory(new ParametrizedString("MOVE FILE \"{?}\" TO \"{?}\""));
            }
        }
        public static LogCategory CopyFileCategory
        {
            get
            {
                return new LogCategory(new ParametrizedString("COPY FILE \"{?}\" TO \"{?}\""));
            }
        }
        public static LogCategory DeleteFileCategory
        {
            get
            {
                return new LogCategory(new ParametrizedString("DELETE FILE \"{?}\""));
            }
        }
    }
}
