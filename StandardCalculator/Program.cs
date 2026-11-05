var calculator = new Calculator();

Console.WriteLine("=== Simple Calculator ===");
Console.WriteLine("Supported operations: +  -  *  /");
Console.WriteLine("Type 'exit' at any time to quit.\n");

while (true)
{
    try
    {
        // --- First number ---
        Console.Write("Enter first number: ");
        string? input1 = Console.ReadLine()?.Trim();

        if (string.Equals(input1, "exit", StringComparison.OrdinalIgnoreCase))
            break;

        if (!double.TryParse(input1, out double number1))
        {
            Console.WriteLine("Invalid number. Please try again.\n");
            continue;
        }

        // --- Operator ---
        Console.Write("Enter operator (+, -, *, /): ");
        string? op = Console.ReadLine()?.Trim();

        if (string.Equals(op, "exit", StringComparison.OrdinalIgnoreCase))
            break;

        if (string.IsNullOrWhiteSpace(op) || !" +-*/".Contains(op))
        {
            Console.WriteLine("Invalid operator. Please use +, -, * or /.\n");
            continue;
        }

        // --- Second number ---
        Console.Write("Enter second number: ");
        string? input2 = Console.ReadLine()?.Trim();

        if (string.Equals(input2, "exit", StringComparison.OrdinalIgnoreCase))
            break;

        if (!double.TryParse(input2, out double number2))
        {
            Console.WriteLine("Invalid number. Please try again.\n");
            continue;
        }

        // --- Calculate ---
        double result = calculator.Calculate(number1, number2, op);

        Console.WriteLine($"\nResult: {number1} {op} {number2} = {result}\n");
        Console.WriteLine(new string('-', 40));
    }
    catch (DivideByZeroException ex)
    {
        Console.WriteLine($"\nError: {ex.Message}\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nUnexpected error: {ex.Message}\n");
    }
}

Console.WriteLine("Goodbye!");