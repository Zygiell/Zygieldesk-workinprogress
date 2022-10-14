using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.AdminPanel.Commands.UpdateUserDetails
{
    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand, UpdateUserDetailsCommandResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UpdateUserDetailsCommandHandler(IAccountRepository accountRepository, IPasswordHasher<User> passwordHasher)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<UpdateUserDetailsCommandResponse> Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _accountRepository.GetByIdAsync(request.UserId);
            if (userToUpdate == null)
            {
                throw new NotFoundException($"User with {request.UserId} id does not exist.");
            }

            // Prevent changes from empty fields.
            if (request.FirstName.Length > 0)
            {
                userToUpdate.FirstName = request.FirstName;
            }
            if (request.LastName.Length > 0)
            {
                userToUpdate.LastName = request.LastName;
            }
            if (request.Password.Length > 0)
            {
                var hashedPassword = _passwordHasher.HashPassword(userToUpdate, request.Password);
                userToUpdate.PasswordHash = hashedPassword;
            }

            await _accountRepository.UpdateAsync(userToUpdate);

            return new UpdateUserDetailsCommandResponse();
        }
    }
}
