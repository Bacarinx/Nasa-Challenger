using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models.Response
{
    public class ResponseUserRegisterJson
    {
        public int id { get; set; }
        public string CPF { get; set; } = String.Empty;
        public string token { get; set; } = String.Empty;
    }
}