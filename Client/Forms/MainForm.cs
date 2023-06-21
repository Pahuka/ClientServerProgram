using Client.Services;

namespace Client.Forms;

public partial class MainForm : Form
{
	private ClientService? _clientService;

	public MainForm()
	{
		InitializeComponent();
		Send.Enabled = false;
	}

	private async void Connect_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(Host.Text) || string.IsNullOrEmpty(Port.Text))
		{
			MessageBox.Show("Адрес сервера или порта пустые", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		_clientService = new ClientService(Host.Text, int.Parse(Port.Text));
		ServerMessageBox.Text += await _clientService.ConnectCommand() + Environment.NewLine;

		if (_clientService.IsConnected)
			Send.Enabled = true;
	}

	private async void SendMessage_Click(object sender, EventArgs e)
	{
		//if (_clientService == null)
		//{
		//	MessageBox.Show("Адрес сервера или порта пустые", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	return;
		//}

		ServerMessageBox.Text += await _clientService.Send(OutMessageBox.Text) + Environment.NewLine;
	}
}