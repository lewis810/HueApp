namespace DropBox
{
    partial class Partition
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
            this.panel_analysis_partition_main = new System.Windows.Forms.Panel();
            this.panel_analysis_partition_left = new System.Windows.Forms.Panel();
            this.tb_analysis_partition_verti = new System.Windows.Forms.TextBox();
            this.tb_analysis_partition_hori = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cb_analysis_partition_selectScenario = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_analysis_show_partition = new System.Windows.Forms.Button();
            this.cb_analysis_partition_selectImage = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_analysis_partition_selectTest = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel_analysis_partition_picture = new System.Windows.Forms.Panel();
            this.panel_analysis_partition_picture2 = new System.Windows.Forms.Panel();
            this.panel_analysis_partition_main.SuspendLayout();
            this.panel_analysis_partition_left.SuspendLayout();
            this.panel_analysis_partition_picture.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_analysis_partition_main
            // 
            this.panel_analysis_partition_main.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel_analysis_partition_main.Controls.Add(this.panel_analysis_partition_left);
            this.panel_analysis_partition_main.Controls.Add(this.panel_analysis_partition_picture);
            this.panel_analysis_partition_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_analysis_partition_main.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_partition_main.Name = "panel_analysis_partition_main";
            this.panel_analysis_partition_main.Size = new System.Drawing.Size(1650, 1000);
            this.panel_analysis_partition_main.TabIndex = 5;
            // 
            // panel_analysis_partition_left
            // 
            this.panel_analysis_partition_left.Controls.Add(this.tb_analysis_partition_verti);
            this.panel_analysis_partition_left.Controls.Add(this.tb_analysis_partition_hori);
            this.panel_analysis_partition_left.Controls.Add(this.label18);
            this.panel_analysis_partition_left.Controls.Add(this.label17);
            this.panel_analysis_partition_left.Controls.Add(this.label16);
            this.panel_analysis_partition_left.Controls.Add(this.cb_analysis_partition_selectScenario);
            this.panel_analysis_partition_left.Controls.Add(this.label6);
            this.panel_analysis_partition_left.Controls.Add(this.btn_analysis_show_partition);
            this.panel_analysis_partition_left.Controls.Add(this.cb_analysis_partition_selectImage);
            this.panel_analysis_partition_left.Controls.Add(this.label8);
            this.panel_analysis_partition_left.Controls.Add(this.cb_analysis_partition_selectTest);
            this.panel_analysis_partition_left.Controls.Add(this.label9);
            this.panel_analysis_partition_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_analysis_partition_left.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_partition_left.Name = "panel_analysis_partition_left";
            this.panel_analysis_partition_left.Size = new System.Drawing.Size(249, 1000);
            this.panel_analysis_partition_left.TabIndex = 2;
            // 
            // tb_analysis_partition_verti
            // 
            this.tb_analysis_partition_verti.Location = new System.Drawing.Point(79, 503);
            this.tb_analysis_partition_verti.Name = "tb_analysis_partition_verti";
            this.tb_analysis_partition_verti.Size = new System.Drawing.Size(113, 28);
            this.tb_analysis_partition_verti.TabIndex = 13;
            // 
            // tb_analysis_partition_hori
            // 
            this.tb_analysis_partition_hori.Location = new System.Drawing.Point(79, 469);
            this.tb_analysis_partition_hori.Name = "tb_analysis_partition_hori";
            this.tb_analysis_partition_hori.Size = new System.Drawing.Size(113, 28);
            this.tb_analysis_partition_hori.TabIndex = 12;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(29, 507);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 18);
            this.label18.TabIndex = 11;
            this.label18.Text = "세로";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(29, 472);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 18);
            this.label17.TabIndex = 10;
            this.label17.Text = "가로";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(29, 438);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 18);
            this.label16.TabIndex = 9;
            this.label16.Text = "분할";
            // 
            // cb_analysis_partition_selectScenario
            // 
            this.cb_analysis_partition_selectScenario.FormattingEnabled = true;
            this.cb_analysis_partition_selectScenario.Location = new System.Drawing.Point(32, 63);
            this.cb_analysis_partition_selectScenario.Name = "cb_analysis_partition_selectScenario";
            this.cb_analysis_partition_selectScenario.Size = new System.Drawing.Size(160, 26);
            this.cb_analysis_partition_selectScenario.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 18);
            this.label6.TabIndex = 7;
            this.label6.Text = "시나리오선택";
            // 
            // btn_analysis_show_partition
            // 
            this.btn_analysis_show_partition.Location = new System.Drawing.Point(29, 636);
            this.btn_analysis_show_partition.Name = "btn_analysis_show_partition";
            this.btn_analysis_show_partition.Size = new System.Drawing.Size(163, 57);
            this.btn_analysis_show_partition.TabIndex = 6;
            this.btn_analysis_show_partition.Text = "SHOW";
            this.btn_analysis_show_partition.UseVisualStyleBackColor = true;
            this.btn_analysis_show_partition.Click += new System.EventHandler(this.btn_analysis_show_partition_Click);
            // 
            // cb_analysis_partition_selectImage
            // 
            this.cb_analysis_partition_selectImage.FormattingEnabled = true;
            this.cb_analysis_partition_selectImage.Location = new System.Drawing.Point(29, 278);
            this.cb_analysis_partition_selectImage.Name = "cb_analysis_partition_selectImage";
            this.cb_analysis_partition_selectImage.Size = new System.Drawing.Size(163, 26);
            this.cb_analysis_partition_selectImage.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 243);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 18);
            this.label8.TabIndex = 2;
            this.label8.Text = "이미지선택";
            // 
            // cb_analysis_partition_selectTest
            // 
            this.cb_analysis_partition_selectTest.FormattingEnabled = true;
            this.cb_analysis_partition_selectTest.Items.AddRange(new object[] {
            "모두보기"});
            this.cb_analysis_partition_selectTest.Location = new System.Drawing.Point(29, 158);
            this.cb_analysis_partition_selectTest.Name = "cb_analysis_partition_selectTest";
            this.cb_analysis_partition_selectTest.Size = new System.Drawing.Size(163, 26);
            this.cb_analysis_partition_selectTest.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "테스트선택";
            // 
            // panel_analysis_partition_picture
            // 
            this.panel_analysis_partition_picture.Controls.Add(this.panel_analysis_partition_picture2);
            this.panel_analysis_partition_picture.Location = new System.Drawing.Point(649, 13);
            this.panel_analysis_partition_picture.Name = "panel_analysis_partition_picture";
            this.panel_analysis_partition_picture.Size = new System.Drawing.Size(431, 617);
            this.panel_analysis_partition_picture.TabIndex = 3;
            // 
            // panel_analysis_partition_picture2
            // 
            this.panel_analysis_partition_picture2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_analysis_partition_picture2.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_partition_picture2.Name = "panel_analysis_partition_picture2";
            this.panel_analysis_partition_picture2.Size = new System.Drawing.Size(431, 617);
            this.panel_analysis_partition_picture2.TabIndex = 0;
            // 
            // Partition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1650, 1000);
            this.Controls.Add(this.panel_analysis_partition_main);
            this.Name = "Partition";
            this.Text = "Partition";
            this.panel_analysis_partition_main.ResumeLayout(false);
            this.panel_analysis_partition_left.ResumeLayout(false);
            this.panel_analysis_partition_left.PerformLayout();
            this.panel_analysis_partition_picture.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_analysis_partition_main;
        private System.Windows.Forms.Panel panel_analysis_partition_left;
        private System.Windows.Forms.TextBox tb_analysis_partition_verti;
        private System.Windows.Forms.TextBox tb_analysis_partition_hori;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cb_analysis_partition_selectScenario;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_analysis_show_partition;
        private System.Windows.Forms.ComboBox cb_analysis_partition_selectImage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_analysis_partition_selectTest;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel_analysis_partition_picture;
        private System.Windows.Forms.Panel panel_analysis_partition_picture2;
    }
}