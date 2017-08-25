using System;
using System.Collections.Generic;
using System.Linq;

namespace DartsGame
{
    // 1. Ask how many players are playing
    // 1.a. Label maximum and minimum amounts.

    // 2. For each player playing ask user to enter the name
    // 3. Select type of game
    // 3a. Game type 1: High_Score_Beats_Max_Score when any 
    // player passes the defined max score, the highest scoring
    // player is the winner.
    // 3b. Game type 2: High_Score_At_End_Of_Fixed_Rounds
    // At the end of the fixed number of rounds, player with
    // the highest score wins.
    // 4. If user select game type 1 then ask what is the max
    // points to win.
    // 4a. We will label to the user max points possible 300
    // 4b. If user selects game type 2, ask user how many rounds
    // label for user max amount of rounds is 10.

    public class DartGame
    {
        private const int MIMUMUM_PLAYERS = 1;
        private const int MAXIMUM_PLAYERS = 4;
        private const int MAXIMUM_SCORE = 300;
        private const int MINIMUM_SCORE = 1;
        private const int MAXIMUM_ROUNDS = 10;
        private const int MINIMUM_ROUNDS = 1;
        private const int MAX_THROWS_PER_ROUND = 3;
        private int _currentRound = 0;
        private TypeOfGame _gameType;
        private List<Player> _players;
        private bool _isGameOver;

        private int _winningScore;
        private int _winningRound;
        public int WinningScore
        {
            get { return _winningScore; }
            set
            {
                if (value > MAXIMUM_SCORE)
                    throw new Exception("Error: Winning score cannot be higher than " + MAXIMUM_SCORE.ToString());
                if (value < MINIMUM_SCORE)
                    throw new Exception("Error: Winning score cannot be lower than " + MINIMUM_SCORE.ToString());

                _winningScore = value;
            }
        }
        public int WinningRound
        {
            get { return _winningRound; }
            set
            {
                if (value > MAXIMUM_ROUNDS)
                    throw new Exception("Error: Winning round cannot be higher than " + MAXIMUM_ROUNDS.ToString());
                if (value < MINIMUM_ROUNDS)
                    throw new Exception("Error: Winning round cannot be lower than " + MINIMUM_ROUNDS.ToString());

                _winningRound = value;
            }
        }

        public List<Player> Players { get { return _players; } }

        public TypeOfGame GameType
        {
            get { return _gameType; }
            set { _gameType = value; }
        }

        public DartGame() { }
        public DartGame(List<Player> players)
        {
            _players = players;
        }
        public DartGame(TypeOfGame GameType)
        {
            _gameType = GameType;
        }
        public DartGame(List<Player> players, TypeOfGame GameType)
        {
            _players = players;
            _gameType = GameType;
        }

        public void Start()
        {
            do
            {
                // Throw darts for each player
                _currentRound++;
                foreach (Player player in _players)
                {
                    for (int i = 0; i < MAX_THROWS_PER_ROUND; i++)
                        player.Throw();
                }

                // Determine if game is over
                _isGameOver = _gameType == TypeOfGame.High_Score_At_End_Of_Fixed_Rounds 
                    ? (_currentRound >= _winningRound)
                    : _players.Any(p => p.Score >= _winningScore);

            } while (!_isGameOver);

            // Determine winner or winners, if a tie
            int maxScore = _players.Aggregate((p1, p2) => p1.Score > p2.Score ? p1 : p2).Score;
            _players.ForEach(p => { if (p.Score == maxScore) p.IsWinner = true; });
        }

        public void SetDefaultRounds()
        {
            WinningRound = MAXIMUM_ROUNDS;
        }

        public void SetDefaultScore()
        {
            WinningScore = MAXIMUM_SCORE;
        }
    }

    public enum TypeOfGame
    {
        High_Score_Beats_Max_Score,
        High_Score_At_End_Of_Fixed_Rounds
    }
}
