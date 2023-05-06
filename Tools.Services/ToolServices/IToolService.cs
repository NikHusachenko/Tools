using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.Database.Entities;
using Tools.Services.Response;
using Tools.Services.ToolServices.Models;

namespace Tools.Services.ToolServices
{
    public interface IToolService
    {
        Task<ResponseService<long>> Create(ToolEntity toolEntity);
        Task<ResponseService<long>> Delete(ToolEntity toolEntity);
        Task<ResponseService> Update(ToolEntity toolEntity);

        Task<ResponseService<ToolEntity>> GetById(long id);
        Task<ICollection<ToolEntity>> GetById(ICollection<long> ids);

        Task<ResponseService<ToolEntity>> ValidateBeforeCreating(CreateToolEntityPostModel vm);
        Task<ToolsSortingGetModel> Sorting(ToolsSortingPostModel vm);
    }
}