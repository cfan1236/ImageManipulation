namespace ImageManipulation
{
	partial class Form1
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.btnOpen = new System.Windows.Forms.Button();
			this.btnToBinary = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnToBinary2 = new System.Windows.Forms.Button();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnRevise = new System.Windows.Forms.Button();
			this.btnSave2 = new System.Windows.Forms.Button();
			this.btnRevise2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(12, 22);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(311, 375);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// btnOpen
			// 
			this.btnOpen.Location = new System.Drawing.Point(760, 45);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(110, 50);
			this.btnOpen.TabIndex = 1;
			this.btnOpen.Text = "打开图片灰度化";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// btnToBinary
			// 
			this.btnToBinary.Location = new System.Drawing.Point(683, 132);
			this.btnToBinary.Name = "btnToBinary";
			this.btnToBinary.Size = new System.Drawing.Size(110, 50);
			this.btnToBinary.TabIndex = 1;
			this.btnToBinary.Text = "二值化(otsu阀值)";
			this.btnToBinary.UseVisualStyleBackColor = true;
			this.btnToBinary.Click += new System.EventHandler(this.btnToBinary_Click);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(683, 323);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(110, 54);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "保存图片";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnToBinary2
			// 
			this.btnToBinary2.Location = new System.Drawing.Point(859, 132);
			this.btnToBinary2.Name = "btnToBinary2";
			this.btnToBinary2.Size = new System.Drawing.Size(110, 50);
			this.btnToBinary2.TabIndex = 1;
			this.btnToBinary2.Text = "二值化(迭代法)";
			this.btnToBinary2.UseVisualStyleBackColor = true;
			this.btnToBinary2.Click += new System.EventHandler(this.btnToBinary2_Click);
			// 
			// pictureBox2
			// 
			this.pictureBox2.Location = new System.Drawing.Point(332, 22);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(325, 375);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 0;
			this.pictureBox2.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(690, 205);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "耗时:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(866, 205);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "耗时:";
			// 
			// btnRevise
			// 
			this.btnRevise.Location = new System.Drawing.Point(683, 243);
			this.btnRevise.Name = "btnRevise";
			this.btnRevise.Size = new System.Drawing.Size(110, 54);
			this.btnRevise.TabIndex = 1;
			this.btnRevise.Text = "倾斜矫正";
			this.btnRevise.UseVisualStyleBackColor = true;
			this.btnRevise.Click += new System.EventHandler(this.btnRevise_Click);
			// 
			// btnSave2
			// 
			this.btnSave2.Location = new System.Drawing.Point(859, 323);
			this.btnSave2.Name = "btnSave2";
			this.btnSave2.Size = new System.Drawing.Size(110, 54);
			this.btnSave2.TabIndex = 1;
			this.btnSave2.Text = "保存图片";
			this.btnSave2.UseVisualStyleBackColor = true;
			this.btnSave2.Click += new System.EventHandler(this.btnSave2_Click);
			// 
			// btnRevise2
			// 
			this.btnRevise2.Location = new System.Drawing.Point(859, 243);
			this.btnRevise2.Name = "btnRevise2";
			this.btnRevise2.Size = new System.Drawing.Size(110, 54);
			this.btnRevise2.TabIndex = 1;
			this.btnRevise2.Text = "倾斜矫正";
			this.btnRevise2.UseVisualStyleBackColor = true;
			this.btnRevise2.Click += new System.EventHandler(this.btnRevise2_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1002, 437);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnRevise2);
			this.Controls.Add(this.btnRevise);
			this.Controls.Add(this.btnSave2);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnToBinary2);
			this.Controls.Add(this.btnToBinary);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.pictureBox1);
			this.Name = "Form1";
			this.Text = "图像基本操作";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Button btnToBinary;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnToBinary2;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnRevise;
		private System.Windows.Forms.Button btnSave2;
		private System.Windows.Forms.Button btnRevise2;
	}
}

