using System.Collections.Generic;
using System.Linq;

namespace quiz.web.Models
{
    public static class SubmissionCorrection
    {
        public static int Correcting(IEnumerable<int> correctAnswers, IEnumerable<int> submittedAnswers)
        {
            var correct = correctAnswers as int[] ?? correctAnswers.ToArray();
            var submitted = submittedAnswers as int[] ?? submittedAnswers.ToArray();

            return correct.Where((t, i) => t == submitted[i]).Count();
        }
    }
}