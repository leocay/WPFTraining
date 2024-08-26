using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ResponseService
{
    public record ResponseProblemDetail(
        string type,
        string title,
        int status,
        Dictionary<string, List<string>> errors,
        string traceId
        );
}
