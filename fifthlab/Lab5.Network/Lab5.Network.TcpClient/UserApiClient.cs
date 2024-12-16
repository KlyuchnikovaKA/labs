using System.Text.Json;
using Lab5.Network.Common;
using Lab5.Network.Common.UserApi;

public class UserApiClient : IUserApi
{
    private readonly NetTcpClient netTcpClient;

    public UserApiClient(NetTcpClient netTcpClient)
    {
        this.netTcpClient = netTcpClient;
    }

    public async Task<bool> AddAsync(User newUser)
    {
        var command = new Command() { 
            Code = (byte)CommandCode.AddUser,
            Arguments = new Dictionary<string, object?>() 
            {
                ["Data"] = newUser
            }
        };

        var result = await netTcpClient.SendAsync(command);

        if (null == result)
        {
            return false;
        }

        var addResult = result.Arguments["Data"]?.ToString();
        bool.TryParse(addResult, out var addResultValue);
        
        
        return addResultValue;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var command = new Command()
        {
            Code = (byte)CommandCode.DeleteUser,
            Arguments = new Dictionary<string, object?>()
            {
                ["Id"] = id
            }
        };

        var result = await netTcpClient.SendAsync(command);

        if (result == null)
        {
            return false;
        }

        var deleteResult = result.Arguments["Data"]?.ToString();
        bool.TryParse(deleteResult, out var deleteResultValue);

        return deleteResultValue;
    }

    public async Task<User[]> GetAllAsync()
    {
        var command = new Command() { 
            Code = (byte)CommandCode.ReadAllUsers
        };

        var result = await netTcpClient.SendAsync(command);

        if (null == result)
        {
            return Array.Empty<User>();
        }

        var usersJson = result.Arguments["Data"]?.ToString();
        return usersJson != null ? 
            JsonSerializer.Deserialize<User[]>(usersJson)! : 
            Array.Empty<User>();
    }

    public async Task<User?> GetAsync(int id)
    {
        var command = new Command() { 
            Code = (byte)CommandCode.ReadUser, 
            Arguments = new Dictionary<string, object?>() {
                ["Id"] = id
            }
        };

        var result = await netTcpClient.SendAsync(command);

        if (null == result)
        {
            return null;
        }

        var userJson = result.Arguments["Data"]?.ToString();
        return userJson != null ? 
            JsonSerializer.Deserialize<User>(userJson) : null;
    }

    public async Task<bool> UpdateAsync(int id, User updateUser)
    {
        var command = new Command()
        {
            Code = (byte)CommandCode.UpdateUser,
            Arguments = new Dictionary<string, object?>()
            {
                ["Id"] = id,
                ["Data"] = updateUser
            }
        };

        var result = await netTcpClient.SendAsync(command);

        if (result == null)
        {
            return false;
        }

        var updateResult = result.Arguments["Data"]?.ToString();
        bool.TryParse(updateResult, out var updateResultValue);

        return updateResultValue;
    }


}
