using Microsoft.AspNetCore.Mvc;
using UsersManagement.Services.DTO;
using UsersManagement.Services.IService;
using UsersManagement.Services.UOW;

namespace UsersManagement.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUniteOfWork _uniteOfWork;
        public RoleController(IRoleService roleService, IUniteOfWork uniteOfWork)
        {
            _roleService = roleService;
            _uniteOfWork = uniteOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRolesAsync();
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
            var name = await _roleService.GetAll(x => x.Name == role.Name);
            if(name.Count() > 0)
            {
                ViewBag.NameExist = "This role exist, please try another name";
                return View();
            }
            else
            {
                await _roleService.AddAsync(role);
                await _uniteOfWork.SaveChangesAsync();
                return RedirectToAction("Index", controllerName: "Role");
            }
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var role = await _roleService.GetByIdAsync(id.Value);
            if (role == null) return NotFound();
            
            return View(role);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleDto role)
        {
            if (ModelState.IsValid)
            {
                var nameDb = await _roleService.GetByNameAsync(role.Name);
                if(nameDb != null && nameDb.Id != role.Id)
                {
                    ModelState.AddModelError(nameof(role.Name),"There is another role with this name");
                    return View(role);
                }
                await _roleService.UpdateAsync(role);
                return RedirectToAction("Index", controllerName: "Role");
            }
            return View(role);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) return NotFound();
            var role = await _roleService.GetByIdAsync(id.Value);
            if(role == null) return NotFound();

            return View(role);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null) return NotFound();
            if(await _roleService.DeleteAsync(id.Value))
            {
                //todo: show message deleted
                ViewBag.SuccessMessage = "File was successfully deleted";
            }
            else
            {
                //todo: show message error
                ViewBag.ErrorMessage = "Role was not deleted, please try again";
            }
            return RedirectToAction("Index", controllerName: "Role");
        }
    }
}
