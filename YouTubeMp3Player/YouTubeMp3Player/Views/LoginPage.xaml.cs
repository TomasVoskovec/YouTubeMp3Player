using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouTubeMp3Player.Models;

namespace YouTubeMp3Player.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            App.StartCheckInternet(NoInternetLabel, this);

            NoInternetLabel.TextColor = Color.White;
            NoInternetLabel.BackgroundColor = Constants.ErrorColor;

            EntryEmail.TextColor = Constants.MainTextColor;
            EntryEmail.PlaceholderColor = Constants.PlaceHolderColor;
            EntryEmail.Completed += (s, e) => EntryPassword.Focus();
            EntryEmail.FontSize = Constants.TextM;

            EntryPassword.TextColor = Constants.MainTextColor;
            EntryPassword.PlaceholderColor = Constants.PlaceHolderColor;
            EntryPassword.Completed += (s, e) => LoginButton_Clicked(s, e);
            EntryPassword.FontSize = Constants.TextM;

            ForgottenPassword.TextColor = Constants.OrangeTextColor;
            ForgottenPassword.FontSize = Constants.TextS;

            LoginButton.FontSize = Constants.TextM;
            LoginButton.TextColor = Constants.ButtonTextColor;

            RegisterLabel.FontSize = Constants.TextS;
            RegisterLabel.TextColor = Constants.ButtonTextColor;

            RegisterLink.TextColor = Constants.OrangeTextColor;

            LoginButtonBackgroundColor.FillColor = Constants.ActiveOrangeColor;

            // Auto login
            Token loadedToken = App.TokenDatabase.GetToken();
            autoLogin(loadedToken);
        }

        async void loginAction()
        {
            User user = new User(EntryEmail.Text, EntryPassword.Text);

            if (user.VertifyData())
            {
                bool isConnected = App.CheckInternet();

                if (isConnected)
                {
                    Token result = await App.RestService.Login(user);
                    //Token result = new Token();

                    if (result.AccessToken != null)
                    {
                        App.TokenDatabase.SaveToken(result);

                        User serverUser = await App.RestService.ValidateToken(result);

                        if (serverUser.Id != 0)
                        {
                            if (Device.RuntimePlatform == Device.Android)
                            {
                                Application.Current.MainPage = new NavigationPage(new MainPage())
                                {
                                    BarBackgroundColor = Color.White,
                                    BarTextColor = Color.Black,
                                };
                            }
                            if (Device.RuntimePlatform == Device.iOS)
                            {
                                await Navigation.PushModalAsync(new NavigationPage(new MainPage())
                                {
                                    BarBackgroundColor = Color.White,
                                    BarTextColor = Color.Black,
                                });
                            }
                        }
                        else
                        {
                            await DisplayAlert("Login", "Token validation error.", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Login", "Login failed, email or password are incorrect.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Login", "Login failed, no internet connection.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Login", "Login failed, email or password are empty.", "OK");
            }
        }

        async void autoLogin(Token token)
        {
            bool isConnected = App.CheckInternet();

            if (isConnected)
            {
                if (token.AccessToken != null)
                {
                    User serverUser = null;

                    try
                    {
                        serverUser = await App.RestService.ValidateToken(token);
                    }
                    catch (Exception)
                    {
                    }

                    if (serverUser.Id != 0)
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            Application.Current.MainPage = new NavigationPage(new MainPage());
                        }
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                        }
                    }
                }
            }
        }

        void LoginButton_Clicked(object sender, EventArgs e)
        {
            loginAction();
        }

        void ForgottenPassword_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Password", "Haaahaaa", "OK");
        }

        void RegisterLink_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Register", "Register page", "OK");
        }
    }
}