using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TangApplication.Models
{
    
    public class TypeModel
    {
        public string id { get; set; }
        public string tipo { get; set; }
        public string titolo { get; set; }
        public string sottotitolo { get; set; }
        public string testo { get; set; }
        public string img { get; set; }
        public string url { get; set; }
        public string Post_Url
        {
            get
            {
                return url.Replace(@"\/", "/");
            }

        }
        public string data_da { get; set; }
        public string Data_Date
        {
            get
            {
                return Convert.ToDateTime(data_da).ToString("dd-MM-yyyy"); ;
            }
           
        }
        public string data_a { get; set; }
        public string stato { get; set; }
        public string regione { get; set; }
        public string provincia { get; set; }
        public string citta { get; set; }
        public string indirizzo { get; set; }
        public string lat { get; set; }
        public string @long { get; set; }
        public string organizzazione { get; set; }
    }

    public class wsTypeModel
    {
        public string mesg { get; set; }
        public string responce { get; set; }
        public int total_records { get; set; }
        public List<TypeModel> data { get; set; }
    }
}
