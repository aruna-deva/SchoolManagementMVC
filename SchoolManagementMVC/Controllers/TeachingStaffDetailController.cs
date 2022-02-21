using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementMVC.Models;
using SchoolManagementMVC.Models.Infrastructure;
using SchoolManagementSystem.Infrastructure;
using SchoolManagementSystem.Models;

namespace SchoolManagement.Controllers
{
    
    public class TeachingStaffDetailController : Controller
    {
        string TypeName;
        int UserId;
        ICRUDRepository<TeachingStaffDetail, int> _repository;
        public sendServiceMessage _sendServiceMessage;
        public TeachingStaffDetailController(ICRUDRepository<TeachingStaffDetail, int>
        repository, sendServiceMessage SendServiceMessage)
        {
            _repository = repository;
            _sendServiceMessage = SendServiceMessage;
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        public ActionResult<IEnumerable<TeachingStaffDetail>> Get()
        {

            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            //end of the code inclusion. 
            try
            {
                var items = _repository.GetAll();
            return View(items); }
            catch(Exception e)
            {
                throw;
            }
        }  

        [HttpGet("{id}")]
        public ActionResult<TeachingStaffDetail> GetDetails(int id)
        {

            try
            {
                var item = _repository.GetDetails(id);
            if( item==null )
                return NotFound();
            return item;   }
            catch(Exception e)
            {
                throw;
            } 
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<TeachingStaffDetail>> Create(TeachingStaffDetail tsd)
        {

            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            //end of the code inclusion. 
            if (tsd==null)
                return BadRequest();
            try{
            _repository.Create(tsd);
                await _sendServiceMessage.SendServiceMessage(new serviceMessageData
                {
                    TeacherName = tsd.TeacherName,
                    action = "Add",
                    actionMessage = "TeacherDetail Added Successfully"
                }
               );
                
                ViewBag.Message = string.Format("TeacherDetail Added successfully");
                return View(tsd);
            //return Redirect("/TeachingStaffDetail/Get"); 
            }
            catch(Exception e)
            {
                throw;
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        public IActionResult Update(int Id)
        {
            TeachingStaffDetail tsd = _repository.GetDetails(Id);
            if (tsd == null)
            {
                return BadRequest();
            }
            else
            {
                return View(tsd);
            }
        }
        [HttpPost]
        public async Task<ActionResult<TeachingStaffDetail>> Update( TeachingStaffDetail tsd)
        {

            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            //end of the code inclusion. 
            if (tsd==null)
                return BadRequest();
           try{
                _repository.Update(tsd);
                await _sendServiceMessage.SendServiceMessage(new serviceMessageData
                {
                    TeacherName = tsd.TeacherName,
                    action = "Update",
                    actionMessage = "TeacherDetail Updated Successfully"
                }
               );
               
                ViewBag.Message = string.Format("TeacherDetail Updated successfully");
                return View(tsd);
                //return Redirect("/TeachingStaffDetail/Get");
            }
            catch(Exception e)
            {
                throw;
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        public IActionResult Delete(int Id)
        {
            TeachingStaffDetail tsd = _repository.GetDetails(Id);
            if (tsd == null)
            {
                return BadRequest();
            }
            else
            {
                return View(tsd);
            }
        }
        [HttpPost]
        public async Task<ActionResult<TeachingStaffDetail>> Delete(int Id, TeachingStaffDetail tsd)
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            //end of the code inclusion. 
            
                try
                { 
                _repository.Delete(Id);
                await _sendServiceMessage.SendServiceMessage(new serviceMessageData
                {
                    TeacherName = tsd.TeacherName,
                    action = "Delete",
                    actionMessage = "TeacherDetail Deleted Successfully"
                }
               );
               
                ViewBag.Message = string.Format("TeacherDetail Deleted successfully");
                return View();
                //return Redirect("/TeachingStaffDetail/Get"); 
            }
            catch(Exception e)
            {
                throw;
            }
        }    
    }
}