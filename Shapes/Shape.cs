using System.Drawing;
using System.Collections.Generic;

public abstract class Shape
{
    //цвет заливки примитива
    public Color FillColor { get; set; }
    //цвет окантовки (рамки) примитива
    public Color BorderColor { get; set; }
    public int BorderWidth { get; set; }
    public Point Location { get; set; }
    public bool IsSelected { get; set; }
    public List<Shape> ConnectedShapes { get; } = new List<Shape>();

    //ширина и высота
    public int Width { get; set; }
    public int Height { get; set; }

    public Shape(Color fillColor, Color borderColor, int borderWidth, Point location)
    {
        FillColor = fillColor;
        BorderColor = borderColor;
        BorderWidth = borderWidth;
        Location = location;
    }

    //метод вывода на Graphics;
    public abstract void Draw(Graphics g);
    //метод проверки лежит ли заданная точка внутри примитива;
    public abstract bool ContainsPoint(Point point);
    public abstract void Resize(Point newLocation);

    public virtual void Move(Point delta)
    {
        if (!IsMoved) // Проверка, была ли фигура уже перемещена
        {
            IsMoved = true; // Устанавливаем флаг перемещения

            Location = new Point(Location.X + delta.X, Location.Y + delta.Y);

            foreach (var shape in ConnectedShapes)
            {
                shape.Move(delta); // Вызываем Move для связанных фигур
            }

            IsMoved = false; // Сбрасываем флаг перемещения
        }
    }

    public virtual void ChangeColor(Color newFillColor, Color newBorderColor)
    {
        FillColor = newFillColor;
        BorderColor = newBorderColor;
    }

    public virtual void DrawSelectionOutline(Graphics g)
    {
        using (var pen = new Pen(Color.Red, 2))
        {
            g.DrawRectangle(pen, Location.X - 2, Location.Y - 2, Width + 4, Height + 4);
        }
    }

    // Добавляем свойство IsMoved в класс Shape
    private bool IsMoved { get; set; } = false;

    public void Connect(Shape shape)
    {
        ConnectedShapes.Add(shape);
        shape.ConnectedShapes.Add(this);
    }

    public void Disconnect(Shape shape)
    {
        ConnectedShapes.Remove(shape);
        shape.ConnectedShapes.Remove(this);
    }

    public virtual PointF GetCenter()
    {
        return new PointF(Location.X + Width / 2f, Location.Y + Height / 2f);
    }
}