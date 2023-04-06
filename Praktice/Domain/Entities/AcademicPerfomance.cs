using Praktice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Praktice.Domain.Entities
{
    public partial class AcademicPerfomance
    {
        public int Pupil { get; set; }
        public int Discipline { get; set; }
        public DateTime Date { get; set; }
        public int? Mark { get; set; }
        //public double AverageMark
        //{
        //    get
        //    {
                
        //    }
        //}
        public virtual Discipline DisciplineNavigation { get; set; } = null!;
        public virtual Pupil PupilNavigation { get; set; } = null!;
    }
}
