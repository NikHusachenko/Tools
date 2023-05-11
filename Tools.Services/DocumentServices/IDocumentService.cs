using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using Tools.Database.Entities;

namespace Tools.Services.DocumentServices
{
    public interface IDocumentService
    {
        Application Application { get; set; }
        Document Word { get; set; }

        void InitDocument(List<ExaminationEntity> examinations);
        void AppendExpiredTable(List<ExaminationEntity> expired);
        void AppendFutureTable(List<ExaminationEntity> future);
        void AppendCompletedTable(List<ExaminationEntity> completed);
        void Show();

        void PrintTools(IList<ToolEntity> tools);
        void PrintTools(IList<long> ids);

        void PrintFutureCertificationsAll(List<ExaminationEntity> examinations, DateTime dateFrom);

        void PrintCertifications(IList<ExaminationEntity> examinations);
        void PrintCertifications(IList<long> ids);
    }
}