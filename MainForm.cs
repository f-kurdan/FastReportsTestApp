using System;
using System.Drawing;
using System.Windows.Forms;

public partial class MainForm : Form
{
    private DrawingPanel _drawingPanel;

    public MainForm()
    {
        InitializeComponent();

        _drawingPanel = new DrawingPanel();
        _drawingPanel.Dock = DockStyle.Fill;
        Controls.Add(_drawingPanel);
    }

    private void InitializeComponent()
    {
        addRectangleButton_Click = new Button();
        addTriangleButton_Click = new Button();
        addCircleButton_Click = new Button();
        сlearButton_Click = new Button();
        SuspendLayout();
        // 
        // addRectangleButton_Click
        // 
        addRectangleButton_Click.Location = new Point(649, 32);
        addRectangleButton_Click.Name = "addRectangleButton_Click";
        addRectangleButton_Click.Size = new Size(139, 53);
        addRectangleButton_Click.TabIndex = 0;
        addRectangleButton_Click.Text = "Добавить прямоугольник";
        addRectangleButton_Click.UseVisualStyleBackColor = true;
        addRectangleButton_Click.Click += AddRectangleButton_Click;
        // 
        // addTriangleButton_Click
        // 
        addTriangleButton_Click.Location = new Point(649, 91);
        addTriangleButton_Click.Name = "addTriangleButton_Click";
        addTriangleButton_Click.Size = new Size(139, 53);
        addTriangleButton_Click.TabIndex = 1;
        addTriangleButton_Click.Text = "Добавить треугольник\r\n";
        addTriangleButton_Click.UseVisualStyleBackColor = true;
        addTriangleButton_Click.Click += AddTriangleButton_Click;
        // 
        // addCircleButton_Click
        // 
        addCircleButton_Click.Location = new Point(649, 150);
        addCircleButton_Click.Name = "addCircleButton_Click";
        addCircleButton_Click.Size = new Size(139, 53);
        addCircleButton_Click.TabIndex = 2;
        addCircleButton_Click.Text = "Добавить круг";
        addCircleButton_Click.UseVisualStyleBackColor = true;
        addCircleButton_Click.Click += AddCircleButton_Click;
        // 
        // button1
        // 
        сlearButton_Click.Location = new Point(649, 209);
        сlearButton_Click.Name = "сlearButton_Click";
        сlearButton_Click.Size = new Size(139, 53);
        сlearButton_Click.TabIndex = 3;
        сlearButton_Click.Text = "Очистить\r\n";
        сlearButton_Click.UseVisualStyleBackColor = true;
        сlearButton_Click.Click += ClearButton_Click;
        // 
        // MainForm
        // 
        ClientSize = new Size(800, 450);
        Controls.Add(сlearButton_Click);
        Controls.Add(addCircleButton_Click);
        Controls.Add(addTriangleButton_Click);
        Controls.Add(addRectangleButton_Click);
        Name = "MainForm";
        Text = "Графические примитивы";
        ResumeLayout(false);
    }

    private void AddRectangleButton_Click(object? sender, EventArgs e)
    {
        var rectangle = new RectangleShape(Color.Blue, Color.Black, 2, new Point(100, 100), 50, 50);
        _drawingPanel.AddShape(rectangle);
    }

    private void AddCircleButton_Click(object? sender, EventArgs e)
    {
        var circle = new Circle(Color.Green, Color.Black, 2, new Point(200, 200), 30);
        _drawingPanel.AddShape(circle);
    }

    private void AddTriangleButton_Click(object? sender, EventArgs e)
    {
        var triangle = new TriangleShape(Color.Red, Color.Black, 2, new Point(300, 300), 40, 0);
        _drawingPanel.AddShape(triangle);
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
        _drawingPanel.ClearShapes();
    }

    private Button addTriangleButton_Click;
    private Button addCircleButton_Click;
    private Button сlearButton_Click;
    private Button addRectangleButton_Click;
}