using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Models.ReqQuoteModels
{
    public class ReqQuoteForm
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = nameof(Country))]
        public int CountryID { get; set; }

        [Required]
        [Display(Name = "Property Type")]
        public byte PropertyTypeID { get; set; }

        [Display(Name = "Property Status")]
        public byte PropertyStatusID { get; set; }

        [Display(Name = "Area Size")]
        public double AreaSize { get; set; } = 0;

        [Display(Name = "Renovation Budget")]
        public decimal RenovBudget { get; set; } = 0;

        //[Display(Name = "Floor Plan Files")]
        //public int FloorPlanFileID { get; set; }

        ////This is sub quote category ID
        //[Display(Name = "Selected Services")]
        //public int SelectedServicesList { get; set; }

        [Display(Name = "Key Collection Period")]
        public string KeyCollectionPeriod { get; set; }

        [Display(Name = "Loan Required")]
        public bool LoanRequired { get; set; }

        public string Style { get; set; }

        public string Comment { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Contact Num")]
        public string ContactNum { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Gender { get; set; }

        public string Address { get; set; }

        //public bool termConditionChecked { get; set; }
    }
}
