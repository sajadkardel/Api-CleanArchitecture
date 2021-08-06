using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Models;
using Api.Models.Identity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Exceptions;
using Data.Contracts;
using ElmahCore;
using Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services;
using Services.Services;
using WebFramework.Api;

namespace Api.Controllers.v1
{
    [ApiVersion("1")]
    public class UsersController : CrudController<UserDto, UserSelectDto, User, int>
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;

        public UsersController(IRepository<User> repository, IMapper mapper, IJwtService jwtService, UserManager<User> userManager) : base(repository, mapper)
        {
            _jwtService = jwtService;
            _userManager = userManager;
        }

        /// <summary>
        /// This method generate JWT Token
        /// </summary>
        /// <param name="tokenRequest">The information of token request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        public virtual async Task<ApiResult<AccessToken>> Token([FromForm] TokenRequest tokenRequest, CancellationToken cancellationToken)
        {
            if (!tokenRequest.grant_type.Equals("password", StringComparison.OrdinalIgnoreCase))
                throw new Exception("OAuth flow is not password.");

            //var user = await userRepository.GetByUserAndPass(username, password, cancellationToken);
            var user = await _userManager.FindByNameAsync(tokenRequest.username);
            if (user == null)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, tokenRequest.password);
            if (!isPasswordValid)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var jwt = await _jwtService.GenerateAsync(user);
            return jwt;
        }

        /// <summary>
        /// Get User By UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public virtual async Task<ApiResult<UserSelectDto>> GetByUserName([FromQuery] string userName, CancellationToken cancellationToken)
        {
            var dto = await Repository.TableNoTracking.ProjectTo<UserSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.UserName.Equals(userName), cancellationToken);
            return dto;
        }

        /// <summary>
        /// Delete User By UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public virtual async Task<ApiResult> DeleteByUserName([FromQuery] string userName, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(user1 => user1.UserName == userName, cancellationToken: cancellationToken);
            await _userManager.DeleteAsync(user);

            return Ok();
        }


        /// <summary>
        /// Update By UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public virtual async Task<ApiResult<UserSelectDto>> UpdateByUserName([FromQuery] string userName, UserDto userDto, CancellationToken cancellationToken)
        {
            var model = await _userManager.Users.SingleOrDefaultAsync(user => user.UserName == userName, cancellationToken: cancellationToken);
            userDto.Id = model.Id;

            model = userDto.ToEntity(Mapper, model);

            await Repository.UpdateAsync(model, cancellationToken);

            var resultDto = await Repository.TableNoTracking.ProjectTo<UserSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);

            return resultDto;
        }
    }
}
