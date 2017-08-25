using System;
using System.Collections.Generic;

namespace DartsGame
{
    public static class Board
    {
        private static Dictionary<Slice, Dictionary<Ring, int>> _dartBoard;
        public static Dictionary<Slice, Dictionary<Ring, int>> DartBoard { get { return _dartBoard; } }

        static Board()
        {
            SetBoardMatrix(); //Maxim
        }

        private static void SetBoardMatrix()
        {
            _dartBoard = new Dictionary<Slice, Dictionary<Ring, int>>();

            foreach (Slice itemSlice in Enum.GetValues(typeof(Slice)))
            {
                _dartBoard.Add(itemSlice, new Dictionary<Ring, int>());
                int sliceValue = (int)itemSlice;

                foreach (Ring itemRing in Enum.GetValues(typeof(Ring)))
                {
                    int ringValue = AssignMultiplier(itemRing);
                    if ((itemRing != Ring.InnerBullsEye) && (itemRing != Ring.OuterBullsEye))
                        _dartBoard[itemSlice].Add(itemRing, (sliceValue * ringValue));
                    else
                        _dartBoard[itemSlice].Add(itemRing, ringValue);
                }
            }
        }

        private static int AssignMultiplier(Ring ringItem)
        {
            int multiplier = ((ringItem == Ring.One) ? multiplier = 0
                : (ringItem == Ring.Three || ringItem == Ring.Five) ? multiplier = 1
                : (ringItem == Ring.Two) ? multiplier = 2
                : (ringItem == Ring.Four) ? multiplier = 3
                : (ringItem == Ring.OuterBullsEye) ? multiplier = 25
                : multiplier = 50);

            return multiplier;
        }
    }

    public enum Slice
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Eleven = 11,
        Twelve = 12,
        Thirteen = 13,
        Fourteen = 14,
        Fifteen = 15,
        Sixteen = 16,
        Seventeen = 17,
        Eighteen = 18,
        Nineteen = 19,
        Twenty = 20
    }
    public enum Ring
    {
        One,
        Two,
        Three,
        Four,
        Five,
        InnerBullsEye,
        OuterBullsEye
    }
}