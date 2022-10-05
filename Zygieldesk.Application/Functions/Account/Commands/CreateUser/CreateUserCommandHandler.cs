using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.Account.Commands.AddUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public CreateUserCommandHandler(IMapper mapper, IAccountRepository accountRepository, IPasswordHasher<User> passwordHasher) 
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new CreateUserCommandResponse(validatorResult);
            }

            var isEmailFree = await _accountRepository.IsEmailAddressFree(request.Email);
            if (!isEmailFree)
            {
                return new CreateUserCommandResponse($"Email address is already taken", false);
            }

            var userRole = await _accountRepository.GetUserRoleId();
            var newUser = new User()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                RoleId = userRole

            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, request.Password);
            newUser.PasswordHash = hashedPassword;
            newUser = await _accountRepository.AddAsync(newUser);
            return new CreateUserCommandResponse($"Account successfully registered");
        }
    }
}
