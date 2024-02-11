using Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

public class CategoryController : BaseController
{
    public IActionResult Create()
    {
        return Ok();
    }
}
