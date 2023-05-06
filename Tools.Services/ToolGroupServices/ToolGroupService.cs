using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tools.Common;
using Tools.Database.Entities;
using Tools.EntityFramework.GenericRepository;
using Tools.Services.Response;

namespace Tools.Services.ToolGroupServices
{
    public class ToolGroupService : IToolGroupService
    {
        private readonly IGenericRepository<ToolGroupEntity> _toolGroupRepository;

        public ToolGroupService(IGenericRepository<ToolGroupEntity> toolGroupRepository)
        {
            _toolGroupRepository = toolGroupRepository;
        }

        public async Task<ResponseService<long>> Create(string name)
        {
            ToolGroupEntity dbRecord = await _toolGroupRepository.GetBy(group => group.Name == name);
            if (dbRecord != null)
            {
                return ResponseService<long>.Error(Errors.WAS_CREATED_ERROR);
            }

            dbRecord = new ToolGroupEntity() { Name = name };
            await _toolGroupRepository.Create(dbRecord);
            return ResponseService<long>.Ok(dbRecord.Id);
        }

        public async Task<ICollection<ToolGroupEntity>> GetAll()
        {
            return await _toolGroupRepository.GetAll()
                .Include(group => group.Subgroups)
                .ToListAsync();
        }

        public async Task<ResponseService<ToolGroupEntity>> GetById(long id)
        {
            ToolGroupEntity dbRecord = await _toolGroupRepository.GetById(id);
            if(dbRecord == null)
            {
                return ResponseService<ToolGroupEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<ToolGroupEntity>.Ok(dbRecord);
        }

        public async Task<ResponseService<ToolGroupEntity>> GetByName(string name)
        {
            ToolGroupEntity dbRecord = await _toolGroupRepository.GetBy(group => group.Name == name);
            if (dbRecord == null)
            {
                return ResponseService<ToolGroupEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<ToolGroupEntity>.Ok(dbRecord);
        }
    }
}