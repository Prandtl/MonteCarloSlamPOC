namespace MonteCarloSlamPOC
{
	partial class TestField
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.drawingBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.drawingBox)).BeginInit();
			this.SuspendLayout();
			// 
			// drawingBox
			// 
			this.drawingBox.Location = new System.Drawing.Point(12, 12);
			this.drawingBox.Name = "drawingBox";
			this.drawingBox.Size = new System.Drawing.Size(760, 510);
			this.drawingBox.TabIndex = 0;
			this.drawingBox.TabStop = false;
			// 
			// TestField
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(788, 536);
			this.Controls.Add(this.drawingBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "TestField";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.drawingBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox drawingBox;
	}
}

