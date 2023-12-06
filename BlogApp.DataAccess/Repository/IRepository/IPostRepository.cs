using BlogApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess.Repository.IRepository
{
    public interface IPostRepository : IRepository<Post>
    {
        void update(Post obj);
    }
}