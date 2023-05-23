using MissionPossible.Application.Common.Exceptions;
using MissionPossible.Application.Features.Admin.GetAllUsers.Dtos;
using MissionPossible.Application.Repository;
using MissionPossible.Domain.Constants;
using MissionPossible.Shared.Exceptions;
using MissionPossible.Shared.Types;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Admin.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PagedResult<GetAllUsersDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<GetAllUsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            if(request.UserType != UserTypes.Admin && request.UserType != UserTypes.Advertiser && request.UserType != UserTypes.Promoter)
            {
                throw new DomainException("INVALID_USER_TYPE");
            }
            var result = await _userRepository.GetAllPagedAsync(request.UserType, request.Pagination);
            var dto = _mapper.Map<IEnumerable<GetAllUsersDto>>(result.Items);
            return PagedResult<GetAllUsersDto>.From(result , dto);
        }
    }
}
