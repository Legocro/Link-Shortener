using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkShortenerAPI.Models
{
    public class ShortLinkModel
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName="nvarchar(2000)")]
        public string ShortLink { get; set; } = "";
        [Column(TypeName = "nvarchar(100)")]
        public string LongLink { get; set; } = "";
        [Column(TypeName = "nvarchar(20)")]
        public string CreationDate { get; set; } = "";
    }
}
