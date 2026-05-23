using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskMangement.Application.Abstractions.Authentication;
using TaskMangement.Application.Features.Auth.Register;
using TaskMangement.Application.Shared;
using TaskMangement.Domain.Models;

namespace TaskMangement.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler
        : IRequestHandler<RegisterCommand, Result<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterCommandHandler(UserManager<User> userManager,IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<string>> Handle(RegisterCommand request,CancellationToken cancellationToken)
        {
            var validatorResult = await new RegisterCommandValidator().ValidateAsync(request,cancellationToken);
            if (!validatorResult.IsValid)
            {
                return Result<string>.Failure(
                    string.Join(",",
                    validatorResult.Errors.Select(x => x.ErrorMessage)));
            }
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user,request.Password);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(
                    string.Join(",",
                    result.Errors.Select(x => x.Description)));
            }

            return Result<string>.Success(_jwtTokenGenerator.GenerateToken(user));
        }
    }
}