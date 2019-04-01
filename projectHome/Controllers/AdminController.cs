using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mor1.Data.IRepositories;
using mor1.Models;
using mor1.Models.IdentityEntities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace mor1.Controllers
{
    [Authorize(Roles = RoleNames.admin + "," + RoleNames.superAdmin)]
    [AutoValidateAntiforgeryToken]
    public class AdminController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly IAdminRepo _adminRepo;

        public AdminController(UserManager<CustomUser> userManager, IAdminRepo adminRepo)
        {
            _userManager = userManager;
            _adminRepo = adminRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Homepage

        public IActionResult AddHomeSlides()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddHomeSlides(IEnumerable<IFormFile> inputFile)
        {
            //======== upload file ==============
            if (inputFile != null && inputFile.Count() > 0)
            {
                var files = inputFile;
                //long size = files.Sum(f => f.Length);
                // full path to file in temp location
                //var filePath = Path.GetTempFileName();
                var filePath = commonName.homeSlideFilePath;
                var modifiedFileName = "";
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0 && FileExtensions.IsImage(formFile.FileName))
                    {
                        modifiedFileName = formFile.FileName;
                        var addedId = _adminRepo.AddHomeSlideFile(modifiedFileName);
                        if (addedId > 0)
                        {
                            using (var stream = new FileStream(Path.Combine(filePath, addedId + "_" + modifiedFileName), FileMode.Create))
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

            return RedirectToAction("EditHomeSlides", "Admin");
        }

        public async Task<IActionResult> EditHomeSlides()
        {
            var result = await _adminRepo.GetAllSlides();
            return View(result);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateHomeSlideOrder([FromBody] JObject reqData)
        {
            var idList = JsonConvert.DeserializeObject<int[]>(reqData["IdList"].ToString());
            var result = _adminRepo.UpdateHomeSlidesOrder(idList.Take(3));

            return Json(new { url = "Home/Index" });
        }

        public async Task<IActionResult> AddHomeVideo()
        {

            return View();
        }

        #endregion


        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}