namespace Cint.RobotCleaner.Application.Features.RobotClean.Dtos;

public record MoveCommand(Direction Direction, int NumberOfMoveSteps);