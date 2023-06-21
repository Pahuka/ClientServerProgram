using System.Net.Sockets;

namespace Client.Services;

public class ClientService
{
	private TcpClient? _client;
	private StreamReader? _reader;
	private StreamWriter? _writer;

	public ClientService(string host, int port)
	{
		Host = host;
		Port = port;
	}

	public string Host { get; set; }
	public int Port { get; set; }
	public bool IsConnected { get; set; }

	//private void Listener()
	//{
	//	Task.Run(() =>
	//	{
	//		while (true)
	//			try
	//			{
	//				if (_client?.Connected == true)
	//				{
	//					var line = _reader?.ReadLine();
	//					if (line != null)
	//					{
	//						_message += line + "\n";
	//					}
	//					else
	//					{
	//						_client.Close();
	//						_message += "Connected error.\n";
	//					}
	//				}

	//				Task.Delay(10).Wait();
	//			}
	//			catch (Exception ex)
	//			{
	//				_message += ex.Message + "\n";
	//			}
	//	});
	//}

	public async Task<string> ConnectCommand()
	{
		try
		{
			_client = new TcpClient();
			await _client.ConnectAsync(Host, Port);
			_reader = new StreamReader(_client.GetStream());
			_writer = new StreamWriter(_client.GetStream());
			//Listener();
			_writer.AutoFlush = true;

			_writer.WriteLine("Connect");
			IsConnected = true;
		}
		catch (SocketException e)
		{
			IsConnected = false;
			return e.Message;
		}

		return await _reader?.ReadLineAsync();
	}

	public async Task<string> Send(string msg)
	{
		try
		{
			if(IsConnected)
				_writer?.WriteLineAsync(msg);
		}
		catch (Exception e)
		{
			return e.Message;
		}

		return await _reader?.ReadLineAsync();
	}
}