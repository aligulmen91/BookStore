using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics;

namespace BookStore.Middlewares
{
    public class CustomExceptionMiddleware 
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew(); //neyin ne kadar sürede gerçekleştiğini izlemek için timer başlatıyoruz
            string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
            Console.WriteLine(message);
            await _next(context); //bir sonraki middleware i bu şekilde çağırabiliyoruz (RequestDelegate)
            watch.Stop(); // timerı durduruyoruz. ne kadar sürede tamamladığını öğreneceğiz
            message = "[Request] HTTP " 
                + context.Request.Method + " - " 
                + context.Request.Path 
                + " responded " + context.Response.StatusCode
                + " in " + watch.ElapsedMilliseconds + " ms";
            Console.WriteLine(message);
        }

    }


    public static class CustomExceptionMiddlewareExtension
    {
        //app.use olarak kullanabilmek için
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {

            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }

}
