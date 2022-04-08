
using JGProject2.Models.ViewModels;
using JGProject2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace JGProject2.Controllers
{
    /// <summary>
    /// If user is a project manager allows them to do crud operations on people,
    /// add or remove people from projects, view details about people and view the people report
    /// </summary>
    [Authorize(Roles = "Project Manager")]
    public class PersonController : Controller
    {
        private IPersonRepository _personRepository;
        private IProjectRepository _projectRepository;
        private IRoleRepository _roleRepository;
        private IProjectRoleRepository _projectRoleRepository;

        //dependency injection for repositories
        public PersonController(IPersonRepository personRepository, IProjectRepository projectRepository,
            IRoleRepository roleRepository, IProjectRoleRepository projectRoleRepository)
        {
            _personRepository = personRepository;
            _projectRepository = projectRepository;
            _roleRepository = roleRepository;
            _projectRoleRepository = projectRoleRepository;
        }
        /// <summary>
        /// People list view 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var people = await _personRepository.ReadAllAsync();
            return View(people);
        }
        /// <summary>
        /// view to allow the creation of people 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Creation of a person
        /// </summary>
        /// <param name="createPersonVM">View model to create a person</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePersonVM createPersonVM)
        {
            var person = createPersonVM.GetPersonInstance();
            bool isValidEmail;
            try// validate email 
            {
                MailAddress email = new MailAddress(person.Email);
                isValidEmail = email.Host.Contains(".");
                if (!isValidEmail)
                {
                    throw new FormatException();
                }
                

                
            }
            catch (FormatException)
            {
                ModelState.AddModelError("Email", "Enter a valid email address");
            }

            if (ModelState.IsValid)
            {

                await _personRepository.CreateAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }
        /// <summary>
        /// Detail view for a specific person
        /// </summary>
        /// <param name="personId">Unique identifier for person</param>
        /// <returns></returns>
        public async Task<IActionResult> Details([Bind(Prefix = "id")] int personId)
        {
            var person = await _personRepository.ReadAsync(personId);
            if (person == null)
            {
                return RedirectToAction("Index");
            }
            var model = new PersonDetailsVM
            {
                Id = person.Id,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                Email = person.LastName
            };
            return View(model);
        }
        /// <summary>
        /// Edit view for a person
        /// </summary>
        /// <param name="personId">Unique identifier for person</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit([Bind(Prefix = "id")] int personId)
        {
            var toEdit = await _personRepository.ReadAsync(personId);

            var model = new EditPersonVM
            {
                Id = toEdit.Id,
                FirstName = toEdit.FirstName,
                MiddleName = toEdit.MiddleName,
                LastName = toEdit.LastName,
                Email = toEdit.Email

            };
            return View(model);
        }
        /// <summary>
        /// allow a person to be edited 
        /// </summary>
        /// <param name="personId">Unique identifier for person</param>
        /// <param name="editPersonVM">view model for editing a person</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(Prefix = "id")] int personId, EditPersonVM editPersonVM)
        {
            bool isValidEmail;
            var person = editPersonVM.GetInstanceOf();
            try// validate email 
            {
                MailAddress email = new MailAddress(person.Email);
                isValidEmail = email.Host.Contains(".");
                if (!isValidEmail)
                {
                    throw new FormatException();
                }
            }
            catch (FormatException)
            {
                ModelState.AddModelError("Email", "Enter a valid email address");
            }

            if (ModelState.IsValid)
            {
                await _personRepository.UpdateAsync(personId, person);
                return RedirectToAction("Details", new { id = personId });
            }
            return View(editPersonVM);

        }

        /// <summary>
        /// Delete View
        /// </summary>
        /// <param name="personId">Unique identifier for person</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete([Bind(Prefix = "id")] int personId)
        {
            var toDelete = await _personRepository.ReadAsync(personId);
            if (toDelete == null)
            {
                return RedirectToAction("Index");
            }

            var model = new DeletePersonVM
            {
                Id = toDelete.Id,
                FirstName = toDelete.FirstName,
                MiddleName = toDelete.MiddleName,
                LastName = toDelete.LastName,
                Email = toDelete.Email

            };
            return View(model);
        }
        /// <summary>
        /// Allow a person to be deleted
        /// </summary>
        /// <param name="personId">unique identifier for person</param>
        /// <returns></returns>

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeleteConf([Bind(Prefix = "id")] int personId)
        {
            await _personRepository.DeleteAsync(personId);
            return RedirectToAction("Index");
        }
       
        /// <summary>
        /// View to assign a role to a person on a specific project and to set a persons wage
        /// </summary>
        /// <param name="projectId">unique identifier for project</param>
        /// <param name="personId">unique identifier for person</param>
        /// <returns></returns>
        public async Task<IActionResult> AssignRole([Bind(Prefix = "id")] int projectId, int personId)
        {
            var project = await _projectRepository.ReadAsync(projectId);
            var person = await _personRepository.ReadAsync(personId);
            var roles = await _roleRepository.ReadAllAsync();
            var projectRoles = await _projectRoleRepository.ReadAllAsync();
            var assignedRoles = from r in roles
                                join pr in projectRoles
                                on r.Id equals pr.RoleId
                                where pr.ProjectId == projectId && pr.PersonId == personId
                                select r;
            var unassignedRoles = roles.Where(r => assignedRoles.All(ar => ar.Id != r.Id)).OrderBy(r => r.Name);//roles the person doesn't have

            ViewData["Project"] = project;
            ViewData["Person"] = person;
            ViewData["Roles"] = unassignedRoles;
            var model = new AssignRoleVM();//sets default values on instantiation
            return View(model);
        }
        /// <summary>
        /// assign a role and hourly wage
        /// </summary>
        /// <param name="projectId">unique identifier for projectid</param>
        /// <param name="personId">unique identifier for person</param>
        /// <param name="assignRoleVM">view model for assigning a person</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, ActionName("AssignRole")]
        public async Task<IActionResult> AssignRolePost([Bind(Prefix = "id")] int projectId, int personId, AssignRoleVM assignRoleVM)
        {
            var project = await _projectRepository.ReadAsync(projectId);
            var person = await _personRepository.ReadAsync(personId);
            if (project == null || person == null)
            {
                ModelState.AddModelError("AssignRole", "Invalid Project or Person");
            }
            if (assignRoleVM.HourlyRate < 8.00m || assignRoleVM.HourlyRate > 100.00m)
            {
                ModelState.AddModelError("AssignRole", "Hourly Rate can't be less than 8.00 or greater than 100.00");
            }
            if (assignRoleVM.RoleId == 0)
            {
                ModelState.AddModelError("AssignRole", "Invalid Role");
            }
            if (ModelState.IsValid)
            {
                var projectRole = new CreateProjectRoleVM
                {
                    ProjectId = projectId,
                    PersonId = personId,
                    RoleId = assignRoleVM.RoleId,
                    HourlyRate = assignRoleVM.HourlyRate

                }.GetProjectRoleInstance();

                await _projectRoleRepository.CreateAsync(projectRole);

            }

            return RedirectToAction("Details", "Project", new { id = projectId });
        }
        /// <summary>
        /// View to remove a role 
        /// </summary>
        /// <param name="projectId">Unique identifier for project</param>
        /// <param name="personId">unique identifier for person</param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveRole([Bind(Prefix ="id")]int projectId, int personId)
        {
            var project = await _projectRepository.ReadAsync(projectId);
            if(project == null)
            {
                return RedirectToAction("Index");
            }
            var person = await _personRepository.ReadAsync(personId);
            if(person == null)
            {
                return RedirectToAction("Details", "Project", new { id = projectId });
            }
            var allRoles = await _roleRepository.ReadAllAsync();
            var projectRoles = await _projectRoleRepository.ReadAllAsync();
            var roles = from pr in projectRoles
                        join r in allRoles
                        on pr.RoleId equals r.Id
                        where pr.PersonId == personId
                        select r;
            roles = roles.OrderBy(r => r.Name);
            if (roles == null)
            {
                return RedirectToAction("Details", "Project", new { id = projectId });
            }
            ViewData["Roles"] = roles.ToList();
            ViewData["Person"] = person;
            ViewData["Project"] = project;

            return View();
        }
        /// <summary>
        /// removes a role that isn't the member role from the person
        /// </summary>
        /// <param name="projectId">unique project identifier</param>
        /// <param name="personId">unique person identifier</param>
        /// <param name="removeRoleVM">view model to remove a role</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, ActionName("RemoveRole")]
        public async Task<IActionResult> RemoveRolePost([Bind(Prefix = "id")] int projectId, int personId,RemoveRoleVM removeRoleVM )
        {
            var role = await _roleRepository.ReadAsync(removeRoleVM.RoleId);
            var personReset = await _personRepository.ReadAsync(personId);
            var projectReset = await _projectRepository.ReadAsync(projectId);
            if (role.Name.ToLower() == "member")
            {
                ModelState.AddModelError("RemoveRole", "Member role is not allowed to be removed");
            }

            if (ModelState.IsValid)
            {
                var AllProjectRoles = await _projectRoleRepository.ReadAllAsync();
                var projectRole = from pr in AllProjectRoles
                                  where pr.ProjectId == projectId && pr.PersonId == personId
                                  && pr.RoleId == removeRoleVM.RoleId
                                  select pr;
                foreach(var person in projectRole)
                {
                    await _projectRoleRepository.DeleteAsync(person.Id);
                }
                return RedirectToAction("Details", "Project", new { id = projectId });

            }
            //if role was member reset view data
            var allRoles = await _roleRepository.ReadAllAsync();
            var projectRoles = await _projectRoleRepository.ReadAllAsync();
            var roles = from pr in projectRoles
                        join r in allRoles
                        on pr.RoleId equals r.Id
                        where pr.PersonId == personId
                        select r;
            roles = roles.OrderBy(r => r.Name);
            ViewData["Roles"] = roles.ToList();
            ViewData["Person"] = personReset;
            ViewData["Project"] = projectReset;
            return View(removeRoleVM);
            
        }

        /// <summary>
        /// View for the person report Not finished
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Report()
        {
            var people = await _personRepository.ReadAllAsync();
            var projects = await _projectRepository.ReadAllAsync();
            var roles = await _roleRepository.ReadAllAsync();
            var projectRoles = await _projectRoleRepository.ReadAllAsync();

            var projectModel = from pr in projects
                               join pro in projectRoles
                               on pr.Id equals pro.ProjectId
                               select pr;
            projectModel = projectModel.OrderBy(pr => pr.Name).Distinct();

            var model = from pr in projectRoles
                              join p in people
                              on pr.PersonId equals p.Id
                              join pro in projectModel
                              on pr.ProjectId equals pro.Id
                              join r in roles
                              on pr.RoleId equals r.Id
                              orderby (pro.Name, p.LastName, p.FirstName)
                              select new PersonReportVM
                              {
                                  ProjectName = pro.Name,
                                  NumProjects = projectModel.Where(pro => pro.Id == pr.ProjectId && pr.PersonId == p.Id).Count(),
                                  Name = $"{p.FirstName} {p.MiddleName} {p.LastName}",
                                  HourlyRate = pr.HourlyRate,
                                  Role = r.Name,
                                  TotalHourly = 0

                              };




            return View(model);
        }
    }  
    
}
