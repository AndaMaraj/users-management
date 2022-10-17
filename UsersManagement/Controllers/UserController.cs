using Microsoft.AspNetCore.Mvc;
using UsersManagement.Services.DTO;
using UsersManagement.Services.IService;
using UsersManagement.Services.UOW;

namespace UsersManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IUniteOfWork uniteOfWork;
        public UserController(IUserService userService, IUniteOfWork uniteOfWork)
        {
            this.userService = userService;
            this.uniteOfWork = uniteOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var users = await userService.GetAllUsersAsync();
            return View(users);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserDto user)
        {
            if (!ModelState.IsValid) return View(user);
            await userService.AddAsync(user);
            await uniteOfWork.SaveChangesAsync();
            return RedirectToAction("Index", controllerName: "User");
        }
    }
}
