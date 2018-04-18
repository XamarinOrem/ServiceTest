using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TangApplication.Models
{
    
    public class Province
    {
        public string codes { get; set; }
        public string name { get; set; }
    }

    public class Region
    {
        public string codes { get; set; }
        public string name { get; set; }
    }
    public class ComboData
    {
        public string  IdNew { get; set; }
        public string Value { get; set; }
    }
    public class wsFilterDropDownsModel
    {
        public string mesg { get; set; }
        public string responce { get; set; }
        public List<Province> province { get; set; }
        public List<Region> region { get; set; }
    }
}
