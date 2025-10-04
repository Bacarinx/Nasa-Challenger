using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models.Request
{
    public class RequestLogin
    {
        public string CPF { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}