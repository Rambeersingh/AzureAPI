using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Omya.AzureApi.Models
{
    public class ExceptionMessage : CommonEntity
    {
        public string Title { get; set; }
        public string StackTrace { get; set; }
        public string Corelationid { get; set; }
    }
}