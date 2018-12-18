using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Omya.AzureApi.Models
{
    public class OmyaApp : CommonEntity
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public string AppType { get; set; }
        public string UniqueAppGuid { get; set; }
        public string Langugage { get; set; }
    }
}