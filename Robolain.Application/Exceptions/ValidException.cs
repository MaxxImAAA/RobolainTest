using FluentValidation.Results;
using Robolain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robolain.Application.Exceptions
{
    public class ValidException : Exception
    {
        public List<ValidationFailure> Errors { get; set; }
        public int ErrorCode { get; set; }

        public ValidException(List<ValidationFailure> Errors) : base()
        {
            this.Errors = Errors;
            ErrorCode = (int)ErrorCodes.ValidationException;
        }
    }
}
