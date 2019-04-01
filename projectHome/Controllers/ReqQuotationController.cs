using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mor1.Data.IRepositories;
using mor1.Models;
using mor1.Models.IdentityEntities;
using mor1.ViewModels;

namespace mor1.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ReqQuotationController : Controller
    {
        private readonly ICompServiceRepo _reqQuoteRepo;
        private readonly ICountryRepo _countryRepo;
        private readonly IPropertyRepo _propertyRepo;

        public ReqQuotationController(ICompServiceRepo reqQuoteRepo, ICountryRepo countryRepo, IPropertyRepo propertyRepo)
        {
            _reqQuoteRepo = reqQuoteRepo;
            _countryRepo = countryRepo;
            _propertyRepo = propertyRepo;
        }

        //to direct to services selection page
        public IActionResult Index()
        {
            reqQuoteIndexVM vm = new reqQuoteIndexVM()
            {
                ServiceCategories = _reqQuoteRepo.GetAllCat().ToList(),
                Services = _reqQuoteRepo.GetAllServices().OrderBy(a => a.CatID).ToList()
            };


            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(reqQuoteIndexVM vm)
        {
            var chkBoxArr = HttpContext.Request.Form["subCatSelect"];
            return RedirectToAction(nameof(reqQuoteForm), routeValues: new { chkBoxArr2 = chkBoxArr });
        }

        //to direct to form page
        public IActionResult reqQuoteForm(string[] chkBoxArr2)
        {
            reqQuoteFormVM vm = new reqQuoteFormVM()
            {
                SelectCountry = _countryRepo.GetAll()
                                .Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.CountryName })
                                .OrderBy(b => b.Text)
                                .ToList(),
                PropertyTypes = _propertyRepo.GetAllPropertyTypes().ToList(),
                SelectPropertyStatus = _propertyRepo.GetAllPropertyStatus()
                                 .Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.Status })
                                 .ToList(),
                SelectedServices = _reqQuoteRepo.GetServicesListById(chkBoxArr2).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult reqQuoteForm(reqQuoteFormVM vm)
        {
            //=========== add qoute form ==============
            var addedId = _reqQuoteRepo.AddQuoteForm(vm.reqQuoteForm);

            //=========== add selected services ===========
            foreach(var service in vm.SelectedServices)
            {
                _reqQuoteRepo.AddSelectedService(addedId, service.ID);
            }

            //======== upload file ==============
            if (vm.FloorPlanFile != null && vm.FloorPlanFile.Count() > 0)
            {
                var files = vm.FloorPlanFile;
                //long size = files.Sum(f => f.Length);
                // full path to file in temp location
                //var filePath = Path.GetTempFileName();
                var filePath = commonName.floorPlanFilePath;
                var modifiedFileName = "";
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0 && FileExtensions.IsImage(formFile.FileName))
                    {
                        modifiedFileName = addedId + "_" + formFile.FileName;
                        if (_reqQuoteRepo.AddFloorPlanFile(modifiedFileName, null, addedId) > 0)
                        {
                            using (var stream = new FileStream(Path.Combine(filePath, modifiedFileName), FileMode.Create))
                            {
                                formFile.CopyTo(stream);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = RoleNames.superAdmin)]
        public async Task<IActionResult> ViewAllFormsData()
        {
            var results = await _reqQuoteRepo.GetReqQuoteForms();
            return View(results);
        }

    }
}