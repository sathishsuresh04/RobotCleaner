namespace Cint.RobotCleaner.Application.Features.RobotClean.Commands;

public class RobotCleanerCommand : IRequest<int>
{
    public RobotCleanerCommand()
    {
        MoveCommands = new List<MoveCommand>();
    }

    public Coordinate? Coordinate { get; set; }

    public List<MoveCommand> MoveCommands { get; }

    public int NumberOfCommands { get; set; }
}