using mor1.Models.AdminModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Data.IRepositories
{
    public interface IAdminRepo
    {
        int AddHomeSlideFile(string filename);
        Task<IEnumerable<HomeSlide>> GetAllSlides();
        Task<int> UpdateHomeSlidesOrder(IEnumerable<int> idList);
    }
}
