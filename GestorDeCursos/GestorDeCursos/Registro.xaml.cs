using System;
using System.Collections.Generic;
using GestorDeCursos.Models;

using Xamarin.Forms;

namespace GestorDeCursos
{	
	public partial class Registro : ContentPage
	{	
		public Registro ()
		{
			InitializeComponent ();
		}

        private async void Btn_Registrar(System.Object sender, System.EventArgs e)
        {

            if (string.IsNullOrEmpty(txtEmailReg.Text))
            {
                await DisplayAlert("AVISO", "El campo de email está vacio", "OK");
                return;

            }

            if (string.IsNullOrEmpty(txtContraReg.Text))
            {
                await DisplayAlert("AVISO", "El campo de contraseña está vacio", "OK");
                return;

            }

            if (string.IsNullOrEmpty(txtNombreReg.Text))
            {
                await DisplayAlert("AVISO", "El campo de nombre está vacio", "OK");
                return;

            }

            if (string.IsNullOrEmpty(txtEdadReg.Text))
            {
				await DisplayAlert("AVISO", "El campo de edad está vacio", "OK");
				return;

            }

            Users usr = new Users()
            {
                Email = txtEmailReg.Text,
                Emailpassword = txtContraReg.Text,
                NombreCompleto = txtNombreReg.Text,
                Edad = int.Parse(txtEdadReg.Text),
                FechaCreacion = DateTime.Now

            };

            await App.SQLiteDB.SaveUserModelAsync(usr);
			await DisplayAlert("AVISO", "¡Usuario registrado con exito!", "OK");

			txtEmailReg.Text = "";
			txtContraReg.Text = "";
			txtNombreReg.Text = "";
			txtEdadReg.Text = "";

			await Navigation.PushAsync(new Login());
        }
    }
}

