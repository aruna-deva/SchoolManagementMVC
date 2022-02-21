using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Hosting;

namespace SchoolManagement.Controllers
{
    
    public class ErrorController : Controller
    {
          [Route("/error-development")]
       public IActionResult HandleErrorDevelopment(
           [FromServices] IHostEnvironment hostEnvironment
      
        )
        {
            if(!hostEnvironment.IsDevelopment())
                return NotFound(); 
            var exceptionHandlerFeature=
                HttpContext.Features.Get<IExceptionHandlerFeature>()!; 
            //! is called as Null-Forgiving operator
            
            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message
            );
        }
        [Route("/error")]
        public IActionResult HandleError() => Problem();
    }
}
    
