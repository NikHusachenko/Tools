using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Tools.Common;
using Tools.Database.Entities;
using Tools.EntityFramework.GenericRepository;
using Tools.Services.Response;

namespace Tools.Services.OrganizationUnitServices
{
    public class OrganizationUnitService : IOrganizationUnitService
    {
        private readonly IGenericRepository<OrganizationUnitEntity> _unitRepository;

        public OrganizationUnitService(IGenericRepository<OrganizationUnitEntity> unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<ResponseService<long>> Create(string name)
        {
            var response = await GetByName(name);
            if(!response.IsError)
            {
                return ResponseService<long>.Error(Errors.WAS_CREATED_ERROR);
            }

            OrganizationUnitEntity dbRecord = new OrganizationUnitEntity()
            {
                CreatedOn = DateTime.Now,
                Name = name,
            };

            try
            {
                await _unitRepository.Create(dbRecord);
            }
            catch (Exception ex)
            {
                return ResponseService<long>.Error(ex.Message);
            }
            return ResponseService<long>.Ok(dbRecord.Id);
        }

        public async Task<ResponseService> Delete(OrganizationUnitEntity entity)
        {
            try
            {
                await _unitRepository.Delete(entity);
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

        public async Task<ICollection<OrganizationUnitEntity>> GetAll()
        {
            return await _unitRepository.GetAll()
                .ToListAsync();
        }

        public async Task<ResponseService<OrganizationUnitEntity>> GetById(long id)
        {
            OrganizationUnitEntity dbRecord = await _unitRepository.GetById(id);
            if (dbRecord == null)
            {
                return ResponseService<OrganizationUnitEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<OrganizationUnitEntity>.Ok(dbRecord);
        }

        public async Task<ResponseService<OrganizationUnitEntity>> GetByName(string name)
        {
            OrganizationUnitEntity dbRecord = await _unitRepository.GetBy(unit => unit.Name == name);
            if (dbRecord == null)
            {
                return ResponseService<OrganizationUnitEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<OrganizationUnitEntity>.Ok(dbRecord);
        }

        public async Task<ResponseService> Rename(string oldName, string newName)
        {
            var oldResponse = await GetByName(oldName);
            if (oldResponse.IsError)
            {
                return ResponseService.Error(oldResponse.ErrorMessage);
            }

            var newResponse = await GetByName(newName);
            if (!newResponse.IsError)
            {
                return ResponseService.Error(Errors.WAS_CREATED_ERROR);
            }

            OrganizationUnitEntity dbRecord = oldResponse.Value;
            dbRecord.Name = newName;
            return await Update(dbRecord);
        }

        public async Task<ResponseService> Update(OrganizationUnitEntity entity)
        {
            try
            {
                await _unitRepository.Update(entity);
            }
            catch (Exception ex)
            {
                return ResponseService.Error(ex.Message);
            }
            return ResponseService.Ok();
        }
    }
}