using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
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
            var isEmailFree = await _accountRepository.IsEmailAddressFree(request.Email);
            if (!isEmailFree)
            {
                throw new BadRequestException($"Email address is already taken");
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
            await _accountRepository.AddAsync(newUser);

            return new CreateUserCommandResponse($"Account successfully registered", true);
        }
    }
}