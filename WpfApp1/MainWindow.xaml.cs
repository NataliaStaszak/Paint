using Emgu.CV;
using Emgu.CV.XPhoto;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.IO.Enumeration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1;
using Point = System.Windows.Point;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        private bool isDrawing = false;
        private bool isRubbering = false;
        private Point startPoint;
        public static SolidColorBrush? currentColor { get; set; }
        private double currentSize = 2;
        private bool isDrawClicked=true;
        private bool isPointClicked = false;
        private bool isLineClicked = false;
        private bool isCircleClicked = false;
        private bool isRectangleClicked = false;
        private bool isSquareClicked = false;
        private bool isRubberClicked = false;

        public MainWindow()
        {
            InitializeComponent();
            currentColor =System.Windows.Media.Brushes.Black;
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
            if (isRubberClicked){
                currentColor = System.Windows.Media.Brushes.White;
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    startPoint = e.GetPosition(canvas);
                    isRubbering = true;
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
            if (isRubbering)
            {
                Point endPoint = e.GetPosition(canvas);

                Line line = new Line
                {
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = endPoint.X,
                    Y2 = endPoint.Y,
                    Stroke = System.Windows.Media.Brushes.White,
                    StrokeThickness = currentSize
                };

                canvas.Children.Add(line);

                startPoint = endPoint;
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            isRubbering = false;
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
        private void pictureButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 newWindow = new Window2();
            newWindow.Show();
        }
        private void rubberButton_Click(object sender, RoutedEventArgs e)
        {
            setAllFalse();
            isRubberClicked = true;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string pictureSrc="";
            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Title = "Wybierz obraz";
            openFileDialog.Filter = "JPG (*.jpg)|*.jpg|PNG (*.png)|*.png";
            openFileDialog.DefaultExt = "PNG";

            if (openFileDialog.ShowDialog() == true)
            {
                pictureSrc = openFileDialog.FileName;
            }
            if (pictureSrc !="")
            {
                Rect bounds = VisualTreeHelper.GetDescendantBounds(canvas);
                double dpi = 96d;

                RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);

                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(canvas);
                    dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
                }

                rtb.Render(dv);

                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

                try
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();

                    pngEncoder.Save(ms);
                    ms.Close();

                    System.IO.File.WriteAllBytes(pictureSrc, ms.ToArray());
                    MessageBox.Show("Zapisano");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            
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
            isRubberClicked = false;
        } 



    }
}
