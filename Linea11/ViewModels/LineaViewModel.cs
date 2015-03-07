using Linea11.Domain;
using Linea11.ViewModels.Interface;
using Linea11.Views;
using SaS.Common;
using SaS.Service.Interface;
using SaS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Linea11.ViewModels
{
    class LineaViewModel : BaseViewModel, ILineaViewModel
    {
        #region Members
        Linea _linea;
        INavigationService _navigationService;
        #endregion Members

        #region Properties
        public int Id
        {
            get { return _linea.Id; }
            set
            {
                if (value != _linea.Id)
                {
                    _linea.Id = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string NombreComercial
        {
            get { return _linea.NombreComercial; }
            set
            {
                if (value != _linea.NombreComercial)
                {
                    _linea.NombreComercial = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ColorLinea
        {
            get { return _linea.ColorLinea; }
            set
            {
                if (value != _linea.ColorLinea)
                {
                    _linea.ColorLinea = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string OrigenLinea
        {
            get { return _linea.OrigenLinea; }
            set
            {
                if (value != _linea.OrigenLinea)
                {
                    _linea.OrigenLinea = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string DestinoLinea
        {
            get { return _linea.DestinoLinea; }
            set
            {
                if (value != _linea.DestinoLinea)
                {
                    _linea.DestinoLinea = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string DestinoIda
        {
            get { return _linea.DestinoIda; }
            set
            {
                if (value != _linea.DestinoIda)
                {
                    _linea.DestinoIda = value;
                }
            }
        }

        public string DestinoVuelta
        {
            get { return _linea.DestinoVuelta; }
            set
            {
                if (value != _linea.DestinoVuelta)
                {
                    _linea.DestinoVuelta = value;
                }
            }
        }
        #endregion Properties

        #region Commands
        public ICommand ViewLineDetailCommand { get; private set; }
        #endregion Commands

        #region Command handlers
        void ViewLineDetail()
        {
            _navigationService.NavigateTo<LineDetailPage>(_linea);
        }
        #endregion Command handlers

        public LineaViewModel() : base()
        {
            _navigationService = new NavigationService();

            ViewLineDetailCommand = new RelayCommand(ViewLineDetail);
        }

        public LineaViewModel(Linea linea) : base()
        {
            _linea = linea;
            _navigationService = new NavigationService();

            ViewLineDetailCommand = new RelayCommand(ViewLineDetail);
        }

        #region Navigation
        public override Task OnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatingFrom(Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override Task OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs args)
        {
            return null;
        }
        #endregion Navigation
    }
}
