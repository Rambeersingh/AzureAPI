using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Omya.AzureApi.Models
{
    public class OmyaAttachment : CommonEntity
    {
        public Guid UniqueID { get; set; }
        public string Name { get; set; }
        public Byte[] File { get; set; }
        public long Length { get; set; }
    }
}