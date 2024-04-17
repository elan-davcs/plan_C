using plan_contenedores.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan_contenedores.ViewModels
{
    public class SelloViewModels : INotifyPropertyChanged
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


        public SelloViewModels()
        {
            Sellos = new List<Sello>
            {
                new Sello {}
            };

        }

        //aqui lo separo para que no se agregue mas cosas

    }
}
