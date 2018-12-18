using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Omya.AzureApi.Models
{
    public class OmyaPlants : CommonEntity
    {
        public string SiteCode { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string PlantName { get; set; }
        public string Mineral { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string[] Certifications { get; set; }
        public string Language { get; set; }
        public Boolean HasAttachments { get; set; }
        public string[] Attachments { get; set; }
    }
}