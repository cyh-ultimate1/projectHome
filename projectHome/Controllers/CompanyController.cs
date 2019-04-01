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
            try
            {
                //get ajax reqData properties
                var servCat = JsonConvert.DeserializeObject<int[]>(reqData["servCatIn"].ToString());
                var serv = JsonConvert.DeserializeObject<int[]>(reqData["servIn"].ToString());
                int page = reqData["subseqPageIdxIn"].Value<int>();
                string sortOption = reqData["sortOptionIn"].Value<string>();

                //get results from DB based search input
                var results2 = await _companyRepo.GetSearchResults(reqData["searchIn"].ToString(), servCat, serv);
                
                //re-order results based on sort option
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

                //create paginated list. 3rd argument is page size
                var results = await PaginatedList<Company>.CreateAsync(results2, page==0 ? 1: page, 5);

                //if results is not null, return partial view with the results.
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

        //function to return autocomplete results
        [HttpGet]
        public async Task<JsonResult> GetSearchAutocompResults(string reqData)
        {
            try
            {
                //get results from DB
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

        //function to get individual company details
        public async Task<IActionResult> GetIndividualCompany(int id)
        {
            //get company details with comments from DB
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

            //rating calculation
            vm.CompComment.Rating = (decimal)(vm.pmRating + vm.priceRating + vm.workManRating) / 3;

            //add comment to DB
            var returnId = await _companyRepo.AddComment(vm.CompComment);

            //update company rating in DB
            await _companyRepo.UpdateCompanyRating(vm.CompComment.CompID);

            //redirect to another action
            return RedirectToAction("GetIndividualCompany", new { id = vm.CompComment.CompID });
        }


        #endregion
    }
}