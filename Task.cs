namespace cyberChatBot_PartTwo.Tasks
{
    //Represents a single task in the chatbots task manager
    //stores task description, creation time, completeion status, and optional reminder time
    public class Task
    {
        public string Description { get; set; } //brief description of task
        public DateTime CreatedAt { get; set; } //exact date and time when the task was created
        public bool IsCompleted { get; set; } //indicates whether the task has been completed
        public DateTime? Reminder { get; set; } //optional reminder time for the task


        //constructs a new Task object with the givendescription and optional remninder time
        public Task(string description, DateTime? reminder = null)
        {
            Description = description;
            CreatedAt = DateTime.Now;
            IsCompleted = false;
        }

        //returns a formatted struing representing the task
        //includes status (completed or pending) and the creation date
        //<returns> a user-friendly representation of the task</returns>
        public override string ToString()
        {
            string status = IsCompleted ? "[Completed]" : "[] Pending";
            return $"{Description} (added on {CreatedAt:dd MMM yyyy, HH:mm})";
        }
    }
}
