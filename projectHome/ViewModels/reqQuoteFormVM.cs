using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using mor1.Models.ReqQuoteModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.ViewModels
{
    public class reqQuoteFormVM
    {
        public ReqQuoteForm reqQuoteForm { get; set; }

        public IEnumerable<SelectListItem> SelectCountry { get; set; }

        public IEnumerable<PropertyType> PropertyTypes { get; set; }

        public IEnumerable<SelectListItem> SelectPropertyStatus { get; set; }

        [Display(Name = "Floor Plan Files")]
        public IEnumerable<IFormFile> FloorPlanFile { get; set; }

        public List<Service> SelectedServices { get; set; }

        //public string[] SelectedServices { get; set; }

    }
}
