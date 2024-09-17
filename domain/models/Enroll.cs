using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace domain_and_repo.models
{

    public class Enroll
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollId { get; set; }
        [Required]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("CourseId")]
        public int CourseId { get; set; }

        public int? degree { get; set; }

        [JsonIgnore]
        public Student? Student { get; set; }

        
        [JsonIgnore]
        public Course? Course { get; set; }
    }
}
