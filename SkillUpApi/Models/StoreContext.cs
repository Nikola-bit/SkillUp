using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SkillUpApi.Models
{
    public partial class StoreContext : DbContext
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CourseLeads> CourseLeads { get; set; }
        public virtual DbSet<CourseType> CourseType { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<SessionStudent> SessionStudent { get; set; }
        public virtual DbSet<SessionStudentNote> SessionStudentNote { get; set; }
        public virtual DbSet<SessionTeacher> SessionTeacher { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Teachers> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=SkillUp;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseLeads>(entity =>
            {
                entity.HasKey(e => e.LeadId);

                entity.Property(e => e.LeadId).HasColumnName("leadId");

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("e-mail")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First Name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseLeads)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseLeads_Courses");
            });

            modelBuilder.Entity<CourseType>(entity =>
            {
                entity.Property(e => e.Android)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BackEnd)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FrontEnd)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ios)
                    .IsRequired()
                    .HasColumnName("IOS")
                    .HasMaxLength(50);

                entity.Property(e => e.Kids)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Courses>(entity =>
            {
                entity.HasKey(e => e.CourseId);

                entity.HasOne(d => d.Coursetype)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CoursetypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Courses_CourseType");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Adress)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rooms_Location");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.DateFrom).HasColumnType("date");

                entity.Property(e => e.DateTo).HasColumnType("date");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.SessionStudentId).HasColumnName("SessionStudentID");

                entity.Property(e => e.SessionTeacherId).HasColumnName("SessionTeacherID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Session_Courses");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Session_Rooms");

                entity.HasOne(d => d.SessionStudent)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.SessionStudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Session_SessionStudent");

                entity.HasOne(d => d.SessionTeacher)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.SessionTeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Session_SessionTeacher");
            });

            modelBuilder.Entity<SessionStudent>(entity =>
            {
                entity.Property(e => e.SessionStudentId).HasColumnName("SessionStudentID");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.SessionStudent)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SessionStudent_Students");
            });

            modelBuilder.Entity<SessionStudentNote>(entity =>
            {
                entity.HasKey(e => e.Noteid);

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.SessionStudent)
                    .WithMany(p => p.SessionStudentNote)
                    .HasForeignKey(d => d.SessionStudentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SessionStudentNote_SessionStudent");
            });

            modelBuilder.Entity<SessionTeacher>(entity =>
            {
                entity.Property(e => e.SessionTeacherId).HasColumnName("SessionTeacherID");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.SessionTeacher)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SessionTeacher_Teachers");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("e-mail")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First Name")
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Teachers>(entity =>
            {
                entity.HasKey(e => e.TeacherId);

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("e-mail");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First Name")
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last Name")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
