using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env; //to check what environment are we running in. Production or development
            _logger = logger; //to display our exception to our terminal window
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)  //we are gonna give this middleware its required method 
        {
            try
            {
                await _next(context); //get a context and pass it on to the next piece of middleware.
                // this piece of middleware is gonna live at the very top of our middleware
            }
            catch(Exception ex)
            {
               _logger.LogError(ex, ex.Message); //show the exception to the terminal
               context.Response.ContentType ="application/json"; //we are gonna write out this exception to our response.
               context.Response.StatusCode = (int) HttpStatusCode.InternalServerError ; //it effectively gonna be a 500
               //Create the response
               var response = _env.IsDevelopment() //We are gonna see what enviroment we are running in. We will use a tenary operator
                 ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                 : new ApiException(context.Response.StatusCode, "Internal Server Error");

                 //We are gonna create some options, because we are gonna send back this to JSON.
                 var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase}; //Serialize on camel case. This ensures that our response,
                 //just goes back as a normal JSON formated response in camel case.
                 var json = JsonSerializer.Serialize(response, options); // We are gonna serialze this response and parse in some options.

                 await context.Response.WriteAsync(json);
            }
        }
    }
}