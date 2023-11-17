using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace GestorDeCursos.Models
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }

        [MaxLength(30)]

        public string Email { get; set; }

        [MaxLength(30)]

        public string Emailpassword { get; set; }

        [MaxLength(30)]

        public string NombreCompleto { get; set; }

        public int Edad { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
