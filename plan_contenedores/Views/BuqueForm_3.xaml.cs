using plan_contenedores.Clases;

namespace plan_contenedores.Views;

public partial class BuqueForm_3 : ContentPage
{
    private string Gancho;
    private string Grua;
    private string Wichero;
    private string OpeWichero;

    public BuqueForm_3(Dictionary<string, object> parametros)
    {
        InitializeComponent();
        // Obtener la información pasada como parámetro
        if (parametros != null && parametros.TryGetValue("Buque", out var NomBuque))
        {
            // Hacer algo con la información recibida
            txt_nomBuque.Text = NomBuque.ToString();
        }
        if (parametros != null && parametros.TryGetValue("Grua", out var NomGrua))
        {
            // Hacer algo con la información recibida
            Grua = NomGrua.ToString();
        }
        if (parametros != null && parametros.TryGetValue("Gancho", out var NomGancho))
        {
            // Hacer algo con la información recibida
            Gancho = NomGancho.ToString();
        }
        if (parametros != null && parametros.TryGetValue("Wichero", out var NomWichero))
        {
            // Hacer algo con la información recibida
            Wichero = NomWichero.ToString();
        }
        if (parametros != null && parametros.TryGetValue("OpeWichero", out var NomOpeWichero))
        {
            // Hacer algo con la información recibida
            OpeWichero = NomOpeWichero.ToString();
        }

        // Llama a la función de restricciones
        Restricciones.Restriccion_contenedor(txt_contenedor);
        Restricciones.Restriccion_letras_numeros(txt_isotype);
        this.BindingContext = this;
        txt_contenedor.TextChanged += ValidarEntry;
        txt_isotype.TextChanged += ValidarEntryIsotype;

    }

    private async void home(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelecionarArea());
    }
    private void ValidarEntry(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txt_contenedor.Text))
        {
            mensajecontenedor.Text = "El campo no puede estar vacío";
        }
        else if (txt_contenedor.Text.Length != 11)
        {
            mensajecontenedor.Text = "El campo debe tener 11 caracteres";
        }
        else
        {
            // Si la entrada es válida, limpiar el texto del Label
            mensajecontenedor.Text = "";
        }
    }
    private void ValidarEntryIsotype(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txt_isotype.Text))
        {
            mensajeiso.Text = "El campo no puede estar vacío";
        }
        else if (txt_isotype.Text.Length != 4)
        {
            mensajeiso.Text = "El campo debe tener 4 caracteres";
        }
        else
        {
            // Si la entrada es válida, limpiar el texto del Label
            mensajeiso.Text = "";
        }
    }
    private async void BuqueForm_4_clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_contenedor.Text) && !string.IsNullOrEmpty(txt_isotype.Text) && txt_contenedor.Text.Length == 11 && txt_isotype.Text.Length == 4)
        {
            var parametros = new Dictionary<string, object>
            {
                { "Buque", txt_nomBuque.Text },
                { "Contenedor", txt_contenedor.Text },
                { "IsoType", txt_isotype.Text },
                { "Gancho", Gancho },
                { "Grua", Grua },
                { "Wichero", Wichero },
                { "OpeWichero",  OpeWichero }
            };
            await Navigation.PushAsync(new BuqueForm_4(parametros));

        }
        else
        {
            // Muestra un mensaje de error si no se ingresaron ambos valores
            DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
        }
    }
}