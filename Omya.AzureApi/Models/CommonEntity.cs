using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Omya.AzureApi.Models
{
    public class CommonEntity
    {
        public int ID { get; set; }
        public DateTime Created { get; set; }
        public string Author { get; set; }
        public DateTime Modified { get; set; }
        public string Editor { get; set; }
    }

}