using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Models;
using Api.Models.Identity;
using AutoMapper;
using Data.Contracts;
using Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.Services;
using WebFramework.Api;

namespace Api.Controllers.v2
{
    [ApiVersion("2")]
    public class UsersController : Api.Controllers.v1.UsersController
    {
        public UsersController(IRepository<User> repository, IMapper mapper, IJwtService jwtService, UserManager<User> userManager) : base(repository, mapper, jwtService, userManager)
        {
        }

        public override Task<ApiResult<AccessToken>> Token(TokenRequest tokenRequest, CancellationToken cancellationToken)
        {
            return base.Token(tokenRequest, cancellationToken);
        }

        public override Task<ApiResult<UserSelectDto>> GetByUserName(string userName, CancellationToken cancellationToken)
        {
            return base.GetByUserName(userName, cancellationToken);
        }

        public override Task<ApiResult> DeleteByUserName(string userName, CancellationToken cancellationToken)
        {
            return base.DeleteByUserName(userName, cancellationToken);
        }

        public override Task<ApiResult<UserSelectDto>> UpdateByUserName(string userName, UserDto userDto, CancellationToken cancellationToken)
        {
            return base.UpdateByUserName(userName, userDto, cancellationToken);
        }


        public override Task<ActionResult<List<UserSelectDto>>> Get(CancellationToken cancellationToken)
        {
            return base.Get(cancellationToken);
        }

        public override Task<ApiResult<UserSelectDto>> Get(int id, CancellationToken cancellationToken)
        {
            return base.Get(id, cancellationToken);
        }

        public override Task<ApiResult<UserSelectDto>> Create(UserDto dto, CancellationToken cancellationToken)
        {
            return base.Create(dto, cancellationToken);
        }

        public override Task<ApiResult<UserSelectDto>> Update(int id, UserDto dto, CancellationToken cancellationToken)
        {
            return base.Update(id, dto, cancellationToken);
        }

        public override Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            return base.Delete(id, cancellationToken);
        }
    }
}
