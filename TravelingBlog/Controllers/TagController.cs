using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class TagController:Controller
    {
        public ILoggerManager loggerManager;
        public IUnitOfWork unitOfWork;
        public TagController(ILoggerManager manager,IUnitOfWork unit)
        {
            loggerManager = manager;
            unitOfWork = unit;
        }      
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllTags()
        {
            try
            {
                var tags = unitOfWork.Tags.GetAllTags();
                var list = new List<TagDTOWithId>();
                for (int i = 0; i < tags.Count(); i++)
                {
                    list.Add(new TagDTOWithId { Id = tags.ElementAt(i).Id,Name = tags.ElementAt(i).Name });
                }
                loggerManager.LogInfo("Returned all tags from TagController");
                return Ok(list);
            }
            catch(Exception ex)
            {
                loggerManager.LogError($"Something went wrong inside GetAllTagsAction:{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }        
        [AllowAnonymous]
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
                    var tagDTO = new TagDTOWithId { Id =tag.Id,Name = tag.Name };
                    loggerManager.LogInfo($"Returned tag with id:{id}");
                    return Ok(tagDTO);
                }
            }
            catch(Exception ex)
            {
                loggerManager.LogError($"Something went wrong inside GetTagByIdAction;{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }        
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
        //[Authorize(Roles ="moderator")]
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
