using System;

public interface ICalculator
{
    double Add(double a, double b);
}

public interface ILogger
{
    void LogError(string message);
    void LogEvent(string message);
}

public class Calculator : ICalculator
{
    private readonly ILogger _logger;
    public Calculator(ILogger logger)
    {
        _logger = logger;
    }

    public double Add(double a, double b)
    {
        _logger.LogEvent("Выполняется сложение...");
        double sum = a + b;
        _logger.LogEvent($"Результат сложения: {sum}");
        return sum;
    }
}

public class ConsoleLogger : ILogger
{
    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public void LogEvent(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Введите первое число:");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите второе число:");
            double b = Convert.ToDouble(Console.ReadLine());

            ILogger logger = new ConsoleLogger();
            ICalculator calculator = new Calculator(logger);
            double sum = calculator.Add(a, b);

            Console.WriteLine("Сумма = " + sum);
        }
        catch (FormatException)
        {
            ILogger logger = new ConsoleLogger();
            logger.LogError("Ошибка ввода числа!");
        }

        catch (Exception ex)
        {
            ILogger logger = new ConsoleLogger();
            logger.LogError("Произошла ошибка: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}