using MissionPossible.Application.Common.Exceptions;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Repository;
using MissionPossible.Domain.Entities.Auth;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Admin.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id);
            if (user == null)
                throw new NotFoundException();

            if(request.Email != user.Email)
            {
                user.SetSecondaryEmail(user.Email);
            }
            var updatedUser = _mapper.Map(request , user);
            await _userRepository.UpdateAsync(updatedUser);
            return Result.Success();
        }
    }
}
