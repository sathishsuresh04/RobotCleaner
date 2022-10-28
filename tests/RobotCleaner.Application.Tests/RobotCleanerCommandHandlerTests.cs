namespace RobotCleaner.Application.Tests;

public class RobotCleanerCommandHandlerTests
{
    private readonly RobotCleanerCommandHandler _sut;

    public RobotCleanerCommandHandlerTests()
    {
        _sut = new RobotCleanerCommandHandler();
    }

    [Fact]
    public async Task RobotCleanerCommandHandler_CheckResponse_ResponseShouldNotBeNull()
    {
        var robotCommand = new RobotCleanerCommand
        {
            NumberOfCommands = 1,
            Coordinate = new Coordinate(10, 22)
        };
        robotCommand.MoveCommands.Add(new MoveCommand(Direction.E, 10));
        //act
        var response = await _sut.Handle(robotCommand, CancellationToken.None);
        //assert
        response.Should().NotBe(null);
    }

    [Fact]
    public async Task RobotCleanerCommandHandler_ExecuteClean_Returns_1001()
    {
        //Arrange
        var robotCleanerCommand = new RobotCleanerCommand
        {
            NumberOfCommands = 1,
            Coordinate = new Coordinate(-2000, 2000)
        };
        robotCleanerCommand.MoveCommands.Add(new MoveCommand(Direction.N, 1000));

        //Act
        var result = await _sut.Handle(robotCleanerCommand, CancellationToken.None);
        result.Should().Be(1001);
    }

    [Fact]
    public async Task RobotCleanerCommandHandler_ExecuteClean_Returns_200000()
    {
        //Arrange
        var robotCleanerCommand = new RobotCleanerCommand
        {
            NumberOfCommands = 4,
            Coordinate = new Coordinate(-100000, 100000)
        };
        robotCleanerCommand.MoveCommands.Add(new MoveCommand(Direction.E, 50000));
        robotCleanerCommand.MoveCommands.Add(new MoveCommand(Direction.S, 50000));
        robotCleanerCommand.MoveCommands.Add(new MoveCommand(Direction.W, 50000));
        robotCleanerCommand.MoveCommands.Add(new MoveCommand(Direction.N, 50000));

        //Act
        var result = await _sut.Handle(robotCleanerCommand, CancellationToken.None);
        result.Should().Be(200000);
    }

    [Fact]
    public async Task RobotCleanerCommandHandler_ExecuteClean_Returns_2001()
    {
        //Arrange
        var robotCleanerCommand = new RobotCleanerCommand
        {
            NumberOfCommands = 2,
            Coordinate = new Coordinate(-10, 10)
        };
        robotCleanerCommand.MoveCommands.Add(new MoveCommand(Direction.S, 2000));
        robotCleanerCommand.MoveCommands.Add(new MoveCommand(Direction.N, 2000));
        //Act
        var result = await _sut.Handle(robotCleanerCommand, CancellationToken.None);
        result.Should().Be(2001);
    }
}