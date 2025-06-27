namespace cyberChatBot_PartTwo
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tableLayoutPanel;
        private RichTextBox rtbChat;
        private TextBox txtInput;
        private Button btnSend;

        private ListBox lstTasks;
        private TextBox txtNewTask;
        private Button btnAddTask;
        private Button btnCompleteTask;
        private Button btnDeleteTask;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            rtbChat = new RichTextBox();
            txtInput = new TextBox();
            btnSend = new Button();
            lstTasks = new ListBox();
            txtNewTask = new TextBox();
            btnAddTask = new Button();
            btnCompleteTask = new Button();
            btnDeleteTask = new Button();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.Controls.Add(rtbChat, 0, 0);
            tableLayoutPanel.Controls.Add(txtInput, 0, 1);
            tableLayoutPanel.Controls.Add(btnSend, 1, 1);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.Size = new Size(981, 623);
            tableLayoutPanel.TabIndex = 0;
            // 
            // rtbChat
            // 
            rtbChat.BackColor = Color.White;
            tableLayoutPanel.SetColumnSpan(rtbChat, 2);
            rtbChat.Dock = DockStyle.Fill;
            rtbChat.Location = new Point(3, 3);
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            rtbChat.Size = new Size(975, 554);
            rtbChat.TabIndex = 0;
            rtbChat.Text = "";
            // 
            // txtInput
            // 
            txtInput.Dock = DockStyle.Fill;
            txtInput.Location = new Point(3, 563);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(827, 31);
            txtInput.TabIndex = 1;
            // 
            // btnSend
            // 
            btnSend.Dock = DockStyle.Fill;
            btnSend.Location = new Point(836, 563);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(142, 57);
            btnSend.TabIndex = 2;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // lstTasks
            // 
            lstTasks.ItemHeight = 25;
            lstTasks.Location = new Point(645, 30);
            lstTasks.Name = "lstTasks";
            lstTasks.Size = new Size(240, 279);
            lstTasks.TabIndex = 0;
            // 
            // txtNewTask
            // 
            txtNewTask.Location = new Point(610, 340);
            txtNewTask.Name = "txtNewTask";
            txtNewTask.Size = new Size(180, 31);
            txtNewTask.TabIndex = 1;
            // 
            // btnAddTask
            // 
            btnAddTask.Location = new Point(800, 340);
            btnAddTask.Name = "btnAddTask";
            btnAddTask.Size = new Size(60, 30);
            btnAddTask.TabIndex = 2;
            btnAddTask.Text = "Add";
            btnAddTask.Click += btnAddTask_Click;
            // 
            // btnCompleteTask
            // 
            btnCompleteTask.Location = new Point(610, 380);
            btnCompleteTask.Name = "btnCompleteTask";
            btnCompleteTask.Size = new Size(120, 30);
            btnCompleteTask.TabIndex = 3;
            btnCompleteTask.Text = "Complete Task";
            btnCompleteTask.Click += btnCompleteTask_Click;
            // 
            // btnDeleteTask
            // 
            btnDeleteTask.Location = new Point(740, 380);
            btnDeleteTask.Name = "btnDeleteTask";
            btnDeleteTask.Size = new Size(120, 30);
            btnDeleteTask.TabIndex = 4;
            btnDeleteTask.Text = "Delete Task";
            btnDeleteTask.Click += btnDeleteTask_Click;
            // 
            // MainForm
            // 
            AcceptButton = btnSend;
            ClientSize = new Size(981, 623);
            Controls.Add(lstTasks);
            Controls.Add(txtNewTask);
            Controls.Add(btnAddTask);
            Controls.Add(btnCompleteTask);
            Controls.Add(btnDeleteTask);
            Controls.Add(tableLayoutPanel);
            Name = "MainForm";
            Text = "Cybersecurity ChatBot";
            Load += MainForm_Load;
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
