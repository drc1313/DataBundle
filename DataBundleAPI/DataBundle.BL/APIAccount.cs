using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DataBundle.BL
{
    public class APIAccount
    {
        [Key]
        [Required]
        [DefaultValue("accountName")]
        public string AccountName { get; set; } = "";

        [DefaultValue("https://example.com")]
        public string DocumenationLink { get; set; } = "";
        
        [Required]
        [DefaultValue("XYZ")]
        public string ApiKey { get; set; } = "";
        
        [DefaultValue("")]
        public string Headers { get; set; } = "";

        [DefaultValue("YYYY-MM-DD")]
        public string DateFormat { get; set; } = "YYYY-MM-DD";

        [DefaultValue(",")]
        public string Delimiter { get; set; } = "";

        public APICategory Category { get; set; }
    }
    public enum APICategory
    {
        Stocks,
        Economy
    }
}
