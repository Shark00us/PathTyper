using System.Drawing.Drawing2D;
using static PathTyper.DrawingUtil;
namespace PathTyper
{
    internal static class GraphicsExtention
    {
        internal static void RotateAndDrawPolygon(this Graphics g,Point[] polygon, float f)
        {
            using (Matrix mtrx = new Matrix())
            {
                Point center = GetCenterPoint(GetPolygonBounds(polygon));
                mtrx.RotateAt(f, center);
                g.Transform = mtrx;
                g.DrawPolygon(Pens.Orange, polygon);
                g.ResetTransform();
            }
        }
        internal static void DrawPoint(this Graphics g,Brush brush,Point p)
        {
            Size size = new Size(6, 6);
            Point topLeft = new Point(p.X - (size.Width / 2), p.Y - (size.Height / 2));
            g.FillEllipse(brush, new Rectangle(topLeft, size));
            g.DrawString($"({p.X},{p.Y})", SystemFonts.CaptionFont, brush, p);
        }
    }
}
