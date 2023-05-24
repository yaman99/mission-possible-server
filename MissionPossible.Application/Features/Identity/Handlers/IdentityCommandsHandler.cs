using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Repository;
using MissionPossible.Domain;
using MissionPossible.Domain.Entities.Auth;
using MissionPossible.Domain.Exceptions;
using MissionPossible.Features.Identity.Commands;
using MissionPossible.Application.EventBus.Bus;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using MissionPossible.Domain.Events;
using MissionPossible.Domain.Constants;
using MissionPossible.Application.Features.Identity.Commands;
using AutoMapper;
using MissionPossible.Application.Common.Interfaces;
using MissionPossible.Application.Features.Identity.Dtos;
using MissionPossible.Shared.Authentication;

namespace MissionPossible.Features.Identity.Handlers
{
    public class IdentityCommandsHandler :
        IRequestHandler<UserSignInCommand, JsonWebToken>,
        IRequestHandler<UserSignOutCommand, Result>,
        IRequestHandler<RefreshAccessTokenCommand, Result>,
        IRequestHandler<ChangePasswordCommand, Result>,
        IRequestHandler<ActivateAccountCommand, Result>


    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;
        private readonly IDomainBus _bus;
        private readonly ICurrentUserService _currentUserService;
        public IdentityCommandsHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtHandler jwtHandler, IDomainBus bus, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
            _bus = bus;
            _currentUserService = currentUserService;
        }

        public async Task<JsonWebToken> Handle(UserSignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Email);
            if (user == null || !user.ValidatePassword(request.Password, _passwordHasher))
            {
                throw new IdentityException(Codes.InvalidCredentials);
            }
            if (!user.IsActive)
            {
                throw new IdentityException(Codes.NotActivated, "Your Account Not Acctivated Yey");
            }
            if (user.IsDeleted)
            {
                throw new IdentityException(Codes.NotActivated, "Your Account Deleted ");
            }

            var jwt = _jwtHandler.CreateToken(user.Id.ToString(), user.Email, user.UserType);
            user.GenerateRefreshToken(_passwordHasher);
            jwt.RefreshToken = user.RefreshToken.Token;

            await _userRepository.UpdateAsync(user);
            return jwt;
        }

        public async Task<Result> Handle(UserSignOutCommand request, CancellationToken cancellationToken)
        {
            var tokenPayload = _jwtHandler.GetTokenPayload(request.AccessToken);

            if (tokenPayload == null)
            {
                throw new IdentityException(Codes.InvalideToken);
            }

            tokenPayload.Claims.TryGetValue(JwtRegisteredClaimNames.UniqueName, out var userId);
            var user = await _userRepository.GetAsync(Guid.Parse(userId));

            user.RefreshToken.Revoke();
            await _userRepository.UpdateAsync(user);

            return Result.Success();
        }

        public async Task<Result> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenPayload = _jwtHandler.GetTokenPayload(request.AccessToken);
            if (tokenPayload == null)
            {
                throw new IdentityException(Codes.InvalideToken);
            }
            else if (tokenPayload.Expires > DateTime.UtcNow.ToTimestamp())
            {
                return Result.Success(request.AccessToken);
            }

            tokenPayload.Claims.TryGetValue(JwtRegisteredClaimNames.UniqueName, out var userId);
            var user = await _userRepository.GetAsync(Guid.Parse(userId));

            // Check Refresh Token
            if (user == null)
            {
                throw new IdentityException(Codes.UserNotFound);
            }
            else if (user.RefreshToken.Token != request.RefreshToken)
            {
                throw new IdentityException(Codes.RefreshTokenNotFound);
            }
            else if (user.RefreshToken.Revoked)
            {
                throw new IdentityException(Codes.RefreshTokenAlreadyRevoked);
            }
            else if (user.RefreshToken.Expired)
            {
                throw new IdentityException(Codes.RefreshTokenNotFound);
            }

            var jwt = _jwtHandler.CreateToken(user.Id.ToString(), user.Email, user.UserType);
            user.GenerateRefreshToken(_passwordHasher);
            jwt.RefreshToken = user.RefreshToken.Token;

            await _userRepository.UpdateAsync(user);

            return Result.Success(jwt);
        }


        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId!;
            var user = await _userRepository.GetAsync(Guid.Parse(userId));

            if (user == null || !user.ValidatePassword(request.CurrentPassword, _passwordHasher))
            {
                throw new IdentityException(Codes.InvalidCredentials);
            }

            if (user.ValidatePassword(request.NewPassword, _passwordHasher))
            {
                throw new IdentityException("New Passwrod Same As Old Password");
            }

            user.SetPassword(request.NewPassword, _passwordHasher);

            await _userRepository.UpdateAsync(user);

            return Result.Success();
        }

        public async Task<Result> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByActivation(request.ActivationCode);

            if (user == null)
                throw new IdentityException(Codes.InvalidActivationCode);

            user.ActivateAccount();
            await _userRepository.UpdateAsync(user);

            return Result.Success();

        }


    }
}
