using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.Database.Entities;
using Tools.Services.Response;

namespace Tools.Services.ToolGroupServices
{
    public interface IToolGroupService
    {
        Task<ResponseService<long>> Create(string name);
        Task<ResponseService<long>> Delete(ToolGroupEntity entity);
        Task<ResponseService<long>> Delete(string name);
        Task<ResponseService> Update(ToolGroupEntity entity);

        Task<ResponseService<ToolGroupEntity>> GetById(long id);
        Task<ResponseService<ToolGroupEntity>> GetByName(string name);
        Task<ICollection<ToolGroupEntity>> GetAll();
    }
}