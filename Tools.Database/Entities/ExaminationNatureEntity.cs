using System;
using System.Collections.Generic;

namespace Tools.Database.Entities
{
    public class ExaminationNatureEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public ICollection<ExaminationEntity> Examinations { get; set; }
    }
}