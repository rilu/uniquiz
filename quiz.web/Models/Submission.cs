using System.Collections.Generic;

namespace quiz.web.Models
{
    public class Submission
    {
        public string Name { get; set; }
        public int[] Answers { get; set; }
        public int Score { get; set; } 
    }
}