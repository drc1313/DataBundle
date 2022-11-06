using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DataBundle.BL
{
    public class APIBundleRequests
    {
        [Required]
        [Key, Column(Order = 0)]
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid BundleId { get; set; }
        [JsonIgnore]
        [ForeignKey("BundleId")]
        public APIBundle? APIBundle { get; set; }        
        
        [Required]
        [Key, Column(Order = 1)]
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid RequestId { get; set; } 
        [JsonIgnore]
        [ForeignKey("RequestId")]
        public APIRequest? APIRequest { get; set; }

        [DefaultValue("RequestProperties")]
        public string RequestProperties { get; set; } = "";

        [DefaultValue(0)]
        public int RequestPriority { get; set; } = 0;

    }
}
