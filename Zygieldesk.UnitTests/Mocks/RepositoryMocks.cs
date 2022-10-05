using Moq;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<ITicketCommentRepository> GetTicketCommentRepository()
        {
            var mockTicketCommentRepository = new Mock<ITicketCommentRepository>();
            var ticketComments = GetTicketComments();
            var tickets = GetTickets();

            mockTicketCommentRepository.Setup(repo => repo.GetAllTicketCommentsFromTicketAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var tic = tickets.FirstOrDefault(c => c.Id == id);
                    return tic.TicketComments.ToList();
                });

            mockTicketCommentRepository.Setup(repo => repo.UpdateAsync(It.IsAny<TicketComment>())).Callback
                <TicketComment>((entity) =>
                {
                    var ticketCommentToBeUpdated = ticketComments.FirstOrDefault(c => c.Id == entity.Id);
                    var updatedTicketComment = entity;
                    ticketComments.Remove(ticketCommentToBeUpdated);
                    ticketComments.Add(updatedTicketComment);
                });

            mockTicketCommentRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var ticCom = ticketComments.FirstOrDefault(c => c.Id == id);
                    return ticCom;
                });

            mockTicketCommentRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(ticketComments);

            mockTicketCommentRepository.Setup(repo => repo.AddAsync(It.IsAny<TicketComment>())).ReturnsAsync((TicketComment ticketComment) =>
                {
                    ticketComments.Add(ticketComment);
                    return ticketComment;
                });

            mockTicketCommentRepository.Setup(repo => repo.DeleteAsync(It.IsAny<TicketComment>()))
                .Callback<TicketComment>((entity) => ticketComments.Remove(entity));




            return mockTicketCommentRepository;
        }
        public static Mock<ITicketRepository> GetTicketRepository()
        {
            var tickets = GetTickets();
            var categories = GetCategories();

            var mockTicketRepository = new Mock<ITicketRepository>();

            mockTicketRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Ticket>())).Callback
                <Ticket>((entity) =>
                {
                    var ticketToBeUpdated = tickets.FirstOrDefault(c => c.Id == entity.Id);
                    var updatedTicket = entity;
                    tickets.Remove(ticketToBeUpdated);
                    tickets.Add(updatedTicket);
                });

            mockTicketRepository.Setup(repo => repo.GetAllTicketsFromCategoryAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var cat = categories.FirstOrDefault(c => c.Id == id);
                    return cat.Tickets.ToList();
                });

            mockTicketRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var tic = tickets.FirstOrDefault(c => c.Id == id);
                    return tic;
                });

            mockTicketRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tickets);

            mockTicketRepository.Setup(repo => repo.AddAsync(It.IsAny<Ticket>())).ReturnsAsync((Ticket ticket) =>
                {
                    tickets.Add(ticket);
                    return ticket;
                });

            mockTicketRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Ticket>()))
                .Callback<Ticket>((entity) => tickets.Remove(entity));

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
                            Status = TicketStatus.Open ,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    Id = 1,
                                    CommentBody = "Test me daddy",
                                    TicketId = 1
                                },
                                new TicketComment()
                                {
                                    Id = 2,
                                    CommentBody = "Test me daddy2",
                                    TicketId= 1
                                },
                                new TicketComment()
                                {
                                    Id = 3,
                                    CommentBody = "Test me daddy3",
                                    TicketId = 1
                                }
                            }
                        },
                        new Ticket()
                        {
                            Id=2,
                            TicketTitle = "I cant log in again!",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help! again",
                            CreatedDate = DateTime.Now,
                            CategoryId = 1,
                            Status = TicketStatus.Open,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    Id = 4,
                                    CommentBody = "Test me daddy",
                                    TicketId = 2
                                },
                                new TicketComment()
                                {
                                    Id = 5,
                                    CommentBody = "Test me daddy2",
                                    TicketId= 2
                                },
                                new TicketComment()
                                {
                                    Id = 6,
                                    CommentBody = "Test me daddy3",
                                    TicketId = 2
                                }
                            }
                        },
                        new Ticket()
                        {
                            Id=3,
                            TicketTitle = "I cant log in again 3!",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help! again 3",
                            CreatedDate = DateTime.Now,
                            CategoryId = 1,
                            Status = TicketStatus.Open,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    Id = 7,
                                    CommentBody = "Test me daddy",
                                    TicketId = 3
                                },
                                new TicketComment()
                                {
                                    Id = 8,
                                    CommentBody = "Test me daddy2",
                                    TicketId= 3
                                },
                                new TicketComment()
                                {
                                    Id = 9,
                                    CommentBody = "Test me daddy3",
                                    TicketId = 3
                                }
                            }
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
                            Status = TicketStatus.Open,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    Id = 10,
                                    CommentBody = "Test me daddy",
                                    TicketId = 4
                                },
                                new TicketComment()
                                {
                                    Id = 11,
                                    CommentBody = "Test me daddy2",
                                    TicketId= 4
                                },
                                new TicketComment()
                                {
                                    Id = 12,
                                    CommentBody = "Test me daddy3",
                                    TicketId = 4
                                }
                            }
                        },
                        new Ticket()
                        {
                            Id=5,
                            TicketTitle = "I cant log in again!",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help! again",
                            CreatedDate = DateTime.Now,
                            CategoryId = 2,
                            Status = TicketStatus.Open,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    Id = 13,
                                    CommentBody = "Test me daddy",
                                    TicketId = 5
                                },
                                new TicketComment()
                                {
                                    Id = 14,
                                    CommentBody = "Test me daddy2",
                                    TicketId= 5
                                },
                                new TicketComment()
                                {
                                    Id = 15,
                                    CommentBody = "Test me daddy3",
                                    TicketId = 5
                                }
                            }
                        },
                        new Ticket()
                        {
                            Id=6,
                            TicketTitle = "I cant log in again 3!",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help! again 3",
                            CreatedDate = DateTime.Now,
                            CategoryId = 2,
                            Status = TicketStatus.Open,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    Id = 16,
                                    CommentBody = "Test me daddy",
                                    TicketId = 6
                                },
                                new TicketComment()
                                {
                                    Id = 17,
                                    CommentBody = "Test me daddy2",
                                    TicketId= 6
                                },
                                new TicketComment()
                                {
                                    Id = 18,
                                    CommentBody = "Test me daddy3",
                                    TicketId = 6
                                }
                            }
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
                            Status = TicketStatus.Open,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    Id = 19,
                                    CommentBody = "Test me daddy",
                                    TicketId = 7
                                },
                                new TicketComment()
                                {
                                    Id = 20,
                                    CommentBody = "Test me daddy2",
                                    TicketId= 7
                                },
                                new TicketComment()
                                {
                                    Id = 21,
                                    CommentBody = "Test me daddy3",
                                    TicketId = 7
                                }
                            }
                        },
                        new Ticket()
                        {
                            Id=8,
                            TicketTitle = "I cant log in again!",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help! again",
                            CreatedDate = DateTime.Now,
                            CategoryId = 3,
                            Status = TicketStatus.Open,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    Id = 22,
                                    CommentBody = "Test me daddy",
                                    TicketId = 8
                                },
                                new TicketComment()
                                {
                                    Id = 23,
                                    CommentBody = "Test me daddy2",
                                    TicketId= 8
                                },
                                new TicketComment()
                                {
                                    Id = 24,
                                    CommentBody = "Test me daddy3",
                                    TicketId = 8
                                }
                            }
                        },
                        new Ticket()
                        {
                            Id=9,
                            TicketTitle = "I cant log in again 3!",
                            TicketBody = "As i said in title i cant log in, i dont know what to do help! again 3",
                            CreatedDate = DateTime.Now,
                            CategoryId = 3,
                            Status = TicketStatus.Open,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    Id = 25,
                                    CommentBody = "Test me daddy",
                                    TicketId = 9
                                },
                                new TicketComment()
                                {
                                    Id = 26,
                                    CommentBody = "Test me daddy2",
                                    TicketId= 9
                                },
                                new TicketComment()
                                {
                                    Id = 27,
                                    CommentBody = "Test me daddy3",
                                    TicketId = 9
                                }
                            }
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

        private static List<TicketComment> GetTicketComments()
        {
            var tickets = GetTickets();
            var ticketComments = new List<TicketComment>();

            foreach(var ticket in tickets)
            {
                if (ticket.TicketComments.Any())
                {
                    foreach(var comment in ticket.TicketComments)
                    {
                        ticketComments.Add(comment);
                    }
                }
            }

            return ticketComments;
        }
    }
}