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
        using (var brush = new SolidBrush(FillColor))
        {
            g.FillEllipse(brush, Location.X, Location.Y, Radius * 2, Radius * 2);
        }
        using (var pen = new Pen(BorderColor, BorderWidth))
        {
            g.DrawEllipse(pen, Location.X, Location.Y, Radius * 2, Radius * 2);
        }
    }

    public override bool ContainsPoint(Point point)
    {
        var center = GetCenter();
        var dx = point.X - center.X;
        var dy = point.Y - center.Y;
        return (dx * dx + dy * dy) <= (Radius * Radius);
    }

    public override void Resize(Point newLocation)
    {
        var center = GetCenter();
        var dx = newLocation.X - center.X;
        var dy = newLocation.Y - center.Y;
        Radius = (int)Math.Sqrt(dx * dx + dy * dy);

        // Обновляем Width и Height
        Width = Radius * 2;
        Height = Radius * 2;
    }

    public override void DrawSelectionOutline(Graphics g)
    {
        using (Pen outlinePen = new Pen(Color.Red, 2))
        {
            outlinePen.DashStyle = DashStyle.Dash;
            g.DrawEllipse(outlinePen, Location.X - 2, Location.Y - 2, (Radius * 2) + 4, (Radius * 2) + 4);
        }

        // Рисуем маркеры на краях круга
        using (SolidBrush markerBrush = new SolidBrush(Color.Red))
        {
            g.FillEllipse(markerBrush, Location.X + Radius - 3, Location.Y - 3, 6, 6); // Верхний
            g.FillEllipse(markerBrush, Location.X + (Radius * 2) - 3, Location.Y + Radius - 3, 6, 6); // Правый
            g.FillEllipse(markerBrush, Location.X + Radius - 3, Location.Y + (Radius * 2) - 3, 6, 6); // Нижний
            g.FillEllipse(markerBrush, Location.X - 3, Location.Y + Radius - 3, 6, 6); // Левый
        }
    }

    public override PointF GetCenter()
    {
        return new PointF(Location.X + Radius, Location.Y + Radius);
    }

    public override void Move(Point delta)
    {
        Location = new Point(Location.X + delta.X, Location.Y + delta.Y);
    }
}