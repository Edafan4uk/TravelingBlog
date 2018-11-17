using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelingBlog.BusinessLogicLayer.Contracts;
using TravelingBlog.BusinessLogicLayer.ViewModels.DTO;
using TravelingBlog.DataAcceesLayer.Models.Entities;

namespace TravelingBlog.Controllers
{
    [Route("api/tag")]
    public class TagController:Controller
    {
        public ILoggerManager loggerManager;
        public IUnitOfWork unitOfWork;
        public TagController(ILoggerManager manager,IUnitOfWork unit)
        {
            loggerManager = manager;
            unitOfWork = unit;
        }
        [Route("")]
        [Route("GetAllTags")]
        [HttpGet]
        public IActionResult GetAllTags()
        {
            try
            {
                var tags = unitOfWork.Tags.GetAllTags();
                loggerManager.LogInfo("Returned all tags from TagController");
                return Ok(tags);
            }
            catch(Exception ex)
            {
                loggerManager.LogError($"Something went wrong inside GetAllTagsAction:{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [Route("{id}")]
        [Route("GetTagById/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetTagById(int id)
        {
            try
            {
                var tag = unitOfWork.Tags.GetTagById(id);
                if(tag==null)
                {
                    loggerManager.LogError($"Tag with id :{id} has not been found in db!");
                    return NotFound();
                }
                else
                {
                    loggerManager.LogInfo($"Returned tag with id:{id}");
                    return Ok(tag);
                }
            }
            catch(Exception ex)
            {
                loggerManager.LogError($"Something went wrong inside GetTagByIdAction;{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [Route("")]
        [Route("CreateTag")]
        [HttpPost]
        public IActionResult CreateTag([FromBody]TagDTO tagDTO)
        {
            try
            {
                if(tagDTO==null)
                {
                    loggerManager.LogError($"Object sent from client is null");
                    return BadRequest("Tag object is null");
                }
                if(!ModelState.IsValid)
                {
                    loggerManager.LogError($"Object state is not valid");
                    return BadRequest("Invalid model object");
                }
                unitOfWork.Tags.Add(new Tag { Name = tagDTO.Name });

                return Ok();

            }
            catch(Exception ex)
            {
                loggerManager.LogError($"Something went wrong inside CreateTagAction:{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /*
        [Route("")]
        [Route("UpdateTag/{id}")]
        [HttpPut("{id}")]
        public IActionResult UpdateTag(int id,[FromBody]TagDTO tagDTO)
        {
            try
            {
                if(tagDTO == null)
                {
                    loggerManager.LogError($"Object sent from client is null");
                    return BadRequest("Tag object is null");
                }
                if(!ModelState.IsValid)
                {
                    loggerManager.LogError("Invalid object tag recieved from client");
                    return BadRequest("Invalid object sent");
                }
                var tag = unitOfWork.Tags.GetTagById(id);
                if(tag==null)
                {
                    loggerManager.LogError($"Tag with id:{id} has not been found");
                    return NotFound();
                }
                tag.Name = tagDTO.Name;
                unitOfWork.Tags.Update(tag);
                return NoContent();
            }
            catch(Exception ex)
            {
                loggerManager.LogError($"Something went wrong inside UpdateTagAction:{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        */
        [HttpDelete("{id}")]
        public IActionResult DeleteTag(int id)
        {
            try
            {
                var tag = unitOfWork.Tags.GetTagById(id);
                if(tag == null)
                {
                    loggerManager.LogError($"Tag with id :{id} has not been found in db!");
                    return NotFound();
                }
                unitOfWork.Tags.Remove(tag);
                return NoContent();
            }            
            catch(Exception ex)
            {
                loggerManager.LogError($"Error occured in DeleteTagAction:{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
