using Praktice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Praktice.Domain.Entities
{
    public partial class Pupil
    {
        public int Id { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public int Class { get; set; }
        public int? Account { get; set; }
        public int? Club { get; set; }
        public int? Mother { get; set; }
        public int? Father { get; set; }
        public int? Caretaker { get; set; }
        [NotMapped]
        public string AccauntData
        {
            get
            {
                return $"Пользователь: {LastName} {FirstName} {Patronymic}";
            }
        }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"Полное имя: {LastName} {FirstName} {Patronymic}";
            }
        }
        [NotMapped]
        public string NormalFullName
        {
            get
            {
                return $"{LastName} {FirstName} {Patronymic}";
            }
        }
        [NotMapped]
        public string GeneralMark
        {
            get
            {
                decimal generalMark = 0;

                using(var context =new ApplicationDbContext())
                {
                    List<Discipline> disciplines = context.Disciplines
                        .ToList();

                    foreach(var discipline in disciplines)
                    {
                        List<AcademicPerfomance> academicPerfomances = context.AcademicPerfomances
                            .Where(ap => ap.DisciplineNavigation.Id == discipline.Id && ap.PupilNavigation.Id==this.Id)
                            .ToList();

                        decimal summ = 0;

                        foreach (var academicPerfomance in academicPerfomances)
                        {
                            summ += Convert.ToDecimal(academicPerfomance.Mark);
                        }

                        decimal averageMark =Math.Round(summ/academicPerfomances.Count,2);
                        generalMark += averageMark;
                        }
                    generalMark=Math.Round(generalMark/disciplines.Count,2);
                }

                return $"Успеваемость: {generalMark}";
            }
        }
        [NotMapped]
        public List<AcademicPerfomance> AcademicPerfomances
        {
            get
            {
                using(var context =new ApplicationDbContext())
                {
                    return context.AcademicPerfomances
                        .Include(ap=>ap.PupilNavigation)
                        .Where(ap => ap.Pupil == this.Id)
                        .ToList();
                }
            }
        }
        public virtual Account? AccountNavigation { get; set; }
        public virtual Parent? CaretakerNavigation { get; set; }
        public virtual Class ClassNavigation { get; set; } = null!;
        public virtual Club? ClubNavigation { get; set; }
        public virtual Parent? FatherNavigation { get; set; }
        public virtual Parent? MotherNavigation { get; set; }
    }
}
