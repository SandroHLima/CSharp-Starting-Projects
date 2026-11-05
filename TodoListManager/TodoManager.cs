using System.Text.Json;
using System.Text.Json.Serialization;

public class TodoManager
{
    private readonly List<TodoItem> _tasks = new();
    private readonly string _storagePath;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public TodoManager()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        _storagePath = Path.Combine(appDataPath, "CSharpStarterProjects", "TodoListManager", "tasks.json");
        LoadTasks();
    }

    public void AddTask(TodoItem task)
    {
        ArgumentNullException.ThrowIfNull(task);
        if (string.IsNullOrWhiteSpace(task.Title))
        {
         throw new ArgumentException("Task title cannot be null or whitespace.");
        }

        _tasks.Add(task);
        SaveTasks();
        Console.WriteLine($"Task '{task.Title}' added successfully.");
    }

    public bool RemoveTask(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        
        if (task is null) return false;

        _tasks.Remove(task);
        SaveTasks();
        Console.WriteLine($"Task '{task.Title}' removed successfully.");
        return true;
    }

    public void ListTasks()
    {
        foreach (var task in _tasks.OrderByDescending(t => t.IsPinned)
                     .ThenByDescending(t => t.Priority)
                     .ThenBy(t => t.CreatedAt))
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = GetPriorityColor(task.Priority);
            Console.WriteLine($"ID: {task.Id}");
            Console.WriteLine($"  Title: {task.Title}");
            Console.WriteLine($"  Description: {task.Description ?? "(none)"}");
            Console.WriteLine($"  Priority: {task.Priority} | Status: {task.Status} | Pinned: {(task.IsPinned ? "Yes" : "No")}");
            Console.ForegroundColor = originalColor;
        }

        if (_tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
        }
    }

    public TodoItem? FindTask(Guid id) => _tasks.FirstOrDefault(task => task.Id == id);

    public bool UpdateTask(Guid id, string title, string? description, Priority priority)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Task title cannot be null or whitespace.", nameof(title));
        }

        var task = FindTask(id);
        if (task is null) return false;

        task.Title = title;
        task.Description = description;
        task.Priority = priority;
        SaveTasks();
        Console.WriteLine($"Task '{task.Title}' updated successfully.");
        return true;
    }

    public bool Complete(Guid id)
    {
        var task = FindTask(id);
        if (task is null) return false;

        task.CompletedAt = DateTime.UtcNow;
        SaveTasks();
        Console.WriteLine($"Task '{task.Title}' marked as completed.");
        return true;
    }

    public bool TogglePin(Guid id)
    {
        var task = FindTask(id);
        if (task is null) return false;

        task.IsPinned = !task.IsPinned;
        SaveTasks();
        Console.WriteLine($"Task '{task.Title}' {(task.IsPinned ? "pinned" : "unpinned")}.");
        return true;
    }

    private void LoadTasks()
    {
        if (!File.Exists(_storagePath)) return;

        var json = File.ReadAllText(_storagePath);
        var savedTasks = JsonSerializer.Deserialize<List<TodoItem>>(json, _jsonOptions)
            ?? throw new InvalidDataException($"Todo data in '{_storagePath}' is empty or invalid.");
        _tasks.AddRange(savedTasks);
    }

    private void SaveTasks()
    {
        var directory = Path.GetDirectoryName(_storagePath)
            ?? throw new InvalidOperationException("The todo data folder could not be determined.");
        Directory.CreateDirectory(directory);
        var json = JsonSerializer.Serialize(_tasks, _jsonOptions);
        File.WriteAllText(_storagePath, json);
    }

    private static ConsoleColor GetPriorityColor(Priority priority) => priority switch
    {
        Priority.Low => ConsoleColor.Green,
        Priority.Medium => ConsoleColor.Yellow,
        Priority.High => ConsoleColor.DarkYellow,
        Priority.Critical => ConsoleColor.Red,
        _ => ConsoleColor.Gray
    };
}