using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Infrastructure;
using System.Security.Claims;
using SchoolManagementMVC.Models.Infrastructure;
using SchoolManagementMVC.Models;

namespace SchoolManagement.Controllers
{
   
    public class TimeTableController : Controller
    {
        string TypeName;
        int UserId;
          ICRUDRepository<TimeTable, int> _repository;
        public sendServiceMessage _sendServiceMessage;
        public TimeTableController(ICRUDRepository<TimeTable, int> repository, sendServiceMessage SendServiceMessage)
        {
            _repository = repository;
            _sendServiceMessage = SendServiceMessage;
        }
        /************** CHANGES : ******************************************
        * Add the Authorize attribute to the method 
        * In the method, we are getting the Claim values like Name, NameIdentifier and Role 
        * if the RoleName is not "admin" or any other role as required, 
        *  then we are returning an Unauthorized() response back to the user.
        * else valid response will be sent. 
        ********************************************************************/
        [Microsoft.AspNetCore.Authorization.Authorize()]
        public ActionResult<IEnumerable<TimeTable>> Get()
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if(role.ToLower()!="principal" &&
            role.ToLower()!="viceprincipal") {
                return Unauthorized();
            }
            //end of the code inclusion. 
            if(UserId==0) return BadRequest();
            try
            {
            var items = _repository.GetAll();
            return View(items); 
            }
            catch(Exception e)
            {
                throw;
            }
        }  
        [Microsoft.AspNetCore.Authorization.Authorize()]

        [HttpGet]
        public ActionResult<TimeTable> GetDetails(int id)
        { 
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if(role.ToLower()!="principal" &&
            role.ToLower()!="viceprincipal") {
                return Unauthorized();
            }
            try{var item = _repository.GetDetails(id);
            if( item==null )
                return NotFound();
            return item;    }
            catch(Exception e)
            { 
                throw;
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [Microsoft.AspNetCore.Authorization.Authorize()]

        [HttpPost]
        public async Task<ActionResult<TimeTable>> Create(TimeTable tt)
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if(role.ToLower()!="principal" &&
            role.ToLower()!="viceprincipal")
            {
                return Unauthorized();
            }
            if(tt==null)
                return BadRequest();
            try{
            _repository.Create(tt);
                await _sendServiceMessage.SendServiceMessage(new serviceMessageData
                {
                    TeacherId = tt.TeacherId,
                    action = "Add",
                    actionMessage = "TimeTable Added Successfully"
                }
                );
                ViewBag.Message = string.Format("TimeTable Added successfully");
                return View(tt);
               // return Redirect("/TimeTable/Get"); 
            }
            catch(Exception e)
            {
                throw;
            }
        }
        
        public IActionResult Update(int Id)
        {
            TimeTable tt = _repository.GetDetails(Id);
            if (tt == null)
            {
                return BadRequest();
            }
            else
            {
                return View(tt);
            }
        }
        [HttpPost]
        public async Task<ActionResult<TimeTable>> Update( TimeTable tt)
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "principal" &&
            role.ToLower() != "viceprincipal")
            {
                return Unauthorized();
            }
            
            if (tt==null)
                return BadRequest();
           try{ 
                _repository.Update(tt);
                await _sendServiceMessage.SendServiceMessage(new serviceMessageData
                {
                    TeacherId = tt.TeacherId,
                    action = "Update",
                    actionMessage = "Timetable Updated Successfully"
                }
           );
                ViewBag.Message = string.Format("Timetable Updated successfully");
                return View(tt);
                //return Redirect("/TimeTable/Get");
            }
            catch(Exception e)
            {
                throw;
            }
        }
        public IActionResult Delete(int Id)
        {
            TimeTable tt = _repository.GetDetails(Id);
            if (tt == null)
            {
                return BadRequest();
            }
            else
            {
                return View(tt);
            }
        }
        [HttpPost]

        public async Task<ActionResult<TimeTable>> Delete(int Id, TimeTable tt)
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "principal" &&
            role.ToLower() != "viceprincipal")
            {
                return Unauthorized();
            }
           
            try
            {
                _repository.Delete(Id);
                await _sendServiceMessage.SendServiceMessage(new serviceMessageData
                {
                    TeacherId = tt.TeacherId,
                    action = "Delete",
                    actionMessage = "TimeTable Deleted Successfully"
                });
                ViewBag.Message = string.Format("Deleted Successfully");
                return View();
                //return Redirect("/TimeTable/Get");
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}