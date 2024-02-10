namespace EM_Lab_1
{
    public struct Interval
    {
        public double LeftEdge { get; set; }

        public double RightEdge { get; set; }

        public Interval() : this(0, 0) { }

        public Interval(double leftEdge, double rightEdge)
        {
            LeftEdge = leftEdge;
            RightEdge = rightEdge;
        }

        public override bool Equals(object? obj)
        {
            return obj is Interval interval &&
                   LeftEdge == interval.LeftEdge &&
                   RightEdge == interval.RightEdge;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LeftEdge, RightEdge);
        }

        public void Deconstruct(out double leftEdge,  out double rightEdge)
        {
            leftEdge = LeftEdge;
            rightEdge = RightEdge;
        }
    }
}
