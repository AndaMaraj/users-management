using Microsoft.AspNetCore.Mvc;
using UsersManagement.Services.DTO;
using UsersManagement.Services.IService;
using UsersManagement.Services.UOW;

namespace UsersManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, IUniteOfWork uniteOfWork, IRoleService roleService)
        {
            _userService = userService;
            _uniteOfWork = uniteOfWork;
            _roleService = roleService;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }
        public async Task<IActionResult> Create()
        {
            var roles = await _roleService.GetAllRolesAsync();
            ViewBag.Roles = roles;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDto user)
        {
            // todo: control if name and email does not exist in db
            if (!ModelState.IsValid) return View(user);
            await _userService.AddAsync(user);
            await _uniteOfWork.SaveChangesAsync();
            return RedirectToAction("Index", controllerName: "User");
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return NotFound();
            var user = await _userService.GetByIdAsync(id);
            var roles = await _roleService.GetAllRolesAsync();
            ViewBag.Roles = roles;
            if(user == null) return NotFound();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserDto user)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateAsync(user);
                return RedirectToAction("Index", controllerName: "User");
            }
            return View(user);
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null) return NotFound();
            if(await _userService.DeleteAsync(id.Value))
            {
                // todo: show delete user success message
            }
            else
            {
                // todo: show delete user error message
            }
            return RedirectToAction("Index", controllerName: "User");
        }
    }
}
