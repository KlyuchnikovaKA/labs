// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using Lab5.Network.Common;
using Lab5.Network.Common.UserApi;

public class ApiTcpServer : TcpServerBase
{
    private readonly IUserApi userApi;

    public ApiTcpServer(IUserApi userApi, Uri listAddress)
        : base(listAddress)
    {
        this.userApi = userApi;
    }

    protected override async Task<Command> ProcessCommandAsync(Command command)
    {
        var commandCode = (CommandCode)command!.Code;
        Console.WriteLine($"+ command: {commandCode}");
        switch (commandCode) 
        {
            case CommandCode.AddUser:
                var userData = command.Arguments["Data"]?.ToString() ?? "{}";
                var addUser = JsonSerializer.Deserialize<User>(userData);
                var addResult = await userApi.AddAsync(addUser!);
                return new Command() 
                { 
                    Code = (byte)CommandCode.AddUser, 
                    Arguments = new Dictionary<string, object?>() 
                    {
                        ["Data"] = addResult 
                    }
                };
            case CommandCode.ReadUser:
                var id = command.Arguments["Id"]?.ToString() ?? "1";
                var userId = Convert.ToInt32(id);
                var user = await userApi.GetAsync(userId);
                return new Command() 
                { 
                    Code = (byte)CommandCode.ReadUser, 
                    Arguments = new Dictionary<string, object?>() 
                    {
                        ["Data"] = user
                    }
                };
            case CommandCode.ReadAllUsers:
                var users = await userApi.GetAllAsync();
                return new Command() 
                { 
                    Code = (byte)CommandCode.ReadUser, 
                    Arguments = new Dictionary<string, object?>() 
                    {
                        ["Data"] = users
                    }
                };
                case CommandCode.DeleteUser:
                    var deleteId = Convert.ToInt32(command.Arguments["Id"]?.ToString());
                    var deleteResult = await userApi.DeleteAsync(deleteId);
                    return new Command()
                    {
                        Code = (byte)CommandCode.DeleteUser,
                        Arguments = new Dictionary<string, object?>()
                        {
                            ["Data"] = deleteResult
                        }
                    };
                case CommandCode.UpdateUser:
                    var updateId = Convert.ToInt32(command.Arguments["Id"]?.ToString());
                    var updateUserData = command.Arguments["Data"]?.ToString() ?? "{}";
                    var updateUser = JsonSerializer.Deserialize<User>(updateUserData);
                    var updateResult = await userApi.UpdateAsync(updateId, updateUser!);
                    return new Command()
                    {
                        Code = (byte)CommandCode.UpdateUser,
                        Arguments = new Dictionary<string, object?>()
                        {
                            ["Data"] = updateResult
                        }
                    };
            default:
                return new Command()
                {
                    Code = 0,
                    Arguments = new Dictionary<string, object?>()
                    {
                        ["Error"] = $"error code {command!.Code}"
                    }
                }; 
        }
    }
}