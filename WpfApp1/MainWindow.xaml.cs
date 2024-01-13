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
            double x = e.GetPosition(canvas).X;
            double y = e.GetPosition(canvas).Y;
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
                    DrawPoint(x, y);
            }
            if (isLineClicked)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    isDrawing = true;
                    DrawPoint(x, y);
                }
            }
            if (isCircleClicked)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    drawCircle(x, y);
            }
            if (isRectangleClicked)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    drawRectangle(x, y);               
            }
            if (isSquareClicked)
            {
                if (e.LeftButton == MouseButtonState.Pressed)              
                    drawSquare(x, y);                
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

        //Buttons Fun
        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)colorComboBox.SelectedItem;
            currentColor = (SolidColorBrush)selectedItem.Background;
            
        }
        private void customButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 secondWindow = new Window1();
            secondWindow.Show();
        }

        private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentSize = sizeSlider.Value;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }
        private void drawButton_Click(object sender, RoutedEventArgs e)
        {
            setAllFalse();
            isDrawClicked = true;
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            setAllFalse();
            isPointClicked = true;     
        }

        private void lineButton_Click(object sender, RoutedEventArgs e)
        {
            setAllFalse();
            isLineClicked = true;
        }
        private void circleButton_Click(object sender, RoutedEventArgs e)
        {
            setAllFalse();
            isCircleClicked = true;
        }
        
        private void rectangleButton_Click(object sender, RoutedEventArgs e)
        {
            setAllFalse();
            isRectangleClicked = true;
        }
        private void squareButton_Click(object sender, RoutedEventArgs e)
        {
            setAllFalse();
            isSquareClicked = true;
        }

        //drawing fun
        private void DrawPoint(double x, double y)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = currentSize;
            ellipse.Height = currentSize;
            ellipse.Fill = currentColor;

            Canvas.SetLeft(ellipse, x - currentSize / 2);
            Canvas.SetTop(ellipse, y - currentSize / 2);

            canvas.Children.Add(ellipse);
        }
        private void drawCircle(double x, double y)
        {
            var bauble = new Ellipse
            {
                Width = currentSize * 10,
                Height = currentSize * 10,
                StrokeThickness = 3,
                Stroke = currentColor
                
            };
            Canvas.SetLeft(bauble, x - currentSize / 2);
            Canvas.SetTop(bauble, y - currentSize / 2);
            canvas.Children.Add(bauble);
        }
        private void drawRectangle(double x, double y)
        {
            var bauble = new System.Windows.Shapes.Rectangle
            {
                Width = currentSize * 30,
                Height = currentSize * 10,
                StrokeThickness = 3,
                Stroke = currentColor

            };
            Canvas.SetLeft(bauble, x - currentSize / 2);
            Canvas.SetTop(bauble, y - currentSize / 2);
            canvas.Children.Add(bauble);
        }
        private void drawSquare(double x, double y)
        {
            var bauble = new System.Windows.Shapes.Rectangle
            {
                Width = currentSize * 10,
                Height = currentSize * 10,
                StrokeThickness = 3,
                Stroke = currentColor

            };
            Canvas.SetLeft(bauble, x - currentSize / 2);
            Canvas.SetTop(bauble, y - currentSize / 2);
            canvas.Children.Add(bauble);
        }

        //help
        private void setAllFalse()
        {
            isDrawClicked = false;
            isLineClicked = false;
            isPointClicked = false;
            isCircleClicked = false;
            isRectangleClicked = false;
            isSquareClicked = false;
        } 



    }
}
