using GestorDeCursos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestorDeCursos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPrincipal : ContentPage
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private async void PaginaEmpleado_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CapturarFoto());
        }
        private async void PaginaCurso_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cursos());
        }
        private async void PaginaSeguimiento_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SeguimientosDeCursoPage());
        }
    }
}