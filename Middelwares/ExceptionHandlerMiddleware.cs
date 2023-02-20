using System.Net;

namespace LaNacionChallenge.Middelwares
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // Enviar una respuesta de error al cliente
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Ha ocurrido un error en el servidor: {ex.Message}");
            }
        }
    }
}
