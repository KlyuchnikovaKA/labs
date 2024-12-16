namespace Lab6.Http.Common;

public interface ITaskApi
{
    Task<bool> AddAsync(TaskItem newTask);

    Task<bool> DeleteAsync(int id);

    Task<bool> UpdateAsync(int id, TaskItem updateTask);

    Task<TaskItem?> GetAsync(int id);

    Task<TaskItem[]> GetAllAsync();
}
