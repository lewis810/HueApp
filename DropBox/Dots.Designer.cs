namespace DropBox
{
    partial class Dots
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
            this.panel_analysis_dot_main = new System.Windows.Forms.Panel();
            this.panel_analysis_dots_right = new System.Windows.Forms.Panel();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_analysis_dots_left = new System.Windows.Forms.Panel();
            this.cb_analysis_dots_selectScenario = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_analysis_show_dot = new System.Windows.Forms.Button();
            this.cb_analysis_dots_selectImage = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_analysis_dots_selectTest = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_analysis_picture = new System.Windows.Forms.Panel();
            this.panel_analysis_picture_dot = new System.Windows.Forms.Panel();
            this.panel_analysis_dot_main.SuspendLayout();
            this.panel_analysis_dots_right.SuspendLayout();
            this.panel_analysis_dots_left.SuspendLayout();
            this.panel_analysis_picture.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_analysis_dot_main
            // 
            this.panel_analysis_dot_main.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel_analysis_dot_main.Controls.Add(this.panel_analysis_dots_right);
            this.panel_analysis_dot_main.Controls.Add(this.panel_analysis_dots_left);
            this.panel_analysis_dot_main.Controls.Add(this.panel_analysis_picture);
            this.panel_analysis_dot_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_analysis_dot_main.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_dot_main.Name = "panel_analysis_dot_main";
            this.panel_analysis_dot_main.Size = new System.Drawing.Size(1034, 649);
            this.panel_analysis_dot_main.TabIndex = 5;
            // 
            // panel_analysis_dots_right
            // 
            this.panel_analysis_dots_right.Controls.Add(this.listBox2);
            this.panel_analysis_dots_right.Controls.Add(this.label3);
            this.panel_analysis_dots_right.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_analysis_dots_right.Location = new System.Drawing.Point(764, 0);
            this.panel_analysis_dots_right.Name = "panel_analysis_dots_right";
            this.panel_analysis_dots_right.Size = new System.Drawing.Size(270, 649);
            this.panel_analysis_dots_right.TabIndex = 2;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 18;
            this.listBox2.Location = new System.Drawing.Point(21, 230);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(237, 328);
            this.listBox2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(270, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "세부정보";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_analysis_dots_left
            // 
            this.panel_analysis_dots_left.Controls.Add(this.cb_analysis_dots_selectScenario);
            this.panel_analysis_dots_left.Controls.Add(this.label5);
            this.panel_analysis_dots_left.Controls.Add(this.btn_analysis_show_dot);
            this.panel_analysis_dots_left.Controls.Add(this.cb_analysis_dots_selectImage);
            this.panel_analysis_dots_left.Controls.Add(this.label2);
            this.panel_analysis_dots_left.Controls.Add(this.cb_analysis_dots_selectTest);
            this.panel_analysis_dots_left.Controls.Add(this.label1);
            this.panel_analysis_dots_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_analysis_dots_left.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_dots_left.Name = "panel_analysis_dots_left";
            this.panel_analysis_dots_left.Size = new System.Drawing.Size(249, 649);
            this.panel_analysis_dots_left.TabIndex = 1;
            // 
            // cb_analysis_dots_selectScenario
            // 
            this.cb_analysis_dots_selectScenario.FormattingEnabled = true;
            this.cb_analysis_dots_selectScenario.Location = new System.Drawing.Point(32, 63);
            this.cb_analysis_dots_selectScenario.Name = "cb_analysis_dots_selectScenario";
            this.cb_analysis_dots_selectScenario.Size = new System.Drawing.Size(160, 26);
            this.cb_analysis_dots_selectScenario.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "시나리오선택";
            // 
            // btn_analysis_show_dot
            // 
            this.btn_analysis_show_dot.Location = new System.Drawing.Point(29, 528);
            this.btn_analysis_show_dot.Name = "btn_analysis_show_dot";
            this.btn_analysis_show_dot.Size = new System.Drawing.Size(163, 57);
            this.btn_analysis_show_dot.TabIndex = 6;
            this.btn_analysis_show_dot.Text = "SHOW";
            this.btn_analysis_show_dot.UseVisualStyleBackColor = true;
            this.btn_analysis_show_dot.Click += new System.EventHandler(this.btn_analysis_show_dot_Click_1);
            // 
            // cb_analysis_dots_selectImage
            // 
            this.cb_analysis_dots_selectImage.FormattingEnabled = true;
            this.cb_analysis_dots_selectImage.Location = new System.Drawing.Point(29, 278);
            this.cb_analysis_dots_selectImage.Name = "cb_analysis_dots_selectImage";
            this.cb_analysis_dots_selectImage.Size = new System.Drawing.Size(163, 26);
            this.cb_analysis_dots_selectImage.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "이미지선택";
            // 
            // cb_analysis_dots_selectTest
            // 
            this.cb_analysis_dots_selectTest.FormattingEnabled = true;
            this.cb_analysis_dots_selectTest.Items.AddRange(new object[] {
            "모두보기"});
            this.cb_analysis_dots_selectTest.Location = new System.Drawing.Point(29, 158);
            this.cb_analysis_dots_selectTest.Name = "cb_analysis_dots_selectTest";
            this.cb_analysis_dots_selectTest.Size = new System.Drawing.Size(163, 26);
            this.cb_analysis_dots_selectTest.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "테스트선택";
            // 
            // panel_analysis_picture
            // 
            this.panel_analysis_picture.Controls.Add(this.panel_analysis_picture_dot);
            this.panel_analysis_picture.Location = new System.Drawing.Point(298, 12);
            this.panel_analysis_picture.Name = "panel_analysis_picture";
            this.panel_analysis_picture.Size = new System.Drawing.Size(431, 617);
            this.panel_analysis_picture.TabIndex = 3;
            this.panel_analysis_picture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_analysis_picture_MouseMove);
            // 
            // panel_analysis_picture_dot
            // 
            this.panel_analysis_picture_dot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_analysis_picture_dot.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_picture_dot.Name = "panel_analysis_picture_dot";
            this.panel_analysis_picture_dot.Size = new System.Drawing.Size(431, 617);
            this.panel_analysis_picture_dot.TabIndex = 0;
            this.panel_analysis_picture_dot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_analysis_picture_dot_MouseMove);
            // 
            // Dots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 649);
            this.Controls.Add(this.panel_analysis_dot_main);
            this.Name = "Dots";
            this.Text = "Dots";
            this.panel_analysis_dot_main.ResumeLayout(false);
            this.panel_analysis_dots_right.ResumeLayout(false);
            this.panel_analysis_dots_left.ResumeLayout(false);
            this.panel_analysis_dots_left.PerformLayout();
            this.panel_analysis_picture.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_analysis_dot_main;
        private System.Windows.Forms.Panel panel_analysis_dots_right;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_analysis_dots_left;
        private System.Windows.Forms.ComboBox cb_analysis_dots_selectScenario;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_analysis_show_dot;
        private System.Windows.Forms.ComboBox cb_analysis_dots_selectImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_analysis_dots_selectTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_analysis_picture;
        private System.Windows.Forms.Panel panel_analysis_picture_dot;
    }
}