using System.Collections.Generic;

namespace quiz.web.Models
{
    public class HighScoreModel
    {
        public IEnumerable<HighScoreRank> HighScoreRankings { get; set; }
    }

    public class HighScoreRank
    {
        public int Rank { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}