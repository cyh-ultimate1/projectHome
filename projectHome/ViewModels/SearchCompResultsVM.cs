using mor1.Models.CompanyModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.ViewModels
{
    public class SearchCompResultsVM
    {
        public IEnumerable<Company> Companies { get; set; }
    }
}
