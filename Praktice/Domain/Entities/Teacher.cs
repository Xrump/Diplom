using Praktice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Praktice.Domain.Entities
{
    public partial class Teacher
    {
        public Teacher()
        {
            Classes = new HashSet<Class>();
            Clubs = new HashSet<Club>();
            Disciplines = new HashSet<Discipline>();
        }

        public int Id { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public int? Account { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName} {Patronymic}";
            }
        }
        [NotMapped]
        public string AccountName
        {
            get
            {
                return $"Пользователь: {FullName}";
            }
        }
        [NotMapped]
        public string Discipline
        {
            get
            {
                int disciplineId = new ApplicationDbContext().Disciplines
                    .FirstOrDefault(a => a.Teacher == this.Id)
                    .Id;

                switch (disciplineId)
                {
                    default:
                        return $"Роль: Учитель алгебры";
                        break;
                    case 2:
                        return $"Роль: Учитель русского языка";
                        break;
                    case 3:
                        return $"Роль: Учитель литературы";
                        break;
                    case 4:
                        return $"Роль: Учитель информатики";
                        break;
                    case 5:
                        return $"Роль: Учитель физики";
                        break;
                    case 6:
                        return $"Роль: Учитель химии";
                        break;
                    case 7:
                        return $"Роль: Учитель биологии";
                        break;
                    case 8:
                        return $"Роль: Учитель физкультуры";
                        break;
                }
            }
        }

        public virtual Account? AccountNavigation { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Club> Clubs { get; set; }
        public virtual ICollection<Discipline> Disciplines { get; set; }
    }
}
