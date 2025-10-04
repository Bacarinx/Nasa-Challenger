using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models.Entities
{
    public class Dependents
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Depends_on { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [ForeignKey("Depends_on")]
        public User DependsOn { get; set; } = null!;
    }
}