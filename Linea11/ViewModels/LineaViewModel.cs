using Linea11.Domain;
using Linea11.ViewModels.Interface;
using Linea11.Views;
using Linea11.Common;
using SaS.Service.Interface;
using SaS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Linea11.Services.Interface;
using Linea11.Services;
using Linea11.Services.Exceptions;

namespace Linea11.ViewModels
{
    class LineaViewModel : BaseViewModel, ILineaViewModel
    {
        #region Members
        Linea _linea;

        IEnumerable<IParadaViewModel> _paradasIda;
        IEnumerable<IParadaViewModel> _paradasVuelta;

        IParadaRepository _paradaRepository;
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

        public IEnumerable<IParadaViewModel> ParadasIda
        {
            get { return _paradasIda; }
            set
            {
                if (value != _paradasIda)
                {
                    _paradasIda = value;
                    RaisePropertyChanged();
                }
            }
        }

        public IEnumerable<IParadaViewModel> ParadasVuelta
        {
            get { return _paradasVuelta; }
            set
            {
                if (value != _paradasVuelta)
                {
                    _paradasVuelta = value;
                    RaisePropertyChanged();
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
            _paradaRepository = new ParadaRepository();
            _navigationService = new NavigationService();

            ViewLineDetailCommand = new RelayCommand(ViewLineDetail);
        }

        public LineaViewModel(Linea linea) : base()
        {
            _linea = linea;

            _paradaRepository = new ParadaRepository();
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

        async public override Task OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs args)
        {
            if (_linea != null && _paradasIda == null || _paradasVuelta == null)
            {
                try
                {
                    IsBusy = true;
                    IList<IParadaViewModel> stops = await _paradaRepository.FindAll(_linea.Id);

                    ParadasIda = stops.Where(s => s.Sentido == Sentido.IDA);
                    ParadasVuelta = stops.Where(s => s.Sentido == Sentido.VUELTA);
                }
                catch (InternalErrorException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                finally
                {
                    IsBusy = false;
                }
            }
            return;
        }
        #endregion Navigation
    }
}
