﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelingBlog.BusinessLogicLayer.Contracts;
using TravelingBlog.BusinessLogicLayer.Contracts.Repositories;
using TravelingBlog.BusinessLogicLayer.ResourseHelpers;
using TravelingBlog.DataAcceesLayer.Data;
using TravelingBlog.DataAcceesLayer.Models.Entities;

namespace TravelingBlog.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        public IUnitOfWork UnitOfWork { get; set; }

        private ILoggerManager logger { get; set; }

        private IHttpContextAccessor contextAccessor { get; set; }

        public SearchController(IUnitOfWork _unitOfWork, ILoggerManager _logger)
        {
            UnitOfWork = _unitOfWork;
            logger = _logger;
        }


        [HttpGet]
        [Route("api/search/findtrips")]
        public IActionResult GetTripsBySearchResult(PagingModel attribute)
        {
            try
            {
                var result = UnitOfWork.PostBlogs.SearchBlog(attribute);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }

        }

        [HttpGet]
        [Route("api/search/filterbycountries")]
        public IActionResult FilterResult(string country)
        {
            var result = UnitOfWork.Trips.FilterTripsByCountry(country);

            return Ok(result);
        }
    }
}