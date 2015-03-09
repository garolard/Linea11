using Linea11.Services;
using Linea11.Services.Exceptions;
using Linea11.Services.Interface;
using Linea11.ViewModels.Interface;
using Linea11.Common;
using SaS.Service.Interface;
using SaS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linea11.ViewModels
{
    class LineasViewModel : BaseViewModel, ILineasViewModel
    {
        #region Members
        ILineaRepository _lineaRepository;

        IInternetService _internetService;
        IDialogService _dialogService;

        IList<ILineaViewModel> _allLines;
        #endregion Members

        #region Properties
        public IList<ILineaViewModel> Lineas
        {
            get { return _allLines; }
            set
            {
                if (value != _allLines)
                {
                    _allLines = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion Properties

        public LineasViewModel()
        {
            _lineaRepository = new LineaRepository();

            _internetService = new InternetService();
            _dialogService = new DialogService();
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
            if (!_internetService.IsConnected())
            {
                await _dialogService.ShowDialogAsync("no hay internet");
                return;
            }

            try
            {
                IsBusy = true;
                Lineas = await _lineaRepository.ListAllAsync();
            }
            catch (InternalErrorException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                IsBusy = false;
            }
            return;
        }
        #endregion Navigation
    }
}
