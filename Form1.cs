using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberSecurityChatbot_POE
{
    public partial class Form1 : Form
    {
        // Chatbot object
        private ChatbotEngine? bot;

        // Form controls
        private Label? lblTitle;
        private Label? lblName;
        private TextBox? txtName;
        private Button? btnStart;
        private RichTextBox? rtbChat;
        private TextBox? txtInput;
        private Button? btnSend;

        public Form1()
        {
            InitializeComponent();

            BuildInterface();
        }

        private void BuildInterface()
        {
            // Main form settings
            Text = "Cyber Guardian - Cybersecurity Awareness Bot";

            Size = new Size(900, 680);

            StartPosition = FormStartPosition.CenterScreen;

            BackColor = Color.FromArgb(20, 25, 35);

            FormBorderStyle = FormBorderStyle.FixedSingle;

            MaximizeBox = false;

            // ================= TITLE =================

            lblTitle = new Label();

            lblTitle.Text = "Cyber Guardian";

            lblTitle.ForeColor = Color.Cyan;

            lblTitle.Font = new Font("Segoe UI", 24, FontStyle.Bold);

            lblTitle.Location = new Point(30, 20);

            lblTitle.AutoSize = true;

            Controls.Add(lblTitle);

            // ================= NAME LABEL =================

            lblName = new Label();

            lblName.Text = "Name";

            lblName.ForeColor = Color.White;

            lblName.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            lblName.Location = new Point(35, 82);

            lblName.AutoSize = true;

            Controls.Add(lblName);

            // ================= NAME TEXTBOX =================

            txtName = new TextBox();

            // Position and size
            txtName.Location = new Point(100, 78);

            txtName.Size = new Size(280, 35);

            // Font styling
            txtName.Font = new Font("Segoe UI", 11);

            // Textbox colours
            txtName.BackColor = Color.White;

            txtName.ForeColor = Color.Gray;

            // Border styling
            txtName.BorderStyle = BorderStyle.FixedSingle;

            // Placeholder text
            txtName.Text = "Enter your name...";

            // Remove placeholder when textbox is focused
            txtName.Enter += (s, e) =>
            {
                if (txtName.Text == "Enter your name...")
                {
                    txtName.Text = "";

                    txtName.ForeColor = Color.Black;
                }
            };

            // Restore placeholder if textbox is empty
            txtName.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    txtName.Text = "Enter your name...";

                    txtName.ForeColor = Color.Gray;
                }
            };

            Controls.Add(txtName);

            // ================= START BUTTON =================

            btnStart = new Button();

            btnStart.Text = "Start Chatbot";

            btnStart.Location = new Point(410, 74);

            btnStart.Size = new Size(150, 42);

            // Button styling
            btnStart.BackColor = Color.Gainsboro;

            btnStart.ForeColor = Color.Navy;

            btnStart.FlatStyle = FlatStyle.Flat;

            btnStart.FlatAppearance.BorderSize = 0;

            btnStart.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            btnStart.Cursor = Cursors.Hand;

            btnStart.Click += BtnStart_Click;

            Controls.Add(btnStart);

            // ================= CHAT AREA =================

            rtbChat = new RichTextBox();

            rtbChat.Location = new Point(35, 140);

            rtbChat.Size = new Size(810, 390);

            rtbChat.ReadOnly = true;

            rtbChat.BackColor = Color.FromArgb(10, 15, 25);

            rtbChat.ForeColor = Color.White;

            rtbChat.BorderStyle = BorderStyle.None;

            rtbChat.Font = new Font("Consolas", 10);

            Controls.Add(rtbChat);

            // ================= USER INPUT =================

            txtInput = new TextBox();

            txtInput.Location = new Point(35, 560);

            txtInput.Size = new Size(660, 40);

            txtInput.Font = new Font("Segoe UI", 11);

            txtInput.Enabled = false;

            txtInput.KeyDown += TxtInput_KeyDown;

            Controls.Add(txtInput);

            // ================= SEND BUTTON =================

            btnSend = new Button();

            btnSend.Text = "Send";

            btnSend.Location = new Point(715, 556);

            btnSend.Size = new Size(130, 42);

            // Button styling
            btnSend.BackColor = Color.Gainsboro;

            btnSend.ForeColor = Color.Navy;

            btnSend.FlatStyle = FlatStyle.Flat;

            btnSend.FlatAppearance.BorderSize = 0;

            btnSend.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            btnSend.Cursor = Cursors.Hand;

            btnSend.Enabled = false;

            btnSend.Click += BtnSend_Click;

            Controls.Add(btnSend);
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            string name = txtName!.Text.Trim();

            // Prevent placeholder text from becoming username
            if (string.IsNullOrWhiteSpace(name)
                || name == "Enter your name...")
            {
                name = "Friend";
            }

            // Create chatbot object
            bot = new ChatbotEngine(name);

            rtbChat!.Clear();

            // Display chatbot intro
            AddBotMessage(bot.GetAsciiArt());

            AddBotMessage("Hello " + name +
                          "! Welcome to the Cybersecurity Awareness Bot.");

            AddBotMessage("You can ask me about passwords, phishing, scams, privacy, safe browsing, or 2FA.");

            AddBotMessage("Try saying things like:");
            AddBotMessage("- I am worried about scams");
            AddBotMessage("- I am interested in privacy");
            AddBotMessage("- Tell me more");

            // Play voice greeting
            AudioPlayer.PlayGreeting("Audio.wav");

            txtInput!.Enabled = true;

            btnSend!.Enabled = true;

            txtInput.Focus();
        }

        private void BtnSend_Click(object? sender, EventArgs e)
        {
            ProcessUserInput();
        }

        private void TxtInput_KeyDown(object? sender, KeyEventArgs e)
        {
            // Allow Enter key to send messages
            if (e.KeyCode == Keys.Enter)
            {
                ProcessUserInput();

                e.SuppressKeyPress = true;
            }
        }

        private void ProcessUserInput()
        {
            if (bot == null)
            {
                MessageBox.Show("Please start the chatbot first.");

                return;
            }

            string userInput = txtInput!.Text.Trim();

            // Prevent empty messages
            if (string.IsNullOrWhiteSpace(userInput))
            {
                AddBotMessage("Please type something so I can help you.");

                return;
            }

            // Display user message
            AddUserMessage(userInput);

            // Generate chatbot response
            string response = bot.GetResponse(userInput);

            AddBotMessage(response);

            txtInput.Clear();

            txtInput.Focus();
        }

        private void AddUserMessage(string message)
        {
            // User messages appear in green
            rtbChat!.SelectionColor = Color.LightGreen;

            rtbChat.AppendText("You: "
                               + message
                               + Environment.NewLine
                               + Environment.NewLine);

            rtbChat.SelectionColor = Color.White;
        }

        private void AddBotMessage(string message)
        {
            // Bot messages appear in cyan
            rtbChat!.SelectionColor = Color.Cyan;

            rtbChat.AppendText("Cyber Guardian: "
                               + message
                               + Environment.NewLine
                               + Environment.NewLine);

            rtbChat.SelectionColor = Color.White;

            rtbChat.ScrollToCaret();
        }
    }
}