using System;
using System.Windows.Forms;
using cyberChatBot_PartTwo.Tasks;
using CyberSec_UI;

namespace cyberChatBot_PartTwo
{
    public partial class MainForm : Form
    {
        //Instance of the chatbot that handles user input and repsonses
        private ChatBot chatbot;

        public MainForm()
        {
            InitializeComponent(); //initializes all UI components
        }

        /// <summary>
        /// Handles initialization logic when the form loads, such as greeting the user
        /// and starting the chatbot instance.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {

            // Play greeting sound (update path as needed)
            SoundManager.PlaySound(@"C:\Users\Lenovo Flex\OneDrive\Documents\PROG6221\VisualStudio2022\CyberSec_UI\greeting.wav");

            //asks the user for their name
            string userName = Prompt.ShowDialog("What is your name?", "Welcome!");
            if (string.IsNullOrWhiteSpace(userName))
                userName = "Friend";

            //initialeese the chatbot with the username and output method
            chatbot = new ChatBot(userName, AppendChatMessage);

            //greets user in the chat box
            AppendChatMessage($"Bot: Welcome, {userName}! Ask me anything about cybersecurity.");
        }

        /// <summary>
        /// Handles the Send button click event, processes user input, and returns a chatbot response.
        /// </summary>
        private void btnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(userInput))
                return;

            //shows the users input in the caht windodw
            AppendChatMessage($"You: {userInput}");

            //gets a response from the chat bot
            string botReply = chatbot.GetResponse(userInput);

            //handles a specific chatbot from the chatbot
            if (botReply == "[EXIT]")
            {
                AppendChatMessage("Bot: Goodbye! Stay safe!");
                btnSend.Enabled = false;
                txtInput.Enabled = false;
                return;
            }
            else if (botReply == "[QUIZ_TRIGGER]")
            {
                //placeholder for quiz functionality
            }
            else if (botReply == "[TASKS_TRIGGER]")
            {
                //shows tasks in a message box
                string tasks = chatbot.GetTaskList();
                MessageBox.Show(tasks, "Your Tasks", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                //displays the chatbots general response
                AppendChatMessage($"Bot: {botReply}");
            }

            txtInput.Clear(); //clears input field
            txtInput.Focus(); //refocuses for next input
        }

        /// Task management functionality
        private TaskManager taskManager = new TaskManager();

        /// <summary>
        /// Adds a new task based on user input from the GUI textbox.
        /// </summary>
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            string description = txtNewTask.Text.Trim();
            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please enter a task description.");
                return;
            }
            taskManager.AddTask(description);
            txtNewTask.Clear();
            RefreshTaskList();
        }

        /// <summary>
        /// Marks the selected task in the list as completed.
        /// </summary>
        private void btnCompleteTask_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstTasks.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Please select a task.");
                return;
            }
            if (taskManager.MarkAsCompleted(selectedIndex))
            {
                MessageBox.Show("Task marked as completed.");
                RefreshTaskList();
            }
            else
            {
                MessageBox.Show("Failed to complete the task.");
            }
        }

        // <summary>
        /// Deletes the selected task from the task list.
        /// </summary>
        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstTasks.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Please select a task.");
                return;
            }
            if (taskManager.DeleteTask(selectedIndex))
            {
                MessageBox.Show("Task deleted.");
                RefreshTaskList();
            }
            else
            {
                MessageBox.Show("Failed to delete the task.");
            }
        }

        /// <summary>
        /// Updates the list box to reflect the current tasks, including completion status.
        /// </summary>
        private void RefreshTaskList()
        {
            lstTasks.Items.Clear();
            foreach (var task in taskManager.GetTasks())
            {
                string displayText = task.Description;
                if (task.IsCompleted)
                    displayText += " (Completed)";
                lstTasks.Items.Add(displayText);
            }
        }

        /// <summary>
        /// Appends a new message to the chat window (RichTextBox).
        /// </summary>
        private void AppendChatMessage(string message)
        {
            rtbChat.AppendText(message + Environment.NewLine);
            rtbChat.ScrollToCaret();
        }
    }
}

