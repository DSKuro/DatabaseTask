using DatabaseTask.Services.ParametrizedStringImplementation;
using DatabaseTask.Services.ParametrizedStringImplementation.Interfaces;

namespace DatabaseTask.Models.Categories
{
    public class ParametrizedMessageBoxCategory
    {
        public string Title { get; private set; }
        public IParametrizedString Content { get; private set; }
        
        public ParametrizedMessageBoxCategory(string title, IParametrizedString
            content)
        {
            Title = title; 
            Content = content;
        }

        public static ParametrizedMessageBoxCategory RenameCatalogMergeMessageBox
        {
            get
            {
                return new ParametrizedMessageBoxCategory("Предупреждение",
                    new ParametrizedString("Каталог \"{?}\" уже существует в каталоге \"{?}\". Хотите выполнить слияние?"));
            }
        }
        public static ParametrizedMessageBoxCategory RenameFileMergeMessageBox
        {
            get
            {
                return new ParametrizedMessageBoxCategory("Предупреждение",
                    new ParametrizedString("Файл \"{?}\" уже существует в каталоге \"{?}\". Хотите выполнить слияние?"));
            }
        }
        public static ParametrizedMessageBoxCategory MoveFileReplaceMessageBox
        {
            get
            {
                return new ParametrizedMessageBoxCategory("Предупреждение",
                    new ParametrizedString("Файл \"{?}\" уже существует в каталоге \"{?}\". Хотите заменить файл?"));
            }
        }
    }
}
