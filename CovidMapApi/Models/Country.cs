using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CovidMapApi.Models
{
    public class Country
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [NotMapped]
        public int TotalCase { get; set; }
        [NotMapped]
        public int TotalRecovered { get; set; }
        [NotMapped]
        public int TotalDeaths { get; set; }
    }
}
