using System.ComponentModel;
using Client.Services;

namespace Client.Forms;

public partial class MainForm : Form
{
	private readonly ClientService? _clientService;

	public MainForm()
	{
		InitializeComponent();
		_clientService = new ClientService(Host.Text, int.Parse(Port.Text), ServerMessageBox);
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

		Send.Enabled = await _clientService.Connect();
	}

	private void SendMessage_Click(object sender, EventArgs e)
	{
		_clientService.Send("CreateFile" + "*" + FilePathBox.Text + "*" + OutMessageBox.Text);
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		_clientService.Disconnect();
	}
}