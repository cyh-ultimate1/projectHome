using mor1.Models;
using mor1.Models.AdminModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Data.IRepositories
{
    public interface IHomepageRepo
    {
        string GetById(int id);
        Task<IEnumerable<HomeSlide>> GetAllSlides();
    }
}
