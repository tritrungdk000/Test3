using Microsoft.EntityFrameworkCore;
using Controller.Model;
using Controller.Data;

namespace REST_API_TEMPLATE.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Courses> Courses { get; set; }
        public DbSet<Students> Students { get; set; }

        public DbSet<StudentCourses> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Courses>().HasKey(c => c.CourseId);
            // Define relationship between books and authors
            builder.Entity<StudentCourses>()
                .HasOne(b => b.Student)
                .WithMany(a => a.StudentCourses)
                .HasForeignKey(b => b.Student);
                

            // Seed database with authors and books for demo
            new DbInitializer(builder).Seed();
        }
    }
}