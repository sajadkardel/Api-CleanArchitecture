using Infrastructure.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Api
{
    [ApiController]
    [AllowAnonymous]
    [ApiResultFilter]
    [Route("api/v{version:apiVersion}/[controller]")]// api/v1/[controller]
    public class BaseController : ControllerBase
    {
        //public UserRepository UserRepository { get; set; } => property injection
        public bool UserIsAuthenticated => HttpContext.User.Identity is { IsAuthenticated: true };
    }
}
