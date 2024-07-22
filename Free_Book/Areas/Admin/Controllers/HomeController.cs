using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace Free_Book.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Permissions.Home.View)]

    public class HomeController : Controller
    {
        [Authorize(Permissions.Home.View)]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Denied()
        {
            return View();
        }
    }
}
