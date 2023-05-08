using System.Threading.Tasks;
using Tools.Services.ExaminationServices.Models;
using Tools.Services.Response;

namespace Tools.Services.ExaminationServices
{
    public interface IExaminationService
    {
        Task<ResponseService<long>> Create(CreateExaminationPostModel vm);
    }
}