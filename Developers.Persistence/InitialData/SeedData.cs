using Developers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Developers.Persistence.InitialData;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DevelopersDbContext(serviceProvider.GetRequiredService<DbContextOptions<DevelopersDbContext>>())) 
        {
            // Verificar si existen datos en Trainer
            if(context.Trainers.Any())
                return; // Ya hay registros

            var trainers = new Trainer[]
            {
                new Trainer{Dni="46528585", FirstName="Juan Carlos", LastName="Jimenez Díaz", Email="juan@correo.com", PhoneNumber="978 852 859"},
                new Trainer{Dni="46854216", FirstName="Alejandra Del Pilar", LastName="Ramos Rázuri", Email="alejandra@correo.com", PhoneNumber="999 999 999"},
                new Trainer{Dni="48451296", FirstName="María José", LastName="Iparraguirre Terrones", Email="maria@correo.com", PhoneNumber="888 888 888"},
                new Trainer{Dni="49854671", FirstName="José María", LastName="Uriarte Málaga", Email="jose@correo.com", PhoneNumber="777 777 777"}
            };

            foreach (var trainer in trainers) { 
                context.Trainers.Add(trainer);
            }
            context.SaveChanges();

            // Verificar si existen datos en Students
            if (context.Students.Any())
                return;

            context.Students.AddRange(
                new Student { Dni="71458296", FirstName="Julisa", LastName="Segovia Gonzales", Email="julisa@correo.com", PhoneNumber="985 999 999"},
                new Student { Dni = "70457896", FirstName = "Sandra", LastName = "Herrera Martínez", Email = "sandra@correo.com", PhoneNumber = "905 555 555" },
                new Student { Dni = "26658974", FirstName = "Paco Jesús", LastName = "Ramos Arzola", Email = "paco@correo.com", PhoneNumber = "900 000 000" },
                new Student { Dni = "42568548", FirstName = "Enzo Tony", LastName = "Reyes García", Email = "enzo@correo.com", PhoneNumber = "911 111 111" }
            );
            context.SaveChanges();

            if (context.Courses.Any()) return;

            context.Courses.Add(new Course { Name="Diseño y Arquitectura de Software", Hours=8, Description="Curso de Desarrollo web con .NET 8",Theme="repositorios", Price = 50});
            context.Courses.Add(new Course { Name = "Sistemas Inteligentes y Machine Learning", Hours = 10, Description = "Curso para aprender a trabajar con Machine Learning y Deep Learning",Theme="Agentes Inteligentes", Price = 45 });
            context.Courses.Add(new Course { Name = "Técnicas de Programación Orientado a Objetos", Hours = 4, Description = "Enfoque orientado a Objetos", Theme= "encapsulamiento", Price = 30 });
            context.Courses.Add(new Course { Name = "Power bi", Hours = 3, Description = "Aprende Power Bi para analizar y visualizar datos para tomar decisiones informadas.", Theme = "Indicadores mundiales ", Price=70 });

            context.SaveChanges();
        }
    }
}
