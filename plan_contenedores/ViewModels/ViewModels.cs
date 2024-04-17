using plan_contenedores.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan_contenedores.ViewModels
{
    /*public class ViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Sello> _sellos;

        public List<Sello> Sellos
        {
            get
            {
                return _sellos;
            }
            set
            {
                _sellos = value;
                OnPropertyChanged(nameof(Sellos));
            }
        }

        public Command AgregarSelloCommand { get; }

        public ViewModels()
        {
            Sellos = new List<Sello>
        {
            new Sello{}
        };

            // Inicializa el comando para agregar sellos
            AgregarSelloCommand = new Command(AgregarNuevoSello);
        }

        private void AgregarNuevoSello()
        {
            // Aquí puedes agregar lógica para obtener los datos de alguna fuente, por ejemplo, un formulario
            // y luego agregar un nuevo sello a la lista
            Sello nuevoSello = ObtenerDatosNuevoSello();

            // Agrega el nuevo sello a la lista
            Sellos.Add(nuevoSello);

            // Notifica a las vistas que la lista de sellos ha cambiado
            OnPropertyChanged(nameof(Sellos));
        }

        private Sello ObtenerDatosNuevoSello()
        {
            // Aquí puedes implementar la lógica para obtener los datos del nuevo sello
            // Puedes obtenerlos de controles en la interfaz de usuario o de cualquier otra fuente de datos

            // Ejemplo: Crear un nuevo sello con datos estáticos para demostración
            return new Sello { TipoSello = "Nuevo Tipo", NombreSello = "NuevoNombre" };
        }
    }*/

}
