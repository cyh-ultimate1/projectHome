using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Models.ReqQuoteModels
{
    public class Service
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImgFilename { get; set; }

        public int CatID { get; set; }
    }
}
