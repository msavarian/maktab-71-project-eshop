﻿using App.Domain.Core.Product.Contacts.Repositories.Model;
using App.Domain.Core.Product.Contacts.Services;
using App.Domain.Core.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Product
{
    public class ModelService : IModelService
    {
        private readonly IModelCommandRepository _commandRepository;
        private readonly IModelQueryRepository _queryRepository;
        public ModelService(IModelCommandRepository categoryCommandRepository, IModelQueryRepository categoryQueryRepository)
        {
            _queryRepository = categoryQueryRepository;
            _commandRepository = categoryCommandRepository;
        }

        public async Task Delete(int id)
        {
            await _commandRepository.Delete(id);
        }

        public async Task EnsureDoesNotExist(string name)
        {
            var record = await _queryRepository.Get(name);
            if (record != null)
            {
                throw new Exception($"Category {name} Already Exists!");
            }
        }

        public async Task EnsureExists(string name)
        {
            await _queryRepository.Get(name);
        }

        public async Task EnsureExists(int id)
        {
            await _queryRepository.Get(id);
        }

        public async Task<ModelDto> Get(int id)
        {
            var record = await _queryRepository.Get(id);
            if (record == null)
            {
                throw new Exception($"Category {id} Doesn't Exists!");
            }
            return record;
        }

        public async Task<ModelDto> Get(string name)
        {
            var record = await _queryRepository.Get(name);
            if (record == null)
            {
                throw new Exception($"Category {name} Doesn't Exists!");
            }
            return record;
        }

        public async Task<List<ModelDto>> GetAll()
        {
            return await _queryRepository.GetAll();
        }

        public async Task Set(ModelDto dto)
        {
            await _commandRepository.Add(dto);
        }

        public async Task Update(ModelDto dto)
        {
            await _commandRepository.Update(dto);
        }
    }
}
