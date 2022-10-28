namespace RobotCleaner.Client.Implementations;

public class ConsoleCommandReader : IConsoleCommandReader
{
    private readonly IConsoleIo _consoleIo;
    public readonly RobotCleanerCommand RobotCleanerCommand;

    public ConsoleCommandReader(IConsoleIo consoleIo)
    {
        _consoleIo = consoleIo;
        RobotCleanerCommand = new RobotCleanerCommand();
    }

    public RobotCleanerCommand ReadRobotCleanerCommand()
    {
        ReadAndAddNoOfCommands();
        ReadAndAddCoordinate();
        while (RobotCleanerCommand.MoveCommands.Count < RobotCleanerCommand.NumberOfCommands) ReadAndAddMoveCommand();
        return RobotCleanerCommand;
    }

    public void ReadAndAddNoOfCommands()
    {
        var value = _consoleIo.ReadLine();
        if (value != null) AddNumberOfCommands(value);
    }

    public void ReadAndAddCoordinate()
    {
        var coordinates = _consoleIo.ReadLine();
        if (coordinates != null) AddCoordinates(coordinates);
    }

    public void ReadAndAddMoveCommand()
    {
        var directionAndStep = _consoleIo.ReadLine();
        if (directionAndStep == null) return;

        var directionAndSteps = directionAndStep.Split(null);
        if (directionAndSteps.Length <= 1) return;

        var direction = GetDirection(directionAndSteps[0]);
        var numberOfSteps = IntParse(directionAndSteps[1]);

        //TODO Assemption:  if number of steps less than 1 and it will be set to 1
        if (numberOfSteps > Constants.MaximumSteps) numberOfSteps = Constants.MaximumSteps;

        if (numberOfSteps < Constants.MinimumSteps) numberOfSteps = Constants.MinimumSteps;
        var moveCommand = new MoveCommand(direction, numberOfSteps);

        RobotCleanerCommand.MoveCommands.Add(moveCommand);
    }

    private void AddNumberOfCommands(string numberOfCommands)
    {
        RobotCleanerCommand.NumberOfCommands = IntParse(numberOfCommands);

        //TODO Assemption:  if the number of commands are below 0, then the number of commands will be set to 0 and if the number of commands are above 10000
        //then it will be sent as 10000
        if (RobotCleanerCommand.NumberOfCommands < IntParse(Constants.MinimumCommands))
            RobotCleanerCommand.NumberOfCommands = IntParse(Constants.MinimumCommands);
        if (RobotCleanerCommand.NumberOfCommands > IntParse(Constants.MaximumCommands))
            RobotCleanerCommand.NumberOfCommands = IntParse(Constants.MaximumCommands);
    }

    private void AddCoordinates(string coordinate)
    {
        var coordinates = coordinate.Split(null);
        if (coordinates.Length > 1)
            RobotCleanerCommand.Coordinate = new Coordinate(IntParse(coordinates[0]), IntParse(coordinates[1]));
    }

    private static Direction GetDirection(string direction)
    {
        return direction switch
        {
            "E" => Direction.E,
            "W" => Direction.W,
            "S" => Direction.S,
            "N" => Direction.N,
            // _ => Direction.U
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    private static int IntParse(string value)
    {
        return int.Parse(value);
    }
}