using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.Special
{
    public class ParseException : ArgumentException
    {
        public ParseException() : base() { }
        public ParseException(string message) : base(message) { }
        public ParseException(string message, Exception innerException) : base(message, innerException) { }
        public ParseException(string message, string paramName) : base(message, paramName) { }
        public ParseException(string message, string paramName, Exception innerException) : base(message, paramName, innerException) { }
    }
}
