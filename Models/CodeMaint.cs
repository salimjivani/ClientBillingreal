using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientBilling.Models
{
    public class RealRoot
    {
        public string Sheet { get; set; }
        public List<string> Titles { get; set; }
        public List<DetailModel> Details { get; set; }
    }

    public class DetailModel
    {
        public int DetailID { get; set; }
        public List<DetailModelValue> DetailModelValues { get; set;}
    }

    public class DetailModelValue
    {
        public string value { get; set; }
    }

    public class InsertDetails
    {
        public int Numbers { get; set; }
        public string BusinesslogicIDs { get; set; }
        public string Values { get; set; }
    }

}




