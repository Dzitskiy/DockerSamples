using System.Collections.Generic;
using Advertisement.Application.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers.User
{
    [Route("api/v1/users")]
    [ApiController]
    [AllowAnonymous]
    public partial class UserController : ControllerBase
    {
        private readonly IUserService _userService;
    }
}