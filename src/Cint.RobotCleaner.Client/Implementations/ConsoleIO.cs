namespace Cint.RobotCleaner.Client.Implementations;

public class ConsoleIo : IConsoleIo
{
    public void WriteLine(string value)
    {
        Console.WriteLine(value);
        Console.ReadLine();
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}