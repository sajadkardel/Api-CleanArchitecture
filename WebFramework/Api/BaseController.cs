﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Attributes;

namespace WebFramework.Api
{
    [ApiController]
    [AllowAnonymous]
    [ApiResultFilter]
    [Route("api/v{version:apiVersion}/[controller]")]// api/v1/[controller]
    public class BaseController : ControllerBase
    {
        //public UserRepository UserRepository { get; set; } => property injection
        public bool UserIsAuthenticated => HttpContext.User.Identity is {IsAuthenticated: true};
    }
}
