using System;
using System.Collections.Generic;

namespace TangApplication.Models
{
    public class NewsModel
    {
        public string post_date1 { get; set; }
        public string Post_Date
        {
            get
            {
                return Convert.ToDateTime(post_date1).ToString("dd-MM-yyyy"); 
            }
           
        }
        public string post_content { get; set; }
        public string post_title { get; set; }
        public string post_name { get; set; }
        public string post_img { get; set; }
        public string post_url { get; set; }

        public string Post_Url
        {
            get
            {
                return post_url.Replace(@"\/", "/"); 
            }
          
        }
    }

    public class wsNewsModel
    {
        public string mesg { get; set; }
        public string responce { get; set; }
        public List<NewsModel> data { get; set; }
    }
}
