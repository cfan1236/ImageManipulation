using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulation
{
	public class gmseDeskew
	{
		public class HougLine
		{
			// Count of points in the line.
			public int Count;
			// Index in Matrix.
			public int Index;
			// The line is represented as all x,y that solve y*cos(alpha)-x*sin(alpha)=d 
			public double Alpha;
			public double d;
		}
		Bitmap cBmp;
		double cAlphaStart = -20;
		double cAlphaStep = 0.2;
		int cSteps = 40 * 5;
		double[] cSinA;
		double[] cCosA;
		double cDMin;
		double cDStep = 1;
		int cDCount;
		int[] cHMatrix;
		public double GetSkewAngle()
		{
			gmseDeskew.HougLine[] hl = null;
			int i = 0;
			double sum = 0;
			int count = 0;
			// Hough Transformation 

			Calc();
			// Top 20 of the detected lines in the image.
			hl = GetTop(20);
			// Average angle of the lines
			for (i = 0; i <= 19; i++)
			{
				sum += hl[i].Alpha;
				count += 1;
			}
			return sum / count;
		}
		private HougLine[] GetTop(int Count)
		{
			HougLine[] hl = null;
			int i = 0;
			int j = 0;
			HougLine tmp = null;
			int AlphaIndex = 0; 
			int dIndex = 0;
			hl = new HougLine[Count + 1];
			for (i = 0; i <= Count - 1; i++)
			{
				hl[i] = new HougLine();
			}
			for (i = 0; i <= cHMatrix.Length - 1; i++)
			{
				if (cHMatrix[i] > hl[Count - 1].Count)
				{
					hl[Count - 1].Count = cHMatrix[i];
					hl[Count - 1].Index = i;
					j = Count - 1;
					while (j > 0 && hl[j].Count > hl[j - 1].Count)
					{
						tmp = hl[j];
						hl[j] = hl[j - 1];
						hl[j - 1] = tmp; j -= 1;
					}
				}
			}
			for (i = 0; i <= Count - 1; i++)
			{
				dIndex = hl[i].Index / cSteps;
				AlphaIndex = hl[i].Index - dIndex * cSteps;
				hl[i].Alpha = GetAlpha(AlphaIndex);
				hl[i].d = dIndex + cDMin;
			}
			return hl;
		}
		public gmseDeskew(Bitmap bmp)
		{
			cBmp = bmp;
		}
		private void Calc()
		{
			int x = 0;
			int y = 0;
			int hMin = cBmp.Height / 4;
			int hMax = cBmp.Height * 3 / 4;
			Init();
			for (y = hMin; y <= hMax; y++)
			{
				for (x = 1; x <= cBmp.Width - 2; x++)
				{    // Only lower edges are considered.
					if (IsBlack(x, y))
					{
						if (!IsBlack(x, y + 1))
						{
							Calc(x, y);
						}
					}
				}
			}
		}
		private void Calc(int x, int y)
		{
			int alpha = 0;
			double d = 0;
			int dIndex = 0;
			int Index = 0;
			for (alpha = 0; alpha <= cSteps - 1; alpha++)
			{
				d = y * cCosA[alpha] - x * cSinA[alpha];
				dIndex = (int)CalcDIndex(d);
				Index = dIndex * cSteps + alpha;
				try
				{
					cHMatrix[Index] += 1;
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.ToString());
				}
			}
		}
		private double CalcDIndex(double d)
		{
			return Convert.ToInt32(d - cDMin);
		}
		private bool IsBlack(int x, int y)
		{
			Color c = default(Color);
			double luminance = 0;
			c = cBmp.GetPixel(x, y);
			luminance = (c.R * 0.299) + (c.G * 0.587) + (c.B * 0.114);
			return luminance < 140;
		}
		private void Init()
		{
			int i = 0;
			double angle = 0;
			// Precalculation of sin and cos. 
			cSinA = new double[cSteps];
			cCosA = new double[cSteps];
			for (i = 0; i <= cSteps - 1; i++)
			{
				angle = GetAlpha(i) * Math.PI / 180.0;
				cSinA[i] = Math.Sin(angle);
				cCosA[i] = Math.Cos(angle);
			}  // Range of d: 
			cDMin = -cBmp.Width;
			cDCount = (int)(2 * (cBmp.Width + cBmp.Height) / cDStep);
			cHMatrix = new int[cDCount * cSteps + 1];
		}
		public double GetAlpha(int Index)
		{
			return cAlphaStart + Index * cAlphaStep;
		}


	}

}
