using Microsoft.AspNetCore.Authorization;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Services;

namespace Zygieldesk.UnitTests.Mocks
{
    public class ServiceMocks
    {
        public static Mock<IAuthorizationService> GetAuthorizationService()
        {
            var mockAuthorizationService = new Mock<IAuthorizationService>();
            mockAuthorizationService.Setup(service => service.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<object>(), 
                It.IsAny<IEnumerable<IAuthorizationRequirement>>())).ReturnsAsync(AuthorizationResult.Success());

            return mockAuthorizationService;
        }

        public static Mock<IUserContextService> GetUserContextService()
        {
            var mockUserContextService = new Mock<IUserContextService>();       
            

            return mockUserContextService;
        }
    }
}
