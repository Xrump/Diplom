using System;
using System.Collections.Generic;

namespace Praktice.Domain.Entities
{
    public partial class Class
    {
        public Class()
        {
            Pupils = new HashSet<Pupil>();
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Curator { get; set; }

        public virtual Teacher? CuratorNavigation { get; set; }
        public virtual ICollection<Pupil> Pupils { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
