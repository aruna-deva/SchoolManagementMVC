using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Infrastructure;
using SchoolManagementMVC.Models.Infrastructure;
using SchoolManagementMVC.Models;
using System.Security.Claims;

namespace SchoolManagement.Controllers
{
    
    public class ClassRoomController : Controller
    {
        string TypeName;
        int UserId;
        ICRUDRepository<ClassRoom, int> _repository;

        public sendServiceMessage _sendServiceMessage;
        public ClassRoomController(ICRUDRepository<ClassRoom, int> repository, sendServiceMessage SendServiceMessage )
        { _repository = repository;
          _sendServiceMessage = SendServiceMessage;
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        public ActionResult<IEnumerable<ClassRoom>> Get()
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")
           
            {
                return Unauthorized();
            }
            //end of the code inclusion. 
            if (UserId == 0) return BadRequest();
            try
            {
                var items = _repository.GetAll();
                return View(items);
            } catch(Exception e)
            {
                throw;
            }
        }  
        [HttpGet("{Standard}")]
        public ActionResult<ClassRoom> GetDetails(int classroomId)
        {
           
            try
            {
                var item = _repository.GetDetails(classroomId);
            if( item==null)
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
        public async Task<ActionResult<ClassRoom>> Create(ClassRoom cls)
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            //end of the code inclusion. 
           
            if (cls==null)
                return BadRequest();
            try {
            _repository.Create(cls);
                ViewBag.Message = string.Format("Classroom Added successfully");
                await _sendServiceMessage.SendServiceMessage(new serviceMessageData
                {
                    Standard = cls.Standard,
                    action = "Add",
                    actionMessage = "Classroom Added Successfully"
                }
                ) ;
                return View(cls);
                //return Redirect("/ClassRoom/Get");
            }
            catch(Exception e){
            throw ;
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        public IActionResult Update(int ClassroomId)
        {
            ClassRoom cls = _repository.GetDetails(ClassroomId);
            if (cls == null)
            {
                return BadRequest();
            }
            else
            {
                return View(cls);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<ClassRoom>> Update( ClassRoom cls)
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            //end of the code inclusion. 
           
            try { 
            if(cls==null)
                return BadRequest();
            //try{
            _repository.Update(cls);
            await _sendServiceMessage.SendServiceMessage(new serviceMessageData
            {
            Standard = cls.Standard,
            action = "Update",
            actionMessage = "Classroom Updated Successfully"
            }
            );
                ViewBag.Message = string.Format("Classroom Updated successfully");
                return View(cls);
            //return Redirect("/ClassRoom/Get");
            }
            catch(Exception e)
            {
                throw;
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        public IActionResult Delete(int ClassroomId)
        {
             ClassRoom cls = _repository.GetDetails(ClassroomId);
                if (cls == null)
                {
                    return BadRequest();
                }
                else
                {
                    return View(cls);
                }
        }
        [HttpPost]
        public async Task<ActionResult<ClassRoom>> Delete(int ClassroomId, ClassRoom cls)
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            //end of the code inclusion. 
            
            try { _repository.Delete(ClassroomId);
          
           await _sendServiceMessage.SendServiceMessage(new serviceMessageData
           {
           Standard = cls.Standard,
           action = "Delete",
           actionMessage = "Classroom Deleted Successfully"
           }
           );
                ViewBag.Message = string.Format("Classroom Deleted successfully");
                return View();
                //return Redirect("/ClassRoom/Get");
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}