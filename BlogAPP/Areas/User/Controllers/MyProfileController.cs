using BlogApp.DataAccess.Repository;
using BlogApp.DataAccess.Repository.IRepository;
using BlogApp.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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


            IEnumerable<Post> posts = _uniteOfWork.Post.GetAll(u => u.UserId == userId);


            return View(posts);
        }

        public IActionResult NewPost()
        {
            IEnumerable<SelectListItem> CategoryList = _uniteOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.CategoryName,
                Value = u.Id.ToString(),
            });
            ViewBag.CategoryList = CategoryList;
            return View();
        }

        [HttpPost]
        public IActionResult NewPost(Post post, IFormFile file)
        {
            IEnumerable<SelectListItem> CategoryList = _uniteOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.CategoryName,
                Value = u.Id.ToString(),
            });

            ViewBag.CategoryList = CategoryList;

            string userId = _userManager.GetUserId(User);           

            if (userId != null)
            {
                post.UserId = userId;
            }

            post.CreatedAt = DateTime.Now;

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

                return RedirectToAction("Index", "MyProfile");
            }

            return View();
        }


    }
}
