using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.Database.Entities;
using Tools.Services.ExaminationServices.Models;
using Tools.Services.Response;

namespace Tools.Services.ExaminationServices
{
    public interface IExaminationService
    {
        Task<ResponseService<long>> Create(CreateExaminationPostModel vm);
        Task<ResponseService> Delete(long id);

        Task<ICollection<ExaminationEntity>> GetByToolFK(long toolFk);
    }
}