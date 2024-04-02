using Application.Dtos.Category;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
public class CategoryController : BaseController
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateCategoryDto dto)
    {
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateCategoryDto dto)
    {
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}
