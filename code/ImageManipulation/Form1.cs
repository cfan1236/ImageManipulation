using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ImageManipulation
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void btnOpen_Click(object sender, EventArgs e)
		{

			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "打开图片文件";
			ofd.Filter = "图片|*.png;*.jpeg;*.bmp;*.jpg";
			var ofd_result = ofd.ShowDialog();
			if (ofd_result == DialogResult.OK)
			{
				pictureBox1.Load(ofd.FileName);
				pictureBox2.Load(ofd.FileName);
			}
			var img = pictureBox1.Image;
			var result = ToGray(new Bitmap(img));
			pictureBox1.Image = result;
			pictureBox1.Refresh();
			pictureBox2.Image = result;
			pictureBox2.Refresh();
		}

		/// <summary>
		/// 图像灰度化
		/// </summary>
		/// <param name="bmp"></param>
		/// <returns></returns>
		public static Bitmap ToGray(Bitmap bmp)
		{
			for (int i = 0; i < bmp.Width; i++)
			{
				for (int j = 0; j < bmp.Height; j++)
				{
					//获取该点的像素的RGB的颜色
					Color color = bmp.GetPixel(i, j);
					//利用公式计算灰度值
					int gray = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
					Color newColor = Color.FromArgb(gray, gray, gray);
					bmp.SetPixel(i, j, newColor);
				}
			}
			return bmp;
		}

		/// <summary>
		/// 二值化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnToBinary_Click(object sender, EventArgs e)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var img = pictureBox1.Image;
			var result = OtsuThreshold(new Bitmap(img));
			sw.Stop();
			pictureBox1.Image = result;
			label1.Text = "耗时:" + sw.ElapsedMilliseconds + "ms";


		}
		/// <summary>
		/// 二值化操作 迭代法
		/// </summary>
		/// <param name="bmp"></param>
		/// <returns></returns>
		public static Bitmap ConvertTo2Bpp2(Bitmap bmp)
		{
			int[] histogram = new int[256];
			int minGrayValue = 255, maxGrayValue = 0;
			//求取直方图
			for (int i = 0; i < bmp.Width; i++)
			{
				for (int j = 0; j < bmp.Height; j++)
				{
					Color pixelColor = bmp.GetPixel(i, j);
					histogram[pixelColor.R]++;
					if (pixelColor.R > maxGrayValue) maxGrayValue = pixelColor.R;
					if (pixelColor.R < minGrayValue) minGrayValue = pixelColor.R;
				}
			}
			//迭代计算阀值
			int threshold = -1;
			int newThreshold = (minGrayValue + maxGrayValue) / 2;
			for (int iterationTimes = 0; threshold != newThreshold && iterationTimes < 100; iterationTimes++)
			{
				threshold = newThreshold;
				int lP1 = 0;
				int lP2 = 0;
				int lS1 = 0;
				int lS2 = 0;
				//求两个区域的灰度的平均值
				for (int i = minGrayValue; i < threshold; i++)
				{
					lP1 += histogram[i] * i;
					lS1 += histogram[i];
				}
				int mean1GrayValue = (lP1 / lS1);
				for (int i = threshold + 1; i < maxGrayValue; i++)
				{
					lP2 += histogram[i] * i;
					lS2 += histogram[i];
				}
				int mean2GrayValue = (lP2 / lS2);
				newThreshold = (mean1GrayValue + mean2GrayValue) / 2;
			}

			//计算二值化
			for (int i = 0; i < bmp.Width; i++)
			{
				for (int j = 0; j < bmp.Height; j++)
				{
					Color pixelColor = bmp.GetPixel(i, j);
					if (pixelColor.R > threshold) bmp.SetPixel(i, j, Color.FromArgb(255, 255, 255));
					else bmp.SetPixel(i, j, Color.FromArgb(0, 0, 0));
				}
			}
			return bmp;
		}

		/// <summary>
		/// 二值化otsu
		/// </summary>
		/// <param name="bitmap"></param>
		/// <returns></returns>
		public Bitmap OtsuThreshold(Bitmap bitmap)
		{
			// 图像灰度化   
			// b = Gray(b);   
			int width = bitmap.Width;
			int height = bitmap.Height;
			byte threshold = 0;
			int[] hist = new int[256];

			int AllPixelNumber = 0, PixelNumberSmall = 0, PixelNumberBig = 0;
			double MaxValue, AllSum = 0, SumSmall = 0, SumBig, ProbabilitySmall, ProbabilityBig, Probability;
			BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			unsafe
			{
				byte* p = (byte*)bmpData.Scan0;
				int offset = bmpData.Stride - width * 4;
				for (int j = 0; j < height; j++)
				{
					for (int i = 0; i < width; i++)
					{
						hist[p[0]]++;
						p += 4;
					}
					p += offset;
				}
				bitmap.UnlockBits(bmpData);
			}
			//计算灰度为I的像素出现的概率   
			for (int i = 0; i < 256; i++)
			{
				AllSum += i * hist[i];     //   质量矩   
				AllPixelNumber += hist[i];  //  质量       
			}
			MaxValue = -1.0;
			for (int i = 0; i < 256; i++)
			{
				PixelNumberSmall += hist[i];
				PixelNumberBig = AllPixelNumber - PixelNumberSmall;
				if (PixelNumberBig == 0)
				{
					break;
				}

				SumSmall += i * hist[i];
				SumBig = AllSum - SumSmall;
				ProbabilitySmall = SumSmall / PixelNumberSmall;
				ProbabilityBig = SumBig / PixelNumberBig;
				Probability = PixelNumberSmall * ProbabilitySmall * ProbabilitySmall + PixelNumberBig * ProbabilityBig * ProbabilityBig;
				if (Probability > MaxValue)
				{
					MaxValue = Probability;
					threshold = (byte)i;
				}
			}
			this.Threshoding(bitmap, threshold);
			bitmap = twoBit(bitmap);
			return bitmap; ;
		}

		public Bitmap twoBit(Bitmap srcBitmap)
		{
			int midrgb = Color.FromArgb(128, 128, 128).ToArgb();
			int stride;//简单公式((width/8)+3)&(~3)
			stride = (srcBitmap.Width % 8) == 0 ? (srcBitmap.Width / 8) : (srcBitmap.Width / 8) + 1;
			stride = (stride % 4) == 0 ? stride : ((stride / 4) + 1) * 4;
			int k = srcBitmap.Height * stride;
			byte[] buf = new byte[k];
			int x = 0, ab = 0;
			for (int j = 0; j < srcBitmap.Height; j++)
			{
				k = j * stride;//因图像宽度不同、有的可能有填充字节需要跳越
				x = 0;
				ab = 0;
				for (int i = 0; i < srcBitmap.Width; i++)
				{
					//从灰度变单色（下法如果直接从彩色变单色效果不太好，不过反相也可以在这里控制）
					if ((srcBitmap.GetPixel(i, j)).ToArgb() > midrgb)
					{
						ab = ab * 2 + 1;
					}
					else
					{
						ab = ab * 2;
					}
					x++;
					if (x == 8)
					{
						buf[k++] = (byte)ab;//每字节赋值一次，数组buf中存储的是十进制。
						ab = 0;
						x = 0;
					}
				}
				if (x > 0)
				{
					//循环实现：剩余有效数据不满1字节的情况下须把它们移往字节的高位部分
					for (int t = x; t < 8; t++) ab = ab * 2;
					buf[k++] = (byte)ab;
				}
			}
			int width = srcBitmap.Width;
			int height = srcBitmap.Height;
			Bitmap dstBitmap = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
			BitmapData dt = dstBitmap.LockBits(new Rectangle(0, 0, dstBitmap.Width, dstBitmap.Height), ImageLockMode.ReadWrite, dstBitmap.PixelFormat);
			Marshal.Copy(buf, 0, dt.Scan0, buf.Length);
			dstBitmap.UnlockBits(dt);
			return dstBitmap;
		}
		public Bitmap Threshoding(Bitmap b, byte threshold)
		{
			int width = b.Width;
			int height = b.Height;
			BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			unsafe
			{
				byte* p = (byte*)data.Scan0;
				int offset = data.Stride - width * 4;
				byte R, G, B, gray;
				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						R = p[2];
						G = p[1];
						B = p[0];
						gray = (byte)((R * 19595 + G * 38469 + B * 7472) >> 16);
						if (gray >= threshold)
						{
							p[0] = p[1] = p[2] = 255;
						}
						else
						{
							p[0] = p[1] = p[2] = 0;
						}
						p += 4;
					}
					p += offset;
				}
				b.UnlockBits(data);
				return b;

			}

		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			SaveFileDialog save = new SaveFileDialog();
			if (save.ShowDialog() == DialogResult.OK)
			{
				var img = pictureBox1.Image;
				img.Save(save.FileName);
			}
			MessageBox.Show("保存成功");
		}
		private void btnToBinary2_Click(object sender, EventArgs e)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var img = pictureBox2.Image;
			var result = ConvertTo2Bpp2(new Bitmap(img));
			sw.Stop();
			pictureBox2.Image = result;
			label2.Text = "耗时:" + sw.ElapsedMilliseconds + "ms";
		}

		private void btnRevise_Click(object sender, EventArgs e)
		{
			var img = new Bitmap(pictureBox1.Image);
			gmseDeskew sk = new gmseDeskew(img);
			double skewangle = sk.GetSkewAngle();
			Bitmap bmpOut = RotateImage(img, -skewangle);
			pictureBox1.Image = bmpOut;

		}


		/// <summary>
		/// 图像旋转
		/// </summary>
		/// <param name="bmp"></param>
		/// <param name="angle"></param>
		/// <returns></returns>
		private Bitmap RotateImage(Bitmap bmp, double angle)
		{
			Graphics g = null;
			Bitmap tmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppRgb);
			tmp.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
			g = Graphics.FromImage(tmp);
			try
			{
				g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
				g.RotateTransform((float)angle);
				g.DrawImage(bmp, 0, 0);
			}
			finally
			{
				g.Dispose();
			}
			return tmp;
		}

		private void btnSave2_Click(object sender, EventArgs e)
		{
			SaveFileDialog save = new SaveFileDialog();
			if (save.ShowDialog() == DialogResult.OK)
			{
				var img = pictureBox2.Image;
				img.Save(save.FileName);
			}
			MessageBox.Show("保存成功");
		}

		private void btnRevise2_Click(object sender, EventArgs e)
		{
			var img = new Bitmap(pictureBox2.Image);

			gmseDeskew sk = new gmseDeskew(img);
			double skewangle = sk.GetSkewAngle();
			Bitmap bmpOut = RotateImage(img, -skewangle);

			pictureBox2.Image = bmpOut;
		}
	}
}
