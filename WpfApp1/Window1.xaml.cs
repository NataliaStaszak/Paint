using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using WpfApplication1;

namespace WpfApp1
{
    
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        private void countClick(object sender, RoutedEventArgs e)
        {
            String red = RText.Text;
            String green = GText.Text;
            String blue = BText.Text;
            if(isInputValid(red)&& isInputValid(green)&&isInputValid(blue)){
                Color color = Color.FromRgb((byte)Int32.Parse(red), (byte)Int32.Parse(green), (byte)Int32.Parse(blue));
            
                hsvText.Content = RGBtoHSV(int.Parse(red), int.Parse(green), int.Parse(blue));
                SolidColorBrush brush = new SolidColorBrush(color);
                ColorSample.Fill = brush;
                MainWindow.currentColor = brush;
            }
            else     
                MessageBox.Show("Nieprawidlowy format");
        }
        public bool isInputValid(String input) {
            try
            {
                Int32.Parse(input);
                if (Int32.Parse(input) <= 255 && Int32.Parse(input) >= 0)
                    return true;
                return false;
            }
            catch { return false; }
        }

        public static String RGBtoHSV(int red, int green, int blue)
        {
            double r = red / 255.0;
            double g = green / 255.0;
            double b = blue / 255.0;
            
            

            double min = Math.Min(Math.Min(r, g), b);
            double max = Math.Max(Math.Max(r, g), b);
            double delta = max - min;

            // Value
            double value = max;
            

            // Saturation
            double saturation = (max == 0) ? 0 : (delta / max);

            // Hue
            double hue = 0;

            if (delta != 0)
            {
                if (max == r)
                    hue = (g - b) / delta + ((g < b) ? 6 : 0);
                else if (max == g)
                    hue = (b - r) / delta + 2;
                else
                    hue = (r - g) / delta + 4;

                hue *= 60;
                hue %= 360;
            }
            return String.Format("H: {0} S: {0}%, V: {0}%", Math.Round(hue,2), Math.Round(saturation, 2), Math.Round(value, 2));
        }

    }
}
