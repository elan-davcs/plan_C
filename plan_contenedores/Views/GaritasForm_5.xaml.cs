
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

    private ObservableCollection<Dano> _danos = new ObservableCollection<Dano>();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    public ICommand EliminarDanoCommand { get; }

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
        danosListView.ItemsSource = _danos;
        // Configura los elementos del Picker (podrías llenarlos desde tu ViewModel si es necesario)
        Cara.ItemsSource = new string[] { "Bottom | Piso", "Door | Puerta", "Front | Frente", "Left | Izquierda", "Right | Derecha", "Top | Techo", "Under structure | Debajo" };
        Dano.ItemsSource = new string[] { "DT | ABOLLADO | DENT", "BW | ABOMBADO |BOWED", "OD | CADUCO LA FECHA DE INSPECCION | OUT OF INSPECCTION DATE",
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
        // Inicializa el comando para eliminar sellos
        EliminarDanoCommand = new Command<Dano>(EliminarDano);
        // Asigna el contexto de la página
        BindingContext = this;

    }
    private void AgregarDano_Clicked(object sender, EventArgs e)
    {
        // Obtiene los valores del Picker y Entry
        string tipoCara = Cara.SelectedItem?.ToString();
        string tipoDano = Dano.SelectedItem?.ToString();

        // Valida que se hayan ingresado ambos valores
        if (!string.IsNullOrEmpty(tipoCara) && !string.IsNullOrEmpty(tipoDano))
        {
            // Crea un nuevo objeto Sello
            Dano nuevoDano = new Dano { TipoCara = tipoCara, TipoDano = tipoDano };

            // Agrega el nuevo sello a la lista
            _danos.Add(nuevoDano);

            // Limpia los controles después de agregar el sello
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
    }
    async Task SaveFile()
    {
        try
        {
            // Verificar la plataforma y seleccionar la carpeta según corresponda
            string folderPath = "";
            if (Device.RuntimePlatform == Device.Android)
            {
                // Implementar la lógica para Android
                folderPath = "/storage/emulated/0/Download";
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                // Implementar la lógica para UWP
                // Por ejemplo, puedes usar un FolderPicker
                /*var folderPicker = new Windows.Storage.Pickers.FolderPicker();
                folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                folderPicker.FileTypeFilter.Add("*");
                Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
                folderPath = folder?.Path;*/
            }
            else
            {
                // Manejar otras plataformas según sea necesario
                await App.Current.MainPage.DisplayAlert("Advertencia", "La selección de carpetas no es compatible en esta plataforma.", "Aceptar");
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
                    Tsellos = listaSellos,
                    Tdanos = _danos.ToList()
                };

                // Serializar los datos en formato JSON
                string jsonData = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });

                // Generar un nombre de archivo único
                string nombreArchivo = $"datos_{DateTime.Now:yyyyMMddHHmmss}.json";

                // Obtener la ruta completa del archivo en la carpeta seleccionada
                string rutaArchivo = Path.Combine(folderPath, nombreArchivo);

                // Escribir los datos en el archivo JSON
                File.WriteAllText(rutaArchivo, jsonData);

                // Mostrar una alerta de éxito
                // await App.Current.MainPage.DisplayAlert("Éxito", $"Se ha creado el archivo: {nombreArchivo}", "Aceptar");

                // Imprimir la ruta del archivo en la consola
                Console.WriteLine(rutaArchivo);
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier error que pueda ocurrir durante la selección de la carpeta
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