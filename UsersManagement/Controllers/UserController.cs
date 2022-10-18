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
            if (!ModelState.IsValid) return View(user);
            await _userService.AddAsync(user);
            await _uniteOfWork.SaveChangesAsync();
            return RedirectToAction("Index", controllerName: "User");
        }
    }
}
