using System;
using System.Collections.Generic;

namespace Praktice.Domain.Entities
{
    public partial class Discipline
    {
        public Discipline()
        {
            ScheduleFridayNavigations = new HashSet<Schedule>();
            ScheduleMondayNavigations = new HashSet<Schedule>();
            ScheduleSaturdayNavigations = new HashSet<Schedule>();
            ScheduleThursdayNavigations = new HashSet<Schedule>();
            ScheduleTuesdayNavigations = new HashSet<Schedule>();
            ScheduleWednesdayNavigations = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Teacher { get; set; }

        public virtual Teacher? TeacherNavigation { get; set; }
        public virtual ICollection<Schedule> ScheduleFridayNavigations { get; set; }
        public virtual ICollection<Schedule> ScheduleMondayNavigations { get; set; }
        public virtual ICollection<Schedule> ScheduleSaturdayNavigations { get; set; }
        public virtual ICollection<Schedule> ScheduleThursdayNavigations { get; set; }
        public virtual ICollection<Schedule> ScheduleTuesdayNavigations { get; set; }
        public virtual ICollection<Schedule> ScheduleWednesdayNavigations { get; set; }
    }
}
