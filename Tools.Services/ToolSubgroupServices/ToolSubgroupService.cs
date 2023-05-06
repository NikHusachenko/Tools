using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tools.Common;
using Tools.Database.Entities;
using Tools.EntityFramework.GenericRepository;
using Tools.Services.Response;

namespace Tools.Services.ToolSubgroupServices
{
    public class ToolSubgroupService : IToolSubgroupService
    {
        private readonly IGenericRepository<ToolSubgroupEntity> _toolSubgroupRepository;

        public ToolSubgroupService(IGenericRepository<ToolSubgroupEntity> toolSubgroupRepository)
        {
            _toolSubgroupRepository = toolSubgroupRepository;
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

        public async Task<ICollection<ToolSubgroupEntity>> GetAll()
        {
            return await _toolSubgroupRepository.GetAll()
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
    }
}