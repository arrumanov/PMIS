using System;

namespace Dogovor.CrossCutting.Exceptions
{
    public class QueryArgumentException : Exception
    {
        public QueryArgumentException()
        {

        }

        public QueryArgumentException(string message) : base(message)
        {

        }
    }
}