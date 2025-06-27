using System;
using System.Collections.Generic;
using System.Linq;
using cyberChatBot_PartTwo.Game;
using cyberChatBot_PartTwo.Tasks;

            /*GeeksforGeeks, 2019
             * C# Dictionary
             * GeeksforGeeks
             * <https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/>
             * [Accessed 23 May 2025]
             */

namespace cyberChatBot_PartTwo
{
    public class ChatBot
    {
        private readonly Action<string> print; // Output method for displaying messages
        private readonly Dictionary<string, List<string>> responses; // Topic-wise responses
        private readonly Dictionary<string, string> cyber_keywords; // Mapping keywords to topics
        private readonly TaskManager taskManager = new TaskManager(); // Handles task and reminders
        private CybersecurityQuiz activeQuiz; // Handles active quiz session
        private CybersecurityQuiz quiz; // Backup reference for quiz in GetResponse

        private bool quizInProgress = false; // Flag to indicate quiz status



        private readonly ActivityLog activityLog = new ActivityLog(); // Logs user actions
        private readonly Dictionary<string, string> lastResponsePerTopic = new(); // Avoids repeat responses
        private readonly Random random = new();
        private string userName;
        private string favoriteTopic;
        private string currentTopic;
        private string reminder;

        // Constructor to initialize chatbot with a username and output method
        public ChatBot(string userName, Action<string> printCallback)
        {
            this.userName = string.IsNullOrWhiteSpace(userName) ? "friend" : userName.Trim();
            this.print = printCallback;

            // Predefined responses for cybersecurity topics
            responses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                // Responses for various topics (phishing, passwords, etc.)
                ["phishing"] = new List<string>
                {
                    "Be wary of emails/messages asking for personal info.",
                    "Check sender addresses carefully.",
                    "Never click suspicious links - hover to preview first.",
                    "When in doubt, contact the company directly.",
                    "Phishing scams try to trick you into giving personal info.",
                    "Always double-check emails and links!"
                },
                ["passwords"] = new List<string>
                {
                    "Use 12+ characters mixing upper/lower case, numbers & symbols.",
                    "Never reuse passwords across sites. Use a password manager.",
                    "Strong passwords are your first line of defense."
                },
                ["safe browsing"] = new List<string>
                {
                    "Always look for HTTPS in URLs.",
                    "Avoid public Wi-Fi for sensitive tasks (use a VPN if needed).",
                    "Keep browsers and plugins updated.",
                    "Use ad-blockers to avoid malicious ads."
                },
                ["2fa"] = new List<string>
                {
                    "Enable on all important accounts.",
                    "Use authenticator apps instead of SMS.",
                    "Keep backup codes stored safely.",
                    "Never share your 2FA codes."
                },
                ["updates"] = new List<string>
                {
                    "Enable automatic updates.",
                    "Regularly update router firmware.",
                    "Remove unused programs."
                },
                ["backups"] = new List<string>
                {
                    "Follow the 3-2-1 rule: 3 copies, 2 local, 1 offsite.",
                    "Use encrypted cloud storage and test backups."
                },
                ["social engineering"] = new List<string>
                {
                    "Verify unexpected requests.",
                    "Never share passwords or codes.",
                    "Be skeptical of urgent/scary messages.",
                    "Train employees in cybersecurity awareness."
                },
                ["tips"] = new List<string>
                {
                    "Top 10 Cybersecurity Tips: 1. Strong passwords, 2. 2FA, 3. Avoid phishing, 4. Keep updated, etc."
                },
                ["bye"] = new List<string>
                {
                    $"Goodbye, {this.userName}! Stay cyber safe!"
                },
                ["thank you"] = new List<string>
                {
                    $"You are most welcome {this.userName}!"
                }
            };

            cyber_keywords = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "phishing", "phishing" }, { "scam", "phishing" },
                { "password", "passwords" }, { "strong password", "passwords" },
                { "https", "safe browsing" }, { "vpn", "safe browsing" },
                { "2fa", "2fa" }, { "authentication", "2fa" },
                { "update", "updates" }, { "patch", "updates" },
                { "backup", "backups" }, { "data loss", "backups" },
                { "social engineering", "social engineering" }, { "trick", "social engineering" },
                { "tips", "tips" }
            };
        }

        // Main method to process user input and route logic
        public void ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                PrintBotMessage("Please enter a valid question.");
                return;
            }

            input = input.ToLower();

            // If quiz is in progress, send answer to quiz
            if (quizInProgress && activeQuiz != null)
            {
                activeQuiz.ReceiveAnswer(input);
                return;
            }

            if (input.Contains("quiz")) //triggers quiz
            {
                activeQuiz = new CybersecurityQuiz(print, () => quizInProgress = false);
                quizInProgress = true;
                activeQuiz.Start();
                return;
            }

            // NLP, sentiment, and topic processing
            if (HandleNLPInput(input) != null) return;
            if (HandleSentiment(input)) return;
           // if (HandleTaskManagement(input)) return;
            if (HandleFavoriteTopic(input)) return;
            if (HandleFollowUp(input)) return;
            HandleResponses(input); // Generic topic-based response


        }

        // Used for GUI integration to return a string instead of printing directly
        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please enter a valid question.";

            input = input.ToLower();

            // Special trigger commands
            if (input.Contains("exit") || input.Contains("bye"))
                return "[EXIT]";

            if (quizInProgress)
            {
                quiz.ReceiveAnswer(input);
                return ""; // Skip default chatbot response during quiz
            }

            input = input.ToLower();

            if (input.Contains("quiz") || input.Contains("start quiz"))
            {
                quizInProgress = true;
                quiz = new CybersecurityQuiz(print);
                quiz.Start();
                return "[QUIZ_TRIGGER]";
            }

            var nlpResult = HandleNLPInput(input);
            if (nlpResult != null) return nlpResult;

            if (HandleNLPInput(input) != null)
                return "Task executed successfully.";
            if (HandleSentiment(input)) return "Got it. Thanks for sharing how you're feeling.";
            //if (HandleTaskManagement(input)) return "Task command processed.";
            if (HandleFavoriteTopic(input)) return $"Thanks! I'll remember that you're interested in {favoriteTopic}.";
            if (HandleFollowUp(input)) return $"Here’s more about {currentTopic}.";

            //match user input with known topics
            foreach (var topic in responses.Keys)
            {
                if (input.Contains(topic))
                {
                    var responseList = responses[topic];
                    string reply = GetRandomResponse(topic, responseList);
                    currentTopic = topic;

                    //personalize response based on favourite topic
                    if (!string.IsNullOrEmpty(favoriteTopic) &&
                        topic.Equals(favoriteTopic, StringComparison.OrdinalIgnoreCase))
                    {
                        reply += $"\nSince you're interested in {favoriteTopic}, feel free to ask more!";
                    }

                    return reply;
                }
            }

            return "I didn’t quite understand that. Try 'tips' or ask a cybersecurity question.";
        }

        // task manager helpers
        public string AddTask(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return "Please provide a valid task description.";

            taskManager.AddTask(description);
            return $"Task added: \"{description}\"";
        }

        public string ShowTasks()
        {
            return taskManager.DisplayTasks();
        }

        public string CompleteTask(int taskNumber)
        {
            int index = taskNumber - 1;
            if (taskManager.MarkAsCompleted(index))
                return $"Task #{taskNumber} marked as completed.";
            else
                return $"Task #{taskNumber} not found.";
        }

        public string DeleteTask(int taskNumber)
        {
            int index = taskNumber - 1;
            if (taskManager.DeleteTask(index))
                return $"Task #{taskNumber} deleted.";
            else
                return $"Task #{taskNumber} not found.";
        }


        //basic sentiment recognition
        private void PrintBotMessage(string message) => print($"Bot: {message}");

        private bool HandleSentiment(string input)
        {
            if (input.Contains("worried"))
            {
                PrintBotMessage("It's okay to feel worried sometimes. Let me help you with some tips to stay secure.");
                return true;
            }
            if (input.Contains("curious"))
            {
                PrintBotMessage("Being curious is great! Ask me anything about cybersecurity.");
                return true;
            }
            if (input.Contains("frustrated"))
            {
                PrintBotMessage("I know cybersecurity can be frustrating. I'm here to make it easier for you.");
                return true;
            }
            return false;
        }

        //handles NLP phrases for reminders, task viewing, logs, etc.
        private string? HandleNLPInput(string input)
        {
            input = input.ToLower();

            // 1. REMINDER/TASK CREATION LOGIC
            if (input.Contains("remind me to") || input.Contains("add reminder") || input.Contains("add a task to") || input.Contains("adda a task to"))
            {
                string taskText = input;

                // Handle common phrases
                string[] triggerPhrases = { "remind me to", "add reminder", "add a task to", "adda a task to" };
                foreach (var phrase in triggerPhrases)
                {
                    if (taskText.Contains(phrase))
                    {
                        int startIndex = taskText.IndexOf(phrase) + phrase.Length;
                        taskText = taskText.Substring(startIndex).Trim();
                        break;
                    }
                }

                if (string.IsNullOrWhiteSpace(taskText))
                    return "Sorry, I didn’t catch what to remind you about.";

                // Handle basic time expressions like 'tomorrow'
                DateTime? reminderDate = null;
                if (taskText.Contains("tomorrow"))
                {
                    reminderDate = DateTime.Now.AddDays(1);
                    taskText = taskText.Replace("tomorrow", "").Trim();
                }
                else if (taskText.Contains("today"))
                {
                    reminderDate = DateTime.Now;
                    taskText = taskText.Replace("today", "").Trim();
                }

                taskManager.AddTask(taskText, reminderDate);
                activityLog.Add($"Reminder set: {taskText}");
                string timeMsg = reminderDate.HasValue ? $" with a reminder for {reminderDate.Value:dd MMM yyyy}" : "";
                return $"Got it! I’ll remind you to: {taskText}{timeMsg}.";
            }

            // 2. TASK LIST CHECK
            if (input.Contains("show reminders") || input.Contains("show tasks") || input.Contains("what do i have to do"))
            {
                var tasks = taskManager.GetTasks();
                if (tasks.Count == 0)
                    return "You have no pending tasks.";

                print("Opening your task list...");
                return "[TASKS_TRIGGER]"; // Signal to GUI to show list
            }

            // 3. TASK ACTIONS (COMPLETE/DELETE etc.)
            string taskResult = HandleTaskCommands(input);
            if (taskResult != null)
                return taskResult;

            // 4. ACTIVITY LOG
            if (input.Contains("activity log") || input.Contains("what have i done") || input.Contains("log"))
            {
                var logs = activityLog.GetRecent();
                if (logs.Count == 0)
                    return "No recent activities recorded.";

                string logList = string.Join("\n• ", logs);
                return $"Here’s what you’ve been up to:\n• {logList}";
            }

            return null;
        }


        private string HandleTaskCommands(string input)
        {
            input = input.ToLower();

            if (input.StartsWith("add task"))
            {
                string desc = input.Substring("add task".Length).Trim();
                return AddTask(desc);
            }
            else if (input.Contains("show tasks") || input.Contains("list tasks"))
            {
                return ShowTasks();
            }
            else if (input.StartsWith("complete task"))
            {
                if (int.TryParse(input.Substring("complete task".Length).Trim(), out int taskNum))
                    return CompleteTask(taskNum);
                return "Please specify the task number to complete.";
            }
            else if (input.StartsWith("delete task"))
            {
                if (int.TryParse(input.Substring("delete task".Length).Trim(), out int taskNum))
                    return DeleteTask(taskNum);
                return "Please specify the task number to delete.";
            }

            return null; // not a task command
        }

        public string GetTaskList()
        {
            var tasks = taskManager.GetTasks();
            if (tasks.Count == 0)
                return "You have no tasks at the moment.";

            string display = "Here are your current tasks:\n";
            for (int i = 0; i < tasks.Count; i++)
            {
                display += $"{i + 1}. {tasks[i]}\n";
            }
            return display;
        }


        private bool HandleFavoriteTopic(string input)
        {
            if (input.StartsWith("i am interested in") || input.StartsWith("i'm interested in"))
            {
                favoriteTopic = input.Substring(input.IndexOf("in") + 3).Trim();
                PrintBotMessage($"Great! I'll remember that you're interested in {favoriteTopic}.");
                return true;
            }
            return false;
        }

        private bool HandleFollowUp(string input)
        {
            if ((input.Contains("more") || input.Contains("explain") || input.Contains("details")) && currentTopic != null)
            {
                if (responses.ContainsKey(currentTopic))
                {
                    var responseList = responses[currentTopic];
                    string followUp = GetRandomResponse(currentTopic, responseList);
                    PrintBotMessage($"More on {currentTopic}: {followUp}");
                    return true;
                }
            }
            return false;
        }

        //ensures non-repetitve random responses per topic
        private bool HandleResponses(string input)
        {
            foreach (var topic in responses.Keys)
            {
                if (input.Contains(topic))
                {
                    var responseList = responses[topic];
                    string reply = GetRandomResponse(topic, responseList);

                    currentTopic = topic;

                    if (favoriteTopic != null && topic.Equals(favoriteTopic, StringComparison.OrdinalIgnoreCase))
                    {
                        reply += $"\nSince you're interested in {favoriteTopic}, feel free to ask more!";
                    }

                    PrintBotMessage(reply);

                    return topic == "bye";
                }
            }

            PrintBotMessage("I didn’t quite understand that. Try 'tips' or ask a cybersecurity question.");
            return false;
        }

        private string GetRandomResponse(string topic, List<string> responseList)
        {
            string lastResponse = lastResponsePerTopic.ContainsKey(topic) ? lastResponsePerTopic[topic] : null;
            string newResponse;

            if (responseList.Count == 1)
            {
                newResponse = responseList[0];
            }
            else
            {
                do
                {
                    newResponse = responseList[random.Next(responseList.Count)];
                } while (newResponse == lastResponse);
            }

            lastResponsePerTopic[topic] = newResponse;
            return newResponse;
        }

        //simple internal class to record user activity history
        public class ActivityLog
        {
            private readonly List<string> log = new();
            public void Add(string entry) => log.Add($"[{DateTime.Now:HH:mm:ss}] {entry}");
            public List<string> GetRecent() => log.TakeLast(10).ToList();
        }
    }
}
