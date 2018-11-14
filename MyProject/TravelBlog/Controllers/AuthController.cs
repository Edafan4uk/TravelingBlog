using Contracts;
using Entities.ExtendedModels;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace TravelBlog.Controllers
{
    public class TestAuthController : Controller
    {
        private IUnitOfWork unitOfWork;
        private ILoggerManager logger;

        public TestAuthController(ILoggerManager logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [Authorize]
        [Route("secret")]
        public IActionResult Secret()
        {
            try
            {
                //unitOfWork.Users.Add(new User(this.User));
                //unitOfWork.Complete();
                logger.LogInfo($"User {this.User.Identity.Name} authorized by Google");
                return View(new User(this.User));
            }

            catch (Exception ex)
            {
                logger.LogInfo($"Something went wrong inside Add action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("home")]
        public IActionResult Home()
        {
            return View(this.User.Identities.Any(v => v.IsAuthenticated));
        }
    }
}
