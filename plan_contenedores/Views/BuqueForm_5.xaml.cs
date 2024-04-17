using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui;
using Microsoft.Maui.Controls.PlatformConfiguration;
using plan_contenedores.Clases;
using plan_contenedores.Models;
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
    private ObservableCollection<Dano> _danos = new ObservableCollection<Dano>();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    public ICommand EliminarDanoCommand { get; }

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
    private async void home(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelecionarArea());
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
}