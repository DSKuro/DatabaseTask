namespace DatabaseTask.Services.ParametrizedStringImplementation.Interfaces
{
    public interface IParametrizedString
    {
        public string GetStringWithParams(params string[] parameters);
    }
}
