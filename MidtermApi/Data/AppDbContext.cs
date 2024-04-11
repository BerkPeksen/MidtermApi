
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MidtermApi.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Student> Students { get; set; }

        public List<Student> GetAllStudents()
        {
            return Students.ToList();
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
            SaveChanges();
        }
        public IEnumerable<Student> GetStudents()
        {
            return Students.ToList();
        }

    }
}