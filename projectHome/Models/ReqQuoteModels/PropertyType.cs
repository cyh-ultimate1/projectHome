using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Models.ReqQuoteModels
{
    public class PropertyType
    {
        [Key]
        public int ID { get; set; }

        public string PropertyTypeName { get; set; }

        public string IconName { get; set; }
    }
}
