using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Tools.Common;
using Tools.Database.Entities;
using Tools.EntityFramework.GenericRepository;
using Tools.Services.Response;

namespace Tools.Services.ExaminationReasonServices
{
    public class ExaminationReasonService : IExaminationReasonService
    {
        private readonly IGenericRepository<ExaminationReasonEntity> _reasonRepository;

        public ExaminationReasonService(IGenericRepository<ExaminationReasonEntity> reasonRepository)
        {
            _reasonRepository = reasonRepository;
        }

        public async Task<ResponseService<long>> Create(string name)
        {
            var response = await GetByName(name);
            if (response.IsError)
            {
                return ResponseService<long>.Error(response.ErrorMessage);
            }

            ExaminationReasonEntity dbRecord = new ExaminationReasonEntity()
            {
                CreatedOn = DateTime.Now,
                Name = name,
            };

            try
            {
                await _reasonRepository.Create(dbRecord);
            }
            catch (Exception ex)
            {
                return ResponseService<long>.Error(ex.Message);
            }
            return ResponseService<long>.Ok(dbRecord.Id);
        }

        public async Task<ResponseService> Delete(ExaminationReasonEntity entity)
        {
            try
            {
                await _reasonRepository.Delete(entity);
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

        public async Task<ICollection<ExaminationReasonEntity>> GetAll()
        {
            return await _reasonRepository.GetAll()
                .ToListAsync();
        }

        public async Task<ResponseService<ExaminationReasonEntity>> GetById(long id)
        {
            ExaminationReasonEntity dbRecord = await _reasonRepository.GetById(id);
            if (dbRecord == null)
            {
                return ResponseService<ExaminationReasonEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<ExaminationReasonEntity>.Ok(dbRecord);
        }

        public async Task<ResponseService<ExaminationReasonEntity>> GetByName(string name)
        {
            ExaminationReasonEntity dbRecord = await _reasonRepository.GetBy(reason => reason.Name == name);
            if (dbRecord == null)
            {
                return ResponseService<ExaminationReasonEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<ExaminationReasonEntity>.Ok(dbRecord);
        }

        public async Task<ResponseService> Rename(string oldName, string newnName)
        {
            var oldResult = await GetByName(oldName);
            if (oldResult.IsError)
            {
                return ResponseService.Error(oldResult.ErrorMessage);
            }

            var newResult = await GetByName(newnName);
            if (!newResult.IsError)
            {
                return ResponseService.Error(Errors.WAS_CREATED_ERROR);
            }

            ExaminationReasonEntity dbRecord = oldResult.Value;
            dbRecord.Name = newnName;
            return await Update(dbRecord);
        }

        public async Task<ResponseService> Update(ExaminationReasonEntity entity)
        {
            try
            {
                await _reasonRepository.Update(entity);
            }
            catch (Exception ex)
            {
                return ResponseService.Error(ex.Message);
            }
            return ResponseService.Ok();
        }
    }
}