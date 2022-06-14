﻿using App.Domain.Core.Product.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Database.SqlServer.Repositories.Contracts
{
    public interface ITagRepository 
    {
        Tag GetById(int Id);
        List<Tag> GetAll();
        int Create(Tag model);
        void Update(Tag model);
        bool Remove(int Id);
    }
}