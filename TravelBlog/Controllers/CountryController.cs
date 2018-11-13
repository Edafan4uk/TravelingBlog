using System;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace TravelBlog.Controllers
{
    [Produces("application/json")]
    [Route("api/country")]
    public class CountryController : Controller
    {
        private IUnitOfWork unitOfWork;
        private ILoggerManager logger;

        public CountryController(ILoggerManager logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        // GET api/values
        [HttpGet]
        public IActionResult GetAllCountries()
        {
            try
            {
                var countries = unitOfWork.Countries.GetAllCountries();
                logger.LogInfo("Return all countries from databse.");
                return Ok(countries);
            }

            catch (Exception ex)
            {
                logger.LogInfo($"Something went wrong inside GetAllContries action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetCountryUsers(int id)
        {
            try
            {
                var country = unitOfWork.Countries.GetCountryWithDetails(id);

                if (country == null)
                {
                    logger.LogError($"Country with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    logger.LogInfo($"Return all users of the country with id {id} from databse.");
                    return Ok(country.Users);
                }
            }

            catch (Exception ex)
            {
                logger.LogInfo($"Something went wrong inside GetCountryUsers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            try
            {
                var country = unitOfWork.Countries.GetCountryById(id);

                if (country == null)
                {
                    logger.LogError($"Country with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    logger.LogInfo($"Returned country with id: {id}");
                    return Ok(country);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
