using Application.Dtos.Auth;
using Application.Services.Contracts;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AuthController(IAuthService authService) : BaseController
{
    private readonly IAuthService _authService = authService;

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto request, CancellationToken cancellationToken = default)
    {
        var response = await _authService.SignUpAsync(request, cancellationToken);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] SignInRequestDto request, CancellationToken cancellationToken = default)
    {
        var response = await _authService.SignInAsync(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> SignOut([FromBody] SignOutRequestDto request, CancellationToken cancellationToken = default)
    {
        await _authService.SignOutAsync(request, cancellationToken);
        return Ok();
    }
}
