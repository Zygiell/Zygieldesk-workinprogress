using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.Categories.Commands.CreateCategory
{
    public class CreatedCategoryCommand : IRequest<CreatedCategoryCommandResponse>
    {

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
