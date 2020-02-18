using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouTubeMp3Player.Models;
using YouTubeMp3Player.Data;

namespace YouTubeMp3Player.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            init();
        }

        void init()
        {
            App.StartCheckInternet(NoInternetLabel, this);

            // Error label style
            NoInternetLabel.TextColor = Color.White;
            NoInternetLabel.BackgroundColor = Constants.ErrorColor;

            // Email entry style
            EntryEmail.TextColor = Constants.MainTextColor;
            EntryEmail.PlaceholderColor = Constants.PlaceHolderColor;
            EntryEmail.Completed += (s, e) => EntryPassword.Focus();
            EntryEmail.FontSize = Constants.TextM;

            // Password entry style
            EntryPassword.TextColor = Constants.MainTextColor;
            EntryPassword.PlaceholderColor = Constants.PlaceHolderColor;
            //EntryPassword.Completed += (s, e) => LoginButton_Clicked(s, e);
            EntryPassword.FontSize = Constants.TextM;

            // Login label style
            LoginLabel.TextColor = Color.White;
            LoginLabel.FontSize = Constants.TextS;

            // Login link style
            LoginLink.TextColor = Constants.OrangeTextColor;
            LoginLink.FontSize = Constants.TextS;

            // Register button style
            RegisterButton.FontSize = Constants.TextM;
            RegisterButton.TextColor = Constants.ButtonTextColor;

            RegisterButtonBackgroundColor.FillColor = Constants.ActiveOrangeColor;
        }

        async void registerAction()
        {
            User user = new User(EntryUsername.Text, EntryPassword.Text, EntryEmail.Text);

            if (user.VertifyData(true))
            {
                bool isConnected = App.CheckInternet();

                if (isConnected)
                {
                    if (EntryPassword.Text == EntryPasswordCorr.Text)
                    {
                        User registredUser = await App.RestService.RegisterUser(user);

                        if (registredUser.ErrorDescription == null || registredUser.ErrorDescription == "")
                        {
                            await DisplayAlert("Registrace", "Uživatel " + user.ErrorDescription + " úspěšně registrován. Nyní se přihlašte.", "OK");
                            await Navigation.PushModalAsync(new LoginPage());
                        }
                        else
                        {
                            await DisplayAlert("Chyba", registredUser.ErrorDescription, "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Chyba", "Zadaná hesla se neshodují.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Chyba", "Registrace selhala, protože je vypnutý internet.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Chyba", "Email, heslo nebo uživatelské jméno není vplněno.", "OK");
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            registerAction();
        }
    }
}