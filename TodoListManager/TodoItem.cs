public enum Priority
{
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}

public class TodoItem
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public bool IsPinned { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    public bool IsCompleted => CompletedAt.HasValue;
    public string Status => IsCompleted ? "Completed" : "Pending";
}
