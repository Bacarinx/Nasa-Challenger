using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back.Context;
using back.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace back.UseCases
{
    public class RespiratoryDiseasesUseCase
    {
        public async Task<List<RespiratoryDiseases>> GetAsync(NasaChallengeContextDb context)
        {
            return await context.RespiratoryDisease
                                .OrderBy(i => i.Id)
                                .ToListAsync();
        }
    }
}