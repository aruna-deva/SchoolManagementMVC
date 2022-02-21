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
    
    public class StudentDetailsController : Controller
    {
        string TypeName;
        int UserId;
        ICRUDRepository<StudentDetail, int> _repository;

        public sendServiceMessage _sendServiceMessage;
        public StudentDetailsController(
            ICRUDRepository<StudentDetail, int> repository, sendServiceMessage SendServiceMessage)
        {
            _repository = repository;
            _sendServiceMessage = SendServiceMessage;
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        public ActionResult<IEnumerable<StudentDetail>> Get()
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            //end of the code inclusion. 
           
            try { var items = _repository.GetAll();
            return View(items); }
            catch(Exception e)
            {
                throw;
            }
        }
        [HttpGet("{id}")]
        public ActionResult<StudentDetail> GetDetails(int id)
        {
           try{ var item = _repository.GetDetails(id);
            if(item==null)
               return NotFound();
            return item; }
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
        public async Task<ActionResult<StudentDetail>> Create(StudentDetail std)
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            //end of the code inclusion. 
           
            if (std==null)
                return BadRequest();
            try{
            _repository.Create(std);
                await _sendServiceMessage.SendServiceMessage(new serviceMessageData
                {
                    StudentName = std.StudentName,
                    action = "Add",
                    actionMessage = "Student Added Successfully"
                }
               );
                ViewBag.Message = string.Format("StudentDetail Added successfully");
                return View(std);
                //return Redirect("/StudentDetails/Get");
            }
            catch(Exception e)
            {
                throw;
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        public IActionResult Update( int Id)
        {
            StudentDetail std = _repository.GetDetails(Id);
            if (std == null)
            {
                return BadRequest();
            }
            else
            {
                return View(std);
            }
        }
        [HttpPost]
        public async Task<ActionResult<StudentDetail>> Update( StudentDetail std)
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            //end of the code inclusion. 
           
            if (std==null)
                return BadRequest();
           try{ _repository.Update(std);
                await _sendServiceMessage.SendServiceMessage(new serviceMessageData
                {
                    StudentName = std.StudentName,
                    action = "Update",
                    actionMessage = "Student Updated Successfully"
                }
              );
                ViewBag.Message = string.Format("StudentDetail updated successfully");
                return View(std);
               // return Redirect("/StudentDetails/Get"); 
            }
            catch(Exception e)
            {
                throw;
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        public IActionResult Delete(int Id)
        {
            StudentDetail std = _repository.GetDetails(Id);
            if (std == null)
            {
                return BadRequest();
            }
            else
            {
                return View(std);
            }
        }
        [HttpPost]
        public async  Task<ActionResult<StudentDetail>> Delete(int Id, StudentDetail std)
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
                    StudentName = std.StudentName,
                    action = "Delete",
                    actionMessage = "Student Deleted Successfully"
                }
              );
                ViewBag.Message = string.Format("StudentDetail Deleted successfully");
                return View();
                //return Redirect("/StudentDetails/Get");
                //return Ok();
            }
            catch(Exception e)
            {
                throw;
            }
        }

    }
}