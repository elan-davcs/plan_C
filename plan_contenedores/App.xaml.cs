
namespace plan_contenedores
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            //MainPage = new NavigationPage(new Views.Login());
            // MainPage = new plan_contenedores.Views.FlyoutPageT();
        }
    }
}
