using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain_and_repo.models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PymentId { get; set; }

        [Required]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]

        public Student Student { get; set; }

        [Required]
        public int LevelId { get; set; }

        public int? PaidValue { get; set; }

        public decimal? TotalRequirs { get; set; }

        [Required]
        public DateTime PymentDate { get; set; }

        [ForeignKey("LevelId")]
        public Level level { get; set; }

    }
}
