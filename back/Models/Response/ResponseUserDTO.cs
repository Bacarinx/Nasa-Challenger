using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models.Response
{
    public class ResponseUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
        public string State { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Street { get; set; } = String.Empty;
        public string Number { get; set; } = String.Empty;
        public string Neighborhood { get; set; } = String.Empty;
    }
}