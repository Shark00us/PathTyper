using System.Drawing.Drawing2D;
using System.Reflection;

namespace PathTyper
{
    public struct Node(Rectangle rct, char c)
    {
        public char Char { get; set; } = c;
        public Rectangle Rectangle { get; set; } = rct;
    }
    public partial class Main : Form
    {
        private static void EnableDoubleBuffer(Control control)
        {
            if (control != null)
            {
                typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(control, true, null);
            }
        }
        private static Point GetCenterPoint(Rectangle rct)
        {
            return new Point(rct.X + (rct.Width / 2), rct.Y + (rct.Height / 2));
        }
        private static Rectangle GetCenterSquare(Rectangle rct)
        {
            int side = Math.Max(15, Math.Min(rct.Width, rct.Height));
            int x = rct.X + ((rct.Width - side) / 2);
            int y = rct.Y + ((rct.Height - side) / 2);
            return new Rectangle(x, y, side, side);
        }
        private static Rectangle ShrinkRectangle(Rectangle r, float percent)
        {
            int newWidth = (int)(r.Width * (percent / 100));
            int newHeight = (int)(r.Height * (percent / 100));
            int x = r.X + ((r.Width - newWidth) / 2);
            int y = r.Y + ((r.Height - newHeight) / 2);
            return new Rectangle(x, y, newWidth, newHeight);
        }
        private static Rectangle GetCellBounds(TableLayoutPanel panel, int column, int row)
        {
            int[] columnWidths = panel.GetColumnWidths();
            int[] rowHeights = panel.GetRowHeights();
            int x = 0;
            for (int i = 0; i < column; i++)
            {
                x += columnWidths[i];
            }

            int y = 0;
            for (int i = 0; i < row; i++)
            {
                y += rowHeights[i];
            }

            int width = columnWidths[column];
            int height = rowHeights[row];

            return new Rectangle(x, y, width, height);
        }
        public static Rectangle[] SplitIntoNine(Rectangle rect)
        {
            Rectangle[] result = new Rectangle[9];

            int baseWidth = rect.Width / 3;
            int baseHeight = rect.Height / 3;

            int remainderWidth = rect.Width % 3;
            int remainderHeight = rect.Height % 3;

            int currentY = rect.Y;

            for (int row = 0; row < 3; row++)
            {
                int cellHeight = baseHeight;

                if (row == 2)
                {
                    cellHeight += remainderHeight;
                }

                int currentX = rect.X;

                for (int col = 0; col < 3; col++)
                {
                    int cellWidth = baseWidth;

                    if (col == 2)
                    {
                        cellWidth += remainderWidth;
                    }

                    int reversedRow = 2 - row;

                    int index = reversedRow * 3 + col;

                    result[index] = new Rectangle
                    (
                        currentX,
                        currentY,
                        cellWidth,
                        cellHeight
                    );

                    currentX += cellWidth;
                }

                currentY += cellHeight;
            }

            return result;
        }
        private static readonly (char[] CHARS, string[] PATHS)[] Paths =
            [
                (['0','O'],["98741236","98741236","89632147","87412369","78963214","74123698","47896321","41236987","14789632","12369874","21478963","23698741","32147896","36987412","63214789","69874123"]),
                (['1'],["852","258"]),
                (['2'],["789654123"]),
                (['3'],["789654321"]),
                (['4'],["745693"]),
                (['5','S'],["987456321"]),
                (['6'],["563214789"]),
                (['7'],["78951"]),
                (['8'],["789632145"]),
                (['9'],["547896321"]),
                (['A'],["14789635"]),
                (['B'],["147896532"]),
                (['C'],["9874123"]),
                (['D'],["147862"]),
                (['E'],["987412356","987456123"]),
                (['F'],["9874156","9874561","1456789"]),
                (['G'],["987412365"]),
                (['H'],["7419635"]),
                (['I'],["7895213","9875231"]),
                (['J'],["8521"]),
                (['K'],["741593"]),
                (['L'],["74123"]),
                (['M'],["1475963"]),
                (['N'],["1475369"]),
                (['P'],["1478965"]),
                (['Q'],["874523"]),
                (['R'],["14789653"]),
                (['T'],["78952","98752"]),
                (['U'],["7412369"]),
                (['V'],["74269"]),
                (['W'],["741258369"]),
                (['X'],["75391"]),
                (['Y'],["7592"]),
                (['Z'],["7895123"]),
                (['.'],["5"]),
                (['-'],["456","654"]),
                (['卐'],["985217463"])
            ];
        private readonly Node[] _nodes;
        private string _input = string.Empty;
        private bool _enterHeld = false;
        private bool _dashHeld = false;
        private CancellationTokenSource? _alertCts = null;
        public Main()
        {
            InitializeComponent();
            EnableDoubleBuffer(tableLayoutPanelMain);
            _nodes = [
                new(Rectangle.Empty, '1'),
                new(Rectangle.Empty, '2'),
                new(Rectangle.Empty, '3'),
                new(Rectangle.Empty, '4'),
                new(Rectangle.Empty, '5'),
                new(Rectangle.Empty, '6'),
                new(Rectangle.Empty, '7'),
                new(Rectangle.Empty, '8'),
                new(Rectangle.Empty, '9')];
            this.KeyPreview = true;
            this.Text = "Numpad Path Typer";
        }
        private void UpdateNodeRectangles()
        {
            Rectangle cellBound = GetCellBounds(tableLayoutPanelMain, 0, 1);
            Rectangle[] rects = SplitIntoNine(cellBound);
            for (int i = 0; i < _nodes.Length; i++)
            {
                _nodes[i].Rectangle = rects[i];
            }
        }
        private Node[] GetActiveNodesInOrder()
        {
            List<Node> actives = [];
            if (_input != null)
            {
                foreach (char c in _input)
                {
                    foreach (var node in _nodes)
                    {
                        if (c == node.Char)
                        {
                            actives.Add(node);
                            break;
                        }
                    }
                }
            }
            return [.. actives];
        }
        private Font GetRelativeFont(float percent, FontStyle style)
        {
            int baseSize = Math.Min(this.Height, this.Width) / 25;
            percent = Math.Max(percent, 1) / 100;
            float finalSize = Math.Max(8f, baseSize * percent);
            return new Font(Font.SystemFontName, finalSize, style);
        }
        private void SetInputAndInvalidateCanvas(string input)
        {
            _input = input ?? string.Empty;
            tableLayoutPanelMain.Invalidate(GetCellBounds(tableLayoutPanelMain, 0, 1));
        }
        private void RenderChar()
        {
            char? selected = null;
            foreach (var (CHARS, PATHS) in Paths)
            {
                foreach (string p in PATHS)
                {
                    if (_input == p)
                    {
                        if (CHARS.Length > 1)
                        {
                            CharSelector form2 = new CharSelector(CHARS);
                            var result = form2.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                selected = form2.SelectedChar;
                            }
                        }
                        else
                        {
                            selected = CHARS[0];
                        }
                        break;
                    }
                }
            }
            if (selected != null)
            {
                textBoxOutput.Text += selected;
                _ = Alert($"Character '{selected}' added.", Color.Blue);
            }
            else
            {
                _ = Alert($"No matching character found.", Color.DarkRed);
            }
        }

        private async void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (c == '-' && textBoxOutput.Text.Length > 0 && _dashHeld)
            {
                _ = Alert($"Last character removed.", Color.DarkGoldenrod);
                textBoxOutput.Text = textBoxOutput.Text[..^1];
            }
            else if (c == '\r' && _enterHeld)
            {
                if (_input.Length > 0)
                {
                    RenderChar();
                    SetInputAndInvalidateCanvas(string.Empty);
                }
                else if (string.IsNullOrEmpty(textBoxOutput.Text) == false)
                {
                    _ = Alert($"Space added.", Color.Blue);
                    textBoxOutput.Text += ' ';
                }
            }
            else if (c == '0' && _input.Length > 0)
            {
                _ = Alert($"Last input cleared.", Color.Blue);
                SetInputAndInvalidateCanvas(_input[..^1]);
            }
            else if (char.IsNumber(c) && _input.Contains(c) == false && c != '0')
            {
                SetInputAndInvalidateCanvas(_input + c);
            }
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_enterHeld)
                {
                    e.SuppressKeyPress = true;
                    return;
                }
                _enterHeld = true;
            }
            else if (e.KeyCode == Keys.Subtract)
            {
                if (_dashHeld)
                {
                    e.SuppressKeyPress = true;
                    return;
                }
                _dashHeld = true;
            }
        }
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _enterHeld = false;
            }
            if (e.KeyCode == Keys.Subtract)
            {
                _dashHeld = false;
            }
        }
        private void textBoxOutput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxOutput.Text)) return;
                Clipboard.SetText(textBoxOutput.Text);
            }
            catch { }
        }
        private async Task Alert(string msg, Color color)
        {
            if (string.IsNullOrEmpty(msg) == false)
            {
                _alertCts?.Cancel();
                _alertCts = new CancellationTokenSource();
                labelAlert.Visible = true;
                labelAlert.BackColor = color;
                labelAlert.Text = msg;
                try
                {
                    await Task.Delay(2500, _alertCts.Token);
                    labelAlert.Text = string.Empty;
                    labelAlert.BackColor = this.BackColor;
                    labelAlert.Visible = false;
                }
                catch (TaskCanceledException) { }
            }
        }
        private Point[] GetTriangle(Rectangle rct)
        {
            return [new Point(rct.X + rct.Width / 2, rct.Y), new Point(rct.X, rct.Y + rct.Height), new Point(rct.X + rct.Width, rct.Y + rct.Height)];
        }
        private Rectangle GetPolygonBounds(Point[] polygon)
        {
            int minX = polygon[0].X;
            int minY = polygon[0].Y;
            int maxX = polygon[0].X;
            int maxY = polygon[0].Y;
            foreach(var point in polygon)
            {
                minX = point.X < minX ? point.X : minX;
                minY = point.Y < minY ? point.Y : minY;
                maxX = point.X > maxX ? point.X : maxX;
                maxY = point.Y > maxY ? point.Y : maxY;
            }
            Point topLeft = new Point(minX, maxY);
            Size size = new Size(maxX - minX,maxY - minY);
            return new Rectangle(topLeft, size);
        }
        private void RotateAndDrawPolygon(Graphics g, PointF[] polygon,Point center,float f)
        {
            using(Matrix mtrx = new Matrix())
            {
                mtrx.RotateAt(f, center);
                g.Transform = mtrx;
                g.DrawPolygon(Pens.Orange, polygon);
                g.ResetTransform();
            }

        }
        private void tableLayoutPanelMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var polygon = GetTriangle(GetCellBounds(tableLayoutPanelMain, 0, 1));
            g.DrawPolygon(Pens.Red, polygon);
            int minX = polygon[0].X;
            int minY = polygon[0].Y;
            int maxX = polygon[0].X;
            int maxY = polygon[0].Y;
            foreach (var point in polygon)
            {
                minX = point.X < minX ? point.X : minX;
                minY = point.Y < minY ? point.Y : minY;
                maxX = point.X > maxX ? point.X : maxX;
                maxY = point.Y > maxY ? point.Y : maxY;
            }
            foreach(var pnt in polygon)
            {
                g.DrawString($"({pnt.X},{pnt.Y})", SystemFonts.CaptionFont, Brushes.Black, pnt);
            }
            Point topLeft = new Point(minX, maxY);
            Size size = new Size(maxX - minX, maxY - minY);
            return;
            UpdateNodeRectangles();
            var activeNodes = GetActiveNodesInOrder();
            List<Rectangle> shrinkedAndCenteredSquares = [];
            foreach (Node node in _nodes)
            {
                Color color = activeNodes.Contains(node) ? Color.Green : Color.LightGray;
                using (var b = new SolidBrush(color))
                {
                    Rectangle shrinked = ShrinkRectangle(node.Rectangle, 30);
                    Rectangle centerSquare = GetCenterSquare(shrinked);
                    shrinkedAndCenteredSquares.Add(centerSquare);
                    g.FillEllipse(b, centerSquare);
                    using (Pen p = new Pen(Color.Red))
                    {
                        var triangle = GetTriangle(centerSquare);
                        var test = GetPolygonBounds([new Point(0,0),new Point(80,30)]);
                        g.DrawRectangle(Pens.Brown,test);
                        using (Matrix m = new Matrix())
                        {
                            //g.DrawRectangle(Pens.Black, centerSquare);
                            g.DrawPolygon(Pens.Orange, triangle);
                            g.ResetTransform();
                        }
                    }
                }
            }
            if (_input != null && _input.Length > 1)
            {

                for (int i = 0; i < activeNodes.Length - 1; i++)
                {
                    Random rnd = new Random();
                    Color cl = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255));
                    using (var p = new Pen(cl, shrinkedAndCenteredSquares[0].Height / 7))
                    {
                        var active = activeNodes[i];
                        g.DrawLine(p, GetCenterPoint(active.Rectangle), GetCenterPoint(activeNodes[i + 1].Rectangle));
                    }
                }
            }
            return;
            using (var b = new SolidBrush(Color.Black))
            {
                int count = 1;
                var nodeFont = GetRelativeFont(100, FontStyle.Bold);
                foreach (var point in shrinkedAndCenteredSquares)
                {
                    StringFormat format = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                    };
                    g.DrawString(count++.ToString(), nodeFont, b, point, format);
                }
                textBoxOutput.Font = GetRelativeFont(125, FontStyle.Regular);
                labelAlert.Font = GetRelativeFont(80, FontStyle.Italic);
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {

        }
    }
}
