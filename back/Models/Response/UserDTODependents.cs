using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models.Response
{
    public class UserDTODependents
    {
        public int Id { get; set; }
        public string CPF { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
    }
}