using BlogApp.DataAccess.Repository;
using BlogApp.DataAccess.Repository.IRepository;
using BlogApp.Model.Models;
using BlogApp.Model.Utilites;
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
            var allComments = _uniteOfWork.Comment.GetAll().ToList();
            PostAndComment PostAndComment = new PostAndComment();
            PostAndComment.Post = allPosts;
            PostAndComment.Comments = allComments;

            return View(PostAndComment);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            string userId = _userManager.GetUserId(User);
            comment.UserId = userId;

            if (ModelState.IsValid)
            {
                _uniteOfWork.Comment.Add(comment);
                _uniteOfWork.Save();
                return RedirectToAction("Index");
            }

            return NotFound();
            
        }

    }
}
