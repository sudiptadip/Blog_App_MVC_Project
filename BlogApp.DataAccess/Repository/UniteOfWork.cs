using BlogApp.DataAccess.Repository.IRepository;

namespace BlogApp.DataAccess.Repository
{
    public class UniteOfWork : IUniteOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IPostRepository Post { get; private set; }

        public ICommentRepository Comment { get; private set; }

        public UniteOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            Post = new PostRepository(_db);
            Comment = new CommentRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
