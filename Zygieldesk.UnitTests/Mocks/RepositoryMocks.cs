using Zygieldesk.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;

namespace Zygieldesk.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<ICategoryRepository> GetCategoryRepository()
        {
            var categories = GetCategories();
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(c => c.GetAllAsync()).ReturnsAsync(categories);

            return mockCategoryRepository;
        }

        private static List<Category> GetCategories()
        {
            var categoryList = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Account",
                    Description = "Account problems"
                    
                },
                new Category()
                {
                    Id = 2,
                    Name = "Payment",
                    Description = "Payment problems"

                },
                new Category()
                {
                    Id = 3,
                    Name = "Technical",
                    Description = "Technical problems"

                }

            };

            return categoryList;
        }
    }
}
