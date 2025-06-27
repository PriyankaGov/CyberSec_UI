using System.Collections.Generic;
using System.Linq;

namespace cyberChatBot_PartTwo.Tasks
{
    //Manages a collection of tasks for the chatbot
    //Provides functionality to add, retrieve, complete, delete and display tasks
    public class TaskManager
    {
        //Stores a list of tasks
        private readonly List<Task> tasks;

        /// <summary>
        /// Adds a new task with the given title, description, and optional reminder.
        /// </summary>
        /// <param name="title">The short title or description of the task.</param>
        /// <param name="description">The full task description.</param>
        /// <param name="reminder">Optional reminder datetime (null if not set).</param>
        public TaskManager()
        {
            tasks = new List<Task>(); //uses title as main description
        }

        public void AddTask(string description, DateTime? reminder = null)
        {
            if (!string.IsNullOrWhiteSpace(description))
                tasks.Add(new Task(description, reminder));
        }

        /// <summary>
        /// Retrieves the full list of tasks.
        /// </summary>
        /// <returns>A list of all current tasks.</returns>
        public List<Task> GetTasks()
        {
            return tasks;
        }

        public void ClearTasks()
        {
            tasks.Clear();
        }

        /// <summary>
        /// Marks the task at the specified index as completed.
        /// </summary>
        /// <param name="index">The index of the task in the list.</param>
        /// <returns>True if the task was successfully marked, false if the index is invalid.</returns>
        public bool MarkAsCompleted(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks[index].IsCompleted = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes the task at the specified index.
        /// </summary>
        /// <param name="index">The index of the task to remove.</param>
        /// <returns>True if deletion was successful, false if the index is invalid.</returns>

        public bool DeleteTask(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a formatted string showing all tasks and their statuses.
        /// </summary>
        /// <returns>A user-friendly list of all tasks or a message if no tasks exist.</returns>
        public string DisplayTasks()
        {
            if ((!tasks.Any()))
                return "You have no tasks right now";

            string display = "Here are your current tasks:\n";
            for (int i = 0; i < tasks.Count; i++)
            {
                var currentTask = tasks[i];
                display += $"{i + 1}. {tasks[i].ToString()}\n";
            }
            return display;
        }

    }
}
