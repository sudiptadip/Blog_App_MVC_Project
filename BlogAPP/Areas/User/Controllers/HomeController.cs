using BlogApp.DataAccess.Repository;
using BlogApp.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPP.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(IUniteOfWork uniteOfWork, UserManager<IdentityUser> userManager)
        {
            _uniteOfWork = uniteOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = _uniteOfWork.ApplicationUser.GetAll(x => x.Id == userId);

            ViewBag.User = user;

            var allPosts = _uniteOfWork.Post.GetAll(IncludePropertyes: "ApplicationUser").ToList();
            return View(allPosts);
        }
        public IActionResult Privacy()
        {
            return View();
        }

    }
}
