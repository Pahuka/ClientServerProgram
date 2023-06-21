namespace Client.Forms
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Connect = new Button();
			OutMessageBox = new TextBox();
			Port = new TextBox();
			ServerMessageBox = new TextBox();
			Send = new Button();
			Host = new TextBox();
			FilePathBox = new TextBox();
			SuspendLayout();
			// 
			// Connect
			// 
			Connect.Location = new Point(12, 152);
			Connect.Name = "Connect";
			Connect.Size = new Size(90, 25);
			Connect.TabIndex = 0;
			Connect.Text = "Соединение";
			Connect.UseVisualStyleBackColor = true;
			Connect.Click += Connect_Click;
			// 
			// OutMessageBox
			// 
			OutMessageBox.Location = new Point(12, 47);
			OutMessageBox.Multiline = true;
			OutMessageBox.Name = "OutMessageBox";
			OutMessageBox.ScrollBars = ScrollBars.Both;
			OutMessageBox.Size = new Size(460, 84);
			OutMessageBox.TabIndex = 1;
			// 
			// Port
			// 
			Port.Location = new Point(261, 153);
			Port.Name = "Port";
			Port.Size = new Size(69, 23);
			Port.TabIndex = 3;
			Port.Text = "5050";
			// 
			// ServerMessageBox
			// 
			ServerMessageBox.Location = new Point(12, 196);
			ServerMessageBox.Multiline = true;
			ServerMessageBox.Name = "ServerMessageBox";
			ServerMessageBox.ReadOnly = true;
			ServerMessageBox.ScrollBars = ScrollBars.Both;
			ServerMessageBox.Size = new Size(460, 57);
			ServerMessageBox.TabIndex = 4;
			// 
			// Send
			// 
			Send.Location = new Point(382, 152);
			Send.Name = "Send";
			Send.Size = new Size(90, 25);
			Send.TabIndex = 5;
			Send.Text = "Отправить";
			Send.UseVisualStyleBackColor = true;
			Send.Click += SendMessage_Click;
			// 
			// Host
			// 
			Host.Location = new Point(108, 153);
			Host.Name = "Host";
			Host.Size = new Size(147, 23);
			Host.TabIndex = 6;
			Host.Text = "localhost";
			// 
			// FilePathBox
			// 
			FilePathBox.Location = new Point(12, 12);
			FilePathBox.Name = "FilePathBox";
			FilePathBox.Size = new Size(460, 23);
			FilePathBox.TabIndex = 7;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(484, 261);
			Controls.Add(FilePathBox);
			Controls.Add(Host);
			Controls.Add(Send);
			Controls.Add(ServerMessageBox);
			Controls.Add(Port);
			Controls.Add(OutMessageBox);
			Controls.Add(Connect);
			MaximizeBox = false;
			MaximumSize = new Size(500, 300);
			MinimumSize = new Size(500, 300);
			Name = "MainForm";
			Text = "MainForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button Connect;
		private TextBox OutMessageBox;
		internal TextBox Port;
		private TextBox ServerMessageBox;
		private Button Send;
		private TextBox Host;
		private TextBox FilePathBox;
	}
}