using plan_contenedores.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan_contenedores.ViewModels
{
    public class DanoViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Dano> _danos;

        public List<Dano> Danos
        {
            get
            {
                return _danos;
            }
            set
            {
                _danos = value;
                OnPropertyChanged(nameof(Danos));
            }
        }

        public DanoViewModels()
        {
            Danos = new List<Dano>
            {
                new Dano{}
            };
        }
    }
}
