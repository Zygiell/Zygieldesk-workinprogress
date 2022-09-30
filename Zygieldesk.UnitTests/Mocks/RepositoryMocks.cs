using Zygieldesk.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryWithTickets;

namespace Zygieldesk.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<ITicketRepository> GetTicketRepository()
        {
            var tickets = GetTickets();

            var mockTicketRepository = new Mock<ITicketRepository>();

            mockTicketRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tickets);

            return mockTicketRepository;
        }
        public static Mock<ICategoryRepository> GetCategoryRepository()
        {
            var categories = GetCategories();
            var tickets = GetTickets();
            var mockCategoryRepository = new Mock<ICategoryRepository>();


            mockCategoryRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Category>())).Callback
                <Category>((entity) => 
                { 
                    var categoryToBeUpdated = categories.FirstOrDefault(c => c.Id == entity.Id);
                    var updatedCategory = entity;
                    categories.Remove(categoryToBeUpdated);
                    categories.Add(updatedCategory);
                });

            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var cat = categories.FirstOrDefault(c => c.Id == id);
                    return cat;
                });
            mockCategoryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories);

            mockCategoryRepository.Setup(repo => repo.GetCategoryWithTickets(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var cat = categories.FirstOrDefault(c => c.Id == id);
                    cat.Tickets = tickets.Where(t => t.CategoryId == cat.Id).ToList();
                    return cat;
                });

            mockCategoryRepository.Setup(repo => repo.AddAsync(It.IsAny<Category>())).ReturnsAsync((Category category) =>
            {
                categories.Add(category);
                return category;
            });

            mockCategoryRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Category>()))
                .Callback<Category>((entity) => categories.Remove(entity));


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
                    Description = "Account problems",
                    Tickets = new List<Ticket>()
                    {
                        new Ticket()
                        {
                            Id=1,
                            TicketTitle = "I cant log in!",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help!",
                            CreatedDate = DateTime.Now,
                            CategoryId = 1,
                            Status = TicketStatus.Open

                        },
                        new Ticket()
                        {
                            Id=2,
                            TicketTitle = "I cant log in again!",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help! again",
                            CreatedDate = DateTime.Now,
                            CategoryId = 1,
                            Status = TicketStatus.Open
                        },
                        new Ticket()
                        {
                            Id=3,
                            TicketTitle = "I cant log in again 3!",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help! again 3",
                            CreatedDate = DateTime.Now,
                            CategoryId = 1,
                            Status = TicketStatus.Open
                        }
                    }

                },
                new Category()
                {
                    Id = 2,
                    Name = "Payment",
                    Description = "Payment problems",
                    Tickets = new List<Ticket>()
                    {
                        new Ticket()
                        {
                            Id=4,
                            TicketTitle = "I cant log in!4",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help!4",
                            CreatedDate = DateTime.Now,
                            CategoryId = 2,
                            Status = TicketStatus.Open

                        },
                        new Ticket()
                        {
                            Id=5,
                            TicketTitle = "I cant log in again!",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help! again",
                            CreatedDate = DateTime.Now,
                            CategoryId = 2,
                            Status = TicketStatus.Open
                        },
                        new Ticket()
                        {
                            Id=6,
                            TicketTitle = "I cant log in again 3!",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help! again 3",
                            CreatedDate = DateTime.Now,
                            CategoryId = 2,
                            Status = TicketStatus.Open
                        }

                    }
                },
                new Category()
                {
                    Id = 3,
                    Name = "Technical",
                    Description = "Technical problems",
                    Tickets = new List<Ticket>()
                    {
                        new Ticket()
                        {
                            Id=7,
                            TicketTitle = "I cant log in!4",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help!4",
                            CreatedDate = DateTime.Now,
                            CategoryId = 3,
                            Status = TicketStatus.Open

                        },
                        new Ticket()
                        {
                            Id=8,
                            TicketTitle = "I cant log in again!",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help! again",
                            CreatedDate = DateTime.Now,
                            CategoryId = 3,
                            Status = TicketStatus.Open
                        },
                        new Ticket()
                        {
                            Id=9,
                            TicketTitle = "I cant log in again 3!",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help! again 3",
                            CreatedDate = DateTime.Now,
                            CategoryId = 3,
                            Status = TicketStatus.Open
                        }

                    }

                }

            };

            return categoryList;
        }

        private static List<Ticket> GetTickets()
        {
            var categories = GetCategories();
            var tickets = new List<Ticket>();
            foreach (var category in categories)
            {
                if (category.Tickets.Any())
                {
                    foreach (var ticket in category.Tickets)
                    {
                        tickets.Add(ticket);
                    }
                }

            }
            return tickets;
        }
    }
}
