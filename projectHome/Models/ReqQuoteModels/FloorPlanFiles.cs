using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Models.ReqQuoteModels
{
    public class FloorPlanFiles
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public int reqQuoteFormId { get; set; }
    }
}
