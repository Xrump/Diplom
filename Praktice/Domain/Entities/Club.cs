using Microsoft.EntityFrameworkCore;
using Praktice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Praktice.Domain.Entities
{
    public partial class Club
    {
        public Club()
        {
            Pupils = new HashSet<Pupil>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Leader { get; set; }
        public string? Activity { get; set; }
        [NotMapped]
        public string FullActivity
        {
            get
            {
                return $"Деятельность клуба: {Activity}";
            }
        }
        [NotMapped]
        public string LeaderFullName
        {
            get
            {
                using (var context=new ApplicationDbContext())
                {
                    Club club = context.Clubs
                        .Include(c=>c.LeaderNavigation)
                        .FirstOrDefault(c => c.Id == this.Id);

                    return $"Глава клуба: {club.LeaderNavigation.FullName}";
                }
            }
        }
        [NotMapped]
        public List<Pupil> ClubMembers
        {
            get
            {
                List<Pupil> members = new List<Pupil>();

                using(var context=new ApplicationDbContext())
                {
                    foreach (var pupil in context.Pupils.Include(p=>p.ClassNavigation))
                    {
                        if(pupil.Club==this.Id)
                            members.Add(pupil);
                    }
                }

                return members;
            }
        }

        public virtual Teacher? LeaderNavigation { get; set; }
        public virtual ICollection<Pupil> Pupils { get; set; }
    }
}
