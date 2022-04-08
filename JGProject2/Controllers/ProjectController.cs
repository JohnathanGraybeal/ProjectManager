


using JGProject2.Models.ViewModels;
using JGProject2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Controllers
{
    /// <summary>
    /// If user is a project manager allows them to do crud operations on projects,
    /// add or remove people from projects, view details about projects and view the project report
    /// </summary>
    [Authorize(Roles = "Project Manager")]
    public class ProjectController : Controller
    {
        private IProjectRepository _projectRepository;
        private IPersonRepository _personRepository;
        private IRoleRepository _roleRepository;
        private IProjectRoleRepository _projectRoleRepository;

        /// <summary>
        /// Inject all repositories 
        /// </summary>
        /// <param name="projectRepository"></param>
        /// <param name="personRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="projectRoleRepository"></param>
        public ProjectController(IProjectRepository projectRepository, IPersonRepository personRepository, IRoleRepository roleRepository, 
            IProjectRoleRepository projectRoleRepository)
        {
            _projectRepository = projectRepository;
            _personRepository = personRepository;
            _roleRepository = roleRepository;
            _projectRoleRepository = projectRoleRepository;
           
        }

        /// <summary>
        /// Displays a list view of the projects
        /// </summary>
        /// <returns></returns>
        
        public async Task<IActionResult> Index()
        {

            var projects = await _projectRepository.ReadAllAsync();

            return View(projects);
        }
        
       /// <summary>
       /// Create View for creating a project
       /// </summary>
       /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        
        
     /// <summary>
     /// If valid creates a project 
     /// </summary>
     /// <param name="createProjectVM">View Model to create a project</param>
     /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProjectVM createProjectVM)
        {
            var project = createProjectVM.GetProjectInstance();
            var errors = project.Validate(new ValidationContext(project, null, null));//custom validator for date
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    foreach (var member in error.MemberNames)
                    {
                        if (member == "StartDate")
                        {
                            ModelState.AddModelError(nameof(createProjectVM.StartDate), "Start date must be greater than or equal to  current date");
                        }
                        else
                        {
                            ModelState.AddModelError(nameof(createProjectVM.DueDate), "Due Date must be greater than or equal to Start Date");
                        }
                    }

                }
            }

            if (ModelState.IsValid)
            {   
                   
                    await _projectRepository.CreateAsync(project);
                
                    return RedirectToAction("Index");
            }
            return View();
        }
        
        /// <summary>
        /// List View of assignable people to the project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Assign([Bind(Prefix = "id")]int projectId)
        {
            var project = await _projectRepository.ReadAsync(projectId);
            var assignablePeople = await _personRepository.ReadAllAsync();//all people
            var projectRoles = await _projectRoleRepository.ReadAllAsync();
            var alreadyAssigned = from ap in assignablePeople// returns everry person that is already assigned to project
                                  join pr in projectRoles
                                  on ap.Id equals pr.PersonId
                                  where pr.ProjectId == projectId
                                  select ap;
            var model = assignablePeople.Where(p => alreadyAssigned.All(aa => aa.Id != p.Id)).OrderBy(p => p.LastName).ThenBy(p => p.FirstName);//people not assigned to a project

            ViewData["Project"] = project;

            return View(model);
            
        }
        /// <summary>
        /// Assigns the person the default role of member
        /// </summary>
        /// <param name="projectId">Unique identifier for project</param>
        /// <param name="personId">Unique identifier for person</param>
        /// <returns></returns>
        public async  Task<IActionResult> AssignPerson([Bind(Prefix = "id")]int projectId,  int personId)
        {

            var project = await _projectRepository.ReadAsync(projectId);
            var person = await _personRepository.ReadAsync(personId);
            var roles = await _roleRepository.ReadAllAsync();
            var role = roles.FirstOrDefault(r => r.Name.ToLower() == "member");//get default role

            if (project == null || person == null)
            {
                return RedirectToAction("Index");
            }
           
           
            var projectRole = new CreateProjectRoleVM
            {
                ProjectId = projectId,
                PersonId = personId,
                RoleId = role.Id, //default role
                HourlyRate = 8.00m //default rate
            }.GetProjectRoleInstance();

            await _projectRoleRepository.CreateAsync(projectRole);
            
            return RedirectToAction("Details", new { id = projectId });
        }
        
        /// <summary>
        /// Project details page 
        /// </summary>
        /// <param name="projectId">unique project identifier</param>
        /// <returns></returns>
        public async Task<IActionResult> Details([Bind(Prefix = "id")] int projectId)
        {
            var project = await _projectRepository.ReadAsync(projectId);
            if (project == null)
            {
                return RedirectToAction("Index");
            }
            var projectRoles = await _projectRoleRepository.ReadAllAsync();
            var roles = await _roleRepository.ReadAllAsync();

            
            var people = await _personRepository.ReadAllAsync();
            
            var peopleAssigned = from pr in projectRoles //all people assigned to the project
                                 join p in people
                                 on pr.PersonId equals p.Id
                                 where pr.ProjectId == projectId && pr.PersonId == p.Id
                                 orderby(p.LastName, p.FirstName, p.MiddleName)
                                 select p;
            peopleAssigned = peopleAssigned.Distinct();//gets unique people in case they have multiple roles meaning multiple entries
            var daysDue = project.DueDate.Subtract(DateTime.Today).Days;

            var model = from pr in projectRoles
                        join p in peopleAssigned
                        on pr.PersonId equals p.Id
                        join r in roles
                        on pr.RoleId equals r.Id
                        where pr.ProjectId == projectId && pr.PersonId == p.Id
                        select new ProjectDetailsVM
                        {
                            Name = project.Name,
                            StartDate = project.StartDate,
                            DueDate = project.DueDate,
                            DaysDue = daysDue,
                            NumPeopleAssigned = peopleAssigned.Count(),
                            PeopleAssigned = peopleAssigned.ToList(),
                            Roles = r.Name,
                            PersonName = $"{p.FirstName} {p.MiddleName} {p.LastName}"
                        };
            //var details = new  ProjectDetailsVM
            //            {
            //Id = project.Id,
            // Name = project.Name,
            // StartDate = project.StartDate,
            // DueDate = project.DueDate,
            // DaysDue = daysDue,
            // NumPeopleAssigned = peopleAssigned.Count(),
            //     PeopleAssigned = peopleAssigned.ToList(),
            //     NumRoles = model.Count()

            // };



            model = model.ToList();

            ViewData["Project"] = project;
            return View(model.ToList());
        }
        /// <summary>
        /// View to edit a project
        /// </summary>
        /// <param name="projectId">Unique id for project</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit([Bind(Prefix = "id")]int projectId)
        {
            var toEdit = await _projectRepository.ReadAsync(projectId);
            var model = new EditProjectVM
            {
                Id = toEdit.Id,
                Name = toEdit.Name,
                StartDate = toEdit.StartDate,
                DueDate = toEdit.DueDate,
                
            };
            return View(model);
        }
        /// <summary>
        /// Edit post with custom date validation
        /// </summary>
        /// <param name="projectId">unique id for project</param>
        /// <param name="editProjectVM">View model for editing a project</param>
        /// <returns></returns>
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(Prefix = "id")] int projectId,EditProjectVM editProjectVM)
        {
            var project = editProjectVM.GetProjectInstance();
           var errors = project.Validate(new ValidationContext(project, null, null));//custom validation
            
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    foreach (var member in error.MemberNames)
                    {
                        if (member == "StartDate")
                        {
                            ModelState.AddModelError(nameof(project.StartDate), "Start date must be greater than or equal to current date");
                        }
                        else
                        {
                            ModelState.AddModelError(nameof(project.DueDate), "Due Date must be greater than Start Date");
                        }
                    }

                }
            }

            if (ModelState.IsValid)
            {
                await _projectRepository.UpdateAsync(projectId, project);
                return RedirectToAction("Details", new { id = projectId });
            }
            return View(editProjectVM);
           
        }

        /// <summary>
        /// View to delete a project
        /// </summary>
        /// <param name="projectId">Unique id for project</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete([Bind(Prefix = "id")] int projectId)
        {
            var project = await _projectRepository.ReadAsync(projectId);
            if (project == null)
            {
                return RedirectToAction("Index");
            }
           
            var model = new DeleteProjectVM
            {
                Id = project.Id,
                Name = project.Name,
                StartDate = project.StartDate,
                DueDate = project.DueDate

            };

            return View(model);
        }
        /// <summary>
        /// Deletes the project
        /// </summary>
        /// <param name="projectId">Unique identifier for project</param>
        /// <returns></returns>

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeleteConf([Bind(Prefix = "id")] int projectId)
        {
            await _projectRepository.DeleteAsync(projectId);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// View to remove a person from project
        /// </summary>
        /// <param name="projectId">Unique identifier for project</param>
        /// <param name="personId">Unique identifier for person</param>
        /// <returns></returns>
        public async Task<IActionResult> RemovePerson([Bind(Prefix = "id")] int projectId, int personId)
        {
            var project = await _projectRepository.ReadAsync(projectId);
            var person = await _personRepository.ReadAsync(personId);
            ViewData["Project"] = project;
            ViewData["Person"] = person;
            return View();
        }
        /// <summary>
        /// Removes the person from the project
        /// </summary>
        /// <param name="projectId">Unique identifier for project</param>
        /// <param name="personId">Unique identifier for person</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, ActionName("RemovePerson")]
        public async Task<IActionResult> RemovePersonPost([Bind(Prefix = "id")] int projectId, int personId)
        {
            var projectRoles = await _projectRoleRepository.ReadAllAsync();
            var toDelete = from pr in projectRoles
                           where pr.ProjectId == projectId && pr.PersonId == personId
                           select pr;
            foreach (var person in toDelete)
            {
                await _projectRoleRepository.DeleteAsync(person.Id);
            }
            return RedirectToAction("Details", "Project", new { id = projectId });
        }

        /// <summary>
        /// View for the project report Not done
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Report()
        {
            var people = await _personRepository.ReadAllAsync();
            var projects = await  _projectRepository.ReadAllAsync();
            var roles = await _roleRepository.ReadAllAsync();
            var projectRoles = await _projectRoleRepository.ReadAllAsync();

            var projectModel = from pr in projects
                               join pro in projectRoles
                               on pr.Id equals pro.ProjectId
                               select pr;
            projectModel = projectModel.OrderBy(pr => pr.Name).Distinct();

            var peopleModel = from pr in projectRoles
                              join p in people
                              on pr.PersonId equals p.Id
                              join pro in projectModel
                              on pr.ProjectId equals pro.Id
                              join r in roles 
                              on pr.RoleId equals r.Id
                              orderby (pro.Name, p.LastName, p.FirstName)
                              select new ProjectReportVM
                              {
                                  ProjectName = pro.Name,
                                  NumPeople = projectRoles.Where(p => p.ProjectId == pro.Id).Count(),
                                  FirstName = p.FirstName,
                                  MiddleName = p.MiddleName,
                                  LastName = p.LastName,
                                  HourlyRate = pr.HourlyRate,
                                  TotalHourly = 0,
                                  Role = r.Name

                              };




            return View(peopleModel);
                            
        }
    }






}

