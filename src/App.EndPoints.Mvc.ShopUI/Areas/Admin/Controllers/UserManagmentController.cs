﻿using System.Data;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using App.Domain.Core.BaseData.Entities;
using App.EndPoints.Mvc.ShopUI.Areas.Admin.Models.ViewModels.Accounts;
using App.EndPoints.Mvc.ShopUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.EndPoints.Mvc.ShopUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManagmentController : Controller
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public UserManagmentController(UserManager<IdentityUser<int>> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string? name)
        {
            List<UserManagmentVM> model;
            if (string.IsNullOrEmpty(name))
            {
                model = await _userManager.Users.Select(x => new UserManagmentVM
                {
                    Id = x.Id,
                    Name = x.UserName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                }).ToListAsync();
            }
            else
            {
                model = await _userManager.Users
                    .Where(x => x.UserName.Contains(name))
                    .Select(x => new UserManagmentVM
                    {
                        Id = x.Id,
                        Name = x.UserName,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                    }).ToListAsync();
            }

            foreach (var user in model)
            {
                user.Roles =
                    await _userManager.GetRolesAsync(await _userManager.Users.FirstAsync(x => x.Id == user.Id));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser<int>
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(user, "CustomerRole");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Remove(int userId)
        {
            var user = await _userManager.Users.FirstAsync(x => x.Id == userId);
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int userId)
        {
            var user = await _userManager.Users.FirstAsync(x => x.Id == userId);
            var model = new UserManagmentVM
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserManagmentVM model)
        {
            var user = await _userManager.Users.FirstAsync(x => x.Id == model.Id);
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.Name;
            await _userManager.UpdateAsync(user);
            if (!string.IsNullOrEmpty(model.Password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, model.Password);
            }

            return RedirectToAction("Index");
        }


        #region Roles

        [HttpGet]
        public async Task<IActionResult> EditRoles(int userId)
        {
            var user = await _userManager.Users.FirstAsync(x => x.Id == userId);
            var model = new UserManagmentVM
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = await _userManager.GetRolesAsync(user)
            };
            ViewBag.Roles = await _roleManager.Roles.Select(x => x.Name).ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(int userId, string role)
        {
            var user = await _userManager.Users.FirstAsync(x => x.Id == userId);
            await _userManager.RemoveFromRoleAsync(user, role);
            return RedirectToAction(nameof(EditRoles), new {userId = userId});
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(int userId, string? role)
        {
            if (!string.IsNullOrEmpty(role))
            {
                var user = await _userManager.Users.FirstAsync(x => x.Id == userId);
                await _userManager.AddToRoleAsync(user, role);
            }

            return RedirectToAction(nameof(EditRoles), new {userId = userId});
        }

        #endregion
    }
}