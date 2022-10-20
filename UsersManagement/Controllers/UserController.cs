using Microsoft.AspNetCore.Mvc;
using UsersManagement.Services.DTO;
using UsersManagement.Services.IService;
using UsersManagement.Services.UOW;

namespace UsersManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _uniteOfWork;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, IUnitOfWork uniteOfWork, IRoleService roleService)
        {
            _userService = userService;
            _uniteOfWork = uniteOfWork;
            _roleService = roleService;
        }
        public async Task<IActionResult> Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                TempData.Remove("SuccessMessage");
            }
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
            if (!ModelState.IsValid) return View(user);
            var email = await _userService.GetAll(x => x.Email == user.Email);
            if (email.Count() == 0)
            {
                await _userService.AddAsync(user);
                await _uniteOfWork.SaveChangesAsync();
                return RedirectToAction("Index", controllerName: "User");
            }
            else
            {
                ModelState.AddModelError(nameof(user.Email), "There is another user with this email! Please try a different one!");
                var roles = await _roleService.GetAllRolesAsync();
                ViewBag.Roles = roles;
                return View();
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return NotFound();
            var user = await _userService.GetByIdAsync(id);
            var roles = await _roleService.GetAllRolesAsync();
            ViewBag.Roles = roles;
            if (user == null) return NotFound();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserDto user)
        {
            if (ModelState.IsValid)
            {
                var userEmail = await _userService.GetByEmail(user.Email);
                if (userEmail != null && userEmail.Id != user.Id)
                {
                    ModelState.AddModelError((nameof(user.Email)), "There is another user with this email! Please try a different one!");
                    var roles = await _roleService.GetAllRolesAsync();
                    ViewBag.Roles = roles;
                    return View();
                }
                // todo: when we try to update with the same email it throws a thread exception
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
            if (await _userService.DeleteAsync(id.Value))
            {
                TempData["SuccessMessage"] = "User was successfully deleted!";
                return RedirectToAction("Index", controllerName: "User");
            }
            else
            {
                var user = await _userService.GetByIdAsync(id.Value);
                if (user == null) return NotFound();
                ViewBag.ErrorMessage = "User was not deleted, please try again";
                return View(user);
            }
        }
    }
}
