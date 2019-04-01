using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Models
{
    public class Country
    {
        [Key]
        public int ID { get; set; }

        //UNIQUE
        [Required]
        public string CountryName { get; set; }
    }
}
