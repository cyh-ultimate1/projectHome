using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mor1.Data.IRepositories;

namespace mor1.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly ICompanyRepo _companyRepo;

        public DirectoryController(ICompanyRepo companyRepo)
        {
            _companyRepo = companyRepo;
        }

        public IActionResult Index()
        {
            return View(_companyRepo.DirectoryGetAll());
        }

        [HttpGet]
        public async Task<JsonResult> GetDirListByInitial(string reqData)
        {
            try
            {
                var results = await _companyRepo.DirGetByInitial(reqData);
                if(results != null)
                {
                    return Json(results);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
    }
}