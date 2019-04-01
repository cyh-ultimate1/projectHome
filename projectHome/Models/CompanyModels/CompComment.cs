using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Models.CompanyModels
{
    public class CompComment
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Comment")]
        public string CommentContent { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string PersonName { get; set; }

        [Display(Name = "Company")]
        public int CompID { get; set; }

        public decimal Rating { get; set; }
    }
}
