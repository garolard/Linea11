using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linea11.ViewModels.Interface;
using Linea11.Common;
using Linea11.Domain;

namespace Linea11.ViewModels
{
    class ParadaViewModel : BaseViewModel, IParadaViewModel
    {
        #region Members
        Parada _parada;
        #endregion Members

        #region Properties
        public int Id
        {
            get { return _parada.Id; }
            set
            {
                if (value != _parada.Id)
                {
                    _parada.Id = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int IdLinea { get; set; }

        public string NombreParada
        {
            get { return _parada.NombreParada; }
            set
            {
                if (value != _parada.NombreParada)
                {
                    _parada.NombreParada = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Sentido Sentido
        {
            get { return _parada.Sentido; }
            set
            {
                if (value != _parada.Sentido)
                {
                    _parada.Sentido = value;
                    RaisePropertyChanged();
                }
            }
        }

        public IEnumerable<Linea> Enlaces
        {
            get { return _parada.Enlaces; }
            set
            {
                if (value != _parada.Enlaces)
                {
                    _parada.Enlaces = value;
                }
            }
        }

        public IEnumerable<Bus> Buses
        {
            get { return _parada.Buses; }
            set
            {
                if (value != _parada.Buses)
                {
                    _parada.Buses = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool HayBusDeLinea
        {
            get
            {
                return _parada.Buses.Any( b => b.Linea == IdLinea);
            }
        }
        #endregion Properties

        #region Commands
        #endregion Commands

        #region Navigation
        public override Task OnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override Task OnNavigatingFrom(Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override Task OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs args)
        {
            throw new NotImplementedException();
        }
        #endregion Navigation

        public ParadaViewModel() : base()
        { }

        public ParadaViewModel(Parada parada) : base()
        {
            _parada = parada;
        }
    }
}
