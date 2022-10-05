using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Persistance.Seeder
{
    public class ZygieldeskSeeder
    {
        private readonly ZygieldeskDbContext _dbContext;

        public ZygieldeskSeeder(ZygieldeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if(await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Categories.Any())
                {
                    var categories = GetCategories();
                    await _dbContext.Categories.AddRangeAsync(categories);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    await _dbContext.Roles.AddRangeAsync(roles);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {                    
                    Name = "User"
                },
                new Role()
                {                    
                    Name = "Support"
                },
                new Role()
                {                    
                    Name = "Admin"
                }
            };
            return roles;
        }
        private IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Account",
                    Description = "Issues with account",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Seeder",
                    Tickets = new List<Ticket>()
                    {
                        new Ticket()
                        {
                            TicketTitle = "Dummy ticket",
                            TicketBody = "Lorem ipsum dolor dummy ticket body",
                            Status = TicketStatus.Open,
                            CreatedDate= DateTime.Now,
                            CreatedBy = "Seeder",
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body2",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body3",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                }

                            }
                        },
                        new Ticket()
                        {
                            TicketTitle = "Dummy ticket2",
                            TicketBody = "Lorem ipsum dolor dummy ticket body2",
                            Status = TicketStatus.Open,
                            CreatedDate= DateTime.Now,
                            CreatedBy = "Seeder",
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body2",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body3",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                }

                            }
                        }
                    }
                },
                new Category()
                {
                    Name = "Payments",
                    Description = "Issues with payments",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Seeder",
                    Tickets = new List<Ticket>()
                    {
                        new Ticket()
                        {
                            TicketTitle = "Dummy ticket",
                            TicketBody = "Lorem ipsum dolor dummy ticket body",
                            Status = TicketStatus.Open,
                            CreatedDate= DateTime.Now,
                            CreatedBy = "Seeder",
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body2",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body3",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                }

                            }
                        },
                        new Ticket()
                        {
                            TicketTitle = "Dummy ticket2",
                            TicketBody = "Lorem ipsum dolor dummy ticket body2",
                            Status = TicketStatus.Open,
                            CreatedDate= DateTime.Now,
                            CreatedBy = "Seeder",
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body2",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body3",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                }

                            }
                        }
                    }
                },
                new Category()
                {
                    Name = "Other",
                    Description = "Other issues",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Seeder",
                    Tickets = new List<Ticket>()
                    {
                        new Ticket()
                        {
                            TicketTitle = "Dummy ticket",
                            TicketBody = "Lorem ipsum dolor dummy ticket body",
                            Status = TicketStatus.Open,
                            CreatedDate= DateTime.Now,
                            CreatedBy = "Seeder",
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body2",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body3",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                }

                            }
                        },
                        new Ticket()
                        {
                            TicketTitle = "Dummy ticket2",
                            TicketBody = "Lorem ipsum dolor dummy ticket body2",
                            Status = TicketStatus.Open,
                            CreatedDate= DateTime.Now,
                            CreatedBy = "Seeder",
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body2",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body3",
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "Seeder"
                                }

                            }
                        }
                    }
                }
            };


            return categories;
        }
    }
}
