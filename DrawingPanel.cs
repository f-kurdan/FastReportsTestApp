using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class DrawingPanel : Panel
{
    private List<Shape> _shapes = new List<Shape>();
    private Shape _selectedShape = null;
    private Point _mouseDownPoint;

    public DrawingPanel()
    {
        DoubleBuffered = true; // Включить двойную буферизацию для плавной отрисовки
        MouseDown += HandleMouseDown;
        MouseMove += HandleMouseMove;
        MouseUp += HandleMouseUp;
        Paint += HandlePaint;
    }

    public List<Shape> Shapes { get;  }

    public Shape SelectedShape { get; set; }


    // Добавить фигуру на панель
    public void AddShape(Shape shape)
    {
        _shapes.Add(shape);
        Invalidate(); // Перерисовать панель
    }

    // Удалить фигуру с панели
    public void RemoveShape(Shape shape)
    {
        _shapes.Remove(shape);
        Invalidate(); // Перерисовать панель
    }

    // Очистить панель от всех фигур
    public void ClearShapes()
    {
        _shapes.Clear();
        Invalidate(); // Перерисовать панель
    }

    // Обработчик события отрисовки
    private void HandlePaint(object sender, PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        foreach (var shape in _shapes)
        {
            shape.Draw(e.Graphics);
        }

        DrawConnectionLines(e.Graphics);

        if (SelectedShape != null)
        {
            SelectedShape.DrawSelectionOutline(e.Graphics);
        }
    }

    // Обработчик события нажатия мыши
    private void HandleMouseDown(object sender, MouseEventArgs e)
    {
        _mouseDownPoint = e.Location;

        _selectedShape = null;
        for (int i = _shapes.Count - 1; i >= 0; i--)
        {
            if (_shapes[i].ContainsPoint(e.Location))
            {
                _selectedShape = _shapes[i];
                break;
            }
        }

        Invalidate();
    }

    // Обработчик события движения мыши
    private void HandleMouseMove(object sender, MouseEventArgs e)
    {
        if (_selectedShape != null) // Если фигура выделена
        {
            if (e.Button == MouseButtons.Left) // Если левая кнопка мыши нажата
            {
                // Переместить фигуру
                _selectedShape.Move(new Point(e.Location.X - _mouseDownPoint.X, e.Location.Y - _mouseDownPoint.Y));
                _mouseDownPoint = e.Location; // Обновить точку нажатия
                Invalidate(); // Перерисовать панель
            }
            else if (e.Button == MouseButtons.Right) // Если правая кнопка мыши нажата
            {
                // Изменить размер фигуры
                _selectedShape.Resize(e.Location);
                Invalidate(); // Перерисовать панель
            }
        }
    }

    // Обработчик события отпускания мыши
    private void HandleMouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right && _selectedShape != null)
        {
            // Проверка на возможность соединения с другой фигурой
            foreach (var shape in _shapes)
            {
                if (shape != _selectedShape && shape.ContainsPoint(e.Location))
                {
                    _selectedShape.Connect(shape);
                    Invalidate();
                    break;
                }
            }
        }
    }

    public void SelectShapeAt(Point point)
    {
        SelectedShape = _shapes.FirstOrDefault(shape => shape.ContainsPoint(point));
        Invalidate();
    }

    public void RedrawShapes()
    {
        Invalidate();
    }

    private void DrawConnectionLines(Graphics g)
    {
        if (_shapes.Count < 2) return;

        using (Pen pen = new Pen(Color.Black, 1))
        {
            for (int i = 0; i < _shapes.Count - 1; i++)
            {
                PointF start = _shapes[i].GetCenter();
                PointF end = _shapes[i + 1].GetCenter();
                g.DrawLine(pen, start, end);
            }
        }
    }
}