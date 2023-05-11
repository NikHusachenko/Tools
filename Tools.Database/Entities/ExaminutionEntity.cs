using System;

namespace Tools.Database.Entities
{
    public class ExaminationEntity
    {
        public long Id { get; set; }
        public DateTime ScheduleExaminationDate { get; set; }
        public DateTime? ActualExaminationDate { get; set; }
        public string ExaminationResult { get; set; }

        public long ExaminationTypeFK { get; set; }
        public ExaminationTypeEntity ExaminationType { get; set; }

        public long ExaminationNatureFK { get; set; }
        public ExaminationNatureEntity ExaminationNature { get; set; }

        public long ExaminationReasonFK { get; set; }
        public ExaminationReasonEntity ExaminationReason { get; set; }

        public long ToolFK { get; set; }
        public ToolEntity Tool { get; set; }
    }
}