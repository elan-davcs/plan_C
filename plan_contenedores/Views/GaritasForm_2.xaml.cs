using plan_contenedores.Clases;

namespace plan_contenedores.Views;

public partial class GaritasForm_2 : ContentPage
{
    //public string[] ContenedoresCantidad { get; set; } = { "1", "2", "3", "4" };

    private string numeroSeleccionado;
    private string numContenedor;
    private string tipoContenedor;
    private string tipoEntrada;
    public GaritasForm_2(Dictionary<string, object> parametros)
    {
        InitializeComponent();
        // Llama a la función de restricciones
        Restricciones.Restriccion_letras(txt_nomOperador);
        //Restricciones.Formato_isotype(txt_isotype);
        Restricciones.Restriccion_letras_numeros(txt_placas);

        if (parametros.TryGetValue("TipoEntrada", out var tEntrada))
        {
            // Hacer algo con la información recibida
            tipoEntrada = tEntrada.ToString();
        }
        if (parametros.TryGetValue("TipoContenedor", out var tContenedor))
        {
            // Hacer algo con la información recibida
            tipoContenedor = tContenedor.ToString();
        }

        ContenedoresCantidad.ItemsSource = new string[] { "1", "2", "3", "4" };

    }
    private async void home(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelecionarArea());
    }

    async void GaritasForm_3_clicked(object sender, EventArgs e)
    {
        string CantContenedor = ContenedoresCantidad.SelectedItem?.ToString();
        //int numeroSeleccionado = ContenedoresCantidad.SelectedItem?.ToString();
        if (!string.IsNullOrEmpty(CantContenedor) && !string.IsNullOrEmpty(txt_nomOperador.Text) && !string.IsNullOrEmpty(txt_placas.Text))
        {
            //string numeroSeleccionadoStr = ContenedoresCantidad.SelectedItem?.ToString();
            var parametros = new Dictionary<string, object>
            {
                { "TipoEntrada", tipoEntrada },
                { "TipoContenedor", tipoContenedor },
                { "CantContenedores", CantContenedor },
                { "NombreOperador", txt_nomOperador.Text },
                { "Placas", txt_placas.Text }
            };
            await Navigation.PushAsync(new GaritasForm_3(parametros));
        }
        else
        {
            // Muestra un mensaje de error si no se ingresaron ambos valores
            DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
        }
    }
}