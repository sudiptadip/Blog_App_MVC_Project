using BlogApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Model.Utilites
{
    public class PostAndComment
    {
        public IEnumerable<Post> Post { get; set; }
        public Comment Comment { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
