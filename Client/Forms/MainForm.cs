using System.ComponentModel;
using Client.Services;

namespace Client.Forms;

public partial class MainForm : Form
{
	private readonly ClientService? _clientService;

	public MainForm()
	{
		InitializeComponent();
		_clientService = new ClientService(Host.Text, int.Parse(Port.Text));
		Send.Enabled = false;
	}

	private async void Connect_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(Host.Text) || string.IsNullOrEmpty(Port.Text))
		{
			MessageBox.Show("Адрес сервера или порта пустые", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		_clientService.Host = Host.Text;
		_clientService.Port = int.Parse(Port.Text);

		ServerMessageBox.Text += await _clientService.Connect() + Environment.NewLine;

		Send.Enabled = _clientService.IsConnected;
	}

	private async void SendMessage_Click(object sender, EventArgs e)
	{
		ServerMessageBox.Text +=
			await _clientService.Send("CreateFile" + "*" + FilePathBox.Text + "*" + OutMessageBox.Text) +
			Environment.NewLine;
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		ServerMessageBox.Text += _clientService.Disconnect() + Environment.NewLine;
	}
}