using MediatR;
using Microsoft.AspNetCore.Identity;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.EventBus.Bus;
using MissionPossible.Application.Features.Admin.Commands;
using MissionPossible.Application.Repository;
using MissionPossible.Domain.Entities.Auth;
using MissionPossible.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Admin.Handlers
{
    public class AdminCommandHandlers : 
        IRequestHandler<AssignNewUserCommand, Result>,
        IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IDomainBus _domainBus;

        public AdminCommandHandlers(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IDomainBus domainBus)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _domainBus = domainBus;
        }

        public async Task<Result> Handle(AssignNewUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(Guid.NewGuid(), request.Email,request.FullName, request.UserType);
            var tempPassword = user.GenerateRandomCode();
            user.SetPassword(tempPassword, _passwordHasher);
            await _userRepository.AddAsync(user);
            await _domainBus.RaiseEvent(new NewAccountWithTempPasswordCreated
            {
                Email = user.Email,
                Password = tempPassword
            });
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.UserId);
            user.SetDelete(true);
            await _userRepository.UpdateAsync(user);
            return Result.Success();
        }
    }
}
