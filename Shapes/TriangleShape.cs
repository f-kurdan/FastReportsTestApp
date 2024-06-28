using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public class TriangleShape : Shape
{
    public int SideLength { get; set; } // Длина стороны
    public double Angle { get; set; } // Угол поворота

    public TriangleShape(Color fillColor, Color borderColor, int borderWidth, Point location, int sideLength, double angle)
        : base(fillColor, borderColor, borderWidth, location)
    {
        SideLength = sideLength;
        Angle = angle;
        //  Устанавливаем Width и Height для треугольника, чтобы он правильно отображался
        Width = SideLength;
        Height = (int)(SideLength * Math.Sqrt(3) / 2);
    }

    public override void Draw(Graphics g)
    {
        // Вычисляем координаты вершин равностороннего треугольника
        var points = new PointF[3];
        points[0] = new PointF(Location.X + SideLength / 2, Location.Y); // Вершина
        points[1] = new PointF(Location.X, Location.Y + (float)(SideLength * Math.Sqrt(3) / 2)); // Левая нижняя вершина
        points[2] = new PointF(Location.X + SideLength, Location.Y + (float)(SideLength * Math.Sqrt(3) / 2)); // Правая нижняя вершина

        // Поворачиваем треугольник на заданный угол
        g.RotateTransform((float)Angle);

        // Рисуем контур треугольника
        using (var pen = new Pen(BorderColor, BorderWidth))
        {
            g.DrawPolygon(pen, points);
        }

        // Заполняем треугольник цветом
        using (var brush = new SolidBrush(FillColor))
        {
            g.FillPolygon(brush, points);
        }

        // Сбрасываем поворот, чтобы не повлиять на другие отрисовки
        g.ResetTransform();
    }

    public override bool ContainsPoint(Point point)
    {
        // Метод проверки пересечения луча с границами треугольника
        var p1 = new PointF(Location.X + SideLength / 2, Location.Y); // Вершина
        var p2 = new PointF(Location.X, Location.Y + (float)(SideLength * Math.Sqrt(3) / 2)); // Левая нижняя вершина
        var p3 = new PointF(Location.X + SideLength, Location.Y + (float)(SideLength * Math.Sqrt(3) / 2)); // Правая нижняя вершина

        // Проверяем пересечение луча с каждой из сторон треугольника
        var intersections = 0;
        if (IsLineIntersectingSegment(point, new Point(point.X + 1000, point.Y), p1, p2)) intersections++;
        if (IsLineIntersectingSegment(point, new Point(point.X + 1000, point.Y), p2, p3)) intersections++;
        if (IsLineIntersectingSegment(point, new Point(point.X + 1000, point.Y), p3, p1)) intersections++;

        // Если количество пересечений нечетное, то точка находится внутри треугольника
        return intersections % 2 == 1;
    }

    // Метод проверки пересечения отрезков
    private bool IsLineIntersectingSegment(Point p1, Point p2, PointF q1, PointF q2)
    {
        // Вычисляем векторные произведения
        var u = (q2.Y - q1.Y) * (p1.X - q1.X) - (q2.X - q1.X) * (p1.Y - q1.Y);
        var v = (q2.Y - q1.Y) * (p2.X - q1.X) - (q2.X - q1.X) * (p2.Y - q1.Y);

        // Проверяем, находятся ли точки на одной стороне отрезка
        return (u * v <= 0);
    }

    public override void Resize(Point newLocation)
    {
        // Изменяем размер треугольника, сохраняя его равностороннюю форму
        SideLength = (int)Math.Sqrt((newLocation.X - Location.X) * (newLocation.X - Location.X) + (newLocation.Y - Location.Y) * (newLocation.Y - Location.Y));

        // Пересчитываем координаты вершин
        var points = new PointF[3];
        points[0] = new PointF(Location.X + SideLength / 2, Location.Y); // Вершина
        points[1] = new PointF(Location.X, Location.Y + (float)(SideLength * Math.Sqrt(3) / 2)); // Левая нижняя вершина
        points[2] = new PointF(Location.X + SideLength, Location.Y + (float)(SideLength * Math.Sqrt(3) / 2)); // Правая нижняя вершина
    }

    // Переопределяем метод Move для пересчета координат вершин при перемещении
    public override void Move(Point delta)
    {
        base.Move(delta); // Вызываем базовый метод Move для изменения Location

        // Пересчитываем координаты вершин
        var points = new PointF[3];
        points[0] = new PointF(Location.X + SideLength / 2, Location.Y); // Вершина
        points[1] = new PointF(Location.X, Location.Y + (float)(SideLength * Math.Sqrt(3) / 2)); // Левая нижняя вершина
        points[2] = new PointF(Location.X + SideLength, Location.Y + (float)(SideLength * Math.Sqrt(3) / 2)); // Правая нижняя вершина
    }

    public override void DrawSelectionOutline(Graphics g)
    {
        // Вычисляем координаты вершин треугольника
        PointF[] points = new PointF[3];
        points[0] = new PointF(Location.X + SideLength / 2, Location.Y);
        points[1] = new PointF(Location.X, Location.Y + (float)(SideLength * Math.Sqrt(3) / 2));
        points[2] = new PointF(Location.X + SideLength, Location.Y + (float)(SideLength * Math.Sqrt(3) / 2));

        // Рисуем контур выделения
        using (Pen outlinePen = new Pen(Color.Red, 2))
        {
            outlinePen.DashStyle = DashStyle.Dash;
            g.DrawPolygon(outlinePen, points);
        }

        // Рисуем маркеры на вершинах
        using (SolidBrush markerBrush = new SolidBrush(Color.Red))
        {
            foreach (PointF point in points)
            {
                g.FillEllipse(markerBrush, point.X - 3, point.Y - 3, 6, 6);
            }
        }
    }
}