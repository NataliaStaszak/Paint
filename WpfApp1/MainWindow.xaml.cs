using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp1;
using Point = System.Windows.Point;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        private bool isDrawing = false;
        private Point startPoint;
        public static SolidColorBrush? currentColor { get; set; }
        private double currentSize = 2;
        private bool isDrawClicked=true;
        private bool isPointClicked = false;
        private bool isLineClicked = false;
        private bool isCircleClicked = false;
        private bool isRectangleClicked = false;
        private bool isSquareClicked = false;

        public MainWindow()
        {
            InitializeComponent();
            currentColor = Brushes.Black;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isDrawClicked){
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    startPoint = e.GetPosition(canvas);
                    isDrawing = true;
                }
            }
            if (isPointClicked)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    double x = e.GetPosition(canvas).X;
                    double y = e.GetPosition(canvas).Y;


                    DrawPoint(x, y, currentSize, currentColor);
                }
            }
            if (isLineClicked)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    isDrawing = true;
                    double x = e.GetPosition(canvas).X;
                    double y = e.GetPosition(canvas).Y;


                    DrawPoint(x, y, currentSize, currentColor);
                }
            }
            if (isCircleClicked)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    double x = e.GetPosition(canvas).X;
                    double y = e.GetPosition(canvas).Y;


                    drawCircle(x, y, currentSize);
                }
            }
            if (isRectangleClicked)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    double x = e.GetPosition(canvas).X;
                    double y = e.GetPosition(canvas).Y;


                    drawRectangle(x, y, currentSize);
                }
            }
            if (isSquareClicked)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    double x = e.GetPosition(canvas).X;
                    double y = e.GetPosition(canvas).Y;


                    drawSquare(x, y, currentSize);
                }
            }


        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                Point endPoint = e.GetPosition(canvas);

                Line line = new Line
                {
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = endPoint.X,
                    Y2 = endPoint.Y,
                    Stroke = currentColor,
                    StrokeThickness = currentSize
                };

                canvas.Children.Add(line);

                startPoint = endPoint;
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)colorComboBox.SelectedItem;
            currentColor = (SolidColorBrush)selectedItem.Background;
            
        }

        private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentSize = sizeSlider.Value;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }

        private Shape Line(Shape line)
        {
            return new Line { X1 = 0, Y1 = 0, X2 = 50, Y2 = 50, Stroke = currentColor, StrokeThickness = currentSize };
        }
        
        private void DrawPoint(double x, double y, double diameter, Brush color)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = diameter;
            ellipse.Height = diameter;
            ellipse.Fill = color;

            Canvas.SetLeft(ellipse, x - diameter / 2);
            Canvas.SetTop(ellipse, y - diameter / 2);

            canvas.Children.Add(ellipse);
        }
        private void point(object sender, RoutedEventArgs e)
        {
            isDrawClicked = false;
            isLineClicked = false;
            isPointClicked = true;
            isCircleClicked = false;
            isRectangleClicked = false;
            isSquareClicked = false;
        }

        private void line(object sender, RoutedEventArgs e)
        {
            isDrawClicked = false;
            isLineClicked = true;
            isPointClicked = false;
            isCircleClicked = false;
            isRectangleClicked = false;
            isSquareClicked = false;
    }
        private void circle(object sender, RoutedEventArgs e)
        {
            isDrawClicked = false;
            isLineClicked = false;
            isPointClicked = false;
            isCircleClicked = true;
            isRectangleClicked = false;
            isSquareClicked = false;
        }
        private void draw(object sender, RoutedEventArgs e)
        {
            isDrawClicked = true;
            isLineClicked = false;
            isPointClicked = false;
            isRectangleClicked = false;
            isSquareClicked = false;
        }
        private void rectangle(object sender, RoutedEventArgs e)
        {
            isDrawClicked = false;
            isLineClicked = false;
            isPointClicked = false;
            isCircleClicked = false;
            isRectangleClicked = true;
            isSquareClicked = false;
        }
        private void square(object sender, RoutedEventArgs e)
        {
            isDrawClicked = false;
            isLineClicked = false;
            isPointClicked = false;
            isCircleClicked = false;
            isRectangleClicked = false;
            isSquareClicked = true;
        }
        private void custom(object sender, RoutedEventArgs e)
        {
            Window1 secondWindow = new Window1();
            secondWindow.Show();
        }
        private void drawCircle(double x, double y, double radius)
        {
            var bauble = new Ellipse
            {
                Width = radius * 10,
                Height = radius * 10,
                StrokeThickness = 3,
                Stroke = currentColor
                
            };
            Canvas.SetLeft(bauble, x - radius / 2);
            Canvas.SetTop(bauble, y - radius / 2);
            canvas.Children.Add(bauble);
        }
        private void drawRectangle(double x, double y, double radius)
        {
            var bauble = new System.Windows.Shapes.Rectangle
            {
                Width = radius * 30,
                Height = radius * 10,
                StrokeThickness = 3,
                Stroke = currentColor

            };
            Canvas.SetLeft(bauble, x - radius / 2);
            Canvas.SetTop(bauble, y - radius / 2);
            canvas.Children.Add(bauble);
        }
        private void drawSquare(double x, double y, double radius)
        {
            var bauble = new System.Windows.Shapes.Rectangle
            {
                Width = radius * 10,
                Height = radius * 10,
                StrokeThickness = 3,
                Stroke = currentColor

            };
            Canvas.SetLeft(bauble, x - radius / 2);
            Canvas.SetTop(bauble, y - radius / 2);
            canvas.Children.Add(bauble);
        }



    }
}
