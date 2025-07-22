using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace AwesomeProject.Common.Middleware
{
    public class ErrorHandlerMiddleware
    {
        protected readonly RequestDelegate Next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = HandleException(ex);
                var error = new { error = ex?.Message };
                var result = JsonConvert.SerializeObject(error);
                await response.WriteAsync(result);
            }
        }
        protected virtual int HandleException(Exception ex)
        {
			return ex switch
			{
                // custom exceptions
				_ => StatusCodes.Status500InternalServerError,
			};
		}
    }
}
