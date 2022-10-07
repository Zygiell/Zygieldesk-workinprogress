using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Persistance.Seeder
{
    public class ZygieldeskSeeder
    {
        private readonly ZygieldeskDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public ZygieldeskSeeder(ZygieldeskDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
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
                if (!_dbContext.Roles.Any(r => r.Name == "Admin"))
                {
                    var roles = GetRoles();
                    var adminRole = roles.FirstOrDefault(r => r.Name == "Admin");
                    await _dbContext.Roles.AddAsync(adminRole);
                    await _dbContext.SaveChangesAsync();
                }
                if (!_dbContext.Users.Any(u => u.Role.Name == "Admin"))
                {
                    if (_dbContext.Users.Any(u => u.Email == "admin@admin.com"))
                    {
                        var brickedAdminAccount = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == "admin@admin.com");
                        _dbContext.Remove(brickedAdminAccount);
                        await _dbContext.SaveChangesAsync();
                    }
                    var adminAccount = await GetAdminAccount();
                    await _dbContext.Users.AddAsync(adminAccount);
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
                    Tickets = new List<Ticket>()
                    {
                        new Ticket()
                        {
                            TicketTitle = "Dummy ticket",
                            TicketBody = "Lorem ipsum dolor dummy ticket body",
                            Status = TicketStatus.Open,
                            CreatedDate= DateTime.Now,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body",
                                    CreatedDate = DateTime.Now
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body2",
                                    CreatedDate = DateTime.Now
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body3",
                                    CreatedDate = DateTime.Now
                                }
                            }
                        },
                        new Ticket()
                        {
                            TicketTitle = "Dummy ticket2",
                            TicketBody = "Lorem ipsum dolor dummy ticket body2",
                            Status = TicketStatus.Open,
                            CreatedDate= DateTime.Now,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body",
                                    CreatedDate = DateTime.Now
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body2",
                                    CreatedDate = DateTime.Now
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body3",
                                    CreatedDate = DateTime.Now
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
                    Tickets = new List<Ticket>()
                    {
                        new Ticket()
                        {
                            TicketTitle = "Dummy ticket",
                            TicketBody = "Lorem ipsum dolor dummy ticket body",
                            Status = TicketStatus.Open,
                            CreatedDate= DateTime.Now,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body",
                                    CreatedDate = DateTime.Now
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body2",
                                    CreatedDate = DateTime.Now
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body3",
                                    CreatedDate = DateTime.Now
                                }
                            }
                        },
                        new Ticket()
                        {
                            TicketTitle = "Dummy ticket2",
                            TicketBody = "Lorem ipsum dolor dummy ticket body2",
                            Status = TicketStatus.Open,
                            CreatedDate= DateTime.Now,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body",
                                    CreatedDate = DateTime.Now
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body2",
                                    CreatedDate = DateTime.Now
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body3",
                                    CreatedDate = DateTime.Now
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
                    Tickets = new List<Ticket>()
                    {
                        new Ticket()
                        {
                            TicketTitle = "Dummy ticket",
                            TicketBody = "Lorem ipsum dolor dummy ticket body",
                            Status = TicketStatus.Open,
                            CreatedDate= DateTime.Now,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body",
                                    CreatedDate = DateTime.Now
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body2",
                                    CreatedDate = DateTime.Now
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body3",
                                    CreatedDate = DateTime.Now
                                }
                            }
                        },
                        new Ticket()
                        {
                            TicketTitle = "Dummy ticket2",
                            TicketBody = "Lorem ipsum dolor dummy ticket body2",
                            Status = TicketStatus.Open,
                            CreatedDate= DateTime.Now,
                            TicketComments = new List<TicketComment>()
                            {
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body",
                                    CreatedDate = DateTime.Now
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body2",
                                    CreatedDate = DateTime.Now
                                },
                                new TicketComment()
                                {
                                    CommentBody = "Dummy body3",
                                    CreatedDate = DateTime.Now
                                }
                            }
                        }
                    }
                }
            };

            return categories;
        }

        private async Task<User> GetAdminAccount()
        {
            var adminRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
            var admin = new User()
            {
                Email = "admin@admin.com",
                RoleId = adminRole.Id,
                FirstName = "",
                LastName = ""
            };
            admin.PasswordHash = _passwordHasher.HashPassword(admin, "admin");

            return admin;
        }
    }
}