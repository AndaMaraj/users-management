using Microsoft.AspNetCore.Mvc;
using UsersManagement.Services.DTO;
using UsersManagement.Services.IService;
using UsersManagement.Services.UOW;

namespace UsersManagement.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;
        private readonly IUniteOfWork uniteOfWork;
        public RoleController(IRoleService roleService, IUniteOfWork uniteOfWork)
        {
            this.roleService = roleService;
            this.uniteOfWork = uniteOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await roleService.GetAllRolesAsync();
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleDto role)
        {
            if (!ModelState.IsValid) return View(role);
            role.CreatedOn = DateTime.UtcNow;
            await roleService.AddAsync(role);
            await uniteOfWork.SaveChangesAsync();
            return RedirectToAction("Index", controllerName: "Role");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var role = await roleService.GetByIdAsync(id.Value);
            if (role == null) return NotFound();
            return View(role);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleDto role)
        {
            if (ModelState.IsValid)
            {
                role.UpdatedOn = DateTime.UtcNow;
                await roleService.UpdateAsync(role);
                await uniteOfWork.SaveChangesAsync();
                return RedirectToAction("Index", controllerName: "Role");
            }
            return View(role);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null) return NotFound();
            var role = await roleService.GetByIdAsync(id.Value);
            await roleService.DeleteAsync(id.Value);
            await uniteOfWork.SaveChangesAsync();
            return RedirectToAction("Index", controllerName: "Role");
        }
    }
}
