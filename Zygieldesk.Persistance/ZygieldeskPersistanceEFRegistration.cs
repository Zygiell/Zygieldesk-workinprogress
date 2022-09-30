﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Persistance.Repositories;

namespace Zygieldesk.Persistance
{
    public static class ZygieldeskPersistanceEFRegistration
    {
        public static IServiceCollection AddZygieldeskPersistanceEFServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ZygieldeskDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ZygieldeskConnectionString")));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketCommentRepository, TicketCommentRepository>();

            return services;
        }
    }
}