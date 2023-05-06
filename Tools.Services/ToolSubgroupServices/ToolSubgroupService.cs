using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tools.Common;
using Tools.Database.Entities;
using Tools.EntityFramework.GenericRepository;
using Tools.Services.Response;
using Tools.Services.ToolGroupServices;

namespace Tools.Services.ToolSubgroupServices
{
    public class ToolSubgroupService : IToolSubgroupService
    {
        private readonly IGenericRepository<ToolSubgroupEntity> _toolSubgroupRepository;
        private readonly IToolGroupService _toolGroupService;

        public ToolSubgroupService(IGenericRepository<ToolSubgroupEntity> toolSubgroupRepository,
            IToolGroupService toolGroupService)
        {
            _toolSubgroupRepository = toolSubgroupRepository;
            _toolGroupService = toolGroupService;
        }

        public async Task<ResponseService<long>> Create(string name, long groupId)
        {
            ToolSubgroupEntity dbRecord = await _toolSubgroupRepository.GetBy(subgroup => subgroup.Name == name &&
                subgroup.GroupFK == groupId);
            if (dbRecord != null)
            {
                return ResponseService<long>.Error(Errors.WAS_CREATED_ERROR);
            }

            dbRecord = new ToolSubgroupEntity()
            {
                GroupFK = groupId,
                Name = name,
            };

            await _toolSubgroupRepository.Create(dbRecord);
            return ResponseService<long>.Ok(dbRecord.Id);
        }

        public async Task<ResponseService<long>> Create(string subgroupName, string groupName)
        {
            ToolSubgroupEntity dbRecord = await _toolSubgroupRepository.GetBy(subgroup => subgroup.Name == subgroupName &&
                subgroup.Group.Name == groupName);
            if (dbRecord != null)
            {
                return ResponseService<long>.Error(Errors.WAS_CREATED_ERROR);
            }

            var getGroupResult = await _toolGroupService.GetByName(groupName);
            if (getGroupResult.IsError)
            {
                return ResponseService<long>.Error(getGroupResult.ErrorMessage);
            }

            dbRecord = new ToolSubgroupEntity()
            {
                GroupFK = getGroupResult.Value.Id,
                Name = subgroupName,
            };

            await _toolSubgroupRepository.Create(dbRecord);
            return ResponseService<long>.Ok(dbRecord.Id);
        }

        public async Task<ResponseService<long>> Delete(ToolSubgroupEntity entity)
        {
            try
            {
                await _toolSubgroupRepository.Delete(entity);
                return ResponseService<long>.Ok(entity.Id);
            }
            catch (Exception ex)
            {
                return ResponseService<long>.Error(ex.Message);
            }
        }

        public async Task<ResponseService<long>> Delete(string name)
        {
            ToolSubgroupEntity dbRecord = await _toolSubgroupRepository.GetBy(subgroup => subgroup.Name == name);
            if (dbRecord == null)
            {
                return ResponseService<long>.Error(Errors.NOT_FOUND_ERROR);
            }

            return await Delete(dbRecord);
        }

        public async Task<ICollection<ToolSubgroupEntity>> GetAll()
        {
            return await _toolSubgroupRepository.GetAll()
                .Include(subgroup => subgroup.Group)
                .ToListAsync();
        }

        public async Task<ResponseService<ToolSubgroupEntity>> GetById(long id)
        {
            ToolSubgroupEntity dbRecord = await _toolSubgroupRepository.GetById(id);
            if (dbRecord == null)
            {
                return ResponseService<ToolSubgroupEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<ToolSubgroupEntity>.Ok(dbRecord);
        }

        public async Task<ResponseService<ToolSubgroupEntity>> GetByName(string name)
        {
            ToolSubgroupEntity dbRecord = await _toolSubgroupRepository.GetAll()
                .Where(subgroup => subgroup.Name == name)
                    .Include(subgroup => subgroup.Group)
                .FirstOrDefaultAsync();

            if (dbRecord == null)
            {
                return ResponseService<ToolSubgroupEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<ToolSubgroupEntity>.Ok(dbRecord);
        }

        public async Task<ResponseService> Rename(string oldName, string newName)
        {
            var oldNameResult = await GetByName(oldName);
            if (oldNameResult.IsError)
            {
                return ResponseService.Error(oldNameResult.ErrorMessage);
            }

            var newNameResult = await GetByName(newName);
            if (newNameResult.IsError)
            {
                return ResponseService.Error(Errors.WAS_CREATED_ERROR);
            }

            ToolSubgroupEntity dbRecord = oldNameResult.Value;
            dbRecord.Name = newName;

            try
            {
                await _toolSubgroupRepository.Update(dbRecord);
            }
            catch (Exception ex)
            {
                return ResponseService.Error(ex.Message);
            }

            return ResponseService.Ok();
        }
    }
}