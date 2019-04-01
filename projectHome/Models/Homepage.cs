using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Models
{
    public class Homepage
    {
        [Key]
        public int ID { get; set; }

        public string filename { get; set; }
    }
}
