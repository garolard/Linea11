using Linea11.Services;
using Linea11.Services.Exceptions;
using Linea11.Services.Interface;
using Linea11.ViewModels.Interface;
using SaS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linea11.ViewModels
{
    class LineasViewModel : BaseViewModel, ILineasViewModel
    {
        ILineaRepository _lineaRepository;

        public LineasViewModel()
        {
            _lineaRepository = new LineaRepository();
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
            try
            {
                IList<Domain.Linea> allLines = await _lineaRepository.ListAllAsync();
            }
            catch (InternalErrorException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return;
        }
        #endregion Navigation
    }
}
