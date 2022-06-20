﻿using App.Domain.Core.BaseData.Contracts.Repositories;
using App.Domain.Core.BaseData.Entities;
using App.Infrastructures.Database.SqlServer.Data;

namespace App.Infrastructures.Database.SqlServer.Repositories
{
    public class ColorQueryRepository : IColorQueryRepository
    {

        private readonly AppDbContext _appDbContext;

        public ColorQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int Create(Color model)
        {
            _appDbContext.Colors.Add(model);
            _appDbContext.SaveChanges();
            return model.Id;
        }
        public void Update(Color model)
        {
            var record = _appDbContext.Colors.FirstOrDefault(p => p.Id == model.Id);
            record.Name = model.Name;
            record.Code = model.Code;
            record.CreationDate = model.CreationDate;
            _appDbContext.SaveChanges();
        }
        public bool Remove(int id)
        {
            var record = _appDbContext.Colors.FirstOrDefault(p => p.Id == id);
            _appDbContext.Colors.Remove(record);
            _appDbContext.SaveChanges();
            return true;
        }


        public List<Color> GetAll()
        {
            var record = _appDbContext.Colors.ToList();
            return record;
        }
        public Color GetById(int id)
        {
            var record = _appDbContext.Colors.FirstOrDefault(p => p.Id == id);
            return record;
        }

        public Color GetExitingColor(string name, string code)
        {
            var record = _appDbContext.Colors.FirstOrDefault(x=>x.Name== name && x.Code == code);
            return record;
        }
    }
}