﻿namespace DropBox
{
    partial class main
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button3 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btCreateProject = new System.Windows.Forms.Button();
            this.lblMyProjects = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Location = new System.Drawing.Point(945, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(65, 40);
            this.listBox1.TabIndex = 0;
            this.listBox1.Visible = false;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_doubleClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(407, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "Create Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(770, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(169, 28);
            this.textBox1.TabIndex = 2;
            this.textBox1.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(597, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(138, 35);
            this.button2.TabIndex = 3;
            this.button2.Text = "Upload File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(945, 58);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(65, 17);
            this.progressBar1.TabIndex = 4;
            this.progressBar1.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(907, 75);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 35);
            this.button3.TabIndex = 5;
            this.button3.Text = "logout";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 246);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1310, 525);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btCreateProject);
            this.panel1.Controls.Add(this.lblMyProjects);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1310, 135);
            this.panel1.TabIndex = 7;
            // 
            // btCreateProject
            // 
            this.btCreateProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCreateProject.Location = new System.Drawing.Point(990, 44);
            this.btCreateProject.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btCreateProject.Name = "btCreateProject";
            this.btCreateProject.Size = new System.Drawing.Size(265, 66);
            this.btCreateProject.TabIndex = 3;
            this.btCreateProject.Text = "+ Create Project";
            this.btCreateProject.UseVisualStyleBackColor = true;
            this.btCreateProject.Click += new System.EventHandler(this.btCreateProject_Click);
            // 
            // lblMyProjects
            // 
            this.lblMyProjects.AutoSize = true;
            this.lblMyProjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMyProjects.Location = new System.Drawing.Point(38, 57);
            this.lblMyProjects.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblMyProjects.Name = "lblMyProjects";
            this.lblMyProjects.Size = new System.Drawing.Size(182, 37);
            this.lblMyProjects.TabIndex = 2;
            this.lblMyProjects.Text = "My Projects";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 771);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btCreateProject;
        private System.Windows.Forms.Label lblMyProjects;
    }
}
