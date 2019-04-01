using mor1.Models.ReqQuoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.ViewModels
{
    public class reqQuoteIndexVM
    {
        public List<ServiceCategory> ServiceCategories { get; set; }

        public List<Service> Services { get; set; }
    }
}
