using System;
using System.Collections.Generic;

namespace Praktice.Domain.Entities
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public int NumberClass { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int Saturday { get; set; }

        public virtual Discipline FridayNavigation { get; set; } = null!;
        public virtual Discipline MondayNavigation { get; set; } = null!;
        public virtual Class NumberClassNavigation { get; set; } = null!;
        public virtual Discipline SaturdayNavigation { get; set; } = null!;
        public virtual Discipline ThursdayNavigation { get; set; } = null!;
        public virtual Discipline TuesdayNavigation { get; set; } = null!;
        public virtual Discipline WednesdayNavigation { get; set; } = null!;
    }
}
