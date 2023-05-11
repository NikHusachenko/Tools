using System.Collections.Generic;
using Tools.Database.Entities;

namespace Tools.Services.DocumentServices
{
    public interface IDocumentService
    {
        void PrintTools(IList<ToolEntity> tools);
        void PrintTools(IList<long> ids);

        void PrintCertifications(IList<ExaminationEntity> examinations);
        void PrintCertifications(IList<long> ids);
    }
}