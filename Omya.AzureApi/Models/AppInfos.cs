using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Omya.AzureApi.Models
{
    public class AppInfos : CommonEntity
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public int? SegmentID { get; set; }
        public int? SegmentOrder { get; set; }
        public string ItemType { get; set; }
        public string Tags { get; set; }
        public string[] Platform { get; set; }
        public string SegmentLink { get; set; }
        public string Language { get; set; }
        public Boolean HasAttachments { get; set; }
        public string[] Attachments { get; set; }
        public List<AppInfos> Segments { get; set; }
    }
}