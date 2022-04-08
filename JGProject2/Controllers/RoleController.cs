using JGProject2.Models.ViewModels;
using JGProject2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JGProject2.Controllers
{
    /// <summary>
    /// If user is a project manager allows them to do crud operations on roles,
    /// add or remove people from projects, view details about roles 
    /// </summary>
     [Authorize(Roles = "Project Manager")]
    public class RoleController : Controller
    {
        private IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        /// <summary>
        /// List view of all roles
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var model = await _roleRepository.ReadAllAsync();
            return View(model);
            
        }
        /// <summary>
        /// Role create view
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Allow user to create a role
        /// </summary>
        /// <param name="createRoleVM"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleVM createRoleVM)
        {
            var role = createRoleVM.GetRoleInstance();
            if (role.Name.ToLower() == "member")
            {
                ModelState.AddModelError("Create", "Creation of another default member role isn't allowed");
            }
            if (ModelState.IsValid)
            {
                await _roleRepository.CreateAsync(role);
                return RedirectToAction("Index");
            }
            return View();

        }
        /// <summary>
        /// Edit view for a role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit([Bind(Prefix = "id")] int roleId)
        {
            var toEdit = await _roleRepository.ReadAsync(roleId);
            var model = new EditRoleVM
            {
                Id = toEdit.Id,
                Name = toEdit.Name,
                Description = toEdit.Description

            };
            return View(model);
        }
        /// <summary>
        /// Edit a role prevent user from editing the member role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="editRoleVM"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(Prefix = "id")] int roleId, EditRoleVM editRoleVM)
        {
            var oldRole = await _roleRepository.ReadAsync(roleId);
            var role = editRoleVM.GetRoleInstance();
            if (oldRole.Name.ToLower() == "member")
            {
                ModelState.AddModelError("Name", "Editing the default role is prohibited");
            }
            if (role.Name.ToLower() == "member")
            {
                ModelState.AddModelError("Name", "Creating another member role is prohibited");
            }

            if (ModelState.IsValid)
            {
                await _roleRepository.UpdateAsync(roleId, role);
                return RedirectToAction("Details", new { id = roleId });
            }
            return View(editRoleVM);

        }
        /// <summary>
        /// View to delete a role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete([Bind(Prefix = "id")] int roleId)
        {
            var role = await _roleRepository.ReadAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("Index");
            }

            var model = new DeleteRoleVM
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
               

            };

            return View(model);
        }
        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeleteConf([Bind(Prefix = "id")] int roleId)
        {
            var role = await _roleRepository.ReadAsync(roleId);
            if(role.Name.ToLower() == "member")
            {
                ModelState.AddModelError("Name", "Deleting the default role is prohibited");
            }
            if (ModelState.IsValid)
            {
                await _roleRepository.DeleteAsync(roleId);
                
            }
            return RedirectToAction("Index");


        }
        /// <summary>
        /// Role details view
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details([Bind(Prefix = "id")] int roleId)
        {
            var role = await _roleRepository.ReadAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var model = new RoleDetailsVM
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description



            };
            return View(model);
        }

    }
}
