using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.Responses
{
    public enum ResponseStatus
    {
        Succes = 0,
        NotFound = 1,
        BadQueryRequest = 2,
        ValidationError = 3,
        Exception = 4,
        DataBaseError = 5,
        OtherClientApiError = 6
    }
}
