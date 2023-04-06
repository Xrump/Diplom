using Praktice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Praktice.Domain.Entities
{
    public partial class Parent
    {
        public Parent()
        {
            PupilCaretakerNavigations = new HashSet<Pupil>();
            PupilFatherNavigations = new HashSet<Pupil>();
            PupilMotherNavigations = new HashSet<Pupil>();
        }

        public int Id { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public int? Account { get; set; }
        [NotMapped]
        public string AccountFullName
        {
            get
            {
                return $"Пользователь: {LastName} {FirstName} {Patronymic}";
            }
        }
        [NotMapped]
        public string KidNme
        {
            get
            {
                var context = new ApplicationDbContext();
                Pupil pupil= context.Pupils
                    .FirstOrDefault(p => p.Mother == this.Id || p.Father == this.Id || p.Caretaker == this.Id);

                return $"Ребёнок: {pupil.LastName} {pupil.FirstName} {pupil.Patronymic}";
            }
        }

        public virtual Account? AccountNavigation { get; set; }
        public virtual ICollection<Pupil> PupilCaretakerNavigations { get; set; }
        public virtual ICollection<Pupil> PupilFatherNavigations { get; set; }
        public virtual ICollection<Pupil> PupilMotherNavigations { get; set; }
    }
}
