using MissionPossible.Application.Common.Models;
using MissionPossible.Application.EventBus.Bus;
using MissionPossible.Application.Repository;
using MissionPossible.Domain.Constants;
using MissionPossible.Domain.Entities.Auth;
using MissionPossible.Domain.Events;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Admin.AddNewAdmin
{
    public class AddNewAdminCommandHandler : IRequestHandler<AddNewAdminCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDomainBus _bus;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AddNewAdminCommandHandler(IUserRepository userRepository, IDomainBus bus, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _bus = bus;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> Handle(AddNewAdminCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var password = Guid.NewGuid().ToString("N").Substring(0, 8);
            var user = new User(id, request.Email, UserTypes.Admin);
            user.SetPassword(password, _passwordHasher);
            await _userRepository.AddAsync(user);
            await _bus.RaiseEvent(new NewAccountWithTempPasswordCreated
            {
                Email = user.Email,
                Password = password
            });
            return Result.Success(id);
        }
    }
}
