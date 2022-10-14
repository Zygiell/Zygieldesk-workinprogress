using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
using Zygieldesk.Application.Services;

namespace Zygieldesk.Application.Functions.Account.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public GetUserByEmailQueryHandler(IMapper mapper, IAccountRepository accountRepository, IAuthorizationService authorizationService,
            IUserContextService userContextService)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<UserViewModel> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.GetUserByEmail(request.Email);
            if (user == null)
            {
                throw new NotFoundException("Invalid user email");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, user,
                new ResourceOperationRequirement(ResourceOperation.Read)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden");
            }

            return _mapper.Map<UserViewModel>(user);
        }
    }
}