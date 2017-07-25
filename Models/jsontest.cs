using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientBilling.Models
{
    public class jsontest
    {

        public class Rootobject
        {
            public RootBusinesslogic RootBusinessLogics { get; set; }
        }

        public class RootBusinesslogic
        {
            public string RootSheet { get; set; }
            public object[] RootTitles { get; set; }
            public string RootDetailsCount { get; set; }
            public int RootNumber { get; set; }
            public string[] RootDetails { get; set; }
        }


    }
}