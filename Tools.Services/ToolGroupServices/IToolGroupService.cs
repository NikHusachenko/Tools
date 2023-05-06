using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.Database.Entities;
using Tools.Services.Response;

namespace Tools.Services.ToolGroupServices
{
    public interface IToolGroupService
    {
        Task<ResponseService<long>> Create(string name);

        Task<ResponseService<ToolGroupEntity>> GetById(long id);
        Task<ResponseService<ToolGroupEntity>> GetByName(string name);
        Task<ICollection<ToolGroupEntity>> GetAll();
    }
}