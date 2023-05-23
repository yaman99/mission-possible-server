using MissionPossible.Application.Common.Interfaces;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.Identity.Dtos;
using MissionPossible.Application.Features.Identity.Queries;
using MissionPossible.Application.Repository;
using MissionPossible.Domain;
using MissionPossible.Domain.Exceptions;
using MissionPossible.Shared.Authentication;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Identity.Handlers
{
    public class IdentityQueryHanlders : IRequestHandler<GetUserQuery, GetUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public IdentityQueryHanlders(IUserRepository userRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId!;
            var user = await _userRepository.GetAsync(Guid.Parse(userId));
            var data = _mapper.Map<GetUserDto>(user);
            return data;
        }
    }
}
