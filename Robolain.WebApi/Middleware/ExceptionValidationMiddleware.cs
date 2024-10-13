using FluentValidation;
using Robolain.Application.Exceptions;
using Robolain.Domain;
using Robolain.Domain.Results;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Robolain.WebApi.Middleware
{
    public class ExceptionValidationMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionValidationMiddleware(RequestDelegate _next)
        {
            this._next = _next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExcpetionValidationMiddleWare(context, ex);
            }

        }

        private Task ExcpetionValidationMiddleWare(HttpContext context, Exception ex) 
        {
            BaseResult? result = null;

            switch(ex)
            {
               
                case NotFoundException _ex:
                    {
                        result = new BaseResult();
                        result.ErrorMessage = _ex.ErrorMessage;
                        result.ErrorCode = _ex.ErrorCode;
                        break;
                    }
                
                case NameExistsException _ex:
                    {
                        result = new BaseResult();
                        result.ErrorMessage = _ex.ErrorMessage;
                        result.ErrorCode = _ex.ErrorCode;
                        break;
                    }

              
               case ValidException _ex:
                    {
                        result = new BaseResult();
                        var exMessage = _ex.Errors.Select(x => new string(x.ErrorMessage))
                                                                  .Aggregate((a, b) => a + ", " + b);
                        result.ErrorMessage = exMessage;
                        result.ErrorCode = (int)ErrorCodes.ValidationException;
                        break;

                    }

            }

            if(result == null)
            {
                result = new BaseResult();
                result.ErrorMessage = ex.Message;
                result.ErrorCode = 400;
            }

            var body = JsonSerializer.Serialize(result);

            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(body);
        }
    }
}
