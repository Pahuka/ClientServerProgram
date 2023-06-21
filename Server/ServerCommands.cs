using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace Server;

public class ServerCommands
{
	private static readonly TcpListener _listener = new(IPAddress.Any, 5050);
	private static TcpClient _client;

	public void Run()
	{
		Console.WriteLine("Сервер запущен");
		_listener.Start();

		while (true)
		{
			_client = _listener.AcceptTcpClient();

			var sr = new StreamReader(_client.GetStream());
			var sw = new StreamWriter(_client.GetStream());
			sw.AutoFlush = true;
			var serverCommandType = typeof(ServerCommands);

			while (_client.Connected)
				try
				{
					var line = sr.ReadLine();

					if (line == null)
					{
						_client.Close();
						break;
					}

					if (line.Contains("*"))
					{
						var methodParts = line.Split('*');
						var serverMethod = serverCommandType.GetMethod(methodParts[0]);
						line = (string)serverMethod.Invoke(this, methodParts.Skip(1)?.ToArray());
						sw.WriteLine(line);
						Console.WriteLine($"Создан файл {methodParts[1]}");
						continue;
					}

					if (line.Equals("Connect"))
					{
						sw.WriteLine($"Подключен к {_client.Client.LocalEndPoint}");
						Console.WriteLine($"{_client.Client.RemoteEndPoint} подключился к серверу");
						continue;
					}
					else
						sw.WriteLine($"Сервер получил сообщение {line}");

					Console.WriteLine(line);
				}
				catch (TargetInvocationException e)
				{
					sw.WriteLine(e.InnerException);
					Console.WriteLine(e.InnerException);
					continue;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					_client.Close();
				}
		}
	}

	public void Stop()
	{
		_listener.Stop();
		Console.WriteLine("Сервер остановлен");
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