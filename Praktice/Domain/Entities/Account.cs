using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Praktice.Domain.Entities
{
    public partial class Account
    {
        public Account()
        {
            Administrations = new HashSet<Administration>();
            Announcements = new HashSet<Announcement>();
            Parents = new HashSet<Parent>();
            Pupils = new HashSet<Pupil>();
            Teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }

        [NotMapped]
        public string OwnerName
        {
            get
            {
                if (Administrations != null)
                {
                    Administration owner = Administrations
                        .SingleOrDefault(a => a.Account == this.Id);

                    return $"{owner.LastName} {owner.FirstName} {owner.Patronymic}";
                }
                else if (Parents != null)
                {
                    Parent owner = Parents
                        .SingleOrDefault(p => p.Account == this.Id);

                    return $"{owner.LastName} {owner.FirstName} {owner.Patronymic}";
                }
                else if (Pupils != null)
                {
                    Pupil owner = Pupils
                        .SingleOrDefault(p => p.Account == this.Id);

                    return $"{owner.LastName} {owner.FirstName} {owner.Patronymic}";
                }
                else
                {
                    Teacher owner = Teachers
                        .SingleOrDefault(t => t.Account == this.Id);

                    return $"{owner.LastName} {owner.FirstName} {owner.Patronymic}";
                }
            }
        }
        public virtual Role RoleNavigation { get; set; } = null!;
        public virtual ICollection<Administration> Administrations { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<Parent> Parents { get; set; }
        public virtual ICollection<Pupil> Pupils { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
