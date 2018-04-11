using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetwork.Problems
{

    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        IntPtr handle;
        Bitmap bitmap; //stores the bitmap
        int width, height; //dimensions of the bitmap
        int[,] yData; //stores luminance data
        int red = 0, blue = 0, green = 0; //stores rgb values
        List<String> images;

        Boolean meow, woof;
        Boolean initialized = false;

        public Form1()
        {
            handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog b = new FolderBrowserDialog();

            if (b.ShowDialog() == DialogResult.OK)
            {
                var folderName = b.SelectedPath;
                GenerateData(folderName);
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {           
            // Show
            ShowWindow(handle, SW_SHOW);

            Problems.HandwrittenDigits.Run();
            Console.ReadLine();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "jpeg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            bitmap = ConvertToBitmap(myStream);
                        }

                        pictureBox1.Image = new Bitmap(bitmap, 200, 300);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void GenerateData(String folder)
        {
            images = GetImagesPath(folder);

            for (int i = 0; i < images.Count; i++)
            {
                Image image = Image.FromFile(images[i]);

                string result = Path.GetFileName(images[i]);

                if (result[0] == 'c')
                {
                    meow = true;
                    woof = false;
                }
                else if (result[0] == 'd')
                {
                    woof = true;
                    meow = false;
                }

                bitmap = ResizeImage(image, 28, 28);
                WriteToCSV(bitmap);
            }
        }
        //converts RGB color space to YCbCr color space
        private void WriteToCSV(Bitmap bitmap)
        {
            width = bitmap.Width;
            height = bitmap.Height;
            yData = new int[width, height]; //luma                   

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);

                    red = pixelColor.R;
                    blue = pixelColor.B;
                    green = pixelColor.G;

                    yData[x, y] = (int)((0.299 * red) + (0.587 * green) + (0.114 * blue));
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color color = new Color();
                    color = Color.FromArgb(255, yData[x, y], yData[x, y], yData[x, y]);

                    bitmap.SetPixel(x, y, color);
                }
            }

            // Write sample data to CSV file
            using (CsvFileWriter writer = new CsvFileWriter("TrainingData.csv", initialized))
            {
                if (!initialized)
                {
                    CsvRow title = new CsvRow();
                    int index = 0;

                    title.Add("label");

                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 1; j < height; j++)
                        {
                            title.Add(String.Format("pixel {0}", index++));
                        }
                    }
                    writer.WriteRow(title);
                    initialized = true;
                }
                CsvRow row = new CsvRow();

                if (meow)
                    row.Add("0");
                else if (woof)
                    row.Add("1");

                for (int i = 0; i < width; i++)
                {
                    for (int j = 1; j < height; j++)
                    {
                        row.Add(String.Format("{0}", yData[i, j]));
                    }
                }
                writer.WriteRow(row);

            }
        }

        public List<String> GetImagesPath(String folderName)
        {

            DirectoryInfo Folder;
            FileInfo[] Images;

            Folder = new DirectoryInfo(folderName);
            Images = Folder.GetFiles();
            List<String> imagesList = new List<String>();

            for (int i = 0; i < Images.Length; i++)
            {
                imagesList.Add(String.Format(@"{0}/{1}", folderName, Images[i].Name));
                // Console.WriteLine(String.Format(@"{0}/{1}", folderName, Images[i].Name));
            }
            return imagesList;
        }

        //converts image to bitmap
        public Bitmap ConvertToBitmap(Stream bmpStream)
        {
            Bitmap bitmap;
            Image image = Image.FromStream(bmpStream);
            bitmap = new Bitmap(image);

            return bitmap;
        }

        // Resize the image to the specified width and height.
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
