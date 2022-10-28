namespace Cint.RobotCleaner.Client.Interfaces;

public interface IConsoleCommandReader
{
    void ReadAndAddNoOfCommands();

    void ReadAndAddCoordinate();

    void ReadAndAddMoveCommand();
    RobotCleanerCommand ReadRobotCleanerCommand();
}