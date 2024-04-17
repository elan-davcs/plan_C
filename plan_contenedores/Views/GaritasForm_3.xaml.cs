using Android.Systems;
using plan_contenedores.Clases;
using System.Runtime.InteropServices.JavaScript;

namespace plan_contenedores.Views;

public partial class GaritasForm_3 : ContentPage
{

    private string totalFormularios;
    private string tipoEntrada;
    private string tipoContenedor;
    private string cantContenedores;
    private string nombreOperador;
    private string placas;

    public GaritasForm_3(Dictionary<string, object> parametros)
    {
        InitializeComponent();

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
        if (parametros.TryGetValue("CantContenedores", out var cContenedores))
        {
            // Hacer algo con la información recibida
            cantContenedores = cContenedores.ToString();
        }
        if (parametros != null && parametros.TryGetValue("NombreOperador", out var nOperador))
        {
            // Hacer algo con la información recibida
            nombreOperador = nOperador.ToString();
        }
        if (parametros.TryGetValue("Placas", out var tplacas))
        {
            // Hacer algo con la información recibida
            placas = tplacas.ToString();
        }
        if (parametros != null && parametros.TryGetValue("TotalFormularios", out var tFormularios))
        {
            // Hacer algo con la información recibida
            totalFormularios = tFormularios.ToString();
        }
        else
        {
            totalFormularios = "1";
        }

        // Llama a la función de restricciones
        Restricciones.Restriccion_contenedor(txt_contenedor);
        Restricciones.Restriccion_letras_numeros(txt_isotype);
        //Restricciones.Formato_isotype(txt_isotype);
        txt_contenedor.TextChanged += ValidarEntry;
        txt_isotype.TextChanged += ValidarEntryIsotype;


        ContenedorTamano.ItemsSource = new string[] {"10OT","20BU","20DC","20FR","20HC","20HE", "20HT","20MA","20OS","20OT","20PL","20PP","20RE","20RH","20TK","20VE",
            "40AC","40BU","40CH","40DC","40FH","40FR","40HP","40HT","40IS","40MA","40OH","40OS","40OT","40PL","40PW","40RE","40RH","40TA","40VE",
            "45DC","45DV","45HC","45RH","45TK"};


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
    public async void GaritasForm_4_clicked(object sender, EventArgs e)
    {
        // Obtener los valores seleccionados o ingresados por el usuario
        string TamContenedor = ContenedorTamano.SelectedItem?.ToString();
        if (!string.IsNullOrEmpty(TamContenedor) && !string.IsNullOrEmpty(txt_contenedor.Text) && !string.IsNullOrEmpty(txt_isotype.Text) && txt_contenedor.Text.Length == 11 && txt_isotype.Text.Length == 4)
        {
            // Convertir el número de repeticiones a un entero
            //int repeticiones = int.Parse(Contenedores);
            var parametros = new Dictionary<string, object>
            {
                { "TipoEntrada", tipoEntrada },
                { "TipoContenedor", tipoContenedor },
                { "CantContenedores", cantContenedores },
                { "NombreOperador", nombreOperador },
                { "Placas", placas },
                { "TotalFormularios", totalFormularios },
                { "TamanoContenedor", TamContenedor },
                { "NombreContenedor", txt_contenedor.Text },
                { "ISOType", txt_isotype.Text },

            };

            await Navigation.PushAsync(new GaritasForm_4(parametros));
        }
        else
        {
            // Mostrar un mensaje de error si los campos necesarios no están completos
            await DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
        }
    }




}