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
        void Save();
    }
}