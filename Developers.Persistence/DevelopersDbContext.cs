using Developers.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Developers.Persistence;

//public class DevelopersDbContext : DbContext
public class DevelopersDbContext : IdentityDbContext
{
    public DevelopersDbContext(DbContextOptions<DevelopersDbContext> options) : base(options)
    { }

    // Aqui considerar todos los modelos
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // La configuración del modelo esta en un archivo externo
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
