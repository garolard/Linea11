using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Linea11.Common
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private Frame appFrame;
        private bool isBusy;

        public BaseViewModel()
        {
        }

        public Frame AppFrame
        {
            get { return appFrame; }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (value != isBusy)
                {
                    isBusy = value;
                    RaisePropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public abstract Task OnNavigatedFrom(NavigationEventArgs args);
        public abstract Task OnNavigatingFrom(NavigatingCancelEventArgs args);
        public abstract Task OnNavigatedTo(NavigationEventArgs args);

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        internal void SetAppFrame(Frame viewFrame)
        {
            appFrame = viewFrame;
        }
    }
}