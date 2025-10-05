using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back.Context;
using back.Models.Entities;
using Microsoft.EntityFrameworkCore;
using OrderSolution.API.Services.LoggedUser;

namespace back.UseCases
{
    public class UserDiseasesUseCase
    {
        private readonly NasaChallengeContextDb _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly User User;

        public UserDiseasesUseCase(NasaChallengeContextDb context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            var loggedUser = new LoggedUser(_httpContext);
            User = loggedUser.GetUser(_context);
        }

        public async Task<List<RespiratoryDiseases>> GetRespiratoryDiseases()
        {
            List<RespiratoryDiseases> diseases = [];

            var diseasesId = await _context.UserDisease.Where(u => u.UserId == User.Id).OrderBy(u => u.Id).ToListAsync();
            foreach (var d in diseasesId)
            {
                diseases.Add(_context.RespiratoryDisease.FirstOrDefault(a => a.Id == d.DiseaseId)!);
            }
            
            return diseases;
        }
    }
}