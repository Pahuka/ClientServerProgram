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

			var sr = new StreamReader(client.GetStream());
			var sw = new StreamWriter(client.GetStream());
			sw.AutoFlush = true;
			var serverCommandType = typeof(ServerCommands);

			while (client.Connected)
				try
				{
					var line = sr.ReadLine();

					if (line == null)
					{
						client.Close();
						break;
					}

					Console.WriteLine(line);

					if (line.Contains("*"))
					{
						var methodParts = line.Split('*');
						var serverMethod = serverCommandType.GetMethod(methodParts[0]);
						line = (string)serverMethod.Invoke(this, methodParts.Skip(1)?.ToArray());
						sw.WriteLine(line);
						continue;
					}

					if (line.Equals("Connect"))
						sw.WriteLine("Connected");
					else
						sw.WriteLine($"Сервер получил сообщение {line}");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					client.Close();
				}
		}
	}

	public void Stop()
	{
		listener.Stop();
		Console.WriteLine("Server stopped");
	}

	public string CreateFile(string filePath, string fileText)
	{
		using (var file = File.CreateText(Path.GetFullPath(filePath)))
		{
			file.WriteAsync(fileText);
		}

		return "Файл создан";
	}
}