using Microsoft.AspNet.Mvc;
using Messenger.Models;

namespace Messenger
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}