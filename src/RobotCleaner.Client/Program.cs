var serviceCollection = new ServiceCollection()
    .AddLogging()
    .AddClientDepedency()
    .AddApplicationDependency();


IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
var commandReader = serviceProvider.GetService<IConsoleCommandReader>();
var mediatR = serviceProvider.GetService<IMediator>();
var consoleIo = serviceProvider.GetService<IConsoleIo>();


if (commandReader != null && mediatR != null && consoleIo != null)
{
    var robotCleaner = new RobotCleaner.Client.RobotCleaner(commandReader, consoleIo, mediatR);
    await robotCleaner.Clean();
}

//Console.ReadLine();
if (serviceProvider is IDisposable disposable) disposable.Dispose();