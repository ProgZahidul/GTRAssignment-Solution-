using Microsoft.AspNetCore.Mvc;

namespace GTR.WebClientMVC.Controllers
{
    
        public class ProductController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }
        }
    }

