using System;
using Tools.Database.Enums;

namespace Tools.Database.Entities
{
    public class ExaminutionEntity
    {
        public long Id { get; set; }
        public ExaminationType ExaminationType { get; set; }
        public ExaminationNatureType ExaminationNature { get; set; }
        public ExaminationReasonType ExaminationReason { get; set; }
        public DateTime ScheduleExaminationDate { get; set; }
        public DateTime ActualExaminationDate { get; set; }
        public string ExaminationResult { get; set; }

        public long ToolFK { get; set; }
        public ToolEntity Tool { get; set; }
    }
}