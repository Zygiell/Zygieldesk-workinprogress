﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById
{
    public class TicketCommentViewModel
    {
        public int Id { get; set; }
        public string CommentBody { get; set; }
        public int TicketId { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastModifiedByUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
