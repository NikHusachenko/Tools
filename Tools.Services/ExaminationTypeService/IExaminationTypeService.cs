using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.Database.Entities;
using Tools.Services.Response;

namespace Tools.Services.ExaminationTypeService
{
    public interface IExaminationTypeService
    {
        Task<ResponseService<long>> Create(string name);
        Task<ResponseService> Delete(ExaminationTypeEntity entity);
        Task<ResponseService> Delete(string name);
        Task<ResponseService> Update(ExaminationTypeEntity entity);
        Task<ResponseService> Rename(string oldName, string newName);

        Task<ResponseService<ExaminationTypeEntity>> GetById(long id);
        Task<ResponseService<ExaminationTypeEntity>> GetByName(string name);

        Task<ICollection<ExaminationTypeEntity>> GetAll();
    }
}