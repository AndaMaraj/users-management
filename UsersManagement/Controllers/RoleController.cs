﻿using Microsoft.AspNetCore.Mvc;
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
                await roleService.UpdateAsync(role);
                return RedirectToAction("Index", controllerName: "Role");
            }
            return View(role);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) return NotFound();
            var role = await roleService.GetByIdAsync(id.Value);
            if(role == null) return NotFound();

            return View(role);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null) return NotFound();
            if(await roleService.DeleteAsync(id.Value))
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
