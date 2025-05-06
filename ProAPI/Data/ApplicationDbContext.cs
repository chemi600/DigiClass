using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RestAPI.Migrations;
using RestAPI.Models.Entity;

namespace RestAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CursoEntity>()
             .HasOne(c => c.Profesor)
             .WithMany(u => u.CursosProfesor)
             .HasForeignKey(c => c.IdProfesor)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CursoEntity>()
           .HasMany(c => c.Participantes)
           .WithMany(u => u.Cursos)
           .UsingEntity<Dictionary<string, object>>(
               "CursoEstudiantes",
               j => j
                   .HasOne<AppUser>()
                   .WithMany()
                   .HasForeignKey("EstudianteId")
                   .HasPrincipalKey("Id")
                   .OnDelete(DeleteBehavior.Cascade),
               j => j
                   .HasOne<CursoEntity>()
                   .WithMany()
                   .HasForeignKey("CursoId")
                   .HasPrincipalKey("Id")
                   .OnDelete(DeleteBehavior.Cascade));
        }
        
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<CursoEntity> Cursos { get; set; }


    }
}
