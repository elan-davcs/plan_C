using plan_contenedores.Clases;

namespace plan_contenedores.Views;

public partial class GaritasForm_1 : ContentPage
{
    public GaritasForm_1()
    {
        InitializeComponent();
        BindingContext = this;

        OpcionesEntrada.ItemsSource = new string[] { "SPF", "Transferencia", "Guardia y custodia", "Buque", "Ferrocarril" };
        OpcionesContenedor.ItemsSource = new string[] { "Vacios", "Llenos" };
    }
    private async void home(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelecionarArea());
    }
    private async void GaritasForm_2_clicked(object sender, EventArgs e)
    {
        string tipoEntrada = OpcionesEntrada.SelectedItem?.ToString();
        string tipoContenedor = OpcionesContenedor.SelectedItem?.ToString();
        if (!string.IsNullOrEmpty(tipoEntrada) && !string.IsNullOrEmpty(tipoContenedor))
        {
            var parametros = new Dictionary<string, object>
            {
                { "TipoEntrada", tipoEntrada.ToString() },
                { "TipoContenedor", tipoContenedor.ToString() }
            };
            await Navigation.PushAsync(new GaritasForm_2(parametros));

        }
        else
        {
            // Muestra un mensaje de error si no se ingresaron ambos valores
            DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
        }
    }
}