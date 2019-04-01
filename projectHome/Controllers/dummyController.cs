using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Dapper.Entities;
using Identity.Dapper.Queries.Role;
using Identity.Dapper.Samples.Web.Models.ManageViewModels;
using Identity.Dapper.SqlServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mor1.Models.IdentityEntities;

namespace mor1.Controllers
{
    public class dummyController : Controller
    {
        private readonly IServiceProvider _serviceProvider;

        public dummyController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = RoleNames.member)]
        public IActionResult AddRole()
        {
            AddRoleViewModel vm = new AddRoleViewModel();
            return View("Views/Manage/AddRole.cshtml", vm);
        }

        
        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel vm)
        {
            var role = vm.RoleName;
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<CustomRole>>();

            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new CustomRole(role));
            }

            return RedirectToAction("Index", "Home");
        }
    }
}