using Microsoft.AspNetCore.Mvc.Rendering;
using mor1.Models.CompanyModels;
using mor1.Models.ReqQuoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.ViewModels
{
    public class SearchCompVM
    {
        #region ForShow

        public IEnumerable<ServiceCategory> ServiceCategories { get; set; }

        public IEnumerable<SelectListItem> Services { get; set; }

        public IEnumerable<string> Location { get; set; }

        #endregion

        #region Output

        public IEnumerable<Company> Companies { get; set; }

        public byte[] SelectedServiceCategoryID { get; set; }

        public byte[] SelectedServiceID { get; set; }

        public decimal SelectedPriceMin { get; set; } = 0;
        public decimal SelectedPriceMax { get; set; }

        #endregion


    }
}
