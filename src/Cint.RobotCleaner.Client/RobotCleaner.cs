namespace Cint.RobotCleaner.Client;

public class RobotCleaner
{
    private readonly IConsoleCommandReader _commandReader;
    private readonly IConsoleIo _consoleIo;
    private readonly IMediator _mediator;

    public RobotCleaner(IConsoleCommandReader commandReader, IConsoleIo consoleIo, IMediator mediator)
    {
        _commandReader = commandReader;
        _consoleIo = consoleIo;
        _mediator = mediator;
    }

    public async Task Clean()
    {
        var robotCleanerCommand = _commandReader.ReadRobotCleanerCommand();
        var response = await _mediator.Send(robotCleanerCommand);
        _consoleIo.WriteLine($"=> Cleaned: {response}");
    }
}