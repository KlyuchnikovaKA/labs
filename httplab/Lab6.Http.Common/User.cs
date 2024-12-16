namespace Lab6.Http.Common;

public class TaskItem
{
    public TaskItem()
    {
    }

    public TaskItem(int id, string title, string duration,string name)
    {
        Id = id;
        Title = title;
        Duration = duration;
        Name = name;
    }

    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Duration { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

}
