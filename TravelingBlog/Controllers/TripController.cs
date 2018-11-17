using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelingBlog.BusinessLogicLayer.Contracts;
using TravelingBlog.BusinessLogicLayer.ViewModels.DTO;
using TravelingBlog.DataAcceesLayer.Models.Entities;

namespace TravelingBlog.Controllers
{
    [Route("api/trip")]
    public class TripController : Controller
    {
        private readonly ClaimsPrincipal caller;
        private IUnitOfWork unitOfWork;
        private ILoggerManager logger;

        public TripController(ILoggerManager logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            caller = httpContextAccessor.HttpContext.User;
        }        
        [HttpGet]
        public IActionResult GetAllTrips()
        {
            try
            {
                var trips = unitOfWork.Trips.GetAllTrips();
                if (trips == null)
                {
                    logger.LogInfo("TripsNotFound");
                    return NotFound();
                }
                logger.LogInfo("Return all trips from database");
                return Ok(trips);
            }
            catch(Exception ex)
            {
                logger.LogError($"Error occured inside GetAllTripsAction:{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}",Name = "GetTrip")]
        public IActionResult GetTrip(int id)
        {
            try
            {
                var trip = unitOfWork.Trips.GetTripById(id);
                if (trip == null)
                {
                    logger.LogInfo("TripNotFound");
                    return NotFound();
                }
                logger.LogInfo("Return trip with id=" + id);
                return Ok(trip);
            }
            catch(Exception ex)
            {
                logger.LogError($"Error occured inside GetTripAction:{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("GetTripWithPosts/{id}", Name = "GetTripWithPost")]
        public IActionResult GetTripWithPostBlogs(int id)
        {
            try
            {
                var trip = unitOfWork.Trips.GetTripById(id);
                if (trip == null)
                {
                    logger.LogInfo("TripNotFound");
                    return NotFound();
                }
                trip = unitOfWork.Trips.GetTripWithPostBlogs(id);
                logger.LogInfo("Return trip with postblogs id=" + id);
                return Ok(trip);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured inside GetTripAction:{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddTripAsync([FromBody]TripDTO model)
        {
            try
            {
                if(model==null)
                {
                    logger.LogError($"Object sent from client is null");
                    return BadRequest("Trip object is null");
                }
                if(!ModelState.IsValid)
                {
                    logger.LogError($"Object state is not valid");
                    return BadRequest("Trip object is invalid");
                }
                var trip = new Trip { Name = model.Name, IsDone = model.IsDone };
                var userId = caller.Claims.Single(c => c.Type == "id");
                var user = await unitOfWork.Users.GetUserByIdentityId(userId.Value);
                trip.UserInfo = user;
                unitOfWork.Trips.Add(trip);
                return Ok(model);
            }
            catch(Exception ex)
            {
                logger.LogError($"Error occured inside AddTripAsync:{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var trip = unitOfWork.Trips.GetTripById(id);
            if (trip == null)
            {
                return NotFound();
            }
            unitOfWork.Trips.Remove(trip);
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody]TripDTO model)
        {
            try
            {
                if (model == null)
                {
                    logger.LogError($"Object sent from client is null");
                    return BadRequest("Trip object is null");
                }
                if (!ModelState.IsValid)
                {
                    logger.LogError("Invalid object trip recieved from client");
                    return BadRequest("Invalid object sent");
                }
                var trip = unitOfWork.Trips.GetTripById(id);
                if (trip == null)
                {
                    logger.LogError($"Trip with id :{id} has not been found");
                    return NotFound();
                }

                unitOfWork.Trips.Update(trip);
                return Ok(trip);
            }
            catch(Exception ex)
            {
                logger.LogError($"An error occured UpdateTripAction");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}