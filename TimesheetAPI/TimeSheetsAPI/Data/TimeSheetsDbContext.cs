using Microsoft.EntityFrameworkCore;
using TimeSheets.API.Models;

namespace TimeSheets.API.Data
{
    public class TimeSheetsDbContext : DbContext
    {
        // Adds the DbContext of which to add DbSets (Tables) to.
        public TimeSheetsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<SheetTemplate> SheetTemplates { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Template_Question> Template_Questions { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<QA> QAs { get; set; }
        public DbSet<Period> Periods { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            


            modelBuilder.Entity<Student>().ToTable("Students", e => e.IsTemporal());
            modelBuilder.Entity<Mentor>().ToTable("Mentors", e => e.IsTemporal());
            modelBuilder.Entity<Company>().ToTable("Companies", e => e.IsTemporal());
            modelBuilder.Entity<Timesheet>().ToTable("Timesheets", e => e.IsTemporal());
            modelBuilder.Entity<SheetTemplate>().ToTable("SheetTemplates", e => e.IsTemporal());
            modelBuilder.Entity<Question>().ToTable("Questions", e => e.IsTemporal());
            modelBuilder.Entity<Template_Question>().ToTable("Template_Questions", e => e.IsTemporal());
            modelBuilder.Entity<Work>().ToTable("Works", e => e.IsTemporal());
            modelBuilder.Entity<QA>().ToTable("QAs", e => e.IsTemporal());
            modelBuilder.Entity<Period>().ToTable("Periods", e => e.IsTemporal());

        }
    }
}
