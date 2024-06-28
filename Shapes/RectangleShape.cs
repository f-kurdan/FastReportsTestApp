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
}