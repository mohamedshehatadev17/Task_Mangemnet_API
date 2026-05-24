using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskMangement.Application.Abstractions.Authentication;
using TaskMangement.Application.Features.Auth.Login;
using TaskMangement.Application.Shared;
using TaskMangement.Domain.Models;

namespace TaskMangement.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler: IRequestHandler<LoginCommand, Result<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginCommandHandler(UserManager<User> userManager,IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<string>> Handle(LoginCommand request,CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return Result<string>.Failure("Invalid credentials");

            var isPasswordCorrect =await _userManager.CheckPasswordAsync(user,request.Password);

            if (!isPasswordCorrect)
                return Result<string>.Failure("Invalid credentials");

            return Result<string>.Success(await _jwtTokenGenerator.GenerateTokenAsync(user));
        }
    }
}