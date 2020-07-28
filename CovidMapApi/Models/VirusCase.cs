using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CovidMapApi.Models
{
    public class VirusCase
    {
        public int Id { get; set; }
        public int Cases { get; set; }
        public int Recovered { get; set; }
        public int Deaths { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
    }
}
