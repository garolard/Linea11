using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linea11.Services.Exceptions
{
    class InternalErrorException : Exception
    {
        public InternalErrorException() : base()
        { }

        public InternalErrorException(string msg) : base(msg)
        { }

        public InternalErrorException(string msg, Exception innerException) : base(msg, innerException)
        { }
    }
}
