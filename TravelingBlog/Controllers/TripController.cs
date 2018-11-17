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
        [Route("")]
        [Route("GetAllTrip")]
        [HttpGet]
        public IActionResult GetAllTrip()
        {
            try
            {
                var trips = unitOfWork.Trips.GetAllTrips();
                logger.LogInfo("Returned all trips from TripController");
                return Ok(trips);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside GetAllTripsAction:{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet]
        public IActionResult GetAllTrips()
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
        [HttpGet("{id}")]
        public IActionResult GetTrip(int id)
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

        [HttpPost]
        public async Task<IActionResult> AddTripAsync([FromBody]TripDTO model)
        {
           
            var trip = new Trip { Name = model.Name, IsDone = model.IsDone };
            var userId = caller.Claims.Single(c => c.Type == "id");
            var user = await unitOfWork.Users.GetUserByIdentityId(userId.Value);
            trip.UserInfo = user;
            unitOfWork.Trips.Add(trip);
            return Ok(model);

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
        [HttpPut]
        public IActionResult Update([FromBody] TripDTOWithId model)
        {
            var trip = unitOfWork.Trips.GetTripById(model.Id);
            if (trip == null)
            {
                return NotFound();
            }
           
            unitOfWork.Trips.Update(trip);
            return Ok(trip);
        }
    }
}