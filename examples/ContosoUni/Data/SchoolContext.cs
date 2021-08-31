using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContosoUniversity
{ 
    public class SchoolContext : DbContext
    {
        //private readonly ILoggerFactory _loggerFactory;
        //private readonly ILogger _logger;

        public SchoolContext(DbContextOptions<SchoolContext> options):base(options)
        {
            //_loggerFactory = loggerFactory;
            //_logger = logger;
        }

        //public SchoolContext(DbContextOptions<SchoolContext> options, ILoggerFactory loggerFactory, ILogger<SchoolContext> logger) : base(options)
        //{
        //    _loggerFactory = loggerFactory;
        //    _logger = logger;
        //}

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options
        //        .LogTo(a => _logger.LogDebug(a))
        //        .UseInMemoryDatabase("uni")
        //        //.UseSqlite("Data Source=uni.db")
        //        .EnableDetailedErrors()
        //        .EnableSensitiveDataLogging()
        //        .UseLoggerFactory(_loggerFactory)
        //        ;
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Student>()
        //        .HasMany(t => t.Enrollments)
        //        .WithOne(t => t.Student)
        //        .HasForeignKey(t => t.StudentId);

        //    modelBuilder.Entity<Enrollment>()
        //        .HasIndex(t => new { t.StudentId, t.CourseId })
        //        .IsUnique();

        //    modelBuilder.Entity<Course>()
        //        .HasMany(t => t.Enrollments)
        //        .WithOne(t => t.Course)
        //        .HasForeignKey(t => t.CourseId);
        //}
    }
}