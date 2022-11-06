using ImageMagick;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFreader
{
    public partial class Form1 : Form
    {
        Bitmap image;

        public Form1()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(Scroller);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        // Кидаем в программу файл и выводим его.
        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            MagickImageCollection images = new MagickImageCollection();
            string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);
            
            images.Read(path[0]);
            IMagickImage vertical = images.AppendVertically();
            
            vertical.Format = MagickFormat.Png;
            vertical.Write(@"F:\asd1\asd.png");
            image = new Bitmap(@"F:\asd1\asd.png");
            pictureBox1.Image = image;
            pictureBox1.Height = image.Height;

            Browse();
        }

        //прокручиваем скролер
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X, vScrollBar1.Value * (-pictureBox1.Height / vScrollBar1.Maximum));
        }

        //подбираем скролер под файл
        private void Browse()
        {
            vScrollBar1.Maximum = image.Height;
            vScrollBar1.LargeChange = vScrollBar1.Height;
        }

        private void Scroller(object sender, MouseEventArgs e)
        {
            //сдвиг
            int shift = 100;

            if(e.Delta > 0)
            {
                pictureBox1.Height += shift;
                pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - (shift/3));
            }
            else
            {
                pictureBox1.Height -= shift;
                pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + (shift/3));
            }

            Browse();
        }
    }
}
