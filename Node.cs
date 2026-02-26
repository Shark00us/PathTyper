namespace PathTyper
{
    public struct Node(Rectangle rct, char c)
    {
        public char Char { get; set; } = c;
        public Rectangle Rectangle { get; set; } = rct;
    }
}
