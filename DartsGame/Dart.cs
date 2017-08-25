using System;

namespace DartsGame
{
    /// <summary>
    /// The dart class should handle throwing a dart and returning the dart score.
    /// </summary>
    public static class Dart
    {
        private static Random _randomizer;
        
        static Dart()
        {
            _randomizer = new Random();
        }

        public static DartScore Throw()
        {
            // Randomly generate the Slice
            Slice randomSlice = (Slice)_randomizer.Next(1, Enum.GetValues(typeof(Slice)).Length);

            // Randomly generate the Ring
            Ring randomRing = (Ring)_randomizer.Next(0, Enum.GetValues(typeof(Ring)).Length - 1);

            // Retrieve the points
            int points = Board.DartBoard[randomSlice][randomRing];

            // Populate DartScore
            return new DartScore()
            {
                RingHit = randomRing,
                SliceHit = randomSlice,
                ThrowPoints = points,
                IsBullseye = (randomRing == Ring.InnerBullsEye || randomRing == Ring.OuterBullsEye)
            };
        }
    }
}