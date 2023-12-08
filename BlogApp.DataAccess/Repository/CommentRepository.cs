using BlogApp.DataAccess.Repository.IRepository;
using BlogApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {

        private readonly ApplicationDbContext _db;

        public CommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(Comment obj)
        {
            _db.Update(obj);
        }
    }
}