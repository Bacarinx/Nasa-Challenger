using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models.Response
{
    public class ResponseDependents
    {
        public int Id { get; set; }
        public UserDTODependents Requester { get; set; }
        public int TargetId { get; set; }
        public bool Accepted { get; set; } = false;
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }
}