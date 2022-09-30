﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Contracts.Persistance
{
    public interface ITicketRepository : IBaseRepository<Ticket>
    {
        Task<IReadOnlyList<Ticket>> GetAllTicketsFromCategoryAsync(int categoryId);

    }
}
