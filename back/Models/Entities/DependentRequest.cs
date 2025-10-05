using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models.Entities
{
    public class DependentRequest
    {
        public int Id { get; set; }
        public int RequesterId { get; set; }  // quem fez o pedido
        public int TargetId { get; set; }     // quem deve aceitar
        public bool Accepted { get; set; } = false;
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("RequesterId")]
        public User Requester { get; set; } = null!;

        [ForeignKey("TargetId")]
        public User Target { get; set; } = null!;
    }
}