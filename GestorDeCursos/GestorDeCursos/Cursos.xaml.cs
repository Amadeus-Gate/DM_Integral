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
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cursos : ContentPage
    {
        public Cursos()
        {
            InitializeComponent();
            headerStack.IsVisible = false;

        }

        private async void BtnRegistrarCurso_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Curso cur = new Curso
                {
                    NombreDelCurso = txtCurso.Text,
                    TipoDeCurso = pickerTipoCurso.SelectedItem.ToString(),
                    DescripcionDelCurso = txtDescripcion.Text,
                    HorasDelCurso = int.Parse(txtHoras.Text),
                };

                await App.SQLiteDB.SaveCursoAsync(cur);

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
            var CursoList = await App.SQLiteDB.GetCursosAsync();
            if (CursoList != null)
            {
                lstCurso.ItemsSource = CursoList;
            }
        }

        public bool ValidarDatos()
        {

            bool respuesta;
            if (string.IsNullOrEmpty(txtCurso.Text))
            {
                respuesta = false;

            }

            else if (string.IsNullOrEmpty(pickerTipoCurso.SelectedItem.ToString()))
            {
                respuesta = false;

            }

            else if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtHoras.Text))
            {
                respuesta = false;

            }
            else
            {
                respuesta = true;
            }
            return respuesta;


        }

        private async void BtnActualizarCurso_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdCurso.Text))
            {
                Curso cur = new Curso()
                {
                    Id = int.Parse(txtIdCurso.Text),
                    NombreDelCurso = txtCurso.Text,
                    TipoDeCurso = pickerTipoCurso.SelectedItem.ToString(),
                    DescripcionDelCurso = txtDescripcion.Text,
                    HorasDelCurso = int.Parse(txtHoras.Text),
                };

                await App.SQLiteDB.SaveCursoAsync(cur);

                await DisplayAlert("Registro", "Se actualizo de manera exitosa", "OK");

                LimpiarControles();
                txtIdCurso.IsVisible = false;
                BtnActualizarCurso.IsVisible = false;
                BtnEliminarCurso.IsVisible = false;
                BtnRegistrarCurso.IsVisible = true;
                headerStack.IsVisible = true;
                llenarDatos();
            }
        }

        private async void lstCurso_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var obj = (Curso)e.SelectedItem;

            txtIdCurso.IsVisible = true;
            BtnRegistrarCurso.IsVisible = false;
            BtnActualizarCurso.IsVisible = true;
            BtnEliminarCurso.IsVisible = true;
            headerStack.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.Id.ToString()))
            {
                var cursos = await App.SQLiteDB.GetCursoByIdAsync(obj.Id);
                if (cursos != null)
                {
                    txtIdCurso.Text = cursos.Id.ToString();
                    txtCurso.Text = cursos.NombreDelCurso;
                    pickerTipoCurso.SelectedItem = cursos.TipoDeCurso;
                    txtDescripcion.Text = cursos.DescripcionDelCurso;
                    txtHoras.Text = cursos.HorasDelCurso.ToString();
                }
            }
        }


        private async void BtnEliminarCurso_Clicked(System.Object sender, System.EventArgs e)
        {
            var curso = await App.SQLiteDB.GetCursoByIdAsync(int.Parse(txtIdCurso.Text));

            if (curso != null)
            {
                await App.SQLiteDB.DeleteCursoAsync(curso);
                await DisplayAlert("Curso", "Se elimino de manera exitosa", "Ok");
                LimpiarControles();
                llenarDatos();
                txtIdCurso.IsVisible = false;
                BtnActualizarCurso.IsVisible = false;
                BtnEliminarCurso.IsVisible = false;
                BtnRegistrarCurso.IsVisible = true;
            }
        }

        public void LimpiarControles()
        {
            txtIdCurso.Text = "";
            txtCurso.Text = "";
            pickerTipoCurso.SelectedItem = null;
            txtDescripcion.Text = "";
            txtHoras.Text = "";
        }

        private async void BtnVerCursos_Clicked(System.Object sender, System.EventArgs e)
        {
            headerStack.IsVisible = false;

            var cursosRestantes = await App.SQLiteDB.GetCursosAsync();
            if (cursosRestantes == null || cursosRestantes.Count == 0)
            {
                await DisplayAlert("Curso", "No hay informacion para mostrar", "Ok");
            }
            else
            {
                headerStack.IsVisible = true;
                llenarDatos();
            }
        }

        private async void BtnSeguimiento_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SeguimientosDeCursoPage());
        }
    }
}