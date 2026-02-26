using System.Reflection;

namespace PathTyper
{
    internal static class DrawingUtil
    {
        internal static void EnableDoubleBuffer(Control control)
        {
            if (control != null)
            {
                typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(control, true, null);
            }
        }
        internal static Point GetCenterPoint(Rectangle rct)
        {
            return new Point(rct.X + (rct.Width / 2), rct.Y + (rct.Height / 2));
        }
        internal static Rectangle GetCenterSquare(Rectangle rct)
        {
            int side = Math.Max(15, Math.Min(rct.Width, rct.Height));
            int x = rct.X + ((rct.Width - side) / 2);
            int y = rct.Y + ((rct.Height - side) / 2);
            return new Rectangle(x, y, side, side);
        }
        internal static Rectangle ShrinkRectangle(Rectangle r, float percent)
        {
            int newWidth = (int)(r.Width * (percent / 100));
            int newHeight = (int)(r.Height * (percent / 100));
            int x = r.X + ((r.Width - newWidth) / 2);
            int y = r.Y + ((r.Height - newHeight) / 2);
            return new Rectangle(x, y, newWidth, newHeight);
        }
        internal static Rectangle GetCellBounds(TableLayoutPanel panel, int column, int row)
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
        internal static Point[] GetLargesTinscribedEquilateralTriangle(Rectangle rct)
        {
            return [new Point(rct.X + rct.Width / 2, rct.Y), new Point(rct.X, rct.Y + rct.Height), new Point(rct.X + rct.Width, rct.Y + rct.Height)];
        }
        internal static Rectangle GetLargestInscribedSquare(Rectangle bounds)
        {
            double a = bounds.Width / 2.0;
            double b = bounds.Height / 2.0;
            double numerator = a * a * b * b;
            double denominator = a * a + b * b;
            double d = Math.Sqrt(numerator / denominator);
            double side = 2.0 * d;
            double centerX = bounds.Left + a;
            double centerY = bounds.Top + b;
            int x = (int)Math.Round(centerX - d);
            int y = (int)Math.Round(centerY - d);
            int s = (int)Math.Round(side);
            return new Rectangle(x, y, s, s);
        }
        internal static Rectangle GetPolygonBounds(Point[] polygon)
        {
            if (polygon == null)
            {
                throw new ArgumentNullException(nameof(polygon));
            }

            if (polygon.Length == 0)
            {
                throw new ArgumentException("Polygon must contain at least one point.", nameof(polygon));
            }
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
            Point topLeft = new Point(minX, minY);
            Size size = new Size(maxX - minX, maxY - minY);
            return new Rectangle(topLeft, size);
        }
        public static Triangle[] FindLargestTriangleInEllipse(RectangleF bounds)
        {
            float a = bounds.Width / 2f;  // semi-major axis
            float b = bounds.Height / 2f; // semi-minor axis
            float cx = bounds.Left + a;
            float cy = bounds.Top + b;

            // Upright triangle: top at 90°, base at 210° and 330°
            float angleTop = (float)Math.PI / 2f;           // 90°
            float angleBaseLeft = 7f * (float)Math.PI / 6f; // 210°
            float angleBaseRight = 11f * (float)Math.PI / 6f; // 330°

            PointF A = new PointF(cx + a * (float)Math.Cos(angleTop), cy - b * (float)Math.Sin(angleTop)); // top
            PointF B = new PointF(cx + a * (float)Math.Cos(angleBaseLeft), cy - b * (float)Math.Sin(angleBaseLeft)); // left base
            PointF C = new PointF(cx + a * (float)Math.Cos(angleBaseRight), cy - b * (float)Math.Sin(angleBaseRight)); // right base

            return triangle;
        }
    }
}
