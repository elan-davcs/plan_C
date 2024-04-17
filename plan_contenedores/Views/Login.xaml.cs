namespace plan_contenedores.Views;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
    }
    private async void SelecionarArea_clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelecionarArea());
    }
}