using plan_contenedores.Clases;
using static plan_contenedores.Views.BuqueForm_1;

namespace plan_contenedores.Views;

public partial class BuqueForm_1 : ContentPage
{
    private string buqueFolderPath; // Ruta de la carpeta
    public BuqueForm_1()
    {
        InitializeComponent();

        //Restrincciones
        //Restricciones.Restriccion_letras_numeros(txt_nomBuque);
    }
    private async void home(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelecionarArea());
    }
    private async void BuqueForm_2_clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_nomBuque.Text))
        {
            string folderPath = "/storage/emulated/0/Download";
            buqueFolderPath = Path.Combine(folderPath, txt_nomBuque.Text);

            // Crea la carpeta si no existe
            if (!Directory.Exists(buqueFolderPath))
            {
                Directory.CreateDirectory(buqueFolderPath);
            }
            var parametros = new Dictionary<string, object>
            {
                { "NombreBuque", txt_nomBuque.Text }
            };
            await Navigation.PushAsync(new BuqueForm_2(parametros));

        }
        else
        {
            // Muestra un mensaje de error si no se ingresaron ambos valores
            DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
        }
    }
}