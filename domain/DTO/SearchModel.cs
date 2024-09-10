using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain_and_repo.DTO
{
    public class SearchModel
    {
        [Required]
        public string Name { get; set; }
        public int? Rank { get; set; }
        public int? TotalDegree { get; set; }
        
    }
}
