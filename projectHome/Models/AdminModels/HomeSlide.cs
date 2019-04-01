using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Models.AdminModels
{
    public class HomeSlide
    {
        public byte ID { get; set; }

        public string Filename { get; set; }

        public virtual byte DisplayOrder { get; set; }
    }
}
