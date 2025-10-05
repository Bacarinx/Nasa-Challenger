using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using back.Context;
using back.Models.Entities;

namespace OrderSolution.API.Services.LoggedUser
{
    public class LoggedUser
    {
        private readonly IHttpContextAccessor _context;

        public LoggedUser(IHttpContextAccessor context)
        {
            _context = context;
        }

        public User GetUser(NasaChallengeContextDb dbContext)
        {
            var jwtToken = _context.HttpContext?.Request.Headers.Authorization.ToString();

            jwtToken = jwtToken![6..].Trim();
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);
            var stringId = token.Claims.First(claim => claim.Type == "id").Value;
            var UserID = Convert.ToInt32(stringId);

            return dbContext.User.First(user => user.Id == UserID);
        }
    }
}