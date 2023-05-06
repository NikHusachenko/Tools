using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.Database.Entities;
using Tools.Services.Response;

namespace Tools.Services.ToolSubgroupServices
{
    public interface IToolSubgroupService
    {
        Task<ResponseService<long>> Create(string name, long groupFK);

        Task<ResponseService<ToolSubgroupEntity>> GetById(long id);
        Task<ResponseService<ToolSubgroupEntity>> GetByName(string name);
        Task<ICollection<ToolSubgroupEntity>> GetAll();
    }
}