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
    public class AuthController : Controller
    {
        [Authorize]
        [Route("secret")]
        public IActionResult Secret()
        {
            return View(new User(this.User));
        }

        [Route("home")]
        public IActionResult Home()
        {
            return View(this.User.Identities.Any(v => v.IsAuthenticated));
        }
    }
}
