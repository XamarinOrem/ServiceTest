using System;
using System.Collections.Generic;
using System.Linq;
using TangApplication.Common;
using TangApplication.Models;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TangApplication.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsListPage : Page
    {
        string systemLanguage = "it";
        string[] availableLanguage = { "it", "en", "de", "es", "fr" };

        List<NewsModel> lstRecords = new List<NewsModel>();
        public NewsListPage()
        {
            this.InitializeComponent();
            var count = Windows.System.UserProfile.GlobalizationPreferences.Languages.Count;
            if (count > 0)
            {
                systemLanguage = Windows.System.UserProfile.GlobalizationPreferences.Languages[0];
                systemLanguage = systemLanguage.Remove(2).ToLower();
            }
            LoadNews ();
          

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

            txtSearch.Text = "Enter text here!";
            txtSearch.GotFocus += TxtSearch_GotFocus;
            txtSearch.LostFocus += TxtSearch_LostFocus;

            dtpFrom.Date = DateTime.Now.AddDays(-10);
            dtpTo.Date = DateTime.Now;
        }
       async void LoadNews(string todate = "", string fromdate = "", string search = "",bool isfilterClick=false )
        {
            if (!availableLanguage.Contains(systemLanguage))
            {
                systemLanguage = "it";
            }
           // string url = "http://www.faitango.it/tyg_news_reader.php?date_from&date_to&filter_text&lang="+systemLanguage+"";
            string url = "http://www.faitango.it/tyg_news_reader.php?lang=" + systemLanguage + "";

            if (todate != string.Empty)
            {
                url += "&date_to="+todate ;
            }
            else
            {
                url += "&date_to";
            }
            if (fromdate != string.Empty)
            {
                url += "&date_from="+fromdate ;
            }
            else
            {
                url += "&date_from";
            }           
            if (search != string.Empty && search.Trim() != "Enter text here!")
            {
                url += "&filter_text="+search ;
            }
            else
            {
                url += "&filter_text";
            }
            try
            {
                progresRing.IsActive = true;
                progresRing.Visibility = Visibility.Visible;
                if (NetworkStatus.CheckInternetAccess())
                {                   
                    wsNewsModel _result = new wsNewsModel();
                    _result = await Common.CommonClass.GetNewsList(url);
                    progresRing.IsActive = false;
                    progresRing.Visibility = Visibility.Collapsed;
                    if (_result != null)
                    {
                        if (_result.data == null || _result.responce == "0" || _result.data.Count == 0)
                        {
                            MessageDialog msgbox = new MessageDialog("No Records");
                            await msgbox.ShowAsync();
                            return;
                        }
                        if (isfilterClick)
                        {
                            btnFilter .Source = new BitmapImage(new Uri(
"ms-appx:///Assets/filter-icon.png", UriKind.Absolute));
                        }
                        lstNews.ItemsSource = _result.data;
                        lstRecords = _result.data;
                    }
                    else
                    {
                        MessageDialog msgbox = new MessageDialog("Server error.");
                        await msgbox.ShowAsync();
                    }
                }
                else
                {
                    MessageDialog msgbox = new MessageDialog("Internet connection is not available."); 
                    await msgbox.ShowAsync();
                }
            }
            catch
            {
            }
            finally
            {
                progresRing.IsActive = false;
                progresRing.Visibility = Visibility.Collapsed;
            }
           
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

      

        private void btnBack_Click(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void lstNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var data = e.AddedItems[0];
                if (data != null)
                {
                    NewsModel _newsData = data as NewsModel ;
                    NewsDetailPage._newData = _newsData;
                    Frame.Navigate(typeof(NewsDetailPage), lstRecords);
                }
            }
        }
        private void TxtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "")
                txtSearch.Text = "Enter text here!";
            (sender as TextBox).Foreground = new SolidColorBrush(Colors.White);
        }

        private void TxtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground = new SolidColorBrush(Colors.Black);
            txtSearch.Text = "";
        }

      
          

        private void popupFilter_Closed(object sender, object e)
        {
            EverythingOnPage.IsHitTestVisible = true;
            EverythingOnPage.Opacity = 1;
            popupFilter.IsOpen = false;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            dtpFrom.Date = DateTime.Now.AddDays(-10);
            dtpTo.Date = DateTime.Now;
            txtSearch.Text = "Enter text here!";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            EverythingOnPage.IsHitTestVisible = true;
            EverythingOnPage.Opacity = 1;
            popupFilter.IsOpen = false;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            btnCancel_Click(null, null);
            LoadNews (dtpTo.Date.ToString("yyyy-MM-dd"), dtpFrom.Date.ToString("yyyy-MM-dd"),
                 txtSearch.Text.Trim(),true);
        }

        private void btnFilter_Click(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (!popupFilter.IsOpen)
            {
                btnClear_Click(null, null);
                popupFilter.IsOpen = true;
                EverythingOnPage.IsHitTestVisible = false;
                EverythingOnPage.Opacity = 0.4;
            }
        }
    }
}
