﻿namespace DropBox
{
    partial class Edit_Image
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tsBar = new System.Windows.Forms.ToolStrip();
            this.tsbtnLine04 = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.link_alloc = new System.Windows.Forms.Button();
            this.panel_image_link = new System.Windows.Forms.Panel();
            this.panel_image_load = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tsBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsBar
            // 
            this.tsBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.tsBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnLine04});
            this.tsBar.Location = new System.Drawing.Point(615, 0);
            this.tsBar.Name = "tsBar";
            this.tsBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsBar.Size = new System.Drawing.Size(24, 654);
            this.tsBar.TabIndex = 3;
            this.tsBar.Text = "tsBar";
            // 
            // tsbtnLine04
            // 
            this.tsbtnLine04.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnLine04.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnLine04.Name = "tsbtnLine04";
            this.tsbtnLine04.Size = new System.Drawing.Size(21, 4);
            this.tsbtnLine04.Text = "toolStripButton1";
            this.tsbtnLine04.ToolTipText = "사각형";
            // 
            // link_alloc
            // 
            this.link_alloc.Location = new System.Drawing.Point(373, 246);
            this.link_alloc.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.link_alloc.Name = "link_alloc";
            this.link_alloc.Size = new System.Drawing.Size(56, 20);
            this.link_alloc.TabIndex = 6;
            this.link_alloc.Text = "링크연동";
            this.link_alloc.UseVisualStyleBackColor = true;
            this.link_alloc.Visible = false;
            this.link_alloc.Click += new System.EventHandler(this.button5_Click);
            // 
            // panel_image_link
            // 
            this.panel_image_link.AutoScroll = true;
            this.panel_image_link.AutoSize = true;
            this.panel_image_link.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_image_link.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_image_link.Location = new System.Drawing.Point(0, 654);
            this.panel_image_link.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel_image_link.Name = "panel_image_link";
            this.panel_image_link.Size = new System.Drawing.Size(615, 0);
            this.panel_image_link.TabIndex = 7;
            this.panel_image_link.Visible = false;
            // 
            // panel_image_load
            // 
            this.panel_image_load.AutoScroll = true;
            this.panel_image_load.BackColor = System.Drawing.Color.Silver;
            this.panel_image_load.Location = new System.Drawing.Point(0, 108);
            this.panel_image_load.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel_image_load.Name = "panel_image_load";
            this.panel_image_load.Size = new System.Drawing.Size(616, 322);
            this.panel_image_load.TabIndex = 8;
            this.panel_image_load.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(243, 481);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(201, 9);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 481);
            this.panel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(615, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 11;
            // 
            // Edit_Image
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(639, 654);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_image_link);
            this.Controls.Add(this.link_alloc);
            this.Controls.Add(this.tsBar);
            this.Controls.Add(this.panel_image_load);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Edit_Image";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tsBar.ResumeLayout(false);
            this.tsBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip tsBar;
        private System.Windows.Forms.ToolStripButton tsbtnLine04;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button link_alloc;
        private System.Windows.Forms.Panel panel_image_link;
        private System.Windows.Forms.Panel panel_image_load;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}
