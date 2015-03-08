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
    }
}
