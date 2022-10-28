namespace RobotCleaner.Client.Tests;

public class CommandReaderTests
{
    private readonly Mock<IConsoleIo> _consoleIo;
    private readonly ConsoleCommandReader _sut;


    public CommandReaderTests()
    {
        _consoleIo = new Mock<IConsoleIo>();
        _sut = new ConsoleCommandReader(_consoleIo.Object);
    }

    [Fact]
    public void CommandReader_ReadNoOfCommands_ReturnsCorrectNumberOfCommands()
    {
        _consoleIo.Setup(x => x.ReadLine()).Returns("10");

        //Act
        _sut.ReadAndAddNoOfCommands();

        //Assert
        var result = _sut.RobotCleanerCommand;
        result.Should().NotBeNull();

        result.NumberOfCommands.Should().Be(10);
    }

    [Fact]
    public void CommandReader_ReadCoordinates_ReturnsCorrectCoordinates()
    {
        _consoleIo.Setup(x => x.ReadLine()).Returns("10 22");

        //Act
        _sut.ReadAndAddCoordinate();

        //Assert
        var result = _sut.RobotCleanerCommand;
        result.Should().NotBeNull();

        var coordinate = result.Coordinate;
        coordinate.Should().NotBeNull();

        coordinate?.XPostion.Should().Be(10);
        coordinate?.YPosition.Should().Be(22);
    }

    [Fact]
    public void CommandReader_ReadMoveCommands_ReturnsCorrectMoveCommands()
    {
        _consoleIo.Setup(x => x.ReadLine()).Returns("N 22");

        //Act
        _sut.ReadAndAddMoveCommand();

        //Assert
        var result = _sut.RobotCleanerCommand.MoveCommands[0];
        result.Direction.Should().Be(Direction.N);
        result.NumberOfMoveSteps.Should().Be(22);
    }

    [Fact]
    public void CommandReader_ReadRobotCleanerCommand_ReturnsRobotCleanerCommand()
    {
        _consoleIo.SetupSequence(x => x.ReadLine()).Returns("4")
            .Returns("333 555")
            .Returns("N 100")
            .Returns("E 1000")
            .Returns("S 500")
            .Returns("W 10");
        var result = _sut.ReadRobotCleanerCommand();
        result.Should().NotBeNull();
        result.NumberOfCommands.Should().Be(4);
        result.Coordinate?.XPostion.Should().Be(333);
        result.Coordinate?.YPosition.Should().Be(555);
        result.MoveCommands.Count.Should().Be(4);
        result.MoveCommands[0].Direction.Should().Be(Direction.N);
        result.MoveCommands[0].NumberOfMoveSteps.Should().Be(100);
    }

    [Fact]
    public void CommandReader_AddMinumumNoOfCommands_MoveCommandsAreCorrect()
    {
        _consoleIo.Setup(x => x.ReadLine()).Returns(Constants.MinimumCommands); //mimimum command is 0

        //Act
        _sut.ReadAndAddNoOfCommands();
        var result = _sut.RobotCleanerCommand;
        result.Should().NotBeNull();

        var moveCommands = result.MoveCommands.Count;

        result.NumberOfCommands.Should().Be(moveCommands);
    }

    [Fact]
    public void CommandReader_AddNooOfCommandsAndMoveCommands_NumberOfCommandsAndMoveCommandsShouldBeSame()
    {
        _consoleIo.SetupSequence(x => x.ReadLine()).Returns("2")
            .Returns("333 555")
            .Returns("E 888")
            .Returns("W 555");

        //Act
        var result = _sut.ReadRobotCleanerCommand();
        result.NumberOfCommands.Should().Be(result.MoveCommands.Count);
    }

    [Fact]
    public void CommandReader_AddStepsMoreThanMaximumSteps_ShouldReturnDefaultMaximumSteps()
    {
        _consoleIo.SetupSequence(x => x.ReadLine()).Returns("1")
            .Returns("333 555")
            .Returns("E 500000"); //max step should be 99999
        //Act
        var result = _sut.ReadRobotCleanerCommand();
        result.MoveCommands[0].NumberOfMoveSteps.Should().Be(Constants.MaximumSteps);
    }

    [Fact]
    public void CommandReader_AddStepsLessThanMinimumSteps_ShouldReturnDefaultMinimumSteps()
    {
        _consoleIo.SetupSequence(x => x.ReadLine()).Returns("1")
            .Returns("333 555")
            .Returns("E -2222"); //mimimum steps should be 1

        //Act
        var result = _sut.ReadRobotCleanerCommand();

        result.MoveCommands[0].NumberOfMoveSteps.Should().Be(Constants.MinimumSteps);
    }
}