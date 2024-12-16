using System.Net.Http.Json;
using Lab6.Http.Common;

public class TaskApiClient : ITaskApi
{
    private readonly HttpClient httpClient;

    public TaskApiClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<bool> AddAsync(TaskItem newTask)
    {
        var response = await httpClient.PostAsJsonAsync("Task", newTask);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"Task/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<TaskItem[]> GetAllAsync()
    {
        var results = await httpClient.GetFromJsonAsync<TaskItem[]>("Task");
        return results?.ToArray() ?? Array.Empty<TaskItem>();
    }

    public async Task<TaskItem?> GetAsync(int id)
    {
        try
        {
            var result = await httpClient.GetFromJsonAsync<TaskItem?>($"Task/{id}");
            return result;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateAsync(int id, TaskItem updateTask)
    {
        var response = await httpClient.PutAsJsonAsync($"Task/{id}", updateTask);
        return response.IsSuccessStatusCode;
    }
}
