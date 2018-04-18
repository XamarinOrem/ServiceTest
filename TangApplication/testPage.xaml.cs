using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using TangApplication.Models;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using System.Windows;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TangApplication
{


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class testPage : Page
    {
        List<TypeModel> lstResults = new List<TypeModel>();
        private MainViewModel _vm;
        public Geopoint Center { get; set; }
        public testPage()
        {

            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            _vm = new MainViewModel();
            // _vm.Locations.Add(new Location { Geopoint = new Geopoint(new BasicGeoposition() { Latitude = 30.6683793, Longitude = 76.7840588 }) });
            //  _vm.Locations.Add(new Location { Geopoint = new Geopoint(new BasicGeoposition() { Latitude = 28.41, Longitude = -81.58 }) });
            DataContext = _vm;
            //Map.Center = new Geopoint(new BasicGeoposition()
            //{
            //    Latitude = 30.6683793,
            //    Longitude = 76.7840588
            //});
            //Map.ZoomLevel = 12;
            //  Map.MaxZoomLevel = 18;
            // Map.MinZoomLevel = 4;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            lstResults = (List<TypeModel>)e.Parameter;
            if (lstResults !=null && lstResults.Count > 0)
            {
                
                Map.ZoomLevel = 10;
                foreach (var item in lstResults)
                {
                    if (item.lat != string.Empty && item.@long != string.Empty)
                    {
                        Map.Center = new Geopoint(new BasicGeoposition()
                        {
                            Latitude = Convert.ToDouble(item.lat),
                            Longitude =
                             Convert.ToDouble(item.@long)
                        });
                        _vm.Locations.Add(new Location
                        {
                            Geopoint = new Geopoint(new BasicGeoposition()
                            {
                                Latitude = Convert.ToDouble(item.lat),
                                Longitude =
                             Convert.ToDouble(item.@long)
                            }),
                            Name = item.titolo

                        });
                    }
                }
             
            }
            //_vm.Locations.Add(new Location
            //{
            //    Geopoint = new Geopoint(new BasicGeoposition() { Latitude = 30.7357351, Longitude = 76.7594881 }),
            //    Name="Sector 33 "

            //});
            //_vm.Locations.Add(new Location
            //{
            //    Geopoint = new Geopoint(new BasicGeoposition() { Latitude = 30.7296426, Longitude = 76.7656 }),
            //    Name = "Chandiarh"
            //});
            //_vm.Locations.Add(new Location
            //{
            //    Geopoint = new Geopoint(new BasicGeoposition() { Latitude = 30.697524, Longitude = 76.7942356 }),
            //    Name = "Ludhaina"
            //});

        }


        private void OnMapTapped(MapControl sender, MapInputEventArgs args)
        {
            //   _vm.Locations.Add(new Location { Geopoint = args.Location });
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
           
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void btnZoomPlus_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Map.ZoomLevel < 20)
            {
                Map.ZoomLevel = Map.ZoomLevel + 1;
            }

        }

        private void btnZoomMinus_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Map.ZoomLevel > 4)
            {
                Map.ZoomLevel = Map.ZoomLevel - 1;
            }

           
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
    public class MainViewModel
    {
        private ICollection<Location> _locations = new ObservableCollection<Location>();

        public ICollection<Location> Locations
        {
            get { return _locations; }
            set { _locations = value; }
        }

    }

    public class Location
    {
        public Geopoint Geopoint { get; set; }
        public string  Name { get; set; }
        public Point Anchor { get { return new Point(0.5, 1); } }
    }
}
