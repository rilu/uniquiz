using NUnit.Framework;
using quiz.web.Models;

namespace quiz.tests
{
    [TestFixture]
    public class SubmissionCorrectionTests
    {
         [Test]
         public void ShouldScoreSubmittedAnswers()
         {
             const int expected = 5;
             var correctAnswer = new[] {0, 1, 2, 0, 1, 2};
             var submittedAnswer = new[] {0, 2, 2, 0, 1, 2};

             var actual = SubmissionCorrection.Correcting(correctAnswer, submittedAnswer);

             Assert.AreEqual(expected, actual);
         }
    }
}