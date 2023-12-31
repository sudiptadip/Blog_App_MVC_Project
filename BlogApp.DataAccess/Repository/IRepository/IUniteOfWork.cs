﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess.Repository.IRepository
{
    public interface IUniteOfWork
    {
        ICategoryRepository Category { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IPostRepository Post { get; }
        ICommentRepository Comment { get; }
        void Save();
    }
}