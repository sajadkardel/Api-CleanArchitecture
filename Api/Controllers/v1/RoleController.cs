using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Models.Identity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Entities.Identity;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;

namespace Api.Controllers.v1
{
    [ApiVersion("1")]
    public class RoleController : BaseController
    {
        private readonly IRepository<Role> _repository;
        private readonly IMapper _mapper;

        public RoleController(IRepository<Role> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<RoleSelectDto>> Get(CancellationToken cancellationToken)
        {
            var roles = await _repository.TableNoTracking.ProjectTo<RoleSelectDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return roles;
        }

        [HttpGet("{id:int}")]
        public async Task<RoleSelectDto> Get(int id, CancellationToken cancellationToken)
        {
            var role = await _repository.TableNoTracking.ProjectTo<RoleSelectDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(roleSelectDto => roleSelectDto.Id == id, cancellationToken);
            return role;
        }

        [HttpPost]
        public async Task<RoleSelectDto> Create(RoleDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(_mapper);
            await _repository.Entities.AddAsync(model, cancellationToken);

            var role = await _repository.TableNoTracking.ProjectTo<RoleSelectDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(roleSelectDto => roleSelectDto.Id == model.Id, cancellationToken);

            return role;
        }

        [HttpPut]
        public async Task<RoleSelectDto> Update(int id, RoleDto dto, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            model = dto.ToEntity(_mapper, model);
            await _repository.UpdateAsync(model, cancellationToken);

            var role = await _repository.TableNoTracking.ProjectTo<RoleSelectDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(roleSelectDto => roleSelectDto.Id == model.Id, cancellationToken);

            return role;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            await _repository.DeleteAsync(model, cancellationToken);
            return Ok();
        }
    }
}