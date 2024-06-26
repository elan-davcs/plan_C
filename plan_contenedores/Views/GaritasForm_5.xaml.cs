
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using plan_contenedores.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Windows.Input;
using static plan_contenedores.Views.GaritasForm_4;

namespace plan_contenedores.Views;


public partial class GaritasForm_5 : ContentPage
{
    ObservableCollection<Foto> fotos = new ObservableCollection<Foto>();
    private ObservableCollection<Dano> _danos = new ObservableCollection<Dano>();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
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
    "DS | SELLO DA�ADO | DAMAGED SEAL", "SM | SELLO MAL COLOCADO | SEAL PLACED INCORRECTLY", "NM | JSELLO NO MANIFESTADO | NON-MANIFESTED SEAL", "NS | SIN SELLO | NO SEAL",
    "DY | SUCIO | DIRTY"};

    public ICommand EliminarDanoCommand { get; }
    private string photoFolderPath; // Ruta de la carpeta para almacenar las fotos
    private string estadoFolderPath; // Ruta de la carpeta rama de carpeta garitas
    private string folderEstado;

    private string totalFormularios;
    private string tipoEntrada;
    private string tipoContenedor;
    private string cantContenedores;
    private string nombreOperador;
    private string placas;
    private string tamanoContenedor;
    private string nombreContenedor;
    private string isoType;
    private List<Sello> listaSellos;
    public GaritasForm_5(Dictionary<string, object> parametros)
    {
        InitializeComponent();

        if (parametros != null && parametros.TryGetValue("Sellos", out var sellost))
        {
            // Hacer algo con la lista de sellos recibida
            listaSellos = (List<Sello>)sellost;
        }

        if (parametros.TryGetValue("TipoEntrada", out var tEntrada))
        {
            // Hacer algo con la informaci�n recibida
            tipoEntrada = tEntrada.ToString();
        }
        if (parametros.TryGetValue("TipoContenedor", out var tContenedor))
        {
            // Hacer algo con la informaci�n recibida
            tipoContenedor = tContenedor.ToString();
        }
        if (parametros.TryGetValue("CantContenedores", out var cContenedores))
        {
            // Hacer algo con la informaci�n recibida
            cantContenedores = cContenedores.ToString();
        }
        if (parametros.TryGetValue("NombreOperador", out var nOperador))
        {
            // Hacer algo con la informaci�n recibida
            nombreOperador = nOperador.ToString();
        }
        if (parametros.TryGetValue("Placas", out var tplacas))
        {
            // Hacer algo con la informaci�n recibida
            placas = tplacas.ToString();
        }
        if (parametros.TryGetValue("TamanoContenedor", out var taContenedor))
        {
            // Hacer algo con la informaci�n recibida
            tamanoContenedor = taContenedor.ToString();
        }
        if (parametros.TryGetValue("NombreContenedor", out var nContenedor))
        {
            // Hacer algo con la informaci�n recibida
            nombreContenedor = nContenedor.ToString();
        }
        if (parametros.TryGetValue("ISOType", out var iType))
        {
            // Hacer algo con la informaci�n recibida
            isoType = iType.ToString();
        }
        if (parametros != null && parametros.TryGetValue("TotalFormularios", out var tFormularios))
        {
            // Hacer algo con la informaci�n recibida
            totalFormularios = tFormularios.ToString();
        }
        else
        {
            totalFormularios = "1";
        }

        // Asigna la lista al ListView
        danosListView.ItemsSource = _danos;
        // Configura los elementos del Picker (podr�as llenarlos desde tu ViewModel si es necesario)
        Cara.ItemsSource = _caras;
        Dano.ItemsSource = _danospicker;
        // Inicializa el comando para eliminar sellos
        EliminarDanoCommand = new Command<Dano>(EliminarDano);
        // Asigna el contexto de la p�gina
        BindingContext = this;
        folderEstado = Path.Combine("/storage/emulated/0/Download/", tipoEntrada);
        estadoFolderPath = Path.Combine(folderEstado, tipoContenedor);
        // Crea la carpeta si no existe
        if (!Directory.Exists(estadoFolderPath))
        {
            Directory.CreateDirectory(estadoFolderPath);
        }

    }
    private void Buscador1(object sender, TextChangedEventArgs e)
    {
        // Filtra la lista de elementos en el Picker seg�n el texto de b�squeda
        string searchText = e.NewTextValue.ToLower();
        Cara.ItemsSource = _caras.Where(c => c.ToLower().Contains(searchText)).ToList();

    }
    private void Buscador2(object sender, TextChangedEventArgs e)
    {
        // Filtra la lista de elementos en el Picker seg�n el texto de b�squeda
        string searchText = e.NewTextValue.ToLower();
        Dano.ItemsSource = _danospicker.Where(d => d.ToLower().Contains(searchText)).ToList();

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
            // Crea un nuevo objeto Sello
            Dano nuevoDano = new Dano { TipoCara = tipoCara, TipoDano = tipoDano };

            // Toma una foto y la asigna al objeto Dano
            string rutaFoto = await TomarFoto();
            if (!string.IsNullOrEmpty(rutaFoto))
            {
                nuevoDano.RutaFoto = rutaFoto;
            }
            // Agrega el nuevo sello a la lista
            _danos.Add(nuevoDano);

            // Limpia los controles despu�s de agregar el sello
            Cara.SelectedIndex = -1;
            Dano.SelectedIndex = -1;
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
        //elimina la foto que esta ligada al da�o
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
    private async void verFoto_clicked(object sender, EventArgs e)
    {
        var imagenButton = (ImageButton)sender;
        var rutaFoto = imagenButton.CommandParameter.ToString();
        await Navigation.PushAsync(new FotoPage(rutaFoto));
    }
    private async void InitializePhotoFolder()
    {
        //string folderbuqe = Buque;
        // Obt�n la ruta de la carpeta de descargas
        //string downloadsFolderPath = Path.Combine("/storage/emulated/0/Download/", Buque);
        string downloadsFolderPath = Path.Combine(folderEstado, estadoFolderPath);

        // Obt�n la ruta de la carpeta para almacenar las fotos
        string Ncarpetafoto = $"Photos_{nombreContenedor}";
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
    private void FotosListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        ((ListView)sender).SelectedItem = null; // Desmarcar la selecci�n
    }
    // Clase para representar una foto
    public class Foto
    {
        public string Nombre { get; set; }
        public string Ruta { get; set; }
    }
    async Task SaveFile()
    {
        try
        {
            // Verificar la plataforma y seleccionar la carpeta seg�n corresponda
            string folderPath = "";
            if (Device.RuntimePlatform == Device.Android)
            {
                // Implementar la l�gica para Android
                folderPath = Path.Combine(folderEstado, estadoFolderPath);
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                // Implementar la l�gica para UWP
                // Por ejemplo, puedes usar un FolderPicker
                /*var folderPicker = new Windows.Storage.Pickers.FolderPicker();
                folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                folderPicker.FileTypeFilter.Add("*");
                Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
                folderPath = folder?.Path;*/
            }
            else
            {
                // Manejar otras plataformas seg�n sea necesario
                await App.Current.MainPage.DisplayAlert("Advertencia", "La selecci�n de carpetas no es compatible en esta plataforma.", "Aceptar");
                return;
            }

            if (!string.IsNullOrEmpty(folderPath))
            {
                // Crear un objeto con los datos que deseas guardar
                DateTime fechaEntrada = DateTime.Now.AddMinutes(1);
                var datos = new
                {
                    TipoEntrada = tipoEntrada,
                    TipoContenedor = tipoContenedor,
                    //CantContenedores = cantContenedores,
                    NombreOperador = nombreOperador,
                    Placas = placas,
                    TamanoContenedor = tamanoContenedor,
                    NombreContenedor = nombreContenedor,
                    ISOType = isoType,
                    Fecha = fechaEntrada,
                    //Fotos = photoFolderPath,
                    Tsellos = listaSellos,
                    Tdanos = _danos.ToList()
                };

                // Serializar los datos en formato JSON
                string jsonData = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });

                // Generar un nombre de archivo �nico
                string nombreArchivo = $"{nombreContenedor}_{DateTime.Now:yyyyMMddHHmmss}.json";

                // Obtener la ruta completa del archivo en la carpeta seleccionada
                string rutaArchivo = Path.Combine(folderPath, nombreArchivo);

                // Escribir los datos en el archivo JSON
                File.WriteAllText(rutaArchivo, jsonData);

                // Mostrar una alerta de �xito
                // await App.Current.MainPage.DisplayAlert("�xito", $"Se ha creado el archivo: {nombreArchivo}", "Aceptar");

                // Imprimir la ruta del archivo en la consola
                Console.WriteLine(rutaArchivo);
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier error que pueda ocurrir durante la selecci�n de la carpeta
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    private async void home(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelecionarArea());
    }
    public async void GaritasForm_clicked(object sender, EventArgs e)
    {
        int numContenedor = int.Parse(cantContenedores);
        int numFormularios = int.Parse(totalFormularios);
        string text = "Se ha creado el archivo correctamente";
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;
        var toast = Toast.Make(text, duration, fontSize);
        await toast.Show(cancellationTokenSource.Token);
        if (numContenedor == numFormularios)
        {

            await Navigation.PushAsync(new GaritasForm_1());
            await SaveFile();
        }
        else
        {
            numFormularios++;
            var parametros = new Dictionary<string, object>
            {
                { "TipoEntrada", tipoEntrada },
                { "TipoContenedor", tipoContenedor },
                { "CantContenedores", cantContenedores },
                { "NombreOperador", nombreOperador },
                { "Placas", placas },
                { "TotalFormularios", numFormularios }
            };
            await Navigation.PushAsync(new GaritasForm_3(parametros));
            await SaveFile();
        }
    }
}