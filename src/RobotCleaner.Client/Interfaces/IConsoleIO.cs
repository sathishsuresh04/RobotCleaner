namespace RobotCleaner.Client.Interfaces;

public interface IConsoleIo
{
    void WriteLine(string value);
    string? ReadLine();
}