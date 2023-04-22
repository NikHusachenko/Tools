using System.Collections.Generic;
using System.Data.Entity;
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
            ToolSubgroupEntity dbRecord = await _toolSubgroupRepository.GetBy(subgroup =>  subgroup.Name == name);
            if (dbRecord == null)
            {
                return ResponseService<ToolSubgroupEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<ToolSubgroupEntity>.Ok(dbRecord);
        }
    }
}