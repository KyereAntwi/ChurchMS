using System.Net;
using System.Text.Json;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Exceptions;

namespace COPDistrictMS.WebApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = new BaseResponse()
            {
                Success = false
            };

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result.Data = validationException.ValidationErrors;
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result.Message = badRequestException.Message;
                    break;
                case NotFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case Exception:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if (result.Message == string.Empty || result.Data == null )
            {
                var response = new BaseResponse()
                {
                    Success = false,
                    Message = exception.Message 
                };
                
                result = response;
            }

            return context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}
