namespace plan_contenedores.Views;

public partial class FotoPage : ContentPage
{
	public FotoPage(string rutaFoto)
	{
		InitializeComponent();
        var image = new Image
        {
            Source = rutaFoto, // Asegúrate de que rutaFoto sea la ruta completa de la imagen
            Aspect = Aspect.AspectFit // Ajusta el aspecto de la imagen para que se ajuste a la ventana emergente
        };

        Content = image; // Establece la imagen como contenido de la página
    }
}