namespace WpfApp1.Domain
{
    public class Position
    {
        public Position (int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public override string ToString() => $"({X}, {Y})";
    }
}
