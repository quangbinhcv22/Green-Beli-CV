using System;

namespace QB.ViewData
{
    public class UnableAccessDataViewException : Exception
    {
        public UnableAccessDataViewException(string message) : base(message)
        {
        }
    }
}