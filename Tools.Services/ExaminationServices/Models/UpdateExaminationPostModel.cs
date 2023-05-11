using System;

namespace Tools.Services.ExaminationServices.Models
{
    public class UpdateExaminationPostModel
    {
        public long Id { get; set; }
        public string ExaminationNatureName { get; set; }
        public string ExaminationReasonName { get; set; }
        public string ExaminationTypeName { get; set; }
        public DateTime ScheduleDate { get; set; }
        public DateTime? FactDate { get; set; }
        public string ExaminationResult { get; set; }
    }
}