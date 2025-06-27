using System;
using System.Collections.Generic;
using System.Linq;

namespace cyberChatBot_PartTwo.Game
{
    //manages the cybersecurity quix logic, including questions, scoring and feedback
    class CybersecurityQuiz
    {
        private readonly List<QuizQuestion> questions; //lIst of quiz questions
        private readonly Action<string> print; //delegate used to print messages to the user
        private readonly Action endQuizCallback; //optiona callback invoked when the quiz ends
        private int score; //Keeps track of the users score and current question index ↓↓
        private int currentIndex;

        /// <summary>
        /// Constructor to initialize the quiz with a print callback and optional end-of-quiz callback.
        /// </summary>
        public CybersecurityQuiz(Action<string> printCallback, Action onQuizEnd = null)
        {
            print = printCallback ?? Console.WriteLine;
            endQuizCallback = onQuizEnd;

            //initialse quiz questions with text, options, correct answer, and explanantions
            questions = new List<QuizQuestion>
            {
                new QuizQuestion("Phishing attacks often come through emails. True or False?", null, "true", "Correct! Phishing scams usually try to steal personal info via deceptive emails."),
                new QuizQuestion("Which is the strongest password?", new List<string> { "a) password123", "b) PurpleTiger$Eats_42Pizza!", "c) 123456" }, "b", "Long passphrases with symbols, numbers, and mixed case letters are stronger."),
                new QuizQuestion("You should avoid public Wi-Fi for sensitive tasks unless using a VPN. True or False?", null, "true", "Right! Public Wi-Fi can be insecure, so always use a VPN when doing sensitive work."),
                new QuizQuestion("What does 2FA stand for?", new List<string> { "a) Two-Factor Authentication", "b) Two-Face Access", "c) Twice-First Authorization" }, "a", "2FA stands for Two-Factor Authentication, adding an extra security layer."),
                new QuizQuestion("Regular software updates help protect against vulnerabilities. True or False?", null, "true", "Correct! Updates patch security holes and keep your system safe."),
                new QuizQuestion("Backing up data using the 3-2-1 rule means:", new List<string> { "a) 3 copies, 2 local, 1 offsite", "b) 3 backups per day", "c) Backup only 3 files" }, "a", "The 3-2-1 rule means 3 copies of data, on 2 different media, with 1 copy offsite."),
                new QuizQuestion("Social engineering attacks trick you into giving info by manipulation. True or False?", null, "true", "Yes! Attackers use psychological tricks to get access to your data."),
                new QuizQuestion("Using a password manager is a good way to create and store passwords. True or False?", null, "true", "Absolutely! Password managers help generate and safely store strong passwords."),
                new QuizQuestion("Enabling 2FA on your accounts is optional and not recommended. True or False?", null, "false", "Wrong! Enabling 2FA significantly improves account security and is recommended."),
                new QuizQuestion("Which of these is NOT a safe browsing practice?", new List<string> { "a) Checking for HTTPS in URLs", "b) Using ad-blockers", "c) Clicking on suspicious ads" }, "c", "Clicking on suspicious ads can expose you to malware or phishing attacks.")
            };
        }


        /// <summary>
        /// Starts the quiz by resetting score and index, and asking the first question.
        /// </summary
        public void Start()
        {
            score = 0;
            currentIndex = 0;

            print("\nStarting Cybersecurity Quiz!");
            print("Answer each question by typing the option letter (e.g., 'a') or 'true'/'false' for T/F questions.\n");

            AskNextQuestion();
        }

        /// <summary>
        /// Accepts the user's answer and evaluates correctness.
        /// Moves on to the next question or ends the quiz.
        /// </summary>
        public void ReceiveAnswer(string input)
        {
            if (currentIndex >= questions.Count)
            {
                print("Quiz is already complete.");
                return;
            }

            var currentQuestion = questions[currentIndex];
            string answer = input?.Trim().ToLower();

            if (!IsValidAnswer(answer, currentQuestion))
            {
                print("Invalid answer. Please try again.");
                return;
            }

            if (answer == currentQuestion.CorrectAnswer)
            {
                print($"Correct! {currentQuestion.Explanation}");
                score++;
            }
            else
            {
                print($"Incorrect. {currentQuestion.Explanation}");
            }

            currentIndex++;

            if (currentIndex < questions.Count)
            {
                AskNextQuestion();
            }
            else
            {
                ShowResults();
            }
        }

        /// <summary>
        /// Displays the next quiz question and its options (if any).
        /// </summary>
        private void AskNextQuestion()
        {
            var question = questions[currentIndex];
            print($"\n{question.QuestionText}");

            if (question.Options != null && question.Options.Count > 0)
            {
                foreach (var option in question.Options)
                {
                    print(option);
                }
            }

            print("Your answer?");
        }

        /// <summary>
        /// Checks whether the user's input is a valid answer for the current question.
        /// </summary>
        private bool IsValidAnswer(string answer, QuizQuestion question)
        {
            if (string.IsNullOrEmpty(answer)) return false;

            if (question.Options == null || question.Options.Count == 0)
            {
                return answer == "true" || answer == "false";
            }
            else
            {
                var validOptions = new List<string> { "a", "b", "c", "d", "e" };
                return validOptions.Take(question.Options.Count).Contains(answer);
            }
        }

        /// <summary>
        /// Shows the final quiz results with a score and personalized feedback.
        /// </summary>
        private void ShowResults()
        {
            double percentage = (double)score / questions.Count;
            print($"\nQuiz complete! Your score: {score}/{questions.Count}");

            if (percentage >= 0.9)
                print("Excellent! You're a cybersecurity pro!");
            else if (percentage >= 0.7)
                print("Good job! Keep learning to stay safer online.");
            else if (percentage >= 0.4)
                print("Not bad, but there's room to improve.");
            else
                print("Keep practicing! Cybersecurity is very important.");

            
            //optional callback when quiz ends
            endQuizCallback.Invoke();
                                    
        }
    }
}
