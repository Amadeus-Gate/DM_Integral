using System;
using System.Collections.Generic;
using System.Linq;
using GestorDeCursos.Models;
using Xamarin.Forms;

namespace GestorDeCursos
{	
	public partial class SeguimientosDeCursoPage : ContentPage
	{
        private List<Empleado> empleados;

        public SeguimientosDeCursoPage ()
		{
			InitializeComponent ();
            CargarEmpleados();
            CargarCursos();
            llenarDatos();
        }

        private async void CargarEmpleados()
        {
            empleados = await App.SQLiteDB.GetEmpleadosAsync();

            if (empleados != null && empleados.Count > 0)
            {
                // Asigna la lista de empleados al origen de datos del Picker
                txtNombre.ItemsSource = empleados.Select(e => e.NombreDelEmpleado).ToList();
            }
        }


        private void TxtNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica si hay elementos seleccionados en el Picker
            if (txtNombre.SelectedIndex != -1)
            {
                // Obtén el empleado seleccionado
                var empleadoSeleccionado = empleados[txtNombre.SelectedIndex];

            }
        }



        private async void CargarCursos()
        {
            var cursos = await App.SQLiteDB.GetCursosAsync();

            if (cursos != null && cursos.Count > 0)
            {
                // Asigna la lista de empleados al origen de datos del Picker
                txtCurso.ItemsSource = cursos.Select(e => e.NombreDelCurso).ToList();
            }
        }


        private async void BtnGuardarSeguimiento_Clicked(System.Object sender, System.EventArgs e)
        {
            {
                if (txtNombre.SelectedIndex == -1 || txtCurso.SelectedIndex == -1)
                {
                    // Asegúrate de que se hayan seleccionado el nombre del empleado y el nombre del curso
                    await DisplayAlert("Error", "Por favor, selecciona el nombre del empleado y el nombre del curso", "OK");
                    return;
                }

                try
                {
                    // Obtén el ID del empleado seleccionado
                    int idEmpleado = empleados[txtNombre.SelectedIndex].Id;

                    // Crea una nueva instancia de SeguimientoCurso
                    Seguimiento nuevoSeguimiento = new Seguimiento
                    {
                        IdEmpleado = idEmpleado,
                        NombreDelEmpleado = txtNombre.SelectedItem.ToString(),
                        NombreDelCurso = txtCurso.SelectedItem.ToString(),
                        LugarDelCurso = txtLugar.Text,
                        Fecha = txtFecha.Date,
                        Hora = txtHora.Time,
                        Estatus = pickerEstatus.SelectedItem.ToString(),
                        Calificacion = Convert.ToInt32(txtCalificacion.Text)
                    };

                    // Guarda el seguimiento en la base de datos
                    await App.SQLiteDB.SaveSeguimientoAsync(nuevoSeguimiento);

                    // Puedes mostrar un mensaje indicando que se guardó correctamente
                    await DisplayAlert("Éxito", "Seguimiento guardado correctamente", "OK");
                    LimpiarControles();
                    llenarDatos();

                }
                catch (Exception ex)
                {
                    // Maneja cualquier excepción que pueda ocurrir al guardar en la base de datos
                    await DisplayAlert("Error", $"Error al guardar el seguimiento: {ex.Message}", "OK");
                }
            }
        }

        private async void BtnTerminar_Clicked(System.Object sender, System.EventArgs e)
        {
            var seguimiento = await App.SQLiteDB.GetSeguimientoByIdAsync(int.Parse(txtId.Text));

            if (seguimiento != null)
            {
                await App.SQLiteDB.DeleteSeguimientoAsync(seguimiento);
                await DisplayAlert("Seguimiento", "Se elimino de manera exitosa", "Ok");
                LimpiarControles();
                llenarDatos();
                BtnActualizar.IsVisible = false;
                BtnTerminar.IsVisible = false;
                BtnGuardarSeguimiento.IsVisible = true;
            }
        }

        private async void BtnActualizar_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                Seguimiento seguimiento = new Seguimiento()
                {
                    Id = int.Parse(txtId.Text),
                    NombreDelEmpleado = txtNombre.SelectedItem.ToString(),
                    NombreDelCurso = txtCurso.SelectedItem.ToString(),
                    LugarDelCurso = txtLugar.Text,
                    Fecha = txtFecha.Date,
                    Hora = txtHora.Time,
                    Estatus = pickerEstatus.SelectedItem.ToString(),
                    Calificacion = int.Parse(txtCalificacion.Text),
                };

                await App.SQLiteDB.SaveSeguimientoAsync(seguimiento);

                await DisplayAlert("Registro", "Se actualizo de manera exitosa", "OK");

                LimpiarControles();
                BtnActualizar.IsVisible = false;
                BtnTerminar.IsVisible = false;
                BtnGuardarSeguimiento.IsVisible = true;
                llenarDatos();
            }
        }

        private async void lstSeguimiento_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var obj = (Seguimiento)e.SelectedItem;

            BtnGuardarSeguimiento.IsVisible = false;
            BtnActualizar.IsVisible = true;
            BtnTerminar.IsVisible = true;
            

            if (!string.IsNullOrEmpty(obj.Id.ToString()))
            {
                var seguimiento = await App.SQLiteDB.GetSeguimientoByIdAsync(obj.Id);
                if (seguimiento != null)
                {
                    txtId.Text = seguimiento.Id.ToString();
                    txtNombre.SelectedItem = seguimiento.NombreDelEmpleado;
                    txtCurso.SelectedItem = seguimiento.NombreDelCurso;
                    txtLugar.Text = seguimiento.LugarDelCurso;
                    txtFecha.Date = seguimiento.Fecha;
                    txtHora.Time = seguimiento.Hora;
                    pickerEstatus.SelectedItem = seguimiento.Estatus;
                    txtCalificacion.Text = seguimiento.Calificacion.ToString();
                }
            }
        }

        public async void llenarDatos()
        {
            var seguimientoList = await App.SQLiteDB.GetSeguimientoAsync();
            if (seguimientoList != null)
            {
                lstSeguimiento.ItemsSource = seguimientoList;
            }
        }

        public void LimpiarControles()
        {
            

            txtNombre.SelectedItem = null;
            txtCurso.SelectedItem = null;
            txtLugar.Text = "";
            pickerEstatus.SelectedItem = null;
            txtCalificacion.Text = "";
        }
    }
}

