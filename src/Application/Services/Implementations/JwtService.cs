using Application.Services.Contracts;
using Domain;
using Domain.Entities.Identity;
using Domain.Markers;
using Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Implementations
{
    public class JwtService : IJwtService, IScopedDependency
    {
        
    }
}
