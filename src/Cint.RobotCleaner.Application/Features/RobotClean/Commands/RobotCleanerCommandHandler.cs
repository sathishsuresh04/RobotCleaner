namespace Cint.RobotCleaner.Application.Features.RobotClean.Commands;

public class RobotCleanerCommandHandler : IRequestHandler<RobotCleanerCommand, int>
{
    private readonly Dictionary<Coordinate, bool> _cleanOffices;
    private Coordinate _coordinate;

    public RobotCleanerCommandHandler()
    {
        _cleanOffices = new Dictionary<Coordinate, bool>();
    }

    private Coordinate CurrentPosition
    {
        set
        {
            _coordinate = value;
            CleanCurrentPosition();
        }
    }

    public async Task<int> Handle(RobotCleanerCommand request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        if (request.Coordinate != null) CurrentPosition = request.Coordinate;
        foreach (var moveCommand in request.MoveCommands)
            for (var i = 0; i < moveCommand.NumberOfMoveSteps; i++)
                await MoveRobot(moveCommand);

        return _cleanOffices.Count;
    }

    private void CleanCurrentPosition()
    {
        _cleanOffices[_coordinate] = true;
    }

    private async Task MoveRobot(MoveCommand moveCommand)
    {
        await Task.Run(() => CurrentPosition = moveCommand.Direction switch
        {
            Direction.E => _coordinate with {XPostion = _coordinate.XPostion + 1},
            Direction.W => _coordinate with {XPostion = _coordinate.XPostion - 1},
            Direction.S => _coordinate with {YPosition = _coordinate.YPosition - 1},
            Direction.N => _coordinate with {YPosition = _coordinate.YPosition + 1},
            _ => _coordinate
        });
    }
}