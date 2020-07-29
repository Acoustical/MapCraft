using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;

namespace Map_Painter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bitmap bitmap;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PhotoImport(object sender, RoutedEventArgs e)
        {
            PhotoScreen.Children.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择数据源文件";
            openFileDialog.Filter = "图片文件(*.jpg, *.gif, *.bmp, *.png) | *.jpg; *.gif; *.bmp; *.png";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "jpg";
            if (openFileDialog.ShowDialog() == false)
            {
                return;
            }
            Bitmap pic = new Bitmap(openFileDialog.FileName);
            this.bitmap = pic;
            GernerateButton.IsEnabled = true;
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            MemoryStream Ms = new MemoryStream();
            pic.Save(Ms, System.Drawing.Imaging.ImageFormat.Bmp);
            Ms.Position = 0;
            BitmapImage ObjBitmapImage = new BitmapImage();
            ObjBitmapImage.BeginInit();
            ObjBitmapImage.StreamSource = Ms;
            ObjBitmapImage.EndInit();
            img.Source = ObjBitmapImage;
            img.Width = 732;
            img.Height = 500;
            img.Stretch = Stretch.Uniform;
            PhotoScreen.Children.Clear();
            RemoveLogicalChild(img);
            PhotoScreen.Children.Add(img);
            double dlst = ((double)pic.Width) / pic.Height;
            double dld = double.MaxValue;
            int fin_i = 1, fin_j = 1;
            for(int i = 1; i < 9; i++)for(int j = 1;j < 9; j++)
                {
                    double dl = Math.Abs(dlst - (double)i / j);
                    if(dl < dld)
                    {
                        dld = dl;
                        fin_i = i;
                        fin_j = j;
                    }
                }
            a.Text = ((int)fin_j).ToString();
            b.Text = ((int)fin_i).ToString();
        }

        private void A_Decrease(object sender, RoutedEventArgs e)
        {
            int value = int.Parse(a.Text);
            if (value == 1) return;
            else a.Text = (value - 1).ToString();
        }

        private void B_Decrease(object sender, RoutedEventArgs e)
        {
            int value = int.Parse(b.Text);
            if (value == 1) return;
            else b.Text = (value - 1).ToString();
        }

        private void A_Increase(object sender, RoutedEventArgs e)
        {
            int value = int.Parse(a.Text);
            a.Text = (value + 1).ToString();
        }

        private void B_Increase(object sender, RoutedEventArgs e)
        {
            int value = int.Parse(b.Text);
            b.Text = (value + 1).ToString();
        }

        private void GerneratePic(object sender, RoutedEventArgs e)
        {
            ShowPainter showPainter = new ShowPainter(this.bitmap, int.Parse(b.Text), int.Parse(a.Text), (bool)flatCheck.IsChecked);
            showPainter.ShowDialog();
        }
    }
}
