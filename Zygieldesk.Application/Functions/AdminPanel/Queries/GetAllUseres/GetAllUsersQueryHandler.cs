using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Account.Queries.GetUserByEmail;
using Zygieldesk.Application.Services;

namespace Zygieldesk.Application.Functions.AdminPanel.Queries.GetAllUseres
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public GetAllUsersQueryHandler(IMapper mapper, IAccountRepository accountRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var userList = await _accountRepository.GetAllUsersWithRoles();

            return _mapper.Map<List<UserViewModel>>(userList);
        }
    }
}