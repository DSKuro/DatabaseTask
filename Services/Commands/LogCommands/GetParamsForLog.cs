using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Interfaces;
using MsBox.Avalonia.Enums;
using System;
using System.Linq;

namespace DatabaseTask.Services.Commands.LogCommands
{
    public class GetParamsForLog : IGetParamsForLog
    {
        private readonly LoggerCommandDTO _commandDto;
        private readonly object? _data;

        public GetParamsForLog(object? data, LoggerCommandDTO commandDto)
        {
            _data = data;
            _commandDto = commandDto;
        }

        public object[] GetParams()
        {
            object[] newParameters;
            if (_data == null || _data is ButtonResult)
            {
                return _commandDto.Parameters;
            }

            return GetNewParamsWithData();
        }

        private object[] GetNewParamsWithData()
        {
            if (_commandDto.IsFirstData)
            {
                return new object[] { _data }.Concat(_commandDto.Parameters).ToArray();
            }

            return GetNewParamsWithLastData();
        }

        private object[] GetNewParamsWithLastData()
        {
            object[] newParameters = new object[_commandDto.Parameters.Length + 1];
            Array.Copy(_commandDto.Parameters, newParameters, _commandDto.Parameters.Length);
            newParameters[_commandDto.Parameters.Length] = _data;
            return newParameters;
        }
    }
}
