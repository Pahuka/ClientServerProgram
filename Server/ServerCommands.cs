using System.Net;
using System.Net.Sockets;

namespace Server;

public class ServerCommands
{
	private static readonly TcpListener listener = new(IPAddress.Any, 5050);
	private static TcpClient client;

	public void Run()
	{
		Console.WriteLine("Server start");
		listener.Start();

		while (true)
		{
			client = listener.AcceptTcpClient();

			Task.Factory.StartNew(() =>
			{
				var sr = new StreamReader(client.GetStream());
				var sw = new StreamWriter(client.GetStream());
				sw.AutoFlush = true;
				var serverCommandType = typeof(ServerCommands);

				while (client.Connected)
					try
					{
						var line = sr.ReadLine();

						Console.WriteLine(line);

						var serverMethod = serverCommandType.GetMethod(line);
						if (serverMethod != null)
						{
							sw.WriteLine($"Сервер выполняет метод {line}");
							line = (string)serverMethod.Invoke(this, null);
						}

						if(line.Equals("Connect"))
							sw.WriteLine("Connected");
						else
							sw.WriteLine($"Сервер получил сообщение {line}");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
			});
		}
	}

	public void Stop()
	{
		client.Close();
		Console.WriteLine("Server stopped");
	}
}