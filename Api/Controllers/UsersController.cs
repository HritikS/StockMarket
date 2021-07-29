using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ObjectResult> PostUser(UserModel model)
        {
            model.Role = "User";
            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, model.Role);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = Url.Action("ConfirmEmail", "Users", new { userId = user.Id, token = token }, Request.Scheme);

                //create Email
                using (var email = new MailMessage())
                {
                    email.From = new MailAddress("FromEmail");
                    email.To.Add(model.Email);
                    email.Subject = "Stock Market Email Confirmation";
                    email.Body = link;
                    email.IsBodyHtml = true;

                    //send Email
                    using (var smtp = new SmtpClient("smtp.outlook.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("FromEmail", "Password");
                        smtp.EnableSsl = true;
                        smtp.Send(email);
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token, string? newEmail = null)
        {
            if (userId == null || token == null)
                return NotFound();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "The User ID {userId} is invalid" });
            try
            {
                if (newEmail == null) 
                {
                    var result = await _userManager.ConfirmEmailAsync(user, token);
                    return Ok(new { message = "Your email is confirmed successfully! Please log in now to continue ahead" });
                }
                else
                {
                    var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
                    return Ok(new { message = "Your email is confirmed successfully! Please log in now to continue ahead" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{userName}")]
        public async Task<ObjectResult> PutUser(string userName, UpdateProfileModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null || model.UserName != userName)
                return BadRequest(new { message = "Cannot find the User" });
            else
            {
                var flag = user.Email == model.Email;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                try
                {
                    user.EmailConfirmed = false;
                    var result = await _userManager.UpdateAsync(user);
                    if (flag)
                        return Ok(result);
                    var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.Email);
                    var link = Url.Action("ConfirmEmail", "Users", new { userId = user.Id, token = token, newEmail = model.Email }, Request.Scheme);

                    //create Email
                    using (var email = new MailMessage())
                    {
                        email.From = new MailAddress("FromEmail");
                        email.To.Add(model.Email);
                        email.Subject = "Stock Market Email Confirmation";
                        email.Body = link;
                        email.IsBodyHtml = true;

                        //send Email
                        using (var smtp = new SmtpClient("smtp.outlook.com", 587))
                        {
                            smtp.Credentials = new NetworkCredential("FromEmail", "Password");
                            smtp.EnableSsl = true;
                            smtp.Send(email);
                        }
                    }
                    return NotFound(new { message = "Email is not verified" });

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [HttpDelete("{userName}")]
        public async Task<ActionResult<User>> DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return NotFound();
            await _userManager.DeleteAsync(user);
            return user;
        }

        [HttpPost("pwd/{userName}")]
        public async Task<IActionResult> ChangePassword(string userName, ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                    return NotFound(new { message = "User not found" });
                try
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                    await _signInManager.RefreshSignInAsync(user);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return NotFound(new { message = "Model State is Invalid" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                if (!await _userManager.IsEmailConfirmedAsync(user))
                    return NotFound(new { message = "Email is not confirmed" });
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or Password is INCORRECT" });
        }
    }
}
