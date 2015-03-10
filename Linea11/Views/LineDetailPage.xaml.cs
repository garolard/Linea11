using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Linea11.Common;
using Linea11.Domain;
using Linea11.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Linea11.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LineDetailPage : BasePage
    {
        public LineDetailPage() : base() 
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var line = e.Parameter as Linea;
            if (line != null)
            {
                this.DataContext = new LineaViewModel(line);
            }
            base.OnNavigatedTo(e);
        }

        private void ForwardStopsListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            args.Handled = true;

            if (args.Phase != 0)
                throw new Exception("Not in phase 0");

            args.RegisterUpdateCallback(ShowStopName);
        }

        private void ShowStopName(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 1)
                throw new Exception("Not in phase 1");

            ViewModels.ParadaViewModel stop = args.Item as ViewModels.ParadaViewModel;
            if (stop != null)
            {
                Grid itemContainer = (Grid)args.ItemContainer.ContentTemplateRoot;
                TextBlock stopName = itemContainer.FindName("stopName") as TextBlock;
                if (stopName != null)
                {
                    stopName.Text = stop.NombreParada;
                }
            }

            args.RegisterUpdateCallback(ShowLinks);
        }

        private void ShowLinks(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 2)
                throw new Exception("Not in phase 2");

            ViewModels.ParadaViewModel stop = args.Item as ViewModels.ParadaViewModel;
            if (stop != null)
            {
                Grid itemContainer = (Grid)args.ItemContainer.ContentTemplateRoot;
                StackPanel linksContainer = itemContainer.FindName("linksContainer") as StackPanel;
                if (linksContainer != null)
                {
                    foreach (Linea link in stop.Enlaces)
                    {
                        IValueConverter stringToColorConverter = App.Current.Resources["StringToColorConverter"] as IValueConverter;
                        Grid enlaceContainer = new Grid();
                        enlaceContainer.Background = (SolidColorBrush)stringToColorConverter.Convert(link.ColorLinea, typeof(SolidColorBrush), null, null);
                        TextBlock nombreEnlaceTextBlock = new TextBlock() { Text = link.NombreComercial, FontSize = 18 };
                        nombreEnlaceTextBlock.Margin = new Thickness(5, 0, 5, 0);
                        enlaceContainer.Children.Add(nombreEnlaceTextBlock);
                        linksContainer.Children.Add(enlaceContainer);
                    }
                }
            }

            ((Grid)args.ItemContainer.ContentTemplateRoot).DataContext = args.Item as Parada;
        }
    }
}
