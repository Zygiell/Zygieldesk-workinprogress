using MediatR;
using Microsoft.AspNetCore.Authorization;
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

namespace Zygieldesk.Application.Functions.Account.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserCommandResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public DeleteUserCommandHandler(IAccountRepository accountRepository, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _accountRepository = accountRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _accountRepository.GetUserByEmail(request.Email);
            if(userToDelete == null)
            {
                throw new NotFoundException("Invalid user email");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, userToDelete,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden");
            }

            await _accountRepository.DeleteAsync(userToDelete);
            return new DeleteUserCommandResponse();
        }
    }
}
