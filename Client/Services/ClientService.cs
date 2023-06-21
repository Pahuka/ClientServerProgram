using System.Net.Sockets;

namespace Client.Services;

public class ClientService
{
	private TcpClient? _client;
	private StreamReader? _reader;
	private StreamWriter? _writer;

	public ClientService(string host, int port, TextBox serverMessage)
	{
		Host = host;
		Port = port;
		ServerMessage = serverMessage;
	}

	public string Host { get; set; }
	public int Port { get; set; }
	public TextBox ServerMessage { get; set; }

	public async Task<bool> Connect()
	{
		try
		{
			Disconnect();
			_client = new TcpClient();
			await _client.ConnectAsync(Host, Port);
			_reader = new StreamReader(_client.GetStream());
			_writer = new StreamWriter(_client.GetStream());
			Listener();
			_writer.AutoFlush = true;
			_writer.WriteLine("Connect");

			return true;
		}
		catch (Exception e)
		{
			ServerMessage.Text += e.Message + Environment.NewLine;
		}

		return false;
	}

	private void Listener()
	{
		Task.Run(() =>
		{
			while (true)
				try
				{
					if (_client?.Connected == true)
					{
						var line = _reader?.ReadLine();
						if (line != null)
						{
							ServerMessage.Invoke(new Action(() => ServerMessage.Text += line + Environment.NewLine));
						}
						else
						{
							_client.Close();
							ServerMessage.Invoke(new Action(() =>
								ServerMessage.Text += "Получен пустой ответ от сервера" + Environment.NewLine));
						}
					}

					Task.Delay(10).Wait();
				}
				catch (Exception e)
				{
					ServerMessage.Invoke(new Action(() => ServerMessage.Text = e.Message + Environment.NewLine));
				}
		});
	}

	public void Disconnect()
	{
		try
		{
			if (_client?.Connected == true)
				_client.Close();
		}
		catch (Exception e)
		{
			ServerMessage.Text += e.Message + Environment.NewLine;
		}
	}

	public async void Send(string msg)
	{
		try
		{
			if (_client.Connected)
			{
				await _writer?.WriteLineAsync(msg);
				ServerMessage.Text += msg + Environment.NewLine;
			}
		}
		catch (Exception e)
		{
			ServerMessage.Text += e.Message + Environment.NewLine;
		}
	}
}