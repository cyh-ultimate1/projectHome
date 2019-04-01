using mor1.Models.CompanyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.ViewModels
{
    public class IndvCompCommVM
    {
        public virtual Company Company { get; set; }

        public virtual CompComment CompComment { get; set; }

        public byte workManRating { get; set; }
        public byte pmRating { get; set; }
        public byte priceRating { get; set; }
    }
}
