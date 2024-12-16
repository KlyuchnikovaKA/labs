using System.Collections.Concurrent;
using Lab5.Network.Common.UserApi;

internal class UserApi : IUserApi
{
    private static readonly ConcurrentDictionary<int, User> userRepository 
        = new ConcurrentDictionary<int, User>() {
            [1] = new User(1, "Банан",  "Ксения", "Наверное"),
            [2] = new User(2, "Мандарин", "Ольга", "Обожает"),
            [3] = new User(3, "Памело", "Вадим", "Да что такое памело?!"),
        };
    private static int _lastId;

    public UserApi()
    {
        _lastId = userRepository.Count + 1;
    }


    public Task<bool> AddAsync(User newUser)
    {
        newUser = newUser with 
        {
            Id = _lastId
        };

        if (userRepository.ContainsKey(_lastId)) 
        {
            return Task.FromResult(false);
        }

        var result = userRepository.TryAdd(_lastId, newUser);

        if (!result) 
        {
            return Task.FromResult(false);
        }
        Interlocked.Increment(ref _lastId);

        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        if (!userRepository.ContainsKey(id)) 
        {
            return Task.FromResult(false);
        }

        var result = userRepository.Remove(id, out var _);

        return Task.FromResult(result);
    }

    public Task<User[]> GetAllAsync()
    {
        return Task.FromResult(userRepository.Values.ToArray());
    }

    public Task<User?> GetAsync(int id)
    {
        if (!userRepository.ContainsKey(id)) 
        {
            return Task.FromResult(default(User));
        }

        return Task.FromResult<User?>(userRepository[id]);
    }

    public Task<bool> UpdateAsync(int id, User updateUser)
    {
        if (!userRepository.ContainsKey(id)) 
        {
            return Task.FromResult(false);
        }
        

        var result = userRepository.TryUpdate(id, updateUser, userRepository[id]);
        return Task.FromResult(result);
    }
}