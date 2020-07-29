using System;
using System.Drawing;
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
using System.Windows.Shapes;
using System.IO;
using System.Runtime.InteropServices;

namespace Map_Painter
{
    /// <summary>
    /// ShowPainter.xaml 的交互逻辑
    /// </summary>
    public partial class ShowPainter : Window
    {
        private System.Windows.Media.Color[,] colors;
        private string[,] info;
        private int row;
        private int colume;
        private int mode;
        private int? sight_row;
        private int? sight_col;
        private int? block_row;
        private int? block_col;
        public ShowPainter(Bitmap pic, int row, int colume, bool flat)
        {
            InitializeComponent();
            this.row = row;
            this.colume = colume;
            int row_adjust = row * 128;
            int col_adjust = colume * 128;
            colors = new System.Windows.Media.Color[col_adjust, row_adjust];
            info = new string[col_adjust, row_adjust];
            double row_i_left, row_i_right , col_j_left, col_j_right, area;
            //int row_i1, row_i2, col_j1, col_j2;
            /*
            for (int i = 0; i < row_adjust; i++)
            {
                row_i = (double)(pic.Width - 1) * (double)i / (double)(row_adjust - 1);
                row_i1 = (int)row_i;
                row_i2 = row_i1 + 1 >= pic.Width ? row_i1 : row_i1 + 1;
                for (int j = 0; j < col_adjust; j++)
                {
                    col_j = (double)(pic.Height - 1) * (double)j / (double)(col_adjust - 1);
                    col_j1 = (int)col_j;
                    col_j2 = col_j1 + 1 >= pic.Height ? col_j1 : col_j1 + 1;
                    System.Drawing.Color i1 = pic.GetPixel(row_i1, col_j1);
                    System.Drawing.Color i2 = pic.GetPixel(row_i2, col_j1);
                    System.Drawing.Color i3 = pic.GetPixel(row_i1, col_j2);
                    System.Drawing.Color i4 = pic.GetPixel(row_i2, col_j2);
                    double row_distance = row_i % 1.0;
                    double col_distance = col_j % 1.0;
                    byte R = (byte)((i1.R * (1 - row_distance) + i3.R * row_distance) * (1 - col_distance) + (i2.R * (1 - row_distance) + i4.R * row_distance) * col_distance);
                    byte G = (byte)((i1.G * (1 - row_distance) + i3.G * row_distance) * (1 - col_distance) + (i2.G * (1 - row_distance) + i4.G * row_distance) * col_distance);
                    byte B = (byte)((i1.B * (1 - row_distance) + i3.B * row_distance) * (1 - col_distance) + (i2.B * (1 - row_distance) + i4.B * row_distance) * col_distance);
                    string info;
                    this.colors[j ,i] = new MinecraftMapColors().FindColor(R, G, B, flat, out info);
                    this.info[j, i] = info;
                }
            }
            */
            double R_total, G_total, B_total;
            for (int i = 0; i < row_adjust; i++)
            {
                row_i_left = pic.Width * (double)i / row_adjust;
                row_i_right = pic.Width * (double)(i+1) / row_adjust;
                for (int j = 0; j < col_adjust; j++)
                {
                    col_j_left = pic.Height * (double)j / col_adjust;
                    col_j_right = pic.Height * (double)(j+1) / col_adjust;
                    area = (row_i_right - row_i_left) * (col_j_right - col_j_left);
                    R_total = .0;
                    G_total = .0;
                    B_total = .0;
                    for (int ii = (int)Math.Floor(row_i_left); ii < Math.Ceiling(row_i_right); ii++)
                    {
                        double left = ii >= row_i_left ? ii : row_i_left;
                        double right = ii + 1 <= row_i_right ? ii + 1 : row_i_right;
                        for (int jj = (int)Math.Floor(col_j_left); jj < Math.Ceiling(col_j_right); jj++)
                        {
                            double bottom = jj >= col_j_left ? jj : col_j_left;
                            double top = jj + 1 <= col_j_right ? jj + 1 : col_j_right;
                            System.Drawing.Color pixel = pic.GetPixel(ii, jj);
                            R_total += pixel.R * (right - left) * (top - bottom);
                            G_total += pixel.G * (right - left) * (top - bottom);
                            B_total += pixel.B * (right - left) * (top - bottom);
                        }
                    }
                    byte R = (byte)(R_total / area);
                    byte G = (byte)(G_total / area);
                    byte B = (byte)(B_total / area);
                    colors[j, i] = new MinecraftMapColors().FindColor(R, G, B, flat, out string info);
                    this.info[j, i] = info;
                }
            }
            if (row == 1 && colume == 1)
            {
                Switch_1(0, 0);
            }
            else
            {
                Switch_0();
            }
        }

        private void Switch_0()
        {
            sight_row = null;
            sight_col = null;
            block_row = null;
            block_col = null;
            int row_adjust = row * 128;
            int col_adjust = colume * 128;
            double top = row_adjust > col_adjust ? 512.0 * (row_adjust - col_adjust) / row_adjust / 2 : 0;
            double left = col_adjust > row_adjust ? 512.0 * (col_adjust - row_adjust) / col_adjust / 2 : 0;
            double width = row_adjust > col_adjust ? 512.0 / (double)row_adjust : 512.0 / (double)col_adjust;
            double BigWidth = width * 128.0;
            byte[] bytes = new byte[3 * row_adjust * col_adjust];
            paint.Children.Clear();
            for (int i = 0; i < col_adjust; i++)
            {
                for (int j = 0; j < row_adjust; j++)
                {
                    bytes[3 * row_adjust * col_adjust - (i * row_adjust + (row_adjust - j - 1)) * 3 - 1]= this.colors[i, j].R;
                    bytes[3 * row_adjust * col_adjust - (i * row_adjust + (row_adjust - j - 1)) * 3 - 2] = this.colors[i, j].G;
                    bytes[3 * row_adjust * col_adjust - (i * row_adjust + (row_adjust - j - 1)) * 3 - 3] = this.colors[i, j].B;
                }
            }

            int stride = row_adjust * 3;
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            int scan0 = (int)handle.AddrOfPinnedObject();
            scan0 += (col_adjust - 1) * stride;
            Bitmap bitmap = new Bitmap(row_adjust, col_adjust, -stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, (IntPtr)scan0);
            handle.Free();

            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            MemoryStream Ms = new MemoryStream();
            bitmap.Save(Ms, System.Drawing.Imaging.ImageFormat.Bmp);
            Ms.Position = 0;
            BitmapImage ObjBitmapImage = new BitmapImage();
            ObjBitmapImage.BeginInit();
            ObjBitmapImage.StreamSource = Ms;
            ObjBitmapImage.EndInit();
            img.Source = ObjBitmapImage;
            img.Width = 512;
            img.Height = 512;
            img.Stretch = Stretch.Uniform;
            paint.Children.Add(img);
            for (int i = 0; i < colume; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    System.Windows.Shapes.Rectangle pixel = new System.Windows.Shapes.Rectangle();
                    pixel.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 255, 255, 255));
                    pixel.Width = BigWidth;
                    pixel.Height = BigWidth;
                    pixel.SetValue(Canvas.LeftProperty, left + j * BigWidth);
                    pixel.SetValue(Canvas.TopProperty, top + i * BigWidth);
                    pixel.SetValue(Panel.ZIndexProperty, 1);
                    pixel.MouseEnter += RectangleEnter;
                    pixel.MouseMove += RectangleEnter;
                    pixel.MouseLeave += RectangleLeave;
                    pixel.MouseLeftButtonUp += RectanglePress;
                    paint.Children.Add(pixel);
                }
            }
            this.mode = 0;
        }

        private void Switch_1(int row, int col)
        {
            sight_row = row;
            sight_col = col;
            block_row = null;
            block_col = null;
            int BigWidth = 64;
            byte[] bytes = new byte[49152];
            paint.Children.Clear();
            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 128; j++)
                {
                    /*
                    System.Windows.Shapes.Rectangle pixel = new System.Windows.Shapes.Rectangle();
                    pixel.Fill = new SolidColorBrush(this.colors[col * 128 + i, row * 128 + j]);
                    pixel.Stroke = new SolidColorBrush(this.colors[col * 128 + i, row * 128 + j]);
                    pixel.Width = width;
                    pixel.Height = width;
                    pixel.SetValue(Canvas.LeftProperty, j * width + 0.0);
                    pixel.SetValue(Canvas.TopProperty, i * width + 0.0);
                    paint.Children.Add(pixel);
                    */
                    bytes[49151 - (i * 128 + (127 - j)) * 3] = this.colors[row * 128 + i, col * 128 + j].R;
                    bytes[49150 - (i * 128 + (127 - j)) * 3] = this.colors[row * 128 + i, col * 128 + j].G;
                    bytes[49149 - (i * 128 + (127 - j)) * 3] = this.colors[row * 128 + i, col * 128 + j].B;
                }
            }

            int stride = 384;
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            int scan0 = (int)handle.AddrOfPinnedObject();
            scan0 += 127 * stride;
            Bitmap bitmap = new Bitmap(128, 128, -stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, (IntPtr)scan0);
            handle.Free();

            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            MemoryStream Ms = new MemoryStream();
            bitmap.Save(Ms, System.Drawing.Imaging.ImageFormat.Bmp);
            Ms.Position = 0;
            BitmapImage ObjBitmapImage = new BitmapImage();
            ObjBitmapImage.BeginInit();
            ObjBitmapImage.StreamSource = Ms;
            ObjBitmapImage.EndInit();
            img.Source = ObjBitmapImage;
            img.Width = 512;
            img.Height = 512;
            img.Stretch = Stretch.Uniform;
            paint.Children.Add(img);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    System.Windows.Shapes.Rectangle pixel = new System.Windows.Shapes.Rectangle();
                    pixel.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 255, 255, 255));
                    pixel.Width = BigWidth;
                    pixel.Height = BigWidth;
                    pixel.SetValue(Canvas.LeftProperty, j * BigWidth + 0.0);
                    pixel.SetValue(Canvas.TopProperty, i * BigWidth + 0.0);
                    pixel.SetValue(Panel.ZIndexProperty, 1);
                    pixel.MouseEnter += RectangleEnter;
                    pixel.MouseMove += RectangleEnter;
                    pixel.MouseLeave += RectangleLeave;
                    pixel.MouseLeftButtonUp += RectanglePress;
                    paint.Children.Add(pixel);
                }
            }
            this.mode = 1;
        }

        private void Switch_2(int row, int col)
        {
            block_row = row;
            block_col = col;
            int width = 32;
            paint.Children.Clear();
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    System.Windows.Shapes.Rectangle pixel = new System.Windows.Shapes.Rectangle();
                    pixel.Fill = new SolidColorBrush(this.colors[(int)this.sight_row * 128 + row * 16 + i, (int)this.sight_col * 128 + col * 16 + j]);
                    pixel.Stroke = new SolidColorBrush(this.colors[(int)this.sight_row * 128 + row * 16 + i, (int)this.sight_col * 128 + col * 16 + j]);
                    pixel.Width = width;
                    pixel.Height = width;
                    pixel.SetValue(Canvas.LeftProperty, j * width + 0.0);
                    pixel.SetValue(Canvas.TopProperty, i * width + 0.0);
                    paint.Children.Add(pixel);
                }
            }
            this.mode = 2;
        }

        private void RectanglePress(object sender, MouseButtonEventArgs e)
        {
            if(mode == 0)
            {
                int h = int.Parse(row_vis.Text) - 1;
                int l = int.Parse(col_vis.Text) - 1;
                Switch_1(h, l);
            }
            else if(mode == 1)
            {
                int h = int.Parse(row_vis.Text) - 1;
                int l = int.Parse(col_vis.Text) - 1;
                Switch_2(h, l);
            }
        }

        private void Backward(object sender, RoutedEventArgs e)
        {
            if(mode == 0 || (mode == 1 && row == 1 && colume == 1))
            {
                Close();
            }
            else if(mode == 1)
            {
                Switch_0();
            }
            else if(mode == 2)
            {
                Switch_1((int)sight_row, (int)sight_col);
            }
        }

        private void RectangleLeave(object sender, MouseEventArgs e)
        {
            System.Windows.Shapes.Rectangle rectangle = (System.Windows.Shapes.Rectangle)sender;
            rectangle.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 255, 255, 255));
            rectangle.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 255, 255, 255));
        }

        private void RectangleEnter(object sender, MouseEventArgs e)
        {
            System.Windows.Shapes.Rectangle rectangle = (System.Windows.Shapes.Rectangle)sender;
            rectangle.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(60, 255, 255, 255));
            rectangle.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(60, 255, 255, 255));
        }

        private void Move(object sender, MouseEventArgs e)
        {
            System.Windows.Point point = e.GetPosition((IInputElement)sender);
            if(mode == 0)
            {
                int row_adjust = row * 128;
                int col_adjust = colume * 128;
                double top = row_adjust > col_adjust ? 512.0 * (row_adjust - col_adjust) / row_adjust / 2 : 0;
                double left = col_adjust > row_adjust ? 512.0 * (col_adjust - row_adjust) / col_adjust / 2 : 0;
                double width = (row_adjust > col_adjust ? 512.0 / (double)row_adjust : 512.0 / (double)col_adjust) * 128.0;
                int h = (int)((point.Y - top) / width) + 1;
                int l = (int)((point.X - left) / width) + 1;
                row_vis.Text = h.ToString();
                col_vis.Text = l.ToString();
            }
            else if(mode == 1)
            {
                int width = 64; 
                int h = (int)(point.Y / width) + 1;
                int l = (int)(point.X / width) + 1;
                row_vis.Text = h.ToString();
                col_vis.Text = l.ToString();
            }
            else if(mode == 2)
            {
                int width = 32;
                int h = (int)(point.Y / width) + 1;
                int l = (int)(point.X / width) + 1;
                row_vis.Text = h.ToString();
                col_vis.Text = l.ToString();
                information.Text = this.info[(int)this.sight_row * 128 + (int)this.block_row * 16 + h - 1, (int)this.sight_col * 128 + (int)this.block_col * 16 + l - 1];
            }
        }

        private void Clear(object sender, MouseEventArgs e)
        {
            row_vis.Text = "";
            col_vis.Text = "";
            if (mode == 2) information.Text = "";
        }
    }
}
