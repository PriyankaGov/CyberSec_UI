Cybersecurity Awareness Chatbot
A Windows Forms chatbot application designed to raise cybersecurity awareness through conversation, task management, and an interactive cybersecurity quiz mini-game.

Features
Conversational Cybersecurity Tips: Provides advice on phishing, passwords, 2FA, safe browsing, updates, backups, and social engineering.
Natural Language Processing (NLP): Understands simple commands to add reminders/tasks, show tasks, and view activity logs.
Task Management: Add, complete, and delete cybersecurity-related tasks via chat or UI controls.
Cybersecurity Quiz Mini-Game: Interactive quiz testing user knowledge with immediate feedback.
Activity Log: Keeps a record of user actions and reminders.

Sound Effects: Plays greeting sound on startup.

Getting Started
Prerequisites
Windows OS

.NET Framework / .NET Core supporting Windows Forms

Visual Studio 2022 (recommended)

Installation
Clone or download the repository.
Open the solution in Visual Studio.
Build the project to restore dependencies.
Run the application.

Usage
On launch, enter your name in the welcome prompt.
Type cybersecurity questions or commands in the input box.
Use commands like:
"Remind me to update my password tomorrow"
"Add a task to enable two-factor authentication"
"Show tasks"
"Start quiz"
Manage tasks via buttons (Add, Complete, Delete) in the UI.
Play the quiz when prompted by the bot.

Project Structure
MainForm.cs: Windows Forms UI handling input/output and task controls.
ChatBot.cs: Core chatbot logic with NLP parsing, responses, and interaction management.
Game/CybersecurityQuiz.cs: Quiz mini-game implementation.
Tasks/TaskManager.cs & Tasks/Task.cs: Task handling classes.
SoundManager.cs: Plays audio files for greetings and feedback.

Future Enhancements
Integrate quiz fully into the GUI with interactive question-answer UI.
Add persistent storage for tasks and logs.
Improve NLP for more natural conversations.
Expand cybersecurity topics and quiz questions.

Credits
Developed by Priyanka
Powered by .NET Windows Forms
