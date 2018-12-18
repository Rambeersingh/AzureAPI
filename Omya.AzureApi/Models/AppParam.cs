using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Omya.AzureApi.Models
{
    public class AppParam
    {
        public string AppTitle { get; set; }
        public string AppKey { get; set; }
        public string AppType { get; set; }
        public string AppLanguage { get; set; }
        public string AppGuid { get; set; }
        public SegmentParam AppSegment { get; set; }
        public PlantParam AppPlant { get; set; }
    }

    public class SegmentParam
    {
        public int ID { get; set; }
    }

    public class PlantParam
    {
        public int ID { get; set; }
    }

}