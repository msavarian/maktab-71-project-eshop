﻿using App.Domain.Core.Product.Contacts.Repositories.Model;
using App.Domain.Core.Product.Dtos;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Database.Repos.Ef.Product.Model
{
    public class ModelQueryRepository : IModelQueryRepository
    {
        private readonly AppDbContext _context;

        public ModelQueryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ModelDto>> GetAll()
        {
            return await _context.Models.Select(p => new ModelDto()
            {
                Id = p.Id,
                CreationDate = p.CreationDate,
                Name = p.Name,
                IsDeleted = p.IsDeleted,
                ParentModelId = p.ParentModelId,
                BrandId = p.BrandId
            }).ToListAsync();
        }

        public async Task<ModelDto>? Get(int id)
        {
            var record = await _context.Models.Where(p => p.Id == id).Select(p => new ModelDto()
            {
                Id = p.Id,
                CreationDate = p.CreationDate,
                Name = p.Name,
                IsDeleted = p.IsDeleted,
                BrandId = p.BrandId,
                ParentModelId = p.ParentModelId,
            }).SingleOrDefaultAsync();
            return record;
        }

        public async Task<ModelDto>? Get(string name)
        {
            var record = await _context.Models.Where(p => p.Name == name).Select(p => new ModelDto()
            {
                Id = p.Id,
                CreationDate = p.CreationDate,
                Name = p.Name,
                IsDeleted = p.IsDeleted,
                BrandId = p.BrandId,
                ParentModelId = p.ParentModelId,
            }).SingleOrDefaultAsync();
            return record;
        }
    }
}
