using Robolain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robolain.Application.Exceptions
{
    public class NotFoundException : Exception, IBaseException
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public NotFoundException(string message, ErrorCodes code) : base(message)
        {
            ErrorMessage = message;
            ErrorCode = (int)code;
        }
    }
}
