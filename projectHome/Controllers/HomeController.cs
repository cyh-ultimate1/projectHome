using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mor1.Data.IRepositories;
using mor1.Models;
using mor1.Models.IdentityEntities;

namespace mor1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomepageRepo _homepageRepo;

        public HomeController(IHomepageRepo homepageRepo)
        {
            _homepageRepo = homepageRepo;
        }

        public async Task<IActionResult> Index()
        {
            //var result = _homepageRepo.GetById(1);

            List<string> resultList = new List<string>();
            //resultList.Add(_homepageRepo.GetById(1));
            //resultList.Add(_homepageRepo.GetById(2));
            //resultList.Add(_homepageRepo.GetById(3));

            var result1 = await _homepageRepo.GetAllSlides();
            foreach(var item in result1)
            {
                resultList.Add(item.Filename);
            }

            return View(nameof(Index), resultList);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult LoginView()
        {
            return PartialView("_LoginModalPartial");
        }
    }
}
