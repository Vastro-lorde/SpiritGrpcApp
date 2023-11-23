// See https://aka.ms/new-console-template for more information


using Grpc.Core;
using Grpc.Net.Client;
using SpiritGrpcServer;


var input = new HelloRequest { Name = "Seun Daniel Omatsola" };
var userDetail = new UserReqModel { Email = "firstname@email.com" };
var channel = GrpcChannel.ForAddress("https://localhost:7242");
var client = new Greeter.GreeterClient(channel);

var userClient = new UserActions.UserActionsClient(channel);

var user = await userClient.GetUserAsync(userDetail);
var reply = await client.SayHelloAsync(input);

Console.WriteLine(reply.Message);
Console.WriteLine(user.Id);
Console.WriteLine(user.Name);
Console.WriteLine(user.Email);
Console.WriteLine(user.Age);
var uClient = userClient.GetAllUsers(new AllUsers { });
    while (await uClient.ResponseStream.MoveNext())
    {
        var newUser = uClient.ResponseStream.Current;
        Console.WriteLine($"Id: {newUser.Id}\nName: {newUser.Name}\nEmail: {newUser.Email}\nAge: {newUser.Age}\n\n");
    }
Console.ReadLine();