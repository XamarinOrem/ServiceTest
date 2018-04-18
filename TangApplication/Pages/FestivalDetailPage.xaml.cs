using System;
using System.Collections.Generic;
using System.Linq;
using TangApplication.Common;
using TangApplication.Models;
using Windows.System;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Windows;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TangApplication.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FestivalDetailPage : Page
    {
        List<TypeModel> lstRecords = new List<TypeModel>();
        public static   TypeModel  _newData = new Models.TypeModel();
        public FestivalDetailPage()
        {
            this.InitializeComponent();         
            
           

            String site = "<html>\n" +
                "<head>\n" +
                "    <script src=\"http://banner.faitango.it/app/banner.js.php?area=androidDevice\"></script>\n" +
                "</head>\n" +
                "\n" +
                "<body style=\"background-color:#96C12C;\">\n" +
                "<div id=\"bannerFaitangoandroidDevice\" style=\"text-align: center;\"></div>\n" +
                "\n" +
                "</body>\n" +
                "</html>";
            webViewADS.NavigateToString(site);
           

        }
       
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                lstRecords = (List<TypeModel>)(e.Parameter);
                List<TypeModel> _cloneList = new List<Models.TypeModel>();
                _cloneList = lstRecords;
                lstFestivals.ItemsSource = lstRecords;

                int _selectedindex = lstRecords.FindIndex(x => x.citta == _newData.citta &&
                x.data_da == _newData.data_da &&
                x.id == _newData.id &&
                x.titolo == _newData.titolo);
                if (_selectedindex != 0)
                {
                    //  lstRecords.RemoveAt(0);
                    //lstRecords.RemoveAt(_selectedindex);

                    // lstRecords.Insert(0, _cloneList[_selectedindex]);
                    // lstRecords.Insert(_selectedindex, _cloneList[0]);

                    lstRecords[0]= _cloneList[_selectedindex];
                    lstRecords[_selectedindex] = _cloneList[0];
                }
            }
            catch
            {
            }
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }


        private async void btnfb_Click(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                Image btn = (sender) as Image;
                string tagUrl = btn.Tag.ToString();
                await Launcher.LaunchUriAsync(new Uri(tagUrl));
            }
            catch
            {
            }

        }

        private async void btntwitter_Click(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                Image btn = (sender) as Image;
                string tagUrl = btn.Tag.ToString();
                await Launcher.LaunchUriAsync(new Uri(tagUrl));
            }
            catch
            {
            }
        }
        private async void Image_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                Image btn = (sender) as Image;
                string tagUrl = btn.Tag.ToString();
                await Launcher.LaunchUriAsync(new Uri(tagUrl));
            }
            catch
            {
            }
        }

       
    }
}
