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
    public partial class CapturarFoto : ContentPage
    {
        public CapturarFoto()
        {
            InitializeComponent();
        }

        private async void TomarFoto_Clicked(object sender, EventArgs e)
        {
            var foto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());

            if (foto != null)
            {
                Foto.Source = ImageSource.FromStream(() =>
                {
                    return foto.GetStream();

                });

                BtnContinuar.IsVisible = true;
                TomarFoto.IsVisible = false;
            }
        }

        private async void BtnContinuar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}