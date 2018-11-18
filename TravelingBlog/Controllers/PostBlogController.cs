using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelingBlog.BusinessLogicLayer.Contracts;
using TravelingBlog.BusinessLogicLayer.ViewModels.DTO;
using TravelingBlog.DataAcceesLayer.Models.Entities;

namespace TravelingBlog.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    [Authorize]
    public class PostBlogController : Controller
    {
        private readonly ClaimsPrincipal caller;
        public ILoggerManager logger;
        public IUnitOfWork unitOfWork;

        public PostBlogController(ILoggerManager _logger, IUnitOfWork _unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            logger = _logger;
            unitOfWork = _unitOfWork;
            caller = httpContextAccessor.HttpContext.User;
        }
        [HttpGet]
        public IActionResult GetAllBlogs()
        {
            var blog = unitOfWork.PostBlogs.GetAllPostBlogs();
            if (blog == null)
            {
                logger.LogInfo("TripsNotFound");
                return NotFound();
            }
            logger.LogInfo("Return all trips from database");
            return Ok(blog);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var blog = unitOfWork.PostBlogs.GetPostBlogById(id);
            if (blog == null)
            {
                logger.LogInfo("TripNotFound");
                return NotFound();
            }
            logger.LogInfo("Return trip with id=" + id);
            return Ok(blog);
        }

        [HttpPost]
        public async Task<IActionResult> AddBlogAsync([FromBody]PostBlogDTO model)
        {
            var blog = new PostBlog
            {
                Name = model.Name,
                DateOfCreation = model.DateOfCreation,
                Plot = model.Plot
            };
            var userId = caller.Claims.Single(c => c.Type == "id");
            //var customer = await appDbContext.UserInfoes.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);
            var user = await unitOfWork.Users.GetUserByIdentityId(userId.Value);
            var trip = unitOfWork.Trips.GetTripById(model.TripId);
            var isUserCreator = unitOfWork.Trips.IsUserCreator(user.Id, trip.Id);
            if (!isUserCreator)
            {
                return BadRequest();
            }
            blog.Trip = trip;
            unitOfWork.PostBlogs.Add(blog);
            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> AsyncDeleteBlog(int id)
        {
            var userId = caller.Claims.Single(c => c.Type == "id");
            var user = await unitOfWork.Users.GetUserByIdentityId(userId.Value);
            var post = unitOfWork.PostBlogs.GetPostBlogById(id);
            var trip = unitOfWork.Trips.GetTripById(post.TripId);
            var isUserCreator = unitOfWork.Trips.IsUserCreator(user.Id, trip.Id);
            if (!isUserCreator)
            {
                return BadRequest();

            }
            unitOfWork.PostBlogs.Remove(post);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AsyncUpdateBlog([FromBody]PostBlogDTOWithId model)
        {
            var postblog = unitOfWork.PostBlogs.GetPostBlogById(model.Id);
            if (postblog == null)
            {
                return NotFound();
            }
            postblog.Name = model.Name;
            postblog.Plot = model.Plot;

            unitOfWork.PostBlogs.Update(postblog);

            return Ok(postblog);
        }


    }
}
