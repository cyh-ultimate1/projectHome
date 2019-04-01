using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Models.CompanyModels
{
    public class Company
    {
        public int ID { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogoFilename { get; set; }

        public string Address { get; set; }

        [Phone]
        public string PhoneNum { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string CompanyRegNum { get; set; }

        public bool isTopPick { get; set; }

        public decimal OverallRating { get; set; }

        public virtual List<CompServ> CompServList { get; set; }

        public virtual List<CompComment> CommentList { get; set; }

        public virtual List<ProjectPhotos> ProjectPhotosList { get; set; }
    }
}
