using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using GestorDeCursos.Models;

namespace GestorDeCursos.Data
{
    public class SQLiteHelper
    {

        SQLiteAsyncConnection db;
        SQLiteAsyncConnection bdCursos;
        SQLiteAsyncConnection bdUser;
        SQLiteAsyncConnection bdSeguimiento;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Empleado>().Wait();

            bdCursos = new SQLiteAsyncConnection(dbPath);
            bdCursos.CreateTableAsync<Curso>().Wait();

            bdUser = new SQLiteAsyncConnection(dbPath);
            bdUser.CreateTableAsync<Users>().Wait();

            bdSeguimiento = new SQLiteAsyncConnection(dbPath);
            bdSeguimiento.CreateTableAsync<Seguimiento>().Wait();
        }

        public Task<int> SaveEmpleadoAsync(Empleado emp)
        {
            if (emp.Id != 0)
            {
                return db.UpdateAsync(emp);
            }
            else
            {
                return db.InsertAsync(emp);
            }

        }



        public Task<int> DeleteEmpleadoAsync(Empleado empleado)
        {
            return db.DeleteAsync(empleado);
        }


        public Task<List<Empleado>> GetEmpleadosAsync()
        {
            return db.Table<Empleado>().ToListAsync();
        }


        public Task<Empleado> GetEmpleadoByIdAsync(int id)
        {
            return db.Table<Empleado>().Where(a => a.Id == id).FirstOrDefaultAsync();
        }


        // Bd Cursos

        public Task<int> SaveCursoAsync(Curso curso)
        {
            if (curso.Id != 0)
            {
                return bdCursos.UpdateAsync(curso);
            }
            else
            {
                return bdCursos.InsertAsync(curso);
            }
        }

        public Task<int> DeleteCursoAsync(Curso curso)
        {
            return bdCursos.DeleteAsync(curso);
        }

        public Task<List<Curso>> GetCursosAsync()
        {
            return bdCursos.Table<Curso>().ToListAsync();
        }

        public Task<Curso> GetCursoByIdAsync(int id)
        {
            return bdCursos.Table<Curso>().Where(a => a.Id == id).FirstOrDefaultAsync();
        }


        //Bd Users

        public Task<int> SaveUserModelAsync(Users user)
        {
            return bdUser.InsertAsync(user);
        }

        public Task<List<Users>> GetUserModelAsync()
        {
            return bdUser.Table<Users>().ToListAsync();
        }

        public Task<Users> GetUserModelAsync(int id)
        {
            return bdUser.Table<Users>().Where(a => a.UserId == id).FirstOrDefaultAsync();
        }

        public Task<Users> GetUserByUsernameAndPassword(string username, string password)
        {
            return bdUser.Table<Users>().Where(a => a.Email == username && a.Emailpassword == password).FirstOrDefaultAsync();
        }

        //bdSeguimietoDelCurso

        public Task<int> SaveSeguimientoAsync(Seguimiento seguimiento)
        {
            if (seguimiento.Id != 0)
            {
                return bdSeguimiento.UpdateAsync(seguimiento);
            }
            else
            {
                return bdSeguimiento.InsertAsync(seguimiento);
            }
        }

        public Task<Seguimiento> GetSeguimientoByIdAsync(int id)
        {
            return bdSeguimiento.Table<Seguimiento>().Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Seguimiento>> GetSeguimientoAsync()
        {
            return bdSeguimiento.Table<Seguimiento>().ToListAsync();
        }

        public Task<int> DeleteSeguimientoAsync(Seguimiento seguimiento)
        {
            return bdSeguimiento.DeleteAsync(seguimiento);
        }
    }
}
