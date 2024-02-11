using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Api;

[ApiController]
[Route("api/[controller]/[action]")]
public class BaseController : ControllerBase
{
}
