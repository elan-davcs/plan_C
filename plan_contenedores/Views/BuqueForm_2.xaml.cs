using plan_contenedores.Clases;

namespace plan_contenedores.Views;

public partial class BuqueForm_2 : ContentPage
{
    private object parametros;

    public BuqueForm_2(Dictionary<string, object> parametros)
    {
        InitializeComponent();
        // Obtener la información pasada como parámetro
        if (parametros.TryGetValue("NombreBuque", out var NomBuque))
        {
            // Hacer algo con la información recibida
            txt_buque.Text = NomBuque.ToString();
        }

        //Restrincciones
        Restricciones.Restriccion_numeros(txt_gancho);
        Restricciones.Restriccion_letras_numeros(txt_grua);
        Restricciones.Restriccion_letras_numeros(txt_wichero);
        Restricciones.Restriccion_letras(txt_operadorWichero);
    }
    private async void home(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelecionarArea());
    }
    private async void BuqueForm_3_clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_gancho.Text) && !string.IsNullOrEmpty(txt_grua.Text) && !string.IsNullOrEmpty(txt_wichero.Text) && !string.IsNullOrEmpty(txt_operadorWichero.Text))
        {
            var parametros = new Dictionary<string, object>
            {
                { "Buque", txt_buque.Text },
                { "Gancho", txt_gancho.Text },
                { "Grua", txt_grua.Text },
                { "Wichero", txt_wichero.Text },
                { "OpeWichero",  txt_operadorWichero.Text }
            };
            await Navigation.PushAsync(new BuqueForm_3(parametros));

        }
        else
        {
            // Muestra un mensaje de error si no se ingresaron ambos valores
            DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
        }
    }
}