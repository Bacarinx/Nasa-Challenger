using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models.Request
{
    public class RequestUserRegisterJson
    {
        public string Name { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string CPF { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }
        public string Country { get; set; } = String.Empty;
        public string State { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Street { get; set; } = String.Empty;
        public string Number { get; set; } = String.Empty;
        public string Neighborhood { get; set; } = String.Empty;
        public List<int> RespiratoryDiesies { get; set; } = [];
    }
}