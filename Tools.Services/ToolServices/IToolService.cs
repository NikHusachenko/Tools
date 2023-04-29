using System.Threading.Tasks;
using Tools.Database.Entities;
using Tools.Services.Response;
using Tools.Services.ToolServices.Models;

namespace Tools.Services.ToolServices
{
    public interface IToolService
    {
        Task<ResponseService<long>> Create(ToolEntity toolEntity);

        Task<ResponseService<ToolEntity>> GetById(long id);

        Task<ResponseService<ToolEntity>> ValidateBeforeCreating(CreateToolEntityPostModel vm);
        Task<ToolsSortingGetModel> Sorting(ToolsSortingPostModel vm);
    }
}