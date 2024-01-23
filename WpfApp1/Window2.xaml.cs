using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }
        private string pictureSrc;
        private BitmapImage bitmapPicture;
        private Image<Gray, Single> imageFilt;

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Obrazy|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Wszystkie pliki|*.*";
            openFileDialog.Title = "Wybierz obraz";

            if (openFileDialog.ShowDialog() == true)
            {
                pictureSrc = openFileDialog.FileName;
            }
            else
            {
                Console.WriteLine("Anulowano wybór pliku.");
            }
            bitmapPicture=new BitmapImage(new Uri(pictureSrc, UriKind.RelativeOrAbsolute));
            
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = bitmapPicture;

            plainPicture.Fill= imageBrush;
            srcLabel.Content= String.Format("src: {0}", pictureSrc);
        }
        private void mapButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            if(pictureSrc!=null)
            {
                Image<Bgr, byte> img1 = new Image<Bgr, byte>(pictureSrc);
                imageFilt = img1.Convert<Gray, Single>();


                ImageBrush imageBrush = new ImageBrush(Bitmap2BitmapImage(imageFilt.ToBitmap()));
                filtredPicture.Fill = imageBrush;

                CvInvoke.WaitKey(0);
            }
            
        }
        private void filterSobButton_Click(object sender, RoutedEventArgs e)
        {
            if (pictureSrc != null)
            {
                Image<Bgr, byte> img1 = new Image<Bgr, byte>(pictureSrc);
                Image<Gray, byte> imageBW = img1.Convert<Gray, byte>();
                imageFilt = (imageBW.Sobel(1, 0, 5));

                ImageBrush imageBrush = new ImageBrush(Bitmap2BitmapImage(imageFilt.ToBitmap()));
                filtredPicture.Fill = imageBrush;


                CvInvoke.WaitKey(0);
            }
            
            
        }

        private void ShowImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (pictureSrc != null)
            {
                CvInvoke.Imshow("Image",imageFilt);
                CvInvoke.WaitKey(0);

            }
                

        }

        private BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();
            return bi;
        }



    }
       
    
}
