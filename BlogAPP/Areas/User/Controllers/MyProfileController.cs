using BlogApp.DataAccess.Repository;
using BlogApp.DataAccess.Repository.IRepository;
using BlogApp.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPP.Areas.User.Controllers
{
    [Area("User")]
    public class MyProfileController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUniteOfWork _uniteOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public MyProfileController(IUniteOfWork uniteOfWork, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _uniteOfWork = uniteOfWork;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = _uniteOfWork.ApplicationUser.Get(x => x.Id == userId);

            ViewBag.User = user;

            return View();
        }

        public IActionResult NewPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewPost(Post post, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string rootPath = _webHostEnvironment.WebRootPath;

                if (file != null && file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(rootPath, "Images/Post", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    post.Url = "/Images/Post/" + fileName;
                    _uniteOfWork.Post.Add(post);
                    _uniteOfWork.Save();
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }


    }
}
