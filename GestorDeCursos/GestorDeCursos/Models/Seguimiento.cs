using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeCursos.Models
{
    public class Seguimiento
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int IdEmpleado { get; set; }

        [MaxLength(50)]
        public string NombreDelEmpleado { get; set; }

        [MaxLength(50)]
        public string NombreDelCurso { get; set; }


        [MaxLength(50)]
        public string LugarDelCurso { get; set; }

        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }

        [MaxLength(50)]
        public string Estatus { get; set; }

        public int Calificacion { get; set; }

    }
}
