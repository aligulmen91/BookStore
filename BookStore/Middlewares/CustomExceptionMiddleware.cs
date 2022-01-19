using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

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

            try
            {
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
            catch (Exception ex)
            {
                watch.Stop(); //eğer hata alırsak bu duramayacak, ilk önce bunu durduralım
                await HandleException(context, ex, watch);
            }
       
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "[ERROR] HTTP " 
                + context.Request.Method 
                + " - " 
                + context.Response.StatusCode 
                + " Error Message " + ex.Message
                + " in "
                + watch.ElapsedMilliseconds
                +" ms";
            Console.WriteLine(message);
          

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);
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
