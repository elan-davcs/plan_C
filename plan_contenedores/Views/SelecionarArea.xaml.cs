using System.Collections.ObjectModel;

namespace plan_contenedores.Views;

public partial class SelecionarArea : ContentPage
{
    public SelecionarArea()
    {
        InitializeComponent();

    }
    private async void GaritasForm_1_clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GaritasForm_1());
    }

    private async void BuqueForm_1_clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BuqueForm_1());
    }
}