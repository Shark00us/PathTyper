namespace PathTyper
{
    public readonly struct Triangle(Point a,Point b,Point c)
    {
        public Point A => a;
        public Point B => b;
        public Point C => c;
    }
}
