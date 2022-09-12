using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DataBundle.BL
{
    public class APIUsage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid UsageID { get; set; } = Guid.NewGuid();

        [Required]
        [DefaultValue("accountName")]
        public string AccountName { get; set; } = "";
        [JsonIgnore]
        [ForeignKey("AccountName")]
        public APIAccount? APIAccount { get; set; }

        [DefaultValue(0)]
        public int CurrentUsage { get; set; }

        [Required]
        [DefaultValue(0)]
        public int MaxUsage { get; set; }
        
        [Required]
        public UsageDuration UsageDuration { get; set; }

        public DateTime LastCallDate { get; set; } = DateTime.Now;
    }

    public enum UsageDuration
    {
        NoLimit,
        Daliy,
        Monthly,
        Yearly
    }

}
