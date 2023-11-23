using Grpc.Core;
using SpiritGrpcServer.Models;

namespace SpiritGrpcServer.Services
{
    public class UserService : UserActions.UserActionsBase
    {
        private readonly ILogger<UserService> _logger;
        private static List<UserModel> users = new List<UserModel>
            {
                new UserModel
                {
                    Id = "kjhjklh",
                    Name = "First Name",
                    Email = "firstname@email.com",
                    Age = 35
                },
                new UserModel
                {
                    Id = "adfaewa",
                    Name = "2 Name",
                    Email = "secondname@email.com",
                    Age = 33
                },
                new UserModel
                {
                    Id = "kkkeadx",
                    Name = "3 Name",
                    Email = "thirdname@email.com",
                    Age = 35
                },
                new UserModel
                {
                    Id = "eiwofasd",
                    Name = "4 Name",
                    Email = "fourthname@email.com",
                    Age = 45
                },
                new UserModel
                {
                    Id = "dakfdksd",
                    Name = "5 Name",
                    Email = "lastname@email.com",
                    Age = 34
                },
            };

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public override Task<UserResModel> GetUser(UserReqModel request, ServerCallContext context)
        {
            string requestEmail = request.Email != null? request.Email : "";
            
            UserModel user = users.FirstOrDefault((user) => user.Email == requestEmail);
            if (user == null)
            {
                return Task.FromResult(new UserResModel());
            }

            return Task.FromResult(new UserResModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Age = user.Age,
            });
        }

        public override async Task GetAllUsers(AllUsers request, IServerStreamWriter<UserResModel> responseStream, ServerCallContext context)
        {
            var res = users;
            foreach (var user in res)
            {
                var sendUser = new UserResModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Age = user.Age
                };
                await Task.Delay(1000);
                await responseStream.WriteAsync(sendUser);
            }
        }
    }
}
