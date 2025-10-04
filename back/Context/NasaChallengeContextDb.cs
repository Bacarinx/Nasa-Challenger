using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace back.Context
{
    public class NasaChallengeContextDb : DbContext
    {
        public NasaChallengeContextDb(DbContextOptions<NasaChallengeContextDb> opt) : base(opt) { }

        public required DbSet<User> User { get; set; }
        public required DbSet<Dependents> Dependent { get; set; }
        public required DbSet<RespiratoryDiseases> RespiratoryDisease { get; set; }
        public required DbSet<UserDiseases> UserDisease { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dependents>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .HasForeignKey(p => p.Depends_on)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDiseases>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDiseases>()
                .HasOne(p => p.Disease)
                .WithMany()
                .HasForeignKey(p => p.DiseaseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RespiratoryDiseases>().HasData(
            new RespiratoryDiseases { Id = 1, Disease = "Asthma" },
            new RespiratoryDiseases { Id = 2, Disease = "Chronic Obstructive Pulmonary Disease (COPD)" },
            new RespiratoryDiseases { Id = 3, Disease = "Chronic Bronchitis" },
            new RespiratoryDiseases { Id = 4, Disease = "Pulmonary Emphysema" },
            new RespiratoryDiseases { Id = 5, Disease = "Allergic Rhinitis" },
            new RespiratoryDiseases { Id = 6, Disease = "Sinusitis" },
            new RespiratoryDiseases { Id = 7, Disease = "Laryngitis" },
            new RespiratoryDiseases { Id = 8, Disease = "Pharyngitis" },
            new RespiratoryDiseases { Id = 9, Disease = "Acute Bronchitis" },
            new RespiratoryDiseases { Id = 10, Disease = "Pneumonia" }

        );
        }
    }
}