using EFCoreEjemplos.Modelo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCoreEjemplos
{
    class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // El connectionString debe venir de un archivo de configuraciones!
            optionsBuilder.UseSqlServer("Data Source=EQUIPOVICTOR02;Initial Catalog=TestEfCoreConsola;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Agregamos una llave compuesta para tabla EstudiantesCursos
            modelBuilder.Entity<EstudianteCurso>().HasKey(x => new { x.CursoId, x.EstudianteId });

            // Filtro por tipo(Este filtro se aplicara para todas las entidades en este proyecto)
            modelBuilder.Entity<EstudianteCurso>().HasQueryFilter(x => x.Activo == true);

            // Table splitting
            modelBuilder.Entity<Estudiante>().HasOne(x => x.Detalles)
                .WithOne(x => x.Estudiante)
                .HasForeignKey<EstudianteDetalle>(x => x.Id);
            modelBuilder.Entity<EstudianteDetalle>().ToTable("Estudiantes");

            // Mapeo Flexible
            modelBuilder.Entity<Estudiante>().Property(x => x.Apellido).HasField("_Apellido");
        }

        //Cuando se salven los cambios,se aplicara este cambio
        public override int SaveChanges()
        {
            // Borrado Suave o Borrado Logico
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted && e.Metadata.GetProperties().Any(x => x.Name == "isDeleted")).ToList())
            {
                item.State = EntityState.Unchanged;
                item.CurrentValues["isDeleted"] = true;
            }

            return base.SaveChanges();
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<EstudianteCurso> EstudiantesCursos { get; set; }

        [DbFunction(Schema = "dbo")]
        public static int Cantidad_De_Cursos_Activos(int EstudianteId)
        {
            throw new Exception();
        }
    }
}
