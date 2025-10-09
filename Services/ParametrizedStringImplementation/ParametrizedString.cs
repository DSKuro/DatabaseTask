using DatabaseTask.Services.ParametrizedStringImplementation.Exceptions;
using DatabaseTask.Services.ParametrizedStringImplementation.Interfaces;
using System.Linq;

namespace DatabaseTask.Services.ParametrizedStringImplementation
{
    public class ParametrizedString : IParametrizedString
    {
        private string _value;

        public ParametrizedString(string value)
        {
            _value = value;
        }

        public string GetStringWithParams(params string[] parameters)
        {
            if (parameters.Length != _value.Count(x => x.Equals('?')))
            {
                throw new ParametrizedStringException("Количество параметров не совпадает");
            }

            return GetStringWithParamsImpl(parameters);
        }

        private string GetStringWithParamsImpl(params string[] parameters)
        {
            string[] temp = _value.Split('?');
            string stringWithParams = "";
            int i = 0;
            for (i = 0; i < temp.Length - 1; i++)
            {
                stringWithParams += temp[i] + parameters[i];
            }
            stringWithParams += temp[i];
            return stringWithParams;
        }
    }
}
