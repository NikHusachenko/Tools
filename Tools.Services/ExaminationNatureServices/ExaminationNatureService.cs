using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Tools.Common;
using Tools.Database.Entities;
using Tools.EntityFramework.GenericRepository;
using Tools.Services.Response;

namespace Tools.Services.ExaminationNatureServices
{
    public class ExaminationNatureService : IExaminationNatureService
    {
        private readonly IGenericRepository<ExaminationNatureEntity> _natureRepository;

        public ExaminationNatureService(IGenericRepository<ExaminationNatureEntity> natureRepository)
        {
            _natureRepository = natureRepository;
        }

        public async Task<ResponseService<long>> Create(string name)
        {
            ExaminationNatureEntity dbRecord = await _natureRepository.GetBy(nature => nature.Name == name);
            if (dbRecord != null)
            {
                return ResponseService<long>.Error(Errors.WAS_CREATED_ERROR);
            }

            dbRecord = new ExaminationNatureEntity()
            {
                CreatedOn = DateTime.Now,
                Name = name,
            };

            try
            {
                await _natureRepository.Create(dbRecord);
            }
            catch (Exception ex)
            {
                return ResponseService<long>.Error(ex.Message);
            }

            return ResponseService<long>.Ok(dbRecord.Id);
        }

        public async Task<ResponseService<long>> Delete(ExaminationNatureEntity entity)
        {
            try
            {
                await _natureRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                return ResponseService<long>.Error(ex.Message);
            }

            return ResponseService<long>.Ok(entity.Id);
        }

        public async Task<ResponseService<long>> Delete(string name)
        {
            ExaminationNatureEntity dbRecord = await _natureRepository.GetBy(nature => nature.Name == name);
            if (dbRecord == null)
            {
                return ResponseService<long>.Error(Errors.NOT_FOUND_ERROR);
            }
            return await Delete(dbRecord);
        }

        public async Task<ICollection<ExaminationNatureEntity>> GetAll()
        {
            return await _natureRepository.GetAll()
                .ToListAsync();
        }

        public async Task<ResponseService<ExaminationNatureEntity>> GetById(long id)
        {
            ExaminationNatureEntity dbRecord = await _natureRepository.GetById(id);
            if (dbRecord == null)
            {
                return ResponseService<ExaminationNatureEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<ExaminationNatureEntity>.Ok(dbRecord);
        }

        public async Task<ResponseService<ExaminationNatureEntity>> GetByName(string name)
        {
            ExaminationNatureEntity dbRecord = await _natureRepository.GetBy(nature => nature.Name == name);
            if (dbRecord == null)
            {
                return ResponseService<ExaminationNatureEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<ExaminationNatureEntity>.Ok(dbRecord);
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

            ExaminationNatureEntity dbRecord = oldNameResponse.Value;
            dbRecord.Name = newName;
            return await Update(dbRecord);
        }

        public async Task<ResponseService> Update(ExaminationNatureEntity entity)
        {
            try
            {
                await _natureRepository.Update(entity);
                return ResponseService.Ok();
            }
            catch (Exception ex)
            {
                return ResponseService.Error(ex.Message);
            }
        }
    }
}