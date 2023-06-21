using Server;

var server = new ServerCommands();
var serverTask = Task.Factory.StartNew(() => server.Run());

var consoleCommand = Task.Factory.StartNew(() =>
{
	Console.WriteLine("Чтобы остановить сервер - наберите 'Exit'");
	while (true)
		if (serverTask.Status == TaskStatus.Running)
		{
			var command = Console.ReadLine();
			if (command.Equals("Exit"))
			{
				server.Stop();
				break;
			}
		}
});

consoleCommand.Wait();