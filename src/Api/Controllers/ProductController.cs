using Domain.Dtos.Product;
using Infrastructure.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
public class ProductController : BaseController
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateProductDto dto)
    {
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateProductDto dto)
    {
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}
