using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tbfyoyl.TAGame
{
    public class Score
    {

        //very basic class the score for a paper
        public int NumQuestions;
        public int NumGradingMistakes;
        public int NumCheaterMistakes;

        public Score()
        {
            NumQuestions = 0;
            NumCheaterMistakes = 0;
            NumGradingMistakes = 0;
        }

        public Score(int numQuestions, int numGradingMistakes, int numCheaterMistakes)
        {
            NumQuestions = numQuestions;
            NumCheaterMistakes = numCheaterMistakes;
            NumGradingMistakes = numGradingMistakes;
        }

        public static Score operator +(Score s1, Score s2)
        {
            return new Score(s1.NumQuestions + s2.NumQuestions,
                s1.NumGradingMistakes + s2.NumGradingMistakes,
                s1.NumCheaterMistakes + s2.NumCheaterMistakes);
        }

        public static Score operator -(Score s1, Score s2)
        {
            return new Score(s1.NumQuestions - s2.NumQuestions,
                s1.NumGradingMistakes - s2.NumGradingMistakes,
                s1.NumCheaterMistakes - s2.NumCheaterMistakes);
        }

    }
}