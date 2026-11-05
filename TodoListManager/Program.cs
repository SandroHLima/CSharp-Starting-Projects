var manager = new TodoManager();

while (true)
{
    Console.WriteLine("Todo List Manager");
    Console.WriteLine("1. Add Task");
    Console.WriteLine("2. Edit Task");
    Console.WriteLine("3. Remove Task");
    Console.WriteLine("4. List Tasks");
    Console.WriteLine("5. Complete Task");
    Console.WriteLine("6. Pin/Unpin Task");
    Console.WriteLine("7. Exit");
    Console.Write("Select an option: ");

    var choice = Console.ReadLine();
    if (choice is null) return;

    switch (choice)
    {
        case "1":
            Console.Write("Enter task title: ");
            var title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Task title cannot be empty.");
                break;
            }

            Console.Write("Enter task description (optional): ");
            var description = Console.ReadLine();
            Console.Write("Enter task priority (Low, Medium, High, Critical): ");
            var priorityInput = Console.ReadLine();
            if (!Enum.TryParse(priorityInput, true, out Priority priority) || !Enum.IsDefined(priority))
            {
                Console.WriteLine("Invalid priority. Task was not added.");
                break;
            }

            var newTask = new TodoItem
            {
                Title = title,
                Description = description,
                Priority = priority
            };
            manager.AddTask(newTask);
            break;

        case "2":
            if (!TryReadTaskId("edit", out var editId)) break;
            var taskToEdit = manager.FindTask(editId);
            if (taskToEdit is null)
            {
                Console.WriteLine("Task not found.");
                break;
            }

            Console.Write($"Enter new title (leave blank to keep '{taskToEdit.Title}'): ");
            var newTitle = Console.ReadLine();
            Console.Write($"Enter new description (leave blank to keep current): ");
            var newDescription = Console.ReadLine();
            Console.Write($"Enter new priority (Low, Medium, High, Critical; current: {taskToEdit.Priority}): ");
            var newPriorityInput = Console.ReadLine();
            var newPriority = taskToEdit.Priority;
            if (!string.IsNullOrWhiteSpace(newPriorityInput)
                && (!Enum.TryParse(newPriorityInput, true, out newPriority) || !Enum.IsDefined(newPriority)))
            {
                Console.WriteLine("Invalid priority. Task was not updated.");
                break;
            }

            manager.UpdateTask(
                editId,
                string.IsNullOrWhiteSpace(newTitle) ? taskToEdit.Title : newTitle,
                string.IsNullOrWhiteSpace(newDescription) ? taskToEdit.Description : newDescription,
                newPriority);
            break;

        case "3":
            if (!TryReadTaskId("remove", out var removeId)) break;
            if (!manager.RemoveTask(removeId)) Console.WriteLine("Task not found.");
            break;

        case "4":
            manager.ListTasks();
            break;

        case "5":
            if (!TryReadTaskId("complete", out var completeId)) break;
            if (!manager.Complete(completeId)) Console.WriteLine("Task not found.");
            break;

        case "6":
            if (!TryReadTaskId("pin or unpin", out var pinId)) break;
            if (!manager.TogglePin(pinId)) Console.WriteLine("Task not found.");
            break;

        case "7":
            return;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

bool TryReadTaskId(string action, out Guid taskId)
{
    Console.Write($"Enter task ID to {action}: ");
    if (Guid.TryParse(Console.ReadLine(), out taskId))
    {
        return true;
    }

    Console.WriteLine("Invalid ID format.");
    return false;
}