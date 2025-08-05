using Microsoft.AspNetCore.Mvc;

namespace GTR.WebClientMVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            ViewBag.StatusCode = statusCode;
            ViewBag.ErrorMessage = statusCode switch
            {
                404 => "Sorry, the resource you requested could not be found",
                500 => "Sorry, something went wrong on the server",
                _ => "An error occurred"
            };
            return View("Error");
        }

        [Route("Error")]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
