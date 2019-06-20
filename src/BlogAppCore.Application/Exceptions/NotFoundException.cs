using System;

namespace BlogAppCore.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(
            string entityName,
            string paramName,
            object value) : base($"Entity \"{entityName}\" with {paramName}: {value} was not found.") { }
    }
}