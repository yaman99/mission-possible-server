using AutoMapper;
using MediatR;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.Admin.Dtos;
using MissionPossible.Application.Features.Admin.Queries;
using MissionPossible.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Admin.Handlers
{
    public class AdminQueryHandlers : IRequestHandler<GetAssignedUsersQuery, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AdminQueryHandlers(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetAssignedUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<GetAllUsersDto>>(users);
            return Result.Success(dto);
        }
    }
}
