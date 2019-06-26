using System;

namespace BlogAppCore.Application.Exceptions
{
    public class DeleteFailureException : Exception
    {
        public DeleteFailureException(string entityName, string paramName,
            object value, string message)
            : base($"Delition of entity \"{entityName}\" with {paramName}: {value} failed. {message}")
        { }
    }
}