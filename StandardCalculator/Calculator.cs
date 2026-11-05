public class Calculator
{
    public double Calculate(double a, double b, string operation)
    {
        return operation switch
        {
            "+" => a + b,
            "-" => a - b,
            "*" => a * b,
            "/" => b == 0 
                    ? throw new DivideByZeroException("Cannot divide by zero.") 
                    : a / b,
            _ => throw new ArgumentException($"Unsupported operation: {operation}")
        };
    }
}