using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreEjemplos.Modelo
{
    class Estudiante
    {
        public int Id { get; set; }
        [ConcurrencyCheck]
        public string Nombre { get; set; }
        private string _Apellido;

        //THIS LOGIC IS FOR MY MAPPING
        public string Apellido
        {
            get { return _Apellido; }
            set
            {
                _Apellido = value.ToUpper();
            }
        }
        public int Edad { get; set; }
        public bool isDeleted { get; set; }
        public Direccion Direccion { get; set; }//THIS PROPERTY IS FOR NAVIGATION
        public int InstitucionId { get; set; }//THIS PROPERTY IS FOR NAVIGATION
        public List<EstudianteCurso> EstudiantesCursos { get; set; }//THIS PROPERTY IS FOR NAVIGATION
        public EstudianteDetalle Detalles { get; set; } //THIS PROPERTY IS FOR NAVIGATION
    }
}
