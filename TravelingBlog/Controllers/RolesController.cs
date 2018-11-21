using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TravelingBlog.DataAcceesLayer.Models.Entities;
using TravelingBlog.BusinessLogicLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using TravelingBlog.BusinessLogicLayer.ViewModels.DTO;
using TravelingBlog.BusinessLogicLayer.Contracts;
using TravelingBlog.DataAcceesLayer.Models;

namespace TravelingBlog.Controllers
{
    //[Authorize(Roles = "moderator, admin")]
    [Route("api/[controller]/[action]")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private ILoggerManager logger;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, ILoggerManager logger)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var rolesList = roleManager.Roles.ToList();
            #region
            //var rolesListDTO = new List<RoleDTO>();
            //try
            //{
            //int IdDTO = 0;

            //    foreach (var role in rolesList)
            //    {
            //        Int32.TryParse(role.Id, out IdDTO);
            //        rolesListDTO.Add(new RoleDTO { Id = IdDTO, Name = role.Name });
            //    }
            //}
            //catch (System.FormatException e)
            //{
            //    logger.LogError($"Wrong input format inside RolesController;{e.Message}");
            //    return StatusCode(500, "Internal Server Error");
            //}
            #endregion

            return Ok(rolesList);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Ok(name);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
        #region
        //[HttpPost]
        //public async Task<IActionResult> Delete(string name)
        //{
        //    IdentityRole role = await roleManager.FindByNameAsync(name);
        //    if (role != null)
        //    {
        //        IdentityResult result = await roleManager.DeleteAsync(role);
        //    }
        //    return RedirectToAction("Index");
        //}
        #endregion
        [HttpGet]
        public IActionResult UserList()
        {
            var userList = userManager.Users.ToList();
            #region
            //var userListDTO = new List<UserInfoDTO>();
            //foreach (var user in userList)
            //{
            //    userListDTO.Add(new UserInfoDTO
            //    {
            //        FirstName = user.UserName,
            //        //LastName = String.Empty,
            //        Phone = user.PhoneNumber
            //    });
            //}
            #endregion
            return Ok(userList);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            AppUser user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserRoles = userRoles,
                };
                return Ok(model);
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            AppUser user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // Усі ролі юзера
                var userRoles = await userManager.GetRolesAsync(user);
                // Усі ролі
                var allRoles = roleManager.Roles.ToList();
                // Ролі юзера, які додаються
                var addedRoles = roles.Except(userRoles);
                // Ролі юзера, які видаляються
                var removedRoles = userRoles.Except(roles);

                await userManager.AddToRolesAsync(user, addedRoles);

                await userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }
            return NotFound();
        }

    }
}
