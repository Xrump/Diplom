using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Praktice.Domain.Entities;

namespace Praktice.Infrastructure.Persistence
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicPerfomance> AcademicPerfomances { get; set; } = null!;
        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Administration> Administrations { get; set; } = null!;
        public virtual DbSet<Announcement> Announcements { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Club> Clubs { get; set; } = null!;
        public virtual DbSet<Discipline> Disciplines { get; set; } = null!;
        public virtual DbSet<Parent> Parents { get; set; } = null!;
        public virtual DbSet<Pupil> Pupils { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-KJ1S6V8\\SQLEXPRESS;Database=NormalDB;TrustServerCertificate=Yes;Trusted_Connection=True;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicPerfomance>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AcademicPerfomance");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.DisciplineNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Discipline)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AcademicP__Disci__3F466844");

                entity.HasOne(d => d.PupilNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Pupil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AcademicP__Pupil__3E52440B");
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__Role__267ABA7A");
            });

            modelBuilder.Entity<Administration>(entity =>
            {
                entity.ToTable("Administration");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Administrations)
                    .HasForeignKey(d => d.Account)
                    .HasConstraintName("FK__Administr__Accou__4222D4EF");
            });

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.ToTable("Announcement");

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Announcements)
                    .HasForeignKey(d => d.Author)
                    .HasConstraintName("FK__Announcem__Autho__5CD6CB2B");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.HasOne(d => d.CuratorNavigation)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.Curator)
                    .HasConstraintName("FK__Class__Curator__31EC6D26");
            });

            modelBuilder.Entity<Club>(entity =>
            {
                entity.ToTable("Club");

                entity.HasOne(d => d.LeaderNavigation)
                    .WithMany(p => p.Clubs)
                    .HasForeignKey(d => d.Leader)
                    .HasConstraintName("FK__Club__Leader__2C3393D0");
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.ToTable("Discipline");

                entity.HasOne(d => d.TeacherNavigation)
                    .WithMany(p => p.Disciplines)
                    .HasForeignKey(d => d.Teacher)
                    .HasConstraintName("FK__Disciplin__Teach__2F10007B");
            });

            modelBuilder.Entity<Parent>(entity =>
            {
                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.Account)
                    .HasConstraintName("FK__Parents__Account__34C8D9D1");
            });

            modelBuilder.Entity<Pupil>(entity =>
            {
                entity.ToTable("Pupil");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Pupils)
                    .HasForeignKey(d => d.Account)
                    .HasConstraintName("FK__Pupil__Account__38996AB5");

                entity.HasOne(d => d.CaretakerNavigation)
                    .WithMany(p => p.PupilCaretakerNavigations)
                    .HasForeignKey(d => d.Caretaker)
                    .HasConstraintName("FK__Pupil__Caretaker__3C69FB99");

                entity.HasOne(d => d.ClassNavigation)
                    .WithMany(p => p.Pupils)
                    .HasForeignKey(d => d.Class)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pupil__Class__37A5467C");

                entity.HasOne(d => d.ClubNavigation)
                    .WithMany(p => p.Pupils)
                    .HasForeignKey(d => d.Club)
                    .HasConstraintName("FK__Pupil__Club__398D8EEE");

                entity.HasOne(d => d.FatherNavigation)
                    .WithMany(p => p.PupilFatherNavigations)
                    .HasForeignKey(d => d.Father)
                    .HasConstraintName("FK__Pupil__Father__3B75D760");

                entity.HasOne(d => d.MotherNavigation)
                    .WithMany(p => p.PupilMotherNavigations)
                    .HasForeignKey(d => d.Mother)
                    .HasConstraintName("FK__Pupil__Mother__3A81B327");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.HasOne(d => d.FridayNavigation)
                    .WithMany(p => p.ScheduleFridayNavigations)
                    .HasForeignKey(d => d.Friday)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedule__Friday__49C3F6B7");

                entity.HasOne(d => d.MondayNavigation)
                    .WithMany(p => p.ScheduleMondayNavigations)
                    .HasForeignKey(d => d.Monday)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedule__Monday__45F365D3");

                entity.HasOne(d => d.NumberClassNavigation)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.NumberClass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedule__Number__44FF419A");

                entity.HasOne(d => d.SaturdayNavigation)
                    .WithMany(p => p.ScheduleSaturdayNavigations)
                    .HasForeignKey(d => d.Saturday)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedule__Saturd__4AB81AF0");

                entity.HasOne(d => d.ThursdayNavigation)
                    .WithMany(p => p.ScheduleThursdayNavigations)
                    .HasForeignKey(d => d.Thursday)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedule__Thursd__48CFD27E");

                entity.HasOne(d => d.TuesdayNavigation)
                    .WithMany(p => p.ScheduleTuesdayNavigations)
                    .HasForeignKey(d => d.Tuesday)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedule__Tuesda__46E78A0C");

                entity.HasOne(d => d.WednesdayNavigation)
                    .WithMany(p => p.ScheduleWednesdayNavigations)
                    .HasForeignKey(d => d.Wednesday)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedule__Wednes__47DBAE45");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.Account)
                    .HasConstraintName("FK__Teacher__Account__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
