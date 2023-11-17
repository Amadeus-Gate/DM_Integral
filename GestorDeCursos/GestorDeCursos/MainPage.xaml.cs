using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorDeCursos.Models;
using Xamarin.Forms;

namespace GestorDeCursos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            llenarDatos();
        }

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Empleado emp = new Empleado
                {
                    NombreDelEmpleado = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    Curp = txtCurp.Text,
                    TipoEmpleado  = pickerTipoEmp.SelectedItem.ToString(),
                    Edad = int.Parse(txtEdad.Text),
                    Telefono = long.Parse(txtTelefono.Text)
                };

                await App.SQLiteDB.SaveEmpleadoAsync(emp);

                await DisplayAlert("Registro", "Se guardo exitosamente", "OK");
                LimpiarControles();
                llenarDatos();
            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos", "OK");
            }
        }

        public async void llenarDatos()
        {
            var empleadoList = await App.SQLiteDB.GetEmpleadosAsync();
            if (empleadoList != null)
            {
                lstEmpleado.ItemsSource = empleadoList;
            }
        }

        public bool ValidarDatos()
        {

            bool respuesta;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;

            }

            else if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                respuesta = false;

            }

            else if (string.IsNullOrEmpty(txtCurp.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(pickerTipoEmp.SelectedItem.ToString()))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;


        }

        private async void BtnActualizar_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdEmpleado.Text))
            {
                Empleado empleado = new Empleado()
                {
                    Id = int.Parse(txtIdEmpleado.Text),
                    NombreDelEmpleado = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    Curp = txtCurp.Text,
                    Edad = int.Parse(txtEdad.Text),
                    Telefono = int.Parse(txtTelefono.Text),
                    TipoEmpleado = pickerTipoEmp.SelectedItem.ToString()
                };

                await App.SQLiteDB.SaveEmpleadoAsync(empleado);

                await DisplayAlert("Registro", "Se actualizo de manera exitosa", "OK");

                LimpiarControles();
                txtIdEmpleado.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnEliminar.IsVisible = false;
                BtnRegistrar.IsVisible = true;
                llenarDatos();
            }
        }

        private async void lstEmpleado_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var obj = (Empleado)e.SelectedItem;

            BtnRegistrar.IsVisible = false;
            txtIdEmpleado.IsVisible = true;
            BtnActualizar.IsVisible = true;
            BtnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.Id.ToString()))
            {
                var empleado = await App.SQLiteDB.GetEmpleadoByIdAsync(obj.Id);
                if (empleado != null)
                {
                    txtIdEmpleado.Text = empleado.Id.ToString();
                    txtNombre.Text = empleado.NombreDelEmpleado;
                    txtDireccion.Text = empleado.Direccion;
                    txtCurp.Text = empleado.Curp;
                    txtEdad.Text = empleado.Edad.ToString();
                    txtTelefono.Text = empleado.Telefono.ToString();
                    pickerTipoEmp.SelectedItem = empleado.TipoEmpleado.ToString();
                }
            }
        }

        private async void BtnEliminar_Clicked(System.Object sender, System.EventArgs e)
        {
            var empleado = await App.SQLiteDB.GetEmpleadoByIdAsync(int.Parse(txtIdEmpleado.Text));

            if (empleado != null)
            {
                await App.SQLiteDB.DeleteEmpleadoAsync(empleado);
                await DisplayAlert("Empleado", "Se elimino de manera exitosa", "Ok");
                LimpiarControles();
                llenarDatos();
                txtIdEmpleado.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnEliminar.IsVisible = false;
                BtnRegistrar.IsVisible = true;
            }
        }

        public void LimpiarControles()
        {
            txtIdEmpleado.Text = "";
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtCurp.Text = "";
            txtEdad.Text = "";
            txtTelefono.Text = "";
            pickerTipoEmp.SelectedItem = null;
        }


        private async void BtnIrACursos_Clicked(System.Object sender, System.EventArgs e)
        {
            // Navegar a la página de Cursos
            await Navigation.PushAsync(new Cursos());
        }
    }
}
