using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;

namespace Zygieldesk.Application.Functions.AdminPanel.Commands.ChangeUserRole
{
    public class ChangeUserRoleCommandHandler : IRequestHandler<ChangeUserRoleCommand, ChangeUserRoleCommandResponse>
    {
        private readonly IAccountRepository _accountRepository;

        public ChangeUserRoleCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<ChangeUserRoleCommandResponse> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userToChangeRole = await _accountRepository.GetByIdAsync(request.UserId);
            if(userToChangeRole == null)
            {
                throw new NotFoundException($"User with {request.UserId} id does not exist");
            }
            var role = await _accountRepository.GetRoleById(request.RoleId);
            if(role== null)
            {
                var existingRoles = await _accountRepository.GetAllRoles();
                var roleInfo = new List<string>();
                foreach(var existingRole in existingRoles)
                {
                    var roleString = $"Role: {existingRole.Name} Role ID: {existingRole.Id} \n";
                    roleInfo.Add(roleString);
                }
                throw new NotFoundException($"Role with {request.RoleId} id does not exist \n Existing roles:\n{string.Join("\n", roleInfo)}");
            }

            userToChangeRole.RoleId = request.RoleId;
            await _accountRepository.UpdateAsync(userToChangeRole);

            return new ChangeUserRoleCommandResponse("Role updated successfully");
            
        }
    }
}
