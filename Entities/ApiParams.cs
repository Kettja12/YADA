using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public record ParametersIn(string Token, string Service, string Data);
    public record Response(string Status, string Data);
    public enum ResponseStatus
    {
        OK,
        FAIL
    }
}
