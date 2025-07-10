using DatabaseTask.Services.ParametrizedStringImplementation;
using DatabaseTask.Services.ParametrizedStringImplementation.Interfaces;

namespace DatabaseTask.Services.Collection
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
                return new LogCategory(new ParametrizedString($"CREATE DIRECTORY \"?\""));
            }
        }
    }
}
