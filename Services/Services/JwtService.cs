using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Markers;
using Common.Settings;
using Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Services.Services
{
    public class JwtService : IJwtService, IScopedDependency
    {
        private readonly GeneralSettings _generalSettings;
        private readonly SignInManager<User> _signInManager;

        public JwtService(IOptionsSnapshot<GeneralSettings> settings, SignInManager<User> signInManager)
        {
            _generalSettings = settings.Value;
            _signInManager = signInManager;
        }

        public async Task<AccessToken> GenerateAsync(User user)
        {

            byte[] secretKey = Encoding.UTF8.GetBytes(_generalSettings.JwtSettings.SecretKey); // longer that 16 character
            SigningCredentials signingCredentials = new(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            byte[] encryptionkey = Encoding.UTF8.GetBytes(_generalSettings.JwtSettings.EncryptKey); //must be 16 character
            EncryptingCredentials encryptingCredentials = new(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            IEnumerable<Claim> claims = await GetClaimsAsync(user);

            SecurityTokenDescriptor descriptor = new()
            {
                Issuer = _generalSettings.JwtSettings.Issuer,
                Audience = _generalSettings.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_generalSettings.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_generalSettings.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            JwtSecurityTokenHandler tokenHandler = new();

            JwtSecurityToken securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

            return new AccessToken(securityToken);
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            ClaimsPrincipal result = await _signInManager.ClaimsFactory.CreateAsync(user);
            List<Claim> list = new(result.Claims);

            return list;
        }
    }
}
