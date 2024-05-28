using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using plan_contenedores.Clases;
using plan_contenedores.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;

namespace plan_contenedores.Views;

public partial class BuqueForm_6 : ContentPage
{
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    private string Contenedor;
    private string Buque;
    private string IsoType;
    private string Gancho;
    private string Grua;
    private string Wichero;
    private string OpeWichero;
    private List<Sello> listaSellos;
    private List<Dano> listaDanos;
    private string ValidarEco = "0";
    private string datoEco;
    public BuqueForm_6(Dictionary<string, object> parametros)
    {
        InitializeComponent();
        Restricciones.Restriccion_letras_numeros(numero_eco);
        if (parametros.TryGetValue("Sellos", out var sellost))
        {
            // Hacer algo con la lista de sellos recibida
            listaSellos = (List<Sello>)sellost;
        }
        if (parametros.TryGetValue("Danos", out var danost))
        {
            // Hacer algo con la lista de sellos recibida
            listaDanos = (List<Dano>)danost;
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

        // Crear los radio buttons
        var opcion1RadioButton = planabtn;
        var opcion2RadioButton = pisobtn;
        // Manejar el evento CheckedChange para cada radio button
        opcion1RadioButton.CheckedChanged += (sender, e) =>
        {
            if (opcion1RadioButton.IsChecked)
            {
                // Acción a realizar si se selecciona la opción 1
                Console.WriteLine("Se seleccionó la Opción 1");
                label_eco.IsVisible = true;
                numero_eco.IsVisible = true;
                ValidarEco = "1";
            }
        };
        opcion2RadioButton.CheckedChanged += (sender, e) =>
        {
            if (opcion2RadioButton.IsChecked)
            {
                // Acción a realizar si se selecciona la opción 2
                Console.WriteLine("Se seleccionó la Opción 2");
                label_eco.IsVisible = false;
                numero_eco.IsVisible = false;
                ValidarEco = "0";
            }
        };
    }
    private async void home(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelecionarArea());
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
                string folderbuqe = Buque; // Path.Combine("/storage/emulated/0/Download", folderName);
                folderPath = Path.Combine("/storage/emulated/0/Download/", folderbuqe);
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                // Implementar la lógica para UWP
                // Por ejemplo, puedes usar un FolderPicker
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
                DateTime fechaEntrada = DateTime.Now;
                if (ValidarEco == "1")
                {
                    datoEco = numero_eco.Text;
                    var datos = new
                    {
                        buque = Buque,
                        gancho = Gancho,
                        grua = Grua,
                        wichero = Wichero,
                        opeWichero = OpeWichero,
                        contenedor = Contenedor,
                        isoType = IsoType,
                        Eco = datoEco,
                        Fecha = fechaEntrada,
                        Tsellos = listaSellos,
                        Tdanos = listaDanos
                    };

                    // Serializar los datos en formato JSON
                    string jsonData = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });

                    // Generar un nombre de archivo único
                    string nombreArchivo = $"{Contenedor}_{DateTime.Now:yyyyMMddHHmmss}.json";

                    // Obtener la ruta completa del archivo en la carpeta seleccionada
                    string rutaArchivo = Path.Combine(folderPath, nombreArchivo);

                    // Escribir los datos en el archivo JSON
                    File.WriteAllText(rutaArchivo, jsonData);

                    // Mostrar una alerta de éxito
                    //await App.Current.MainPage.DisplayAlert("Éxito", $"Se ha creado el archivo: {nombreArchivo}", "Aceptar");

                    // Imprimir la ruta del archivo en la consola
                    Console.WriteLine(rutaArchivo);
                }
                else
                {
                    datoEco = "Piso";
                    var datos = new
                    {
                        buque = Buque,
                        gancho = Gancho,
                        grua = Grua,
                        wichero = Wichero,
                        opeWichero = OpeWichero,
                        contenedor = Contenedor,
                        isoType = IsoType,
                        Eco = datoEco,
                        Fecha = fechaEntrada,
                        Tsellos = listaSellos,
                        Tdanos = listaDanos
                    };
                    // Serializar los datos en formato JSON
                    string jsonData = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });

                    // Generar un nombre de archivo único
                    string nombreArchivo = $"{Contenedor}_{DateTime.Now:yyyyMMddHHmmss}.json";

                    // Obtener la ruta completa del archivo en la carpeta seleccionada
                    string rutaArchivo = Path.Combine(folderPath, nombreArchivo);

                    // Escribir los datos en el archivo JSON
                    File.WriteAllText(rutaArchivo, jsonData);

                    // Mostrar una alerta de éxito
                    //await App.Current.MainPage.DisplayAlert("Éxito", $"Se ha creado el archivo: {nombreArchivo}", "Aceptar");

                    // Imprimir la ruta del archivo en la consola
                    Console.WriteLine(rutaArchivo);
                }

                /* var datos = new
                 {
                     buque = Buque,
                     gancho = Gancho,
                     grua = Grua,
                     wichero = Wichero,
                     opeWichero = OpeWichero,
                     contenedor = Contenedor,
                     isoType = IsoType,
                     Eco = ValidarEco,
                     Fecha = fechaEntrada,
                     Tsellos = listaSellos,
                     Tdanos = listaDanos
                 };*/
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier error que pueda ocurrir durante la selección de la carpeta
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    private async void BuqueForm_3_clicked(object sender, EventArgs e)
    {
        await SaveFile();

        string text = "Se ha creado el archivo correctamente";
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;
        var toast = Toast.Make(text, duration, fontSize);
        await toast.Show(cancellationTokenSource.Token);


        var parametros = new Dictionary<string, object>
            {
                { "Buque", Buque },
                { "Gancho", Gancho },
                { "Grua", Grua },
                { "Wichero", Wichero },
                { "OpeWichero",  OpeWichero },

            };
        await Navigation.PushAsync(new BuqueForm_3(parametros));
    }
}