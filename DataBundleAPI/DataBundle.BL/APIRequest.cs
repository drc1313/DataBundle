using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DataBundle.BL
{
    public class APIRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid RequestId { get; set; } = Guid.NewGuid();

        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid AccountId { get; set; }
        [JsonIgnore]
        [ForeignKey("AccountId")]
        public APIAccount? APIAccount { get; set; }

        [DefaultValue("requestName")]
        public string RequestName { get; set; } = "";

        [DefaultValue("https://example/api/request")]
        public string RequestURL { get; set; } = "";


    }
}
