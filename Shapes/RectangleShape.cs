using System.Drawing;
using System.Drawing.Drawing2D;

public class RectangleShape : Shape
{
    public new int Width { get; set; }
    public new int Height { get; set; }

    public RectangleShape(Color fillColor, Color borderColor, int borderWidth, Point location, int width, int height) : base(fillColor, borderColor, borderWidth, location)
    {
        Width = width;
        Height = height;
    }

    public override void Draw(Graphics g)
    {
        using (var pen = new Pen(BorderColor, BorderWidth))
        {
            g.DrawRectangle(pen, Location.X, Location.Y, Width, Height);
        }
        using (var brush = new SolidBrush(FillColor))
        {
            g.FillRectangle(brush, Location.X, Location.Y, Width, Height);
        }
    }

    public override bool ContainsPoint(Point point)
    {
        return point.X >= Location.X && point.X <= Location.X + Width &&
               point.Y >= Location.Y && point.Y <= Location.Y + Height;
    }

    public override void Resize(Point newLocation)
    {
        Width = newLocation.X - Location.X;
        Height = newLocation.Y - Location.Y;
    }

    public override void DrawSelectionOutline(Graphics g)
    {
        // Рисуем контур выделения
        using (Pen outlinePen = new Pen(Color.Red, 2))
        {
            outlinePen.DashStyle = DashStyle.Dash;
            g.DrawRectangle(outlinePen, Location.X - 2, Location.Y - 2, Width + 4, Height + 4);
        }

        // Рисуем маркеры на углах
        using (SolidBrush markerBrush = new SolidBrush(Color.Red))
        {
            g.FillRectangle(markerBrush, Location.X - 3, Location.Y - 3, 6, 6);
            g.FillRectangle(markerBrush, Location.X + Width - 3, Location.Y - 3, 6, 6);
            g.FillRectangle(markerBrush, Location.X - 3, Location.Y + Height - 3, 6, 6);
            g.FillRectangle(markerBrush, Location.X + Width - 3, Location.Y + Height - 3, 6, 6);
        }
    }

    public override PointF GetCenter()
    {
        return new PointF(Location.X + Width / 2f, Location.Y + Height / 2f);
    }
}