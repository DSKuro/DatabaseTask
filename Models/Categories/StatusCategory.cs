namespace DatabaseTask.Models.Categories
{
    public class StatusCategory
    {
        public string Path { get; set; }

        public StatusCategory(string path)
        {
            Path = path;
        }

        public static StatusCategory CorrectStatus { get { return new StatusCategory("/Assets/Status/correct.svg"); } }
        public static StatusCategory WrongStatus { get { return new StatusCategory("/Assets/Status/incorrect.svg"); } }
    }
}
