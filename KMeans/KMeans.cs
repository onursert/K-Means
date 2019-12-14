using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace KMeans
{
    public partial class KMeans : Form
    {
        Graphics graphics;
        Pen pen;
        Point point;

        public KMeans()
        {
            InitializeComponent();

            graphics = pictureBox1.CreateGraphics();
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            pen = new Pen(Color.Black, 3);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Drawing coordinate system.
            Image image = new Bitmap(1040, 1040);
            pictureBox1.Image = image;
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);
            Font font = new Font("Calibri", 10);
            graphics.Transform = new Matrix();
            Matrix matrix = new Matrix();
            //Vertical
            for (int i = 0; i <= 1000; i += 50)
            {
                graphics.DrawString((i / 10).ToString(), font, Brushes.Red, 5, (i - 10));

                graphics.DrawLine(pen, -5, i, 5, i);
                graphics.DrawLine(pen, i, -5, i, 5);
            }
            //Horizontal
            matrix.Rotate(90, MatrixOrder.Prepend);
            graphics.Transform = matrix;
            for (int i = 0; i <= 1000; i += 50)
            {
                graphics.DrawString((i / 10).ToString(), font, Brushes.Red, 5, (-i - 10));
            }
        }

        Random random = new Random();

        int[] redXY = new[] { 0, 0 };
        int[] greenXY = new[] { 0, 0 };
        int[] blueXY = new[] { 0, 0 };
        int[] yellowXY = new[] { 0, 0 };
        int[] purpleXY = new[] { 0, 0 };

        int[] redXYForCheck = new[] { 0, 0 };
        int[] greenXYForCheck = new[] { 0, 0 };
        int[] blueXYForCheck = new[] { 0, 0 };
        int[] yellowXYForCheck = new[] { 0, 0 };
        int[] purpleXYForCheck = new[] { 0, 0 };

        List<int[]> item = new List<int[]>();

        List<int[]> redPoint = new List<int[]>();
        List<int[]> greenPoint = new List<int[]>();
        List<int[]> bluePoint = new List<int[]>();
        List<int[]> yellowPoint = new List<int[]>();
        List<int[]> purplePoint = new List<int[]>();
        private void buttonStart_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();

            redXY = new[] { 0, 0 };
            greenXY = new[] { 0, 0 };
            blueXY = new[] { 0, 0 };
            yellowXY = new[] { 0, 0 };
            purpleXY = new[] { 0, 0 };

            redXYForCheck = new[] { 0, 0 };
            greenXYForCheck = new[] { 0, 0 };
            blueXYForCheck = new[] { 0, 0 };
            yellowXYForCheck = new[] { 0, 0 };
            purpleXYForCheck = new[] { 0, 0 };

            redPoint = new List<int[]>();
            greenPoint = new List<int[]>();
            bluePoint = new List<int[]>();
            yellowPoint = new List<int[]>();
            purplePoint = new List<int[]>();

            step1();

            Thread.Sleep(2000);

            step2();

            Thread.Sleep(2000);
            pictureBox1.Refresh();

            step3();

            while (!checkIfSameList())
            {
                Thread.Sleep(300);
                pictureBox1.Refresh();

                step3();
            }
        }
        public bool checkIfSameList()
        {
            var redBool = redXY.Except(redXYForCheck).ToList();
            var greenBool = greenXY.Except(greenXYForCheck).ToList();
            var blueBool = blueXY.Except(blueXYForCheck).ToList();
            var yellowBool = yellowXY.Except(yellowXYForCheck).ToList();
            var purpleBool = purpleXY.Except(purpleXYForCheck).ToList();

            return !redBool.Any() && !greenBool.Any() && !blueBool.Any() && !yellowBool.Any() && !purpleBool.Any();
        }

        public void step1()
        {
            //Step 1
            //Drawing items.
            item.Clear();
            for (int i = 0; i < Convert.ToInt16(numericUpDownC.Value); i++)
            {
                int[] coordinates = new int[] { 0, 0 };
                coordinates[0] = random.Next(0, 1000);
                coordinates[1] = random.Next(0, 500);

                graphics.DrawEllipse(new Pen(Brushes.Black, 3), coordinates[0], coordinates[1], 5, 5);
                item.Add(coordinates);
            }
        }
        public void step2()
        {
            //Step 2
            //Drawing random k means points.
            for (int k = 0; k < Convert.ToInt16(numericUpDownK.Value); k++)
            {
                if (k == 0)
                {
                    redXY[0] = random.Next(0, 1000);
                    redXY[1] = random.Next(0, 500);

                    graphics.DrawEllipse(new Pen(Brushes.DarkRed, 3), redXY[0], redXY[1], 20, 20);
                }
                else if (k == 1)
                {
                    greenXY[0] = random.Next(0, 1000);
                    greenXY[1] = random.Next(0, 500);

                    graphics.DrawEllipse(new Pen(Brushes.DarkGreen, 3), greenXY[0], greenXY[1], 20, 20);
                }
                else if (k == 2)
                {
                    blueXY[0] = random.Next(0, 1000);
                    blueXY[1] = random.Next(0, 500);

                    graphics.DrawEllipse(new Pen(Brushes.DarkBlue, 3), blueXY[0], blueXY[1], 20, 20);
                }
                else if (k == 3)
                {
                    yellowXY[0] = random.Next(0, 1000);
                    yellowXY[1] = random.Next(0, 500);

                    graphics.DrawEllipse(new Pen(Brushes.Gold, 3), yellowXY[0], yellowXY[1], 20, 20);
                }
                else if (k == 4)
                {
                    purpleXY[0] = random.Next(0, 1000);
                    purpleXY[1] = random.Next(0, 500);

                    graphics.DrawEllipse(new Pen(Brushes.Indigo, 3), purpleXY[0], purpleXY[1], 20, 20);
                }
            }
        }
        public void step3()
        {
            redXYForCheck = redXY;
            greenXYForCheck = greenXY;
            blueXYForCheck = blueXY;
            yellowXYForCheck = yellowXY;
            purpleXYForCheck = purpleXY;

            redPoint = new List<int[]>();
            greenPoint = new List<int[]>();
            bluePoint = new List<int[]>();
            yellowPoint = new List<int[]>();
            purplePoint = new List<int[]>();

            //For every item, calculate distance between items and random k points. Find min. distance and draw again.
            for (int i = 0; i < item.Count; i++)
            {
                List<double> distance = new List<double>();

                for (int k = 0; k < Convert.ToInt16(numericUpDownK.Value); k++)
                {
                    if (k == 0)
                    {
                        distance.Add(calculateDistance(item[i][0], item[i][1], redXY[0], redXY[1]));
                    }
                    else if (k == 1)
                    {
                        distance.Add(calculateDistance(item[i][0], item[i][1], greenXY[0], greenXY[1]));
                    }
                    else if (k == 2)
                    {
                        distance.Add(calculateDistance(item[i][0], item[i][1], blueXY[0], blueXY[1]));
                    }
                    else if (k == 3)
                    {
                        distance.Add(calculateDistance(item[i][0], item[i][1], yellowXY[0], yellowXY[1]));
                    }
                    else if (k == 4)
                    {
                        distance.Add(calculateDistance(item[i][0], item[i][1], purpleXY[0], purpleXY[1]));
                    }
                }

                if (distance.Count > 0 && distance.Min() == distance[0])
                {
                    graphics.DrawEllipse(new Pen(Brushes.Red, 3), item[i][0], item[i][1], 5, 5);
                    redPoint.Add(item[i]);
                }
                else if (distance.Count > 1 && distance.Min() == distance[1])
                {
                    graphics.DrawEllipse(new Pen(Brushes.Green, 3), item[i][0], item[i][1], 5, 5);
                    greenPoint.Add(item[i]);
                }
                else if (distance.Count > 2 && distance.Min() == distance[2])
                {
                    graphics.DrawEllipse(new Pen(Brushes.Blue, 3), item[i][0], item[i][1], 5, 5);
                    bluePoint.Add(item[i]);
                }
                else if (distance.Count > 3 && distance.Min() == distance[3])
                {
                    graphics.DrawEllipse(new Pen(Brushes.Yellow, 3), item[i][0], item[i][1], 5, 5);
                    yellowPoint.Add(item[i]);
                }
                else if (distance.Count > 4 && distance.Min() == distance[4])
                {
                    graphics.DrawEllipse(new Pen(Brushes.Purple, 3), item[i][0], item[i][1], 5, 5);
                    purplePoint.Add(item[i]);
                }
            }

            for (int k = 0; k < Convert.ToInt16(numericUpDownK.Value); k++)
            {
                if (k == 0 && redPoint.Count > 0)
                {
                    int x = 0;
                    int y = 0;
                    for (int i = 0; i < redPoint.Count; i++)
                    {
                        x += redPoint[i][0];
                        y += redPoint[i][1];
                    }
                    x = x / redPoint.Count;
                    y = y / redPoint.Count;
                    graphics.DrawEllipse(new Pen(Brushes.DarkRed, 3), x, y, 20, 20);

                    redXY = new[] { x, y };
                }
                if (k == 1 && greenPoint.Count > 0)
                {
                    int x = 0;
                    int y = 0;
                    for (int i = 0; i < greenPoint.Count; i++)
                    {
                        x += greenPoint[i][0];
                        y += greenPoint[i][1];
                    }
                    x = x / greenPoint.Count;
                    y = y / greenPoint.Count;
                    graphics.DrawEllipse(new Pen(Brushes.DarkGreen, 3), x, y, 20, 20);

                    greenXY = new[] { x, y };
                }
                else if (k == 2 && bluePoint.Count > 0)
                {
                    int x = 0;
                    int y = 0;
                    for (int i = 0; i < bluePoint.Count; i++)
                    {
                        x += bluePoint[i][0];
                        y += bluePoint[i][1];
                    }
                    x = x / bluePoint.Count;
                    y = y / bluePoint.Count;
                    graphics.DrawEllipse(new Pen(Brushes.DarkBlue, 3), x, y, 20, 20);

                    blueXY = new[] { x, y };
                }
                else if (k == 3 && yellowPoint.Count > 0)
                {
                    int x = 0;
                    int y = 0;
                    for (int i = 0; i < yellowPoint.Count; i++)
                    {
                        x += yellowPoint[i][0];
                        y += yellowPoint[i][1];
                    }
                    x = x / yellowPoint.Count;
                    y = y / yellowPoint.Count;
                    graphics.DrawEllipse(new Pen(Brushes.Gold, 3), x, y, 20, 20);

                    yellowXY = new[] { x, y };
                }
                else if (k == 4 && purplePoint.Count > 0)
                {
                    int x = 0;
                    int y = 0;
                    for (int i = 0; i < purplePoint.Count; i++)
                    {
                        x += purplePoint[i][0];
                        y += purplePoint[i][1];
                    }
                    x = x / purplePoint.Count;
                    y = y / purplePoint.Count;
                    graphics.DrawEllipse(new Pen(Brushes.Indigo, 3), x, y, 20, 20);

                    purpleXY = new[] { x, y };
                }
            }
        }
        public double calculateDistance(int itemX, int itemY, int kPointX, int kPointY)
        {
            double x2 = Math.Pow(itemX - kPointX, 2);
            double y2 = Math.Pow(itemY - kPointY, 2);
            double squareRoot = Math.Sqrt(x2 + y2);
            return squareRoot;
        }
    }
}
