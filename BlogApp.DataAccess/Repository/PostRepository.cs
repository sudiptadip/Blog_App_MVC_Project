using BlogApp.DataAccess.Repository.IRepository;
using BlogApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {

        private readonly ApplicationDbContext _db;

        public PostRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(Post obj)
        {
            _db.Update(obj);
        }
    }
}