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
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey(nameof(level))]
        public int Levelid { get; set; }
        
        [JsonIgnore]
        public Level? level { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        //[Required]
        //[StringLength(30)]
        //public string Password { get; set; }

        public decimal? TotalFees { get; set; }

        public decimal? Installments { get; set; }
        [JsonIgnore]
        public ICollection<Payment>? Payments { get; set; } = new List<Payment>();
        [JsonIgnore]
        public ICollection<Enroll>? Enrolls { get; set; } = new List<Enroll>();
    }
}
