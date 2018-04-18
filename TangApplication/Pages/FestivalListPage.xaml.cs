using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TangApplication.Common;
using TangApplication.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TangApplication.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FestivalListPage : Page
    {
        List<TypeModel> lstRecords = new List<TypeModel>();
        string systemLanguage = "it";
        string[] availableLanguage = { "it", "en", "de", "es", "fr" };
        public FestivalListPage()
        {
            this.InitializeComponent();

            var count = Windows.System.UserProfile.GlobalizationPreferences.Languages.Count;
            if (count > 0)
            {
                systemLanguage = Windows.System.UserProfile.GlobalizationPreferences.Languages[0];
                systemLanguage = systemLanguage.Remove(2).ToLower();
            }
            LoadFestivals();
             BindDropDowns();
          

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

        private void TxtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "")
                txtSearch.Text = "Enter text here!";
            (sender as TextBox).Foreground = new SolidColorBrush(Colors.White );
        }

        private void TxtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Foreground  = new SolidColorBrush(Colors.Black );
            txtSearch.Text = "";            
        }

        async void LoadFestivals(string todate="",string fromdate="",string province="",string region="",string search="",
            bool isfilterClick=false)
        {
            if (!availableLanguage.Contains(systemLanguage))
            {
                systemLanguage = "it";
            }
            //string url = "http://api.faitango.it/tango.php?type=festival&lang" + systemLanguage + "&to&from&provincia&region&search";
            string url = "http://api.faitango.it/tango.php?type=festival&lang=" + systemLanguage + "";
            if (todate != string.Empty)
            {
                url += "&to=" + todate + "";
            }
            else
            {
                url += "&to";
            }
            if (fromdate != string.Empty)
            {
                url += "&from="+fromdate+"";
            }
            else
            {
                url += "&from";
            }
            if (province  != string.Empty && province != "0")
            {
                url += "&provincia="+province+"";
            }
            else
            {
                url += "&provincia";
            }
            if (region  != string.Empty && region !="0")
            {
                url += "&region="+region +"";
            }
            else
            {
                url += "&region";
            }
            if (search  != string.Empty  && search.Trim() != "Enter text here!")
            {
                url += "&search="+search ;
            }
            else
            {
                url += "&search";
            }
            try
            {
                progresRing.IsActive = true;
                progresRing.Visibility = Visibility.Visible;
                if (NetworkStatus.CheckInternetAccess())
                {
                    wsTypeModel _result = new wsTypeModel();
                    _result = await Common.CommonClass.GetServiceData(url);

                    progresRing.IsActive = false;
                    progresRing.Visibility = Visibility.Collapsed;
                    lstFestivals.ItemsSource = null;

                    if (_result != null)
                    {
                        if(_result.data ==null || _result.data.Count==0)
                        {
                            MessageDialog msgbox = new MessageDialog(_result.mesg);                          
                            await msgbox.ShowAsync();
                            return;
                        }
                        if (isfilterClick)
                        {
                            btnFilter.Source = new BitmapImage(new Uri(
"ms-appx:///Assets/filter-icon.png", UriKind.Absolute));
                        }
                        lstFestivals.ItemsSource = _result.data;
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
       
        async void BindDropDowns()
        {
            btnFilter.IsHitTestVisible = false;
            string url = "http://api.faitango.it/filter.php?";
            try
            {
               
                if (NetworkStatus.CheckInternetAccess())
                {
                    wsFilterDropDownsModel  _result = new wsFilterDropDownsModel();
                    _result = await Common.CommonClass.GetFiltersDropDowns (url);
                    
                    if (_result != null)
                    {
                        List<Province> lstProvince = new List<Models.Province>();                        
                        lstProvince = _result.province.ToList();
                        lstProvince.Insert(0, new Models.Province { codes = "0", name = "Select province" });

                        cmbProvince.ItemsSource = lstProvince;
                        cmbProvince.DisplayMemberPath = "name";
                        cmbProvince.SelectedValuePath = "codes";

                        cmbProvince.SelectedIndex = 0;

                        List<Region > lstRegion = new List<Models.Region >();
                        lstRegion = _result.region .ToList();
                        lstRegion.Insert(0, new Models.Region  { codes = "0", name = "Select region" });

                        cmbRegion .ItemsSource = lstRegion;
                        cmbRegion.DisplayMemberPath = "name";
                        cmbRegion.SelectedValuePath = "codes";

                        cmbRegion.SelectedIndex = 0;
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

                btnFilter.IsHitTestVisible = true;
            }

        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
       
       
      
        private void btnBack_Click(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void lstFestivals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var data = e.AddedItems[0];
                if (data != null)
                {
                    TypeModel  _Data = data as TypeModel;
                    FestivalDetailPage ._newData = _Data;
                    Frame.Navigate(typeof(FestivalDetailPage), lstRecords);
                }
            }
        }

        private void popupFilter_Closed(object sender, object e)
        {
            EverythingOnPage.IsHitTestVisible = true ;
            EverythingOnPage.Opacity = 1;
            popupFilter.IsOpen = false ;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtSearch.Text = "Enter text here!";
                dtpFrom.Date = DateTime.Now.AddDays(-5);
                dtpTo.Date = DateTime.Now;
                cmbProvince.SelectedIndex = 0;
                cmbProvince.SelectedValue = "0";
                cmbRegion.SelectedIndex = 0;
               
            }
            catch
            {

            }
          
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            EverythingOnPage.IsHitTestVisible = true;
            EverythingOnPage.Opacity = 1;
            popupFilter.IsOpen = false;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            btnCancel_Click(null,null);
            LoadFestivals(dtpTo.Date.ToString("yyyy-MM-dd"), dtpFrom.Date.ToString("yyyy-MM-dd"),
              cmbProvince.SelectedValue !=null ? cmbProvince.SelectedValue.ToString():"",
                cmbRegion.SelectedValue != null ? cmbRegion.SelectedValue.ToString() : "" 
                , txtSearch.Text.Trim(),true );
        }

        private void btnFilter_Click(object sender, TappedRoutedEventArgs e)
        {
            if (!popupFilter.IsOpen)
            {
                btnClear_Click(null, null);
                popupFilter.IsOpen = true;
                EverythingOnPage.IsHitTestVisible = false;
                EverythingOnPage.Opacity = 0.4;
            }
        }

        bool isMapShown = false;
        private void btnMap_Click(object sender, TappedRoutedEventArgs e)
        {
            if (!isMapShown)
            {
                frameMap.Visibility = Visibility.Visible;
                btnMap.Source = new BitmapImage(new Uri(
  "ms-appx:///Assets/menu.png", UriKind.Absolute));
                frameMap.Navigate(typeof(testPage), lstRecords);
                isMapShown = true;
            }
            else
            {
                btnMap.Source = new BitmapImage(new Uri(
    "ms-appx:///Assets/earth.png", UriKind.Absolute));
                frameMap.Visibility = Visibility.Collapsed;
                isMapShown = false;
            }
        }
    }
}
