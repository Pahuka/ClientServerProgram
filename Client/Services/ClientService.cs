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

	public async Task<string> Connect()
	{
		try
		{
			Disconnect();
			_client = new TcpClient();
			await _client.ConnectAsync(Host, Port);
			_reader = new StreamReader(_client.GetStream());
			_writer = new StreamWriter(_client.GetStream());
			_writer.AutoFlush = true;

			_writer.WriteLine("Connect");
			IsConnected = true;
		}
		catch (Exception e)
		{
			IsConnected = false;
			return e.Message;
		}

		return await _reader?.ReadLineAsync();
	}

	public string Disconnect()
	{
		try
		{
			if (IsConnected)
				_client.Close();
		}
		catch (Exception e)
		{
			return e.Message;
		}

		return "Disconnect";
	}

	public async Task<string> Send(string msg)
	{
		try
		{
			if (IsConnected)
				_writer?.WriteLineAsync(msg);
		}
		catch (Exception e)
		{
			return e.Message;
		}

		return await _reader?.ReadLineAsync();
	}
}