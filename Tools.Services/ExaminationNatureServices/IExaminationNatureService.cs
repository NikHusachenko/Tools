using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.Database.Entities;
using Tools.Services.Response;

namespace Tools.Services.ExaminationNatureServices
{
    public interface IExaminationNatureService
    {
        Task<ResponseService<long>> Create(string name);
        Task<ResponseService> Update(ExaminationNatureEntity entity);
        Task<ResponseService<long>> Delete(ExaminationNatureEntity entity);
        Task<ResponseService<long>> Delete(string names);

        Task<ResponseService<ExaminationNatureEntity>> GetById(long id);
        Task<ResponseService<ExaminationNatureEntity>> GetByName(string name);

        Task<ICollection<ExaminationNatureEntity>> GetAll();

        Task<ResponseService> Rename(string oldName, string newName);
    }
}