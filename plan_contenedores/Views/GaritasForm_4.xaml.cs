using Microsoft.Maui.Controls;
using plan_contenedores.Clases;
using plan_contenedores.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace plan_contenedores.Views;

public partial class GaritasForm_4 : ContentPage
{
    private ObservableCollection<Sello> _sellos = new ObservableCollection<Sello>();

    public ICommand EliminarSelloCommand { get; }

    private string totalFormularios;
    private string tipoEntrada;
    private string tipoContenedor;
    private string cantContenedores;
    private string nombreOperador;
    private string placas;
    private string tamanoContenedor;
    private string nombreContenedor;
    private string isoType;
    public GaritasForm_4(Dictionary<string, object> parametros)
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
        if (parametros.TryGetValue("NombreOperador", out var nOperador))
        {
            // Hacer algo con la información recibida
            nombreOperador = nOperador.ToString();
        }
        if (parametros.TryGetValue("Placas", out var tplacas))
        {
            // Hacer algo con la información recibida
            placas = tplacas.ToString();
        }
        if (parametros.TryGetValue("TamanoContenedor", out var taContenedor))
        {
            // Hacer algo con la información recibida
            tamanoContenedor = taContenedor.ToString();
        }
        if (parametros.TryGetValue("NombreContenedor", out var nContenedor))
        {
            // Hacer algo con la información recibida
            nombreContenedor = nContenedor.ToString();
        }
        if (parametros.TryGetValue("ISOType", out var iType))
        {
            // Hacer algo con la información recibida
            isoType = iType.ToString();
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
        // Asigna la lista al ListView
        sellosListView.ItemsSource = _sellos;
        // Configura los elementos del Picker (podrías llenarlos desde tu ViewModel si es necesario)
        tipoSelloPicker.ItemsSource = new string[] { "Tapon", "Alambre", "Lamina", "Plastico", "Otro", "Acero", "Genset" };
        // Inicializa el comando para eliminar sellos
        EliminarSelloCommand = new Command<Sello>(EliminarSello);
        // Asigna el contexto de la página
        BindingContext = this;
        // Puedes agregar elementos iniciales si lo deseas
    }
    private async void home(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelecionarArea());
    }
    private void AgregarSello_Clicked(object sender, EventArgs e)
    {
        stackSellos.IsVisible = true;
        // Obtiene los valores del Picker y Entry
        string tipoSello = tipoSelloPicker.SelectedItem?.ToString();
        string nombreSello = nombreSelloEntry.Text;

        // Valida que se hayan ingresado ambos valores
        if (!string.IsNullOrEmpty(tipoSello) && !string.IsNullOrEmpty(nombreSello))
        {
            // Crea un nuevo objeto Sello
            Sello nuevoSello = new Sello { TipoSello = tipoSello, NombreSello = nombreSello };

            // Agrega el nuevo sello a la lista
            _sellos.Add(nuevoSello);

            // Limpia los controles después de agregar el sello
            tipoSelloPicker.SelectedIndex = -1;
            nombreSelloEntry.Text = string.Empty;
        }
        else
        {
            // Muestra un mensaje de error si no se ingresaron ambos valores
            DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
        }
    }
    private void EliminarSello(Sello sello)
    {
        _sellos.Remove(sello);
    }

    public async void GaritasForm_5_clicked(object sender, EventArgs e)
    {
        var parametros = new Dictionary<string, object>
            {
                { "TipoEntrada", tipoEntrada },
                { "TipoContenedor", tipoContenedor },
                { "CantContenedores", cantContenedores },
                { "NombreOperador", nombreOperador },
                { "Placas", placas },
                { "TotalFormularios", totalFormularios },
                { "TamanoContenedor", tamanoContenedor },
                { "NombreContenedor", nombreContenedor },
                { "ISOType", isoType },
                { "Sellos", _sellos.ToList() }

            };
        await Navigation.PushAsync(new GaritasForm_5(parametros));
    }
}