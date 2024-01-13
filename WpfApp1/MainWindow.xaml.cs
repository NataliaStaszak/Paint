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

        public MainWindow()
        {
            InitializeComponent();
            currentColor = Brushes.Black;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isDrawClicked == true){
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    startPoint = e.GetPosition(canvas);
                    isDrawing = true;
                }
            }
            if (isPointClicked == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    double x = e.GetPosition(canvas).X;
                    double y = e.GetPosition(canvas).Y;


                    DrawPoint(x, y, currentSize, currentColor);
                }
            }
            if (isLineClicked == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    isDrawing = true;
                    double x = e.GetPosition(canvas).X;
                    double y = e.GetPosition(canvas).Y;


                    DrawPoint(x, y, currentSize, currentColor);
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
        
        private void point(object sender, RoutedEventArgs e)
        {
            isDrawClicked = false;
            isLineClicked = false;
            isPointClicked = true;
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

        private void line(object sender, RoutedEventArgs e)
        {
            isDrawClicked = false;
            isLineClicked = true;
            isPointClicked = false;
        }
        private void draw(object sender, RoutedEventArgs e)
        {
            isDrawClicked = true;
            isLineClicked = false;
            isPointClicked = false;
        }
        private void custom(object sender, RoutedEventArgs e)
        {
            Window1 secondWindow = new Window1();
            secondWindow.Show();
        }


    }
}
