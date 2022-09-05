using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DataBundle.BL
{
    public class SingleAPIRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("00000000-0000-0000-0000-000000000000")]

        public Guid RequestId { get; set; } = Guid.NewGuid();

        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        [JsonIgnore]
        public APIAccounts? APIAccounts { get; set; }

        public string? RequestName { get; set; }
        public string? RequestURL { get; set; }

        public List<Tokens>? Tokens { get; set; }

    }

    [Serializable]
    public class Tokens
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        [JsonIgnore]
        public Guid TokenId { get; set; }
        public string? Token { get; set; }
        public TokenCategories Categories { get; set; }
        public string? ConfigData { get; set; }

        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        [JsonIgnore]
        public Guid RequestId { get; set; }
        [JsonIgnore]
        [ForeignKey("RequestId")]
        public SingleAPIRequest? SingleAPIRequest { get; set; }
    }

    public enum TokenCategories 
    {
        ApiKey,
        Symbol,
        Date    
    }

}
