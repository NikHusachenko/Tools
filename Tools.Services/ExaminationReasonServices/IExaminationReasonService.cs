using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.Database.Entities;
using Tools.Services.Response;

namespace Tools.Services.ExaminationReasonServices
{
    public interface IExaminationReasonService
    {
        Task<ResponseService<long>> Create(string name);
        Task<ResponseService> Delete(ExaminationReasonEntity entity);
        Task<ResponseService> Delete(string name);
        Task<ResponseService> Update(ExaminationReasonEntity entity);

        Task<ResponseService<ExaminationReasonEntity>> GetById(long id);
        Task<ResponseService<ExaminationReasonEntity>> GetByName(string name);
        Task<ICollection<ExaminationReasonEntity>> GetAll();

        Task<ResponseService> Rename(string oldName, string newnName);
    }
}