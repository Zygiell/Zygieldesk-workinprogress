using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
using Zygieldesk.Application.Services;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.Account.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UpdateUserCommandHandler(IMapper mapper, IAccountRepository accountRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService, IPasswordHasher<User> passwordHasher)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
            _passwordHasher = passwordHasher;
        }
        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToEdit = await _accountRepository.GetUserByEmail(request.Email);            
            if (userToEdit == null)
            {
                throw new NotFoundException("Wrong user email");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, userToEdit,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden");
            }

            // Prevent changes from empty fields.
            if (request.FirstName.Length > 0)
            {
                userToEdit.FirstName = request.FirstName;
            }
            if (request.LastName.Length > 0)
            {
                userToEdit.LastName = request.LastName;
            }
            if (request.Password.Length > 0)
            {
                var hashedPassword = _passwordHasher.HashPassword(userToEdit, request.Password);
                userToEdit.PasswordHash = hashedPassword;
            }

            await _accountRepository.UpdateAsync(userToEdit);

            return new UpdateUserCommandResponse();

        }
    }
}
