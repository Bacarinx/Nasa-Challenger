using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models.Entities
{
    public class UserDiseases
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DiseaseId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [ForeignKey("DiseaseId")]
        public RespiratoryDiseases Disease { get; set; } = null!;
    }
}