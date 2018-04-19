using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TangApplication.Models;

namespace TangApplication.Common
{
    
    public class CommonClass
    {
        public static string AppUrl = "http://api.faitango.it";
        /// <summary>
        /// Created By: Harinder Kaur
        /// Created Date: 28 Dec, 2016
        /// Descrition: Get News List 
        /// <param>Url(url of webservice which return json data)</param>
        /// <returns>class(News List data)</returns>
        ///</summary>
        public static async Task<wsNewsModel> GetNewsList(string url)
        {
            wsNewsModel objData = new wsNewsModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    objData = JsonConvert.DeserializeObject<wsNewsModel>(await result.Content.ReadAsStringAsync());

                }

            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        /// <summary>
        /// Created By: Harinder Kaur
        /// Created Date: 28 Dec, 2016
        /// Descrition: Get Service Data List 
        /// <param>Url(url of webservice which return json data)</param>
        /// <returns>class(Service data)</returns>
        ///</summary>
        public static async Task<wsTypeModel> GetServiceData(string url)
        {
            wsTypeModel objData = new wsTypeModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    objData = JsonConvert.DeserializeObject<wsTypeModel>(await result.Content.ReadAsStringAsync());

                }

            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        /// <summary>
        /// Created By: Harinder Kaur
        /// Created Date: 29 Dec, 2016
        /// Descrition: Get Service Data List 
        /// <param>Url(url of webservice which return json data)</param>
        /// <returns>class(Service List data)</returns>
        ///</summary>
        public static async Task<wsFilterDropDownsModel> GetFiltersDropDowns(string url)
        {
            wsFilterDropDownsModel objData = new wsFilterDropDownsModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    objData = JsonConvert.DeserializeObject<wsFilterDropDownsModel>(await result.Content.ReadAsStringAsync());

                }

            }
            catch (Exception ex)
            {
            }
            return objData;
        }
    }
}
