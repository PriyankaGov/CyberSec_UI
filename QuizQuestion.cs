using System.Collections.Generic;

namespace cyberChatBot_PartTwo.Game
{
    /// <summary>
    /// Represents a single quiz question in the Cybersecurity Quiz.
    /// </summary>
    public class QuizQuestion
    {
        /// <summary>
        /// The text of the question to be displayed to the user.
        /// </summary>
        public string QuestionText { get; }

        /// <summary>
        /// A list of possible answer options (optional, used for multiple-choice questions).
        /// If null, the question is assumed to be True/False.
        /// </summary>
        public List<string> Options { get; }  // null or empty for True/False questions
        public string CorrectAnswer { get; } //correct answer. can be a letter (a, b, c, d)
        public string Explanation { get; } // explanation of the correct answer, shown after answering

        /// <summary>
        /// Constructor to initialize the quiz question with necessary data.
        /// </summary>
        /// <param name="questionText">The question prompt shown to the user.</param>
        /// <param name="options">Optional list of choices (set to null for True/False questions).</param>
        /// <param name="correctAnswer">The correct answer ("true", "false", or choice letter like "a").</param>
        /// <param name="explanation">Feedback shown after the question is answered.</param>
        public QuizQuestion(string questionText, List<string> options, string correctAnswer, string explanation)
        {
            QuestionText = questionText;
            Options = options;
            CorrectAnswer = correctAnswer.ToLower();
            Explanation = explanation;
        }
    }
}

