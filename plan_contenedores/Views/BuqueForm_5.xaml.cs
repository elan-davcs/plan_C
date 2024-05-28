using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui;
using Microsoft.Maui.Controls.PlatformConfiguration;
using plan_contenedores.Clases;
using plan_contenedores.Models;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using System.Windows.Input;
using static plan_contenedores.Views.BuqueForm_4;
using static plan_contenedores.Views.GaritasForm_4;
using static System.Net.Mime.MediaTypeNames;
namespace plan_contenedores.Views;

public partial class BuqueForm_5 : ContentPage
{
    ObservableCollection<Foto> fotos = new ObservableCollection<Foto>();
    private ObservableCollection<Dano> _danos = new ObservableCollection<Dano>();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    public ICommand EliminarDanoCommand { get; }
    private string photoFolderPath; // Ruta de la carpeta para almacenar las fotos
    private List<string> _caras = new List<string> { "Bottom | Piso", "Door | Puerta", "Front | Frente", "Left | Izquierda", "Right | Derecha", "Top | Techo", "Under structure | Debajo" };
    private List<string> _danospicker = new List<string> { "DT | ABOLLADO | DENT", "BW | ABOMBADO |BOWED", "OD | CADUCO LA FECHA DE INSPECCION | OUT OF INSPECCTION DATE",
    "SO | CANTONERA | CORNER BRACKET", "NL | CLAVOS EN PISO | NAILS IN FLOOR", "CT | CONTAMINADO | COMPRESSION LINE",
    "CU | CORTADO | CUT", "WT | DESGASTE POR USO | WEAR AND TEAR", "GD | DESPRENDIDO | DELAMINATED",
    "BT | DOBLADO | BENT", "DB | ESCOMBROS, RESTOS DE ESTIBA | DEBRIS, DUNNAGE", "M | FALTA | MISSING",
    "PF | FALTA DE PINTURA | PAINT FAILURE", "MP | FALTA ETIQUETA MP | LACK OF DANGEROUS LABEL", "M4 | FALTA ETIQUETA MP 4 LADOS | LACK OF DANGEROUS LABERL",
    "LO | FLOJO | LOOSE COMPONENT", "CO | OXIDADO | CORRODED, RUSTED", "MS | PERDIDO | MISSING-LOST COMPONENT",
    "HO | PERFORADO | HOLE", "RO | PODRIDO | ROOTTED", "CK | QUEBRADO | CRACKED",
    "CR | QUEDRADO | CRACKED", "BN | QUEMADO | BURNED", "SC | RASPADO | SCRACH", "SA | RASPADO OXIDADO | SCRATCHED ABRADED",
    "GT |REMOVER PEGAMENTO Y CINTA | REMOVE GLUE AND TAPE", "IR | JREP. IMP. | JIMPROPER REPAIRL",
    "BR | ROTO | BROKEN, SPLIT", "OL | SATURACION DE ACEITE | OIL SATURATED", "SD | SELLADOR DESPRENDIDO | LOOSE SEALING",
    "DS | SELLO DAÑADO | DAMAGED SEAL", "SM | SELLO MAL COLOCADO | SEAL PLACED INCORRECTLY", "NM | JSELLO NO MANIFESTADO | NON-MANIFESTED SEAL", "NS | SIN SELLO | NO SEAL",
    "DY | SUCIO | DIRTY"};
    private string Contenedor;
    private string Buque;
    private string IsoType;
    private string Gancho;
    private string Grua;
    private string Wichero;
    private string OpeWichero;
    private List<Sello> listaSellos;
    //private List<listSellos> tSellos;
    public BuqueForm_5(Dictionary<string, object> parametros)
    {
        InitializeComponent();

        if (parametros.TryGetValue("Sellos", out var sellost))
        {
            // Hacer algo con la lista de sellos recibida
            listaSellos = (List<Sello>)sellost;
        }
        if (parametros.TryGetValue("Buque", out var NomBuque))
        {
            // Hacer algo con la información recibida
            Buque = NomBuque.ToString();
        }
        if (parametros.TryGetValue("Contenedor", out var NomContenedor))
        {
            // Hacer algo con la información recibida
            Contenedor = NomContenedor.ToString();
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

        // Asigna la lista al ListView
        danosListView.ItemsSource = _danos;
        // Configura los elementos del Picker (podrías llenarlos desde tu ViewModel si es necesario)
        Cara.ItemsSource = _caras;
        Dano.ItemsSource = _danospicker;
        // Inicializa el comando para eliminar sellos
        EliminarDanoCommand = new Command<Dano>(EliminarDano);
        // Asigna el contexto de la página
        BindingContext = this;

    }
    private void Buscador1(object sender, TextChangedEventArgs e)
    {
        // Filtra la lista de elementos en el Picker según el texto de búsqueda
        string searchText = e.NewTextValue.ToLower();
        Cara.ItemsSource = _caras.Where(c => c.ToLower().Contains(searchText)).ToList();

    }
    private void Buscador2(object sender, TextChangedEventArgs e)
    {
        // Filtra la lista de elementos en el Picker según el texto de búsqueda
        string searchText = e.NewTextValue.ToLower();
        Dano.ItemsSource = _danospicker.Where(d => d.ToLower().Contains(searchText)).ToList();

    }
    private async void home(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelecionarArea());
    }
    private async void AgregarDano_Clicked(object sender, EventArgs e)
    {
        stackDanos.IsVisible = true;
        // Obtiene los valores del Picker y Entry
        string tipoCara = Cara.SelectedItem?.ToString();
        string tipoDano = Dano.SelectedItem?.ToString();

        // Valida que se hayan ingresado ambos valores
        if (!string.IsNullOrEmpty(tipoCara) && !string.IsNullOrEmpty(tipoDano))
        {
            // Crea un nuevo objeto daño
            Dano nuevoDano = new Dano { TipoCara = tipoCara, TipoDano = tipoDano };

            // Toma una foto y la asigna al objeto Dano
            string rutaFoto = await TomarFoto();
            if (!string.IsNullOrEmpty(rutaFoto))
            {
                nuevoDano.RutaFoto = rutaFoto;
            }
            // Agrega el nuevo daño a la lista
            _danos.Add(nuevoDano);


            // Limpia los controles después de agregar el sello
            Cara.SelectedIndex = -1;
            Dano.SelectedIndex = -1;
            EntryBuscar1.Text = string.Empty;
            EntryBuscar2.Text = string.Empty;
        }
        else
        {
            // Muestra un mensaje de error si no se ingresaron ambos valores
            DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
        }
    }
    private void EliminarDano(Dano dano)
    {
        _danos.Remove(dano);
        //elimina la foto que esta ligada al daño
        if (!string.IsNullOrEmpty(dano.RutaFoto))
        {
            try
            {
                File.Delete(dano.RutaFoto);
            }
            catch (Exception ex)
            {
                //manejo de cualquier error que exista
                Console.WriteLine($"Error al eliminar la foto: {ex.Message}");
            }
        }
    }
    private async void BuqueForm_6_clicked(object sender, EventArgs e)
    {
        var parametros = new Dictionary<string, object>
            {
                { "Buque", Buque },
                { "Gancho", Gancho },
                { "Grua", Grua },
                { "Wichero", Wichero },
                { "OpeWichero",  OpeWichero },
                { "Contenedor", Contenedor},
                { "IsoType", IsoType },
                { "Sellos", listaSellos.ToList() },
                { "Danos", _danos.ToList() }

            };
        await Navigation.PushAsync(new BuqueForm_6(parametros));
    }

    private async void InitializePhotoFolder()
    {
        //string folderbuqe = Buque;
        // Obtén la ruta de la carpeta de descargas
        string downloadsFolderPath = Path.Combine("/storage/emulated/0/Download/", Buque);

        // Obtén la ruta de la carpeta para almacenar las fotos
        string Ncarpetafoto = $"Photos_{Contenedor}";
        photoFolderPath = Path.Combine(downloadsFolderPath, Ncarpetafoto);

        // Crea la carpeta si no existe
        if (!Directory.Exists(photoFolderPath))
        {
            Directory.CreateDirectory(photoFolderPath);
        }
    }
    private async Task<string> TomarFoto()
    {
        InitializePhotoFolder(); // Inicializa la carpeta para almacenar fotos
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                string fileName = $"{Guid.NewGuid().ToString()}.jpg";
                string filePath = Path.Combine(photoFolderPath, fileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(filePath);
                await sourceStream.CopyToAsync(localFileStream);

                return filePath;
            }
        }
        return null;
    }

    private async void verFoto_clicked(object sender, EventArgs e)
    {
        var imagenButton = (ImageButton)sender;
        var rutaFoto = imagenButton.CommandParameter.ToString();
        await Navigation.PushAsync(new FotoPage(rutaFoto));
    }
    private void FotosListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        ((ListView)sender).SelectedItem = null; // Desmarcar la selección
    }
    public class Foto
    {
        public string Nombre { get; set; }
        public string Ruta { get; set; }
    }
}