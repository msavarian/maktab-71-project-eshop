﻿using App.EndPoints.Mvc.AdminUI.Models;
using App.EndPoints.Mvc.AdminUI.Models.Brand;
using App.Infrastructures.Database.SqlServer.Data;
using App.Infrastructures.Database.SqlServer.Entities;
using App.Infrastructures.Database.SqlServer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Database.SqlServer.Repositories
{

    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _eshop;
        public BrandRepository(AppDbContext appDbContext)
        {
            this._eshop = appDbContext;
        }

        public void Create(BrandSaveViewModel model)
        {

            var brand = new Brand
            {
                Name = model.Name,
                DisplayOrder = model.DisplayOrder,
                CreationDate = DateTime.Now,
                IsDeleted = false
            };
            _eshop.Brands.Add(brand);
            _eshop.SaveChanges();
        }

        public void Update(BrandSaveViewModel model)
        {
            var brand = _eshop.Brands.First(p => p.Id == model.Id);
            brand.Name = model.Name;
            brand.DisplayOrder = model.DisplayOrder;
            _eshop.SaveChanges();
        }

        public void Remove(int id)
        {
            var brand = _eshop.Brands.First(p => p.Id == id);
            _eshop.Brands.Remove(brand);
            _eshop.SaveChanges();
        }

        public List<BrandListViewModel> GetAll()
        {
            return _eshop.Brands.Select(b => new BrandListViewModel
            {
                Id = b.Id,
                Name = b.Name,
                CreationDate = b.CreationDate,
                DisplayOrder = b.DisplayOrder
            }).ToList();
        }

        public BrandSaveViewModel GetBy(int id)
        {
            try
            {
                var model = _eshop.Brands.Select(b => new BrandSaveViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    DisplayOrder = b.DisplayOrder
                }).SingleOrDefault(b => b.Id == id);
                if (model == null) return new BrandSaveViewModel();
                return model;
            }
            catch (Exception dbx)
            {
                throw new Exception(dbx.Message, dbx.InnerException);
            }
        }
    }
}


