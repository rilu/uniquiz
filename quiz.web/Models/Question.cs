using System.Collections.Generic;

namespace quiz.web.Models
{
    public class QuestionList
    {
        public string Id { get; set; }
        public IEnumerable<Question> Questions { get; set; }

        public static QuestionList New()
        {
            return new QuestionList() {Id = "QuestionLists/QuestionList", Questions = new List<Question>()};
        }
    }

    public class Question
    {
        public string QuestionTitle { get; set; }
        public int CorrectOption { get; set; }
        public List<string> Options { get; set; }
    }
}