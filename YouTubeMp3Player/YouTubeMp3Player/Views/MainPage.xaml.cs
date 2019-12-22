using BottomBar.XamarinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YouTubeMp3Player.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : BottomBarPage
    {
        public MainPage()
        {
            InitializeComponent();
            init();
        }

        void init()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            // Initialize pages
            this.Children.Add(new MusicPlayer() { IconImageSource = "player_ico.png" });
        }
    }
}