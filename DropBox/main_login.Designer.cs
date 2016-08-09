namespace DropBox
{
    partial class main_login
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
            this.panel_center = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_back = new System.Windows.Forms.PictureBox();
            this.panel_center.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_back)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_center
            // 
            this.panel_center.BackColor = System.Drawing.Color.Transparent;
            this.panel_center.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_center.Controls.Add(this.button1);
            this.panel_center.Controls.Add(this.pictureBox1);
            this.panel_center.ForeColor = System.Drawing.Color.Transparent;
            this.panel_center.Location = new System.Drawing.Point(375, 272);
            this.panel_center.Name = "panel_center";
            this.panel_center.Size = new System.Drawing.Size(265, 180);
            this.panel_center.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::DropBox.Properties.Resources._1_login;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(21, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(219, 59);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::DropBox.Properties.Resources._01_logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(5, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(265, 121);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox_back
            // 
            this.pictureBox_back.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_back.BackgroundImage = global::DropBox.Properties.Resources._2_bg_50_50;
            this.pictureBox_back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_back.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_back.Name = "pictureBox_back";
            this.pictureBox_back.Size = new System.Drawing.Size(1010, 750);
            this.pictureBox_back.TabIndex = 1;
            this.pictureBox_back.TabStop = false;
            // 
            // main_login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1010, 750);
            this.Controls.Add(this.panel_center);
            this.Controls.Add(this.pictureBox_back);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "main_login";
            this.Text = "main_login";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel_center.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_back)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_center;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox_back;
    }
}