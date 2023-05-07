using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Tools.Common;
using Tools.Database.Entities;
using Tools.EntityFramework.GenericRepository;
using Tools.Services.Response;

namespace Tools.Services.ExaminationTypeService
{
    public class ExaminationTypeService : IExaminationTypeService
    {
        private readonly IGenericRepository<ExaminationTypeEntity> _typeRepository;

        public ExaminationTypeService(IGenericRepository<ExaminationTypeEntity> typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public async Task<ResponseService<long>> Create(string name)
        {
            var response = await GetByName(name);
            if (!response.IsError)
            {
                return ResponseService<long>.Error(Errors.WAS_CREATED_ERROR);
            }

            ExaminationTypeEntity dbRecord = new ExaminationTypeEntity()
            {
                CreatedOn = DateTime.Now,
                Name = name,
            };

            try
            {
                await _typeRepository.Create(dbRecord);
            }
            catch (Exception ex)
            {
                return ResponseService<long>.Error(ex.Message);
            }
            return ResponseService<long>.Ok(dbRecord.Id);
        }

        public async Task<ResponseService> Delete(ExaminationTypeEntity entity)
        {
            try
            {
                await _typeRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                return ResponseService.Error(ex.Message);
            }
            return ResponseService.Ok();
        }

        public async Task<ResponseService> Delete(string name)
        {
            var response = await GetByName(name);
            if (response.IsError)
            {
                return ResponseService.Error(response.ErrorMessage);
            }

            return await Delete(response.Value);
        }

        public async Task<ICollection<ExaminationTypeEntity>> GetAll()
        {
            return await _typeRepository.GetAll()
                .ToListAsync();
        }

        public async Task<ResponseService<ExaminationTypeEntity>> GetById(long id)
        {
            ExaminationTypeEntity dbRecord = await _typeRepository.GetById(id);
            if (dbRecord == null)
            {
                return ResponseService<ExaminationTypeEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<ExaminationTypeEntity>.Ok(dbRecord);
        }

        public async Task<ResponseService<ExaminationTypeEntity>> GetByName(string name)
        {
            ExaminationTypeEntity dbRecord = await _typeRepository.GetBy(type => type.Name == name);
            if (dbRecord == null)
            {
                return ResponseService<ExaminationTypeEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<ExaminationTypeEntity>.Ok(dbRecord);
        }

        public async Task<ResponseService> Rename(string oldName, string newName)
        {
            var oldNameResponse = await GetByName(oldName);
            if (oldNameResponse.IsError)
            {
                return ResponseService.Error(oldNameResponse.ErrorMessage);
            }

            var newNameResponse = await GetByName(newName);
            if (!newNameResponse.IsError)
            {
                return ResponseService.Error(Errors.WAS_CREATED_ERROR);
            }

            ExaminationTypeEntity dbRecord = oldNameResponse.Value;
            dbRecord.Name = newName;
            return await Update(dbRecord);
        }

        public async Task<ResponseService> Update(ExaminationTypeEntity entity)
        {
            try
            {
                await _typeRepository.Update(entity);
            }
            catch (Exception ex)
            {
                return ResponseService.Error(ex.Message);
            }
            return ResponseService.Ok();
        }
    }
}