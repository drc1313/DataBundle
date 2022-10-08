using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DataBundle.BL
{
    public class APIRequestMetadata
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string RequestId { get; set; }
        [JsonIgnore]
        [ForeignKey("RequestId")]
        public RequestId RequestId { get; set; }
        
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
