using plan_contenedores.Clases;
using plan_contenedores.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows.Input;

namespace plan_contenedores.Views;

public partial class BuqueForm_4 : ContentPage
{
    private ObservableCollection<Sello> _sellos = new ObservableCollection<Sello>();
    public ICommand EliminarSelloCommand { get; }

    private string IsoType;
    private string Gancho;
    private string Grua;
    private string Wichero;
    private string OpeWichero;
    public BuqueForm_4(Dictionary<string, object> parametros)
    {
        InitializeComponent();

        if (parametros.TryGetValue("Buque", out var NomBuque))
        {
            // Hacer algo con la información recibida
            txt_buque.Text = NomBuque.ToString();
        }
        if (parametros.TryGetValue("Contenedor", out var NomContenedor))
        {
            // Hacer algo con la información recibida
            txt_contenedor.Text = NomContenedor.ToString();
        }
        if (parametros != null && parametros.TryGetValue("IsoType", out var NomIsoType))
        {
            // Hacer algo con la información recibida
            IsoType = NomIsoType.ToString();
        }
        if (parametros != null && parametros.TryGetValue("Gancho", out var NomGancho))
        {
            // Hacer algo con la información recibida
            Gancho = NomGancho.ToString();
        }
        if (parametros != null && parametros.TryGetValue("Grua", out var NomGrua))
        {
            // Hacer algo con la información recibida
            Grua = NomGrua.ToString();
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

        //restricciones
        Restricciones.Restriccion_sellos(nombreSelloEntry);

        // Asigna la lista al ListView
        sellosListView.ItemsSource = _sellos;
        // Configura los elementos del Picker (podrías llenarlos desde tu ViewModel si es necesario)
        tipoSelloPicker.ItemsSource = new string[] { "Tapon", "Alambre", "Lamina", "Plastico", "Otro", "Acero", "Genset" };
        // Inicializa el comando para eliminar sellos
        EliminarSelloCommand = new Command<Sello>(EliminarSello);
        // Asigna el contexto de la página
        BindingContext = this;
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

            //tSellos.Add(new listSellos { Tiposel = tipoSello, Nomsel = nombreSello });
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
    //tSellos.Add(new listSellos { Tiposel = tipoSello, Nomsel = nombreSello });
    private void EliminarSello(Sello sello)
    {
        _sellos.Remove(sello);
    }
    private async void BuqueForm_5_clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmación", "¿Estas seguro de que deseas continuar?", "Confirmar", "Cancelar");
        Debug.WriteLine("Answer: " + answer);
        if (answer == true)
        {
            var parametros = new Dictionary<string, object>
            {
                { "Buque", txt_buque.Text },
                { "Contenedor", txt_contenedor.Text },
                { "IsoType", IsoType },
                { "Gancho", Gancho },
                { "Grua", Grua },
                { "Wichero", Wichero },
                { "OpeWichero",  OpeWichero },
                { "Sellos", _sellos.ToList() }
            };
            await Navigation.PushAsync(new BuqueForm_5(parametros));
        }
    }

}