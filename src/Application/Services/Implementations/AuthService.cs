using Application.Dtos.Auth;
using Application.Services.Contracts;
using Domain.Constants;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Domain.Markers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Implementations;

public class AuthService(IOptions<ConfigurationConst> configuraion, UserManager<ApplicationUser> userManager) : IAuthService, IScopedDependency
{
    private readonly ConfigurationConst _configuration = configuraion.Value;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<AccessTokenResponseDto> SignUpAsync(SignUpRequestDto request, CancellationToken cancellationToken = default)
    {
        var result = await _userManager.CreateAsync(new ApplicationUser
        {
            UserName = request.UserName,
            FirstName = request.FisrtName,
            LastName = request.LastName,
        }, request.Password);
        if (result.Succeeded is false) throw new BadRequestException();

        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user is null) throw new NotFoundException("نام کاربری یا رمز عبور اشتباه است");

        var token = GenerateToken(user);

        return token;
    }

    public async Task<AccessTokenResponseDto> SignInAsync(SignInRequestDto request, CancellationToken cancellationToken = default)
    {
        if (!request.GreantType.Equals("password", StringComparison.OrdinalIgnoreCase)) throw new BadRequestException("OAuth flow is not password.");

        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user is null) throw new NotFoundException("نام کاربری یا رمز عبور اشتباه است");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (isPasswordValid is false) throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

        var token = GenerateToken(user);

        return token;
    }

    public Task SignOutAsync(SignOutRequestDto request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    // private methods

    private AccessTokenResponseDto GenerateToken(ApplicationUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtConfigurationConst.SecretKey));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        var encryptionkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtConfigurationConst.EncryptKey));
        EncryptingCredentials encryptingCredentials = new(encryptionkey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
        };

        SecurityTokenDescriptor descriptor = new()
        {
            Issuer = _configuration.JwtConfigurationConst.Issuer,
            Audience = _configuration.JwtConfigurationConst.Audience,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow.AddMinutes(_configuration.JwtConfigurationConst.NotBeforeMinutes),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.JwtConfigurationConst.ExpirationMinutes),
            SigningCredentials = credentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claims)
        };

        JwtSecurityTokenHandler tokenHandler = new();
        JwtSecurityToken securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

        return new AccessTokenResponseDto
        {
            token_type = "Bearer",
            access_token = tokenHandler.WriteToken(securityToken),
            expires_in = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds
        };
    }
}
