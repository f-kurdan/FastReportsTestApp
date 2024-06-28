using System.Drawing;
using System.Drawing.Drawing2D;

public class Circle : Shape
{
    public int Radius { get; set; }

    public Circle(Color fillColor, Color borderColor, int borderWidth, Point location, int radius) : base(fillColor, borderColor, borderWidth, location)
    {
        Radius = radius;
        //  Устанавливаем Width и Height для круга, чтобы он правильно отображался
        Width = Radius * 2;
        Height = Radius * 2;
    }

    public override void Draw(Graphics g)
    {
        using (var pen = new Pen(BorderColor, BorderWidth))
        {
            g.DrawEllipse(pen, Location.X - Radius, Location.Y - Radius, 2 * Radius, 2 * Radius);
        }
        using (var brush = new SolidBrush(FillColor))
        {
            g.FillEllipse(brush, Location.X - Radius, Location.Y - Radius, 2 * Radius, 2 * Radius);
        }
    }

    public override bool ContainsPoint(Point point)
    {
        return (point.X - Location.X) * (point.X - Location.X) + (point.Y - Location.Y) * (point.Y - Location.Y) <= Radius * Radius;
    }

    public override void Resize(Point newLocation)
    {
        Radius = (int)Math.Sqrt((newLocation.X - Location.X) * (newLocation.X - Location.X) + (newLocation.Y - Location.Y) * (newLocation.Y - Location.Y));
    }
}