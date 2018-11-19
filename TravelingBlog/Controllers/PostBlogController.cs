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
        private ILoggerManager logger;
        private IUnitOfWork unitOfWork;

        public PostBlogController(ILoggerManager _logger, IUnitOfWork _unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            logger = _logger;
            unitOfWork = _unitOfWork;
            caller = httpContextAccessor.HttpContext.User;
        }
        [HttpGet]
        public IActionResult GetAllBlogs()
        {
            try
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
            catch (Exception ex)
            {

                logger.LogError($"Something went wrong inside GetAllBlogs;{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside GetAllBlogs by Id;{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddBlogAsync([FromBody]PostBlogDTO model)
        {
            try
            {
                var post = new PostBlog
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
                post.Trip = trip;
                unitOfWork.PostBlogs.Add(post);
                return Ok(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside AddBlogAsync;{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> AsyncDeleteBlog(int id)
        {
            try
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
                return Ok(post);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside AsyncDeleteBlog;{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AsyncUpdateBlog(int id, [FromBody]PostBlogDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var userId = caller.Claims.Single(c => c.Type == "id");
                var user = await unitOfWork.Users.GetUserByIdentityId(userId.Value);
                var trip = unitOfWork.Trips.GetTripWithPostBlogs(model.TripId);
                var isUserCreator = unitOfWork.Trips.IsUserCreator(user.Id, trip.Id);
                if (!isUserCreator)
                {
                    return BadRequest();

                }
                var post = unitOfWork.PostBlogs.GetPostBlogById(id);
                if (post == null)
                {
                    return NotFound();
                }

                post.Name = model.Name;
                post.Plot = model.Plot;

                unitOfWork.PostBlogs.Update(post);

                return Ok(post);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside AsyncUpdateBlog;{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }


    }
}
