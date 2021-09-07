using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.User.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers.User
{
    public partial class UserController
    {
        public sealed class UserRegisterRequest
        {
            [Required]
            [MaxLength(30, ErrorMessage = "Максимальная длина имени не должна превышать 30 символов")]
            public string Name { get; set; }
            
            [Required]
            public string Password { get; set; }
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(UserRegisterRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.Register(new Register.Request
            {
                Name = request.Name,
                Password = request.Password
            }, cancellationToken);
            
            return Created($"api/v1/users/{user.UserId}", new {});
        }
    }
}