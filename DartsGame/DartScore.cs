namespace DartsGame
{
    public class DartScore
    {
        public Slice SliceHit { get; set; }
        public Ring RingHit { get; set; }
        public int ThrowPoints { get; set; }
        public bool IsBullseye { get; set; }

        public override string ToString()
        {
            if (RingHit == Ring.One)
                return "Miss";
            if (RingHit == Ring.InnerBullsEye)
                return "Double Bullseye!";
            if (RingHit == Ring.OuterBullsEye)
                return "Bullseye!";
            if (RingHit == Ring.Two)
                return $"Double {SliceHit}";
            if (RingHit == Ring.Four)
                return $"Triple {SliceHit}";
            if (RingHit == Ring.Three || RingHit == Ring.Five)
                return SliceHit.ToString();
            return base.ToString();
        }
    }
}