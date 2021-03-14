using EFCoreEjemplos.Modelo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreEjemplos
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<Estudiante> estudiantes = null;

            //using (var context = new ApplicationDbContext())
            //{
            //    estudiantes = context.Estudiantes.Where(x => x.Nombre == "Richard Henderson").ToList();

            //    estudiantes[0].Nombre = "Bobby Brown";
            //}

            //EjemploActualizarEstudianteModeloDesconectado(estudiantes[0]);

            //THIS CODE IS TO UPDATE A DIRECTION FOR A STUDENT
            //Estudiante nEstudiante = new Estudiante();
            //nEstudiante.Id = 1;

            //Direccion nDireccion = new Direccion();
            //nDireccion.Id = 1;
            //nDireccion.Calle = "Ejemplo 1";
            //nDireccion.EstudianteId = nEstudiante.Id;

            //AgregarModeloUnoAUnoModeloDesconectado(nDireccion);

            //Estudiante nEstudiante = new Estudiante();
            //nEstudiante.Id = 2;

            //Direccion direccion = new Direccion();
            //direccion.Calle = "Mapple Street";
            //direccion.EstudianteId = nEstudiante.Id;
            //AddNewDirection(direccion);

            //SeedDatabase();

            //TraerEstudiantes();

            FuncionEscalarEnEF();

            Console.WriteLine("Query Aplicado");
            Console.ReadLine();
        }

        //USE THIS METHOD TO FILL A DATA-BASE WITH TEST DATA
        static void SeedDatabase()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var institucion1 = new Institucion();
                institucion1.Nombre = "Institucion 1";

                var estudiante1 = new Estudiante();
                estudiante1.Nombre = "Felipe";
                estudiante1.Apellido = "carvajal";
                estudiante1.Edad = 999;
                estudiante1.Detalles = new EstudianteDetalle() { Becado = true, CategoriaDePago = 1 };

                var estudiante2 = new Estudiante();
                estudiante2.Nombre = "Claudia";
                estudiante2.Apellido = "Chavarria";
                estudiante2.Edad = 15;
                estudiante2.Detalles = new EstudianteDetalle() { Becado = false, Carrera = "Ingeniería de Software", CategoriaDePago = 1 };


                var estudiante3 = new Estudiante();
                estudiante3.Nombre = "Roberto";
                estudiante3.Apellido = "gonzaleS";
                estudiante3.Edad = 25;
                estudiante3.Detalles = new EstudianteDetalle() { Becado = true, Carrera = "Licenciatura en Derecho", CategoriaDePago = 2 };


                var direccion1 = new Direccion();
                direccion1.Calle = "Avenida Siempreviva 123";
                estudiante1.Direccion = direccion1;

                var curso1 = new Curso();
                curso1.Nombre = "Calculo";

                var curso2 = new Curso();
                curso2.Nombre = "Algebra Lineal";

                var institucion2 = new Institucion();
                institucion2.Nombre = "Institucion 2";

                institucion1.Estudiantes.Add(estudiante1);
                institucion1.Estudiantes.Add(estudiante2);

                institucion2.Estudiantes.Add(estudiante3);

                context.Add(institucion1);
                context.Add(institucion2);
                context.Add(curso1);
                context.Add(curso2);

                context.SaveChanges();

                var estudianteCurso = new EstudianteCurso();
                estudianteCurso.Activo = true;
                estudianteCurso.CursoId = curso1.Id;
                estudianteCurso.EstudianteId = estudiante1.Id;

                var estudianteCurso2 = new EstudianteCurso();
                estudianteCurso2.Activo = false;
                estudianteCurso2.CursoId = curso1.Id;
                estudianteCurso2.EstudianteId = estudiante2.Id;

                context.Add(estudianteCurso);
                context.Add(estudianteCurso2);
                context.SaveChanges();
            }
        }

        static void TraerEstudiantes()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiantes = context.Estudiantes.ToList();

                foreach (Estudiante nEstudiante in estudiantes)
                {
                    Console.WriteLine("Nombre : {0}", nEstudiante.Nombre);
                    Console.WriteLine("Apellido : {0}", nEstudiante.Apellido);
                    Console.WriteLine("Edad : {0}", nEstudiante.Edad);
                    Console.WriteLine("Direccion : {0}", nEstudiante.Direccion);
                    Console.WriteLine();
                }

                Console.Read();
            }
        }

        static void EjemploInsertarEstudiante()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiante = new Estudiante();
                estudiante.Nombre = "Claudia";
                context.Add(estudiante);
                context.SaveChanges();
            }
        }

        //USO DEL MODELO CONECTADO
        static void EjemploActualizarEstudianteModeloConectado()
        {
            using (var context = new ApplicationDbContext())
            {
                List<Estudiante> estudiantes = context.Estudiantes.Where(x => x.Nombre == "James").ToList();
                estudiantes[0].Nombre = "Richard Henderson";
                context.SaveChanges();
            }
        }

        //USO DEL MODEL DESCONECTADO
        static void EjemploActualizarEstudianteModeloDesconectado(Estudiante estudiante)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Entry(estudiante).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }

        static void EjemploRemoverEstudianteModeloConectado()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiante = context.Estudiantes.FirstOrDefault();
                context.Remove(estudiante);
                context.SaveChanges();
            }
        }

        static void EjemploRemoverEstudianteModeloDesonectado(Estudiante estudiante)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Entry(estudiante).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        static void AgregarModeloUnoAUnoConectado()
        {
            using (var context = new ApplicationDbContext())
            {
                // Aquí agregamos un nuevo estudiante y su dirección
                var estudiante = new Estudiante();
                estudiante.Nombre = "Claudio";
                estudiante.Edad = 99;

                var direccion = new Direccion();
                direccion.Calle = "Ejemplo";
                estudiante.Direccion = direccion;

                context.Add(estudiante);
                context.SaveChanges();
            }
        }

        static void AgregarModeloUnoAUnoModeloDesconectado(Direccion direccion)
        {
            // Modelo desconectado (el campo direccion.EstudianteId debe estar lleno)
            using (var context = new ApplicationDbContext())
            {
                context.Entry(direccion).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }

        static void AddNewDirection(Direccion direccion)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Entry(direccion).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                context.SaveChanges();
            }
        }

        static void SelectDirectionStudents()
        {
            using (var context = new ApplicationDbContext())
            {
                List<Estudiante> estudiantes = context.Estudiantes.Include(x => x.Direccion).ToList();

                foreach (Estudiante nEstudiante in estudiantes)
                {
                    Console.WriteLine(nEstudiante.Nombre);
                    Console.WriteLine(nEstudiante.Edad);
                    Console.WriteLine(nEstudiante.Direccion.Calle);
                }
            }

            Console.ReadLine();
        }

        static void TraerDataRelacionadaUnoAMuchos()
        {
            using (var context = new ApplicationDbContext())
            {

                var institucionesEstudiantes1 = context.Instituciones.Where(x => x.Id == 1).Include(x => x.Estudiantes).ToList();

                // error 
                // var institucion = context.Instituciones.Where(x => x.Id == 1).Include(x => x.Estudiantes.Where(e => e.Edad > 18)).ToList();

                // proyección
                //var persona = context.Estudiantes.Select(x => new { prop =  x.Id, prop1 = x.Nombre }).FirstOrDefault();    

                var institucionesEstudiantes = context.Instituciones.Where(x => x.Id == 1)
                    .Select(x => new { Institucion = x, Estudiantes = x.Estudiantes.Where(e => e.Edad > 18).ToList() }).ToList();

                foreach (var nInstitucionesEstudiantes in institucionesEstudiantes)
                {
                    Console.WriteLine(nInstitucionesEstudiantes.Institucion.Id);
                    Console.WriteLine(nInstitucionesEstudiantes.Institucion.Nombre);

                    foreach (Estudiante nEstudiante in nInstitucionesEstudiantes.Estudiantes)
                    {

                        Console.WriteLine();
                        Console.WriteLine(nEstudiante.Nombre);
                        Console.WriteLine(nEstudiante.Edad);
                        Console.WriteLine(nEstudiante.Direccion);
                        Console.WriteLine();
                    }
                }

                Console.ReadLine();
            }
        }

        static void TraerDataRelacionadaMuchosAMuchos()
        {
            using (var context = new ApplicationDbContext())
            {
                var curso = context.Cursos.Where(x => x.Id == 1).Include(x => x.EstudiantesCursos)
                    .ThenInclude(y => y.Estudiante).FirstOrDefault();
            }
        }

        static void TraerEstudiantesCursos()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiantesCursos = context.EstudiantesCursos.ToList();
            }
        }

        static void StringInterpolationEnEF2()
        {
            using (var context = new ApplicationDbContext())
            {
                var nombre = "Felipe or 1=1";
                // Así evitamos SQL Injection
                var estudiante = context.Estudiantes.FromSqlInterpolated($"select * from Estudiantes where Nombre = {nombre}").ToList();
            }
        }

        static void EjemploConcurrencyCheck()
        {
            using (var context = new ApplicationDbContext())
            {
                var est = context.Estudiantes.FirstOrDefault();
                est.Nombre += " 2";
                est.Edad += 1;

                context.SaveChanges();
            }
        }

        static void FuncionEscalarEnEF()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiantes = context.Estudiantes
                    .Where(x => ApplicationDbContext.Cantidad_De_Cursos_Activos(x.Id) > 0).ToList();
            }
        }

        static void FuncionalidadTableSplitting()
        {
            using (var context = new ApplicationDbContext())
            {
                // Para insertar un nuevo estudiante ahora necesitamos colocar la info del detalle
                //var estudiante = new Estudiante();
                //estudiante.Nombre = "Carlos";
                //estudiante.Edad = 45;
                //estudiante.EstaBorrado = false;
                //estudiante.InstitucionId = 1;

                //var detalle = new EstudianteDetalle();
                //detalle.Becado = false;
                //detalle.Carrera = "Lic. en Matemáticas";
                //detalle.CategoriaDePago = 1;

                //estudiante.Detalles = detalle;
                //context.Add(estudiante);
                //context.SaveChanges();

                var estudiantes = context.Estudiantes.Include(x => x.Detalles).ToList();

                foreach (var _nEstudiante in estudiantes)
                {
                    Console.WriteLine();
                    Console.WriteLine("Nombre : " + _nEstudiante.Nombre + " Edad : " + _nEstudiante.Edad + " Direccion : " + _nEstudiante.Direccion);
                    Console.WriteLine();
                    Console.WriteLine("Carrera : " + _nEstudiante.Detalles.Carrera + " Categoria de Pago : " + _nEstudiante.Detalles.CategoriaDePago);
                }

                Console.ReadLine();
            }
        }

    }
}

