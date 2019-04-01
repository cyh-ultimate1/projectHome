using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mor1.Data.IRepositories;
using mor1.Models;
using mor1.Models.CompanyModels;
using mor1.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace mor1.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepo _companyRepo;
        private readonly ICompServiceRepo _compServiceRepo;

        public CompanyController(ICompanyRepo companyRepo, ICompServiceRepo compServiceRepo)
        {
            _companyRepo = companyRepo;
            _compServiceRepo = compServiceRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Search

        public IActionResult SearchCompanies()
        {
            SearchCompVM vm = new SearchCompVM
            {
                ServiceCategories = _compServiceRepo.GetAllCat().ToList(),
                Services = _compServiceRepo.GetAllServices().Select(s=> new SelectListItem { Value = s.ID.ToString(), Text = s.Title }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SearchCompanies([FromBody]JObject reqData)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            try
            {
                var servCat = JsonConvert.DeserializeObject<int[]>(reqData["servCatIn"].ToString());
                var serv = JsonConvert.DeserializeObject<int[]>(reqData["servIn"].ToString());
                int page = reqData["subseqPageIdxIn"].Value<int>();
                string sortOption = reqData["sortOptionIn"].Value<string>();

                var results2 = await _companyRepo.GetSearchResults(reqData["searchIn"].ToString(), servCat, serv);
                
                if(sortOption == "rating")
                {
                    results2 = results2.OrderByDescending(b => b.OverallRating);
                }else if(sortOption == "nameAZ")
                {
                    results2 = results2.OrderBy(b => b.CompanyName);
                }else if(sortOption == "nameZA")
                {
                    results2 = results2.OrderByDescending(b => b.CompanyName);
                }

                var results = await PaginatedList<Company>.CreateAsync(results2, page==0 ? 1: page, 5);

                //dynamic obj = results["servIn"];
                //var a = obj.servIn;
                //var jsonSer = JsonConvert.DeserializeObject((string)results);
                //var results = await _companyRepo.GetCompanyByInput(reqData.GetValue("searchIn").Value<string>());
                //var a = reqData.GetValue("servCatIn").Value<string[]>();
                //if(results != null)
                //{
                //    return Json(results);
                //}
                if (results != null)
                {
                    return PartialView("_SearchResultsPartial", results);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        [HttpGet]
        public async Task<JsonResult> GetSearchAutocompResults(string reqData)
        {
            try
            {
                var result = await _companyRepo.GetSearchAutocompByInput(reqData);
                if( result != null)
                {
                    return Json(result);
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        #endregion

        #region IndividualCompany

        public async Task<IActionResult> GetIndividualCompany(int id)
        {
            var results = await _companyRepo.GetCompanyWcommentsById(id);
            var vm = new IndvCompCommVM()
            {
                Company = results,
                CompComment = new CompComment()
            };
            return View("IndividualCompany", vm);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(IndvCompCommVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            vm.CompComment.Rating = (decimal)(vm.pmRating + vm.priceRating + vm.workManRating) / 3;
            var returnId = await _companyRepo.AddComment(vm.CompComment);
            await _companyRepo.UpdateCompanyRating(vm.CompComment.CompID);
            return RedirectToAction("GetIndividualCompany", new { id = vm.CompComment.CompID });
        }


        #endregion
    }
}