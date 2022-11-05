using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DataBundle.BL
{
    public class APIBundle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid BundleId { get; set; } = Guid.NewGuid();

        [Required]
        [DefaultValue("BundleName")]
        public string BundleName { get; set; } = "";

        [DefaultValue("Description")]
        public string? BundleDescription { get; set; } = "";
    }
}
