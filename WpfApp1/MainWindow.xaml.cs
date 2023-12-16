using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Point = System.Windows.Point;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        private bool isDrawing = false;
        private Point startPoint;
        private SolidColorBrush currentColor = Brushes.Black;
        private double currentSize = 2;
        private bool isDrawClicked=true;

        public MainWindow()
        {
            InitializeComponent();
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
        /*private void point(object sender, RoutedEventArgs e)
        {
            //Ellipse elipse = DrawPiont(currentSize, 2,2);
            Ellipse elipse= DrawPiont(1,1,1);
            canvas.Children.Add(elipse);
        }
        private Ellipse DrawPiont(double size, double x, double y){
            Ellipse elipse = new Ellipse();
            elipse.Width = currentSize;
            elipse.Height = currentSize;
            /*Canvas.SetTop(elipse, y - elipse.Height / 2);
            Canvas.SetLeft(elipse, x - elipse.Width / 2);

            Brush brushColor = new SolidColorBrush(currentColor.Color);*/

        //return elipse;
        //}/*
        /*
        private void DrawPoint(double x, double y, double diameter, Brush color)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = diameter;
            ellipse.Height = diameter;
            ellipse.Fill = color;

            Canvas.SetLeft(ellipse, x - diameter / 2);
            Canvas.SetTop(ellipse, y - diameter / 2);

            canvas.Children.Add(ellipse);
        }*/
        private void point(object sender, RoutedEventArgs e)
        {
            isDrawClicked = false;
        }

    }
}
