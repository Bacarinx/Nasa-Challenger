using back.Context;
using back.Models.Entities;
using back.Models.Request;
using back.Models.Response;
using OrderSolution.API.Secutiry.Criptograph;
using OrderSolution.API.Secutiry.Token;

namespace back.UseCases
{
    public class UserUseCase
    {
        public ResponseUserRegisterJson Execute(RequestUserRegisterJson request, NasaChallengeContextDb context)
        {
            var cript = new BCryptCriptograph();

            var newUser = new User
            {
                Name = request.Name,
                Password = cript.Criptographor(request.Password),
                CPF = request.CPF,
                PhoneNumber = request.PhoneNumber,
                BirthDate = request.BirthDate,
                Country = request.Country,
                State = request.State,
                City = request.City,
                Street = request.Street,
                Number = request.Number,
                Neighborhood = request.Neighborhood
            };

            context.User.Add(newUser);
            context.SaveChanges();

            foreach(var i in request.RespiratoryDiesies){
                context.UserDisease.Add(new UserDiseases
                {
                    UserId = newUser.Id,
                    DiseaseId = i
                });
            }
            context.SaveChanges();

            var tokenGenerator = new JwtToken();

            return new ResponseUserRegisterJson
            {
                CPF = request.CPF,
                token = tokenGenerator.Generate(newUser)
            };
        }
         public ResponseUserRegisterJson Login(RequestLogin request, NasaChallengeContextDb context)
        {
            var Bcrypt = new BCryptCriptograph();
            var token = new JwtToken();

            var UserDb = context.User.FirstOrDefault(user => user.CPF == request.CPF);
            if (UserDb == null || !Bcrypt.Verify(request.Password, UserDb))
            {
                throw new Exception("Email ou senha Incorretos!");
            }

            var response = new ResponseUserRegisterJson
            {
                CPF = UserDb.CPF,
                token = token.Generate(UserDb)
            };

            return response;
        }
    }
}