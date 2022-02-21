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
    
    public class StaffClassificationController : Controller
    {
        string TypeName;
        int UserId;
        ICRUDRepository<Staffclassification, int> _repository;
        public sendServiceMessage _sendServiceMessage;
        public StaffClassificationController(ICRUDRepository<Staffclassification, int>
        repository, sendServiceMessage SendServiceMessage)
        {
            _repository = repository;
            _sendServiceMessage = SendServiceMessage;
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        public ActionResult<IEnumerable<Staffclassification>> Get()
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            try
            {
                var items = _repository.GetAll();
                return View(items);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        [HttpGet]
        public ActionResult<Staffclassification> GetDetails(int id)
        {

            try
            {
                var item = _repository.GetDetails(id);
                if (item == null)
                    return NotFound();
                return item;
            }
            catch (Exception e)
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
        public async Task<ActionResult<Staffclassification>> Create(Staffclassification scl)
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            if (scl == null)
                return BadRequest();
            try
            {
                _repository.Create(scl);
                await _sendServiceMessage.SendServiceMessage(new serviceMessageData
                {
                    TypeName = scl.TypeName,
                    action = "Add",
                    actionMessage = "Staff Added Successfully"
                }
                );
                ViewBag.Message = string.Format("Staff Added successfully");
                return View(scl);
                //return Redirect("/StaffClassification/Get");
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        public IActionResult Update(int StaffTypeId)
        {
            Staffclassification scl = _repository.GetDetails(StaffTypeId);
            if (scl == null)
            {
                return BadRequest();
            }
            else
            {
                return View(scl);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Staffclassification>> Update( Staffclassification scl)
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            if (scl == null)
                return BadRequest();
            try
            {
                _repository.Update(scl);
                await _sendServiceMessage.SendServiceMessage(new serviceMessageData
                {
                    TypeName = scl.TypeName,
                    action = "Update",
                    actionMessage = "Staff Updated Successfully"
                }
           );
                ViewBag.Message = string.Format("Staff Updated successfully");
                return View(scl);
                //return Redirect("/StaffClassification/Get");
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]

        public IActionResult Delete(int StaffTypeId)
        {
            Staffclassification scl = _repository.GetDetails(StaffTypeId);
            if (scl == null)
            {
                return BadRequest();
            }
            else
            {
                return View(scl);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Staffclassification>> Delete(int StaffTypeId, Staffclassification scl)
        {
            TypeName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            UserId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role.ToLower() != "administrator")

            {
                return Unauthorized();
            }
            try
            {
                _repository.Delete(StaffTypeId);
                await _sendServiceMessage.SendServiceMessage(new serviceMessageData
                {
                    TypeName = scl.TypeName,
                    action = "Delete",
                    actionMessage = "Staff Deleted Successfully"
                }
             );
                ViewBag.Message = string.Format("Staff Deleted successfully");
                return View();
                //return Redirect("/StaffClassification/Get");
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}