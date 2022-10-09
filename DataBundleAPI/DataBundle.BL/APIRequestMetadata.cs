using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DataBundle.BL
{
    public class APIRequestMetadata
    {

        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Guid RequestId { get; set; }
        [JsonIgnore]
        [ForeignKey("RequestId")]
        public APIRequest? APIRequest { get; set; }
        
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }

    public class APIRequestMetadataDTO 
    {
        [Required]
        public string Value { get; set; }
    }

}
