using JGProject2.Models;
using JGProject2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Controllers
{
    /// <summary>
    /// Home page that serves as a way to reach each project management task
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApplicationUserRepository _userRepo;
        private readonly IPersonRepository _personRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IRoleRepository _roleRepository;

        public HomeController(ILogger<HomeController> logger, IApplicationUserRepository userRepo,
            IPersonRepository personRepository, IProjectRepository projectRepository, IRoleRepository roleRepository)
        {
            _logger = logger;
            _userRepo = userRepo;
            _personRepository = personRepository;
            _projectRepository = projectRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Home page to allow navigation to project,role,person management
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles= "Project Manager")]
        public async Task<IActionResult> Index()
        {
            var people = await _personRepository.ReadAllAsync();
            var projects = await _projectRepository.ReadAllAsync();
            var roles = await _roleRepository.ReadAllAsync();
            ViewData["People"] = people.Count();
            ViewData["Projects"] = projects.Count();
            ViewData["Roles"] = roles.Count();
           
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
    }
}
