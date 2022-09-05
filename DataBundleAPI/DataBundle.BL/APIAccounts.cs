using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DataBundle.BL
{
    public class APIAccounts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid AccountId { get; set; } = Guid.NewGuid();
        public string ApiHostName { get; set; } = "";
        public string DocumenationLink { get; set; } = "";
        public string ApiKey { get; set; } = "";
        public APICategory Category { get; set; }
    }
    public enum APICategory
    {
        Stocks,
        Economy
    }
}
