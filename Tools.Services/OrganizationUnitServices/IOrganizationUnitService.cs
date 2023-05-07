using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Tools.Database.Entities;
using Tools.Services.Response;

namespace Tools.Services.OrganizationUnitServices
{
    public interface IOrganizationUnitService
    {
        Task<ResponseService<long>> Create(string name);
        Task<ResponseService> Delete(OrganizationUnitEntity entity);
        Task<ResponseService> Delete(string name);
        Task<ResponseService> Update(OrganizationUnitEntity entity);
        Task<ResponseService> Rename(string oldName, string newName);

        Task<ResponseService<OrganizationUnitEntity>> GetById(long id);
        Task<ResponseService<OrganizationUnitEntity>> GetByName(string name);

        Task<ICollection<OrganizationUnitEntity>> GetAll();
    }
}