using System;

namespace Tools.Services.ExaminationServices.Models
{
    public class ExaminationPostMode
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public string Nature { get; set; }
        public DateTime ScheduleDate { get; set; }
        public DateTime FactDate { get; set; }
    }
}