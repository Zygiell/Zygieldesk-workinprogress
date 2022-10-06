using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Authentication;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Account.Commands.AddUser;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.Account.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public LoginUserCommandHandler(IAccountRepository accountRepository, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new LoginUserCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (!validatorResult.IsValid)
            {
                return new LoginUserCommandResponse(validatorResult);
            }

            var user = await _accountRepository.GetUserByEmail(request.Email);
            if(user == null)
            {
                return new LoginUserCommandResponse("Invalid username or password", false);
            }

           var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                return new LoginUserCommandResponse("Invalid username or password", false);
            }
            var claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, $"{user.Email}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer,
                claims, expires: expires, signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();

            return new LoginUserCommandResponse(tokenHandler.WriteToken(token));
        }
    }
}
