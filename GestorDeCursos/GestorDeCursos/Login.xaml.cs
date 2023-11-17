using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorDeCursos.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestorDeCursos
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void LoginBtn(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtEmailLogin.Text))
            {
                await DisplayAlert("AVISO", "Debes ingresar un email valido", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtContraLogin.Text))
            {
                await DisplayAlert("AVISO", "Debes ingresar una contraseña valida", "OK");
                return;
            }

            // Realizar una consulta para buscar un usuario con el nombre de usuario proporcionado
            
            Users user = await App.SQLiteDB.GetUserByUsernameAndPassword(txtEmailLogin.Text, txtContraLogin.Text);

            if (user != null)
            {
                txtEmailLogin.Text = "";
                txtContraLogin.Text = "";
                await Navigation.PushAsync(new MenuPrincipal());
            }
            else
            {
                txtEmailLogin.Text = "";
                txtContraLogin.Text = "";
                await DisplayAlert("AVISO", "Email o contraseña incorrectos", "OK");
                return;
            }
        }

        private async void BtnRegistrarse_Clicked(System.Object sender, System.EventArgs e)
        {
            txtEmailLogin.Text = "";
            txtContraLogin.Text = "";
            await Navigation.PushAsync(new Registro());
        }

    }
}
