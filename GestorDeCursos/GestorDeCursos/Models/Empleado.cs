using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace GestorDeCursos.Models
{
    public class Empleado
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string NombreDelEmpleado { get; set; }

        [MaxLength(50)]
        public string Direccion { get; set; }

        [MaxLength(50)]
        public string Curp { get; set; }
        [MaxLength(50)]
        public string TipoEmpleado { get; set; }

        public int Edad { get; set; }

        public long Telefono { get; set; }

    }
}
