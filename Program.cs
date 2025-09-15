using System;

class Calculator
{
    private double currentVal = 0;
    private double memoryVal = 0;
    private string currentOperation = "";

    public void ProcessDigit(string input)
    {
        try
        {
            int digit = Convert.ToInt32(input);
            currentVal = digit;
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка! Нужно ввести цифру");
        }
    }

    public void ProcessOperation(string operation)
    {
        try
        {
            double previousVal = currentVal;

            switch (operation)
            {
                case "+":
                    currentVal = currentVal + GetNextNumber();
                    break;

                case "-":
                    currentVal = currentVal - GetNextNumber();
                    break;

                case "*":
                    currentVal = currentVal * GetNextNumber();
                    break;

                case "/":
                    double divisor = GetNextNumber();
                    if (divisor == 0)
                        throw new InvalidOperationException("Делитель не может быть нулевым");
                    currentVal = currentVal / divisor;
                    break;

                case "%":
                    double modDivisor = GetNextNumber();
                    if (modDivisor == 0)
                        throw new InvalidOperationException("Остаток нельзя найти деля на ноль");
                    currentVal = currentVal % modDivisor;
                    break;

                case "1/x":
                    if (currentVal == 0)
                        throw new InvalidOperationException("Делитель не может быть нулевым");
                    currentVal = 1 / currentVal;
                    break;

                case "x^2":
                    currentVal = currentVal * currentVal;
                    break;

                case "sqrt":
                    if (currentVal < 0)
                        throw new InvalidOperationException("Нельзя найти корень из отрицательного числа");
                    currentVal = Math.Sqrt(currentVal);
                    break;

                case "M+":
                    memoryVal += currentVal;
                    break;

                case "M-":
                    memoryVal -= currentVal;
                    break;

                case "MR":
                    currentVal = memoryVal;
                    break;

                default:
                    throw new InvalidOperationException("Неправильная операция");
            }

            currentOperation = operation;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
        }
    }

    private double GetNextNumber()
    {
        Console.Write("Введите следующее число: ");
        string input = Console.ReadLine();
        
        if (double.TryParse(input, out double number))
        {
            return number;
        }
        else
        {
            throw new InvalidOperationException("Нужно ввести число");
        }
    }

    public double GetCurrentVal()
    {
        return currentVal;
    }

    public double GetMemoryVal()
    {
        return memoryVal;
    }

    public string GetCurrentOperation()
    {
        return currentOperation;
    }
}

class Program
{
    static void Main()
    {
        Calculator calc = new Calculator();
        Console.WriteLine("Доступные операции: +, -, *, /, %, 1/x, x^2, sqrt, M+, M-, MR");
        Console.WriteLine("Введите 'exit' для выхода");
        
        while (true)
        {
            Console.WriteLine($"\nТекущее значение: {calc.GetCurrentVal()}");  
            Console.WriteLine($"Значение в памяти: {calc.GetMemoryVal()}");
            
            Console.Write("Введите цифру или операцию: ");
            string input = Console.ReadLine();
            
            if (input.ToLower() == "exit")
                break;
                
            if (int.TryParse(input, out int digit))
            {
                calc.ProcessDigit(input);
            }
            else
            {
                calc.ProcessOperation(input);
            }
        }
        Console.WriteLine("Калькулятор закрыт");
    }
}