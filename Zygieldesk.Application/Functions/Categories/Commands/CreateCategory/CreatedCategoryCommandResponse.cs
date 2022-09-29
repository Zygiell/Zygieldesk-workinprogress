﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.Categories.Commands.CreateCategory
{
    public class CreatedCategoryCommandResponse : BaseResponse
    {
        public int? CategoryId { get; set; }
        public CreatedCategoryCommandResponse()
        {
        }

        public CreatedCategoryCommandResponse(string message = null) : base(message)
        {
        }

        public CreatedCategoryCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public CreatedCategoryCommandResponse(string message, bool success) : base(message, success)
        {
        }
        public CreatedCategoryCommandResponse(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
