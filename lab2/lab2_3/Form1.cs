﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2_3
{
    public partial class Form1 : Form
    {
        Bitmap image;
        Bitmap imHSV;
        double[][] hsv_im;
        double[][] hsv_im_ideal;
        //int [][] hsv_im = new int [][3];
        int trackBarH = 0;
        int trackBarS = 0;
        int trackBarV = 0;

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
        }

        public void Task3()
        {
            int iw = image.Width;
            int ih = image.Height;
            hsv_im = new double[iw * ih][];
            hsv_im_ideal = new double[iw * ih][];

            imHSV = new Bitmap(iw, ih);
            int cnt = 0;
            for (int x = 0; x < iw; x++)
            {
                for (int y = 0; y < ih; y++)
                {
                    Color pixelColor = ((Bitmap)image).GetPixel(x, y);
                    double r = pixelColor.R / 255.0;
                    double g = pixelColor.G / 255.0;
                    double b = pixelColor.B / 255.0;


                    double max = Math.Max(r, Math.Max(g, b));
                    double min = Math.Min(r, Math.Min(g, b));
                    double h = 0, s = 0, v = max;

                    double d = max - min;
                    s = max == 0 ? 0 : d / max;

                    if (max == min)
                        h = 0;
                    else
                    {
                        if (max == r)
                            h = 60 * (g - b) / d + (g < b ? 360 : 0);
                        else if (max == g)
                            h = 60 * (b - r) / d + 120;
                        else if (max == b)
                            h = 60* (r - g) / d + 240;

                        
                    }



                    //hsv_im[cnt] = (hue, saturation, value);
                    hsv_im[cnt] = new double[] { h, s, v };
                    hsv_im_ideal[cnt] = new double[] { h, s, v };

                    Color convertedPixel = BackToRGB(h, s, v);
                    imHSV.SetPixel(x, y, convertedPixel);
                    cnt++;
                }
            }

            pictureBox2.Image = imHSV;
        }

     

        Color BackToRGB(double h, double s, double v)
        {
            double r = 0, g = 0, b = 0;

            if (s == 0)
                r = g = b = v;
            else
            {
                int i = (int)Math.Floor(h / 60) % 6;
                double f = h / 60 - (int)Math.Floor(h / 60);
                double p = v * (1 - s);
                double q = v * (1 - f * s);
                double t = v * (1 - (1 - f) * s);

                switch (i)
                {
                    case 0:
                        r = v;
                        g = t;
                        b = p;
                        break;
                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;
                    case 2:
                        r = p;
                        g = v;
                        b = t;
                        break;
                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;
                    case 4:
                        r = t;
                        g = p;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = p;
                        b = q;
                        break;
                }
            }

            int red = (int)(r * 255);
            int green = (int)(g * 255);
            int blue = (int)(b * 255);

            return Color.FromArgb(red, green, blue);
        }


        void moveTrackBar1(int h, int s, int v)
        {
            int cnt = 0;

            for (int i = 0; i < hsv_im.Length; i++)
            {
                hsv_im[cnt][0] = hsv_im_ideal[cnt][0] + h ; 
                cnt++;
            }

            Bitmap newImg = new Bitmap(image.Width, image.Height);
            cnt = 0;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color newPixelColor = BackToRGB(hsv_im[cnt][0], hsv_im[cnt][1], hsv_im[cnt][2]);
                    newImg.SetPixel(x, y, newPixelColor);
                    cnt++;
                }
            }

            pictureBox2.Image = newImg;

        }

        void moveTrackBar2(int h, int s, int v)
        {
            int cnt = 0;

            for (int i = 0; i < hsv_im.Length; i++)
            {
                hsv_im[cnt][1] = Math.Min(1, Math.Max(0, hsv_im_ideal[cnt][1] + s / 100.0));//Math.Min(1, Math.Max(0, s / 100.0)) ;
                cnt++;

            }

            Bitmap newImg = new Bitmap(image.Width, image.Height);
            cnt = 0;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color newPixelColor = BackToRGB(hsv_im[cnt][0], hsv_im[cnt][1], hsv_im[cnt][2]);
                    newImg.SetPixel(x, y, newPixelColor);
                    cnt++;
                }
            }

            pictureBox2.Image = newImg;

        }

        void moveTrackBar3(int h, int s, int v)
        {
            int cnt = 0;

            for (int i = 0; i < hsv_im.Length; i++)
            {
                hsv_im[cnt][2] = Math.Min(1, Math.Max(0, hsv_im_ideal[cnt][2] + (v ) / 100.0));
                cnt++;

            }

            Bitmap newImg = new Bitmap(image.Width, image.Height);
            cnt = 0;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color newPixelColor = BackToRGB(hsv_im[cnt][0], hsv_im[cnt][1], hsv_im[cnt][2]);
                    newImg.SetPixel(x, y, newPixelColor);
                    cnt++;
                }
            }

            pictureBox2.Image = newImg;

        }



        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            moveTrackBar1(trackBar1.Value, trackBar2.Value, trackBar3.Value);
            trackBarH = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            moveTrackBar2(trackBar1.Value, trackBar2.Value, trackBar3.Value);
            trackBarS = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            moveTrackBar3(trackBar1.Value, trackBar2.Value, trackBar3.Value);
            trackBarV = trackBar3.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Image Files|.bmp;.jpg;.jpeg;.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(openFileDialog.FileName);
                pictureBox1.Image = image;

                Task3();
                button2.Enabled = true;
                trackBar1.Value = 0;
                trackBar2.Value = 0;
                trackBar3.Value = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image.Save("newim.jpg");
        }
    }
}

