using System;
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
        Task<ResponseService<ExaminationEntity>> Update(UpdateExaminationPostModel vm);

        Task<ResponseService<ExaminationEntity>> GetById(long id);
        Task<ICollection<ExaminationEntity>> GetByToolFK(long toolFk);

        Task<ICollection<ExaminationEntity>> GetFutureExaminations(DateTime endDate);
        Task<ICollection<ExaminationEntity>> GetFutureByOrganizationUnit(DateTime endDate, OrganizationUnitEntity organizationUnit);
        Task<ICollection<ExaminationEntity>> GetFutureBySubgroup(DateTime endDate, ToolSubgroupEntity subgroup);
        Task<ICollection<ExaminationEntity>> GetFutureByExaminationType(DateTime endDate, ExaminationTypeEntity examinationType);
        Task<ICollection<ExaminationEntity>> GetExpiredAll();
        Task<ICollection<ExaminationEntity>> GetExpiredByOrganizationUnit(OrganizationUnitEntity organizationUnit);
        Task<ICollection<ExaminationEntity>> GetExpiredBySubgroup(ToolSubgroupEntity subgroup);
        Task<ICollection<ExaminationEntity>> GetExpiredByExaminationType(ExaminationTypeEntity examinationType);
    }
}