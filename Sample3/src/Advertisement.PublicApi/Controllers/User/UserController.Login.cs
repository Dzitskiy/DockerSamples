using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.User.Contracts;
using Advertisement.Application.Services.User.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Advertisement.PublicApi.Controllers.User
{
    public partial class UserController
    {
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request, CancellationToken cancellationToken)
        {
            var token = await _userService.Login(new Login.Request
            {
                Name = request.UserName,
                Password = request.Password
            }, cancellationToken);

            return Ok(token);
        }

        public class UserLoginRequest
        {
            [Required]
            public string UserName { get; set; }
            
            [Required]
            public string Password { get; set; }
        }
    }
}