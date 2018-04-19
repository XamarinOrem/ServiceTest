using System;
using System.Collections.Generic;
using TangApplication.Models;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TangApplication.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsDetailPage : Page
    {
        List<NewsModel> lstRecords = new List<NewsModel>();
        public static   NewsModel _newData = new Models.NewsModel();
        public NewsDetailPage()
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
                lstRecords = (List<NewsModel>)(e.Parameter);
                List<NewsModel> _cloneList = new List<Models.NewsModel>();
                _cloneList = lstRecords;
                lstNews.ItemsSource = lstRecords;

                int _selectedindex = lstRecords.FindIndex(x => x.post_content == _newData.post_content &&
                x.post_date == _newData.post_date &&
                x.post_img == _newData.post_img &&
                x.post_name == _newData.post_name &&
                x.post_title == _newData.post_title);

                if (_selectedindex != 0)
                {
                    lstRecords[0] = _cloneList[_selectedindex];
                    lstRecords[_selectedindex] = _cloneList[0];
                }
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
                Image  btn = (sender) as Image;
                string tagUrl = btn.Tag.ToString();
                await Launcher.LaunchUriAsync(new Uri(tagUrl));
            }
            catch
            {
            }
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
