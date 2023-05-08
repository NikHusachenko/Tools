using Tools.Database.Entities;
using Tools.EntityFramework.GenericRepository;

namespace Tools.Services.ExaminationServices
{
    public class ExaminationService : IExaminationService
    {
        private readonly IGenericRepository<ExaminationEntity> _examinationRepository;

        public ExaminationService(IGenericRepository<ExaminationEntity> examinationRepository)
        {
            _examinationRepository = examinationRepository;
        }


    }
}