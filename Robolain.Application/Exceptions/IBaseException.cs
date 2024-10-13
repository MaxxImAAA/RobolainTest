using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robolain.Application.Exceptions
{
    public interface IBaseException
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
    }
}
