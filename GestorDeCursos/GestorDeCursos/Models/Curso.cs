using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeCursos.Models
{
    public class Curso
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string NombreDelCurso { get; set; }

        [MaxLength(50)]
        public string TipoDeCurso { get; set; }
        [MaxLength(50)]
        public string DescripcionDelCurso { get; set; }

        public int HorasDelCurso { get; set; }
    }
}
