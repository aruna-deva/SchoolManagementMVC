using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SchoolManagementSystem.Infrastructure;
using SchoolManagementSystem.Models;

namespace SchoolManagement.Controllers
{
    
    public class StaffController : Controller
    {
        SchoolManagementSystem.Infrastructure.IUserService _userService; 
        public StaffController(IUserService service)=> _userService=service;
       
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(User model)
        {
            //check whether all the properties are filled with values and
            //sent from the client side.All the required properties should
            //have values.Else throw error;
            if(!ModelState.IsValid)
                return BadRequest();
            //invoke the authenticate method which will hit the DB and return bool status
                var LogInStatus=_userService.Authenticate(model);
                if(LogInStatus==false) //user does not exist
                {
                    return NotFound();
                }
                /*********CHANGES*******/
                var role = _userService.GetUserRole(model.Id);
                //build a claims identity and Sign the User as was done in the Login();
                /*************** CHANGES TO THE CLAIMS ************************************
                *  The claim types are updated to reflect the application requirements. 
                * The first claim added is for the userName, 
                * the second claim is for the NameIdentifier or Id 
                * the third claim is for the Role to which the user belongs.
                ************************************************************************/
                 var claims=new List<Claim>
                {
                new Claim(ClaimTypes.Name, model.TypeName),
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(ClaimTypes.Role, role.RoleName)
                };
                var claimsIdentity=new ClaimsIdentity(
                claims: claims,
                authenticationType: CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties=new AuthenticationProperties
                {
                    AllowRefresh=true,
                    IsPersistent=true
                };
                await HttpContext.SignInAsync(
                    scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                    principal: new ClaimsPrincipal(claimsIdentity),
                    properties: authProperties
                );
            //return Ok();
            //return Redirect("/TimeTable/Get");
            return Redirect("/ClassRoom/Get");
        }
      

        [HttpGet]
        public async Task<IActionResult> LogOut() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
        static Dictionary<int, string> users=new Dictionary<int, string>
        {
            {1001, "Principal"},{1002, "Vice Principal"}, {1005, "Administrator"}
        };
    }
}