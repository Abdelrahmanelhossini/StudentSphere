using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace domain_and_repo.models
{
    public class Level
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LevelId { get; set; }

        [Required]
        [StringLength(20)]
        public string LevelName { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        public decimal Base { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        public decimal Addition { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        public decimal Deduction { get; set; }


        [NotMapped]
        public decimal ValuePaid => Base + Addition - Deduction;
        [JsonIgnore]
        public ICollection<Payment>? Payments { get; set; }

        [JsonIgnore]
        public ICollection<Student>? Students { get; set; }
    }
}
