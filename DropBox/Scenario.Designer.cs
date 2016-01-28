namespace DropBox
{
    partial class Scenario
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
            this.CONFIRM_btn = new System.Windows.Forms.Button();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.textBoxPurpose = new System.Windows.Forms.TextBox();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.EndImage_btn = new System.Windows.Forms.Button();
            this.StartImage_btn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CONFIRM_btn
            // 
            this.CONFIRM_btn.Location = new System.Drawing.Point(904, 259);
            this.CONFIRM_btn.Name = "CONFIRM_btn";
            this.CONFIRM_btn.Size = new System.Drawing.Size(114, 44);
            this.CONFIRM_btn.TabIndex = 4;
            this.CONFIRM_btn.Text = "CONFIRM";
            this.CONFIRM_btn.UseVisualStyleBackColor = true;
            this.CONFIRM_btn.Click += new System.EventHandler(this.CONFIRM_btn_Click);
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(164, 12);
            this.textBoxTitle.Multiline = true;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(854, 53);
            this.textBoxTitle.TabIndex = 5;
            // 
            // textBoxPurpose
            // 
            this.textBoxPurpose.Location = new System.Drawing.Point(164, 72);
            this.textBoxPurpose.Multiline = true;
            this.textBoxPurpose.Name = "textBoxPurpose";
            this.textBoxPurpose.Size = new System.Drawing.Size(854, 53);
            this.textBoxPurpose.TabIndex = 6;
            // 
            // textBoxTime
            // 
            this.textBoxTime.Location = new System.Drawing.Point(164, 131);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.Size = new System.Drawing.Size(854, 28);
            this.textBoxTime.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 50);
            this.label1.TabIndex = 8;
            this.label1.Text = "TITLE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 50);
            this.label2.TabIndex = 9;
            this.label2.Text = "PURPOSE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 31);
            this.label3.TabIndex = 10;
            this.label3.Text = "TIME";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.EndImage_btn);
            this.panel1.Controls.Add(this.CONFIRM_btn);
            this.panel1.Controls.Add(this.StartImage_btn);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxTitle);
            this.panel1.Controls.Add(this.textBoxTime);
            this.panel1.Controls.Add(this.textBoxPurpose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1045, 329);
            this.panel1.TabIndex = 11;
            // 
            // EndImage_btn
            // 
            this.EndImage_btn.Location = new System.Drawing.Point(164, 208);
            this.EndImage_btn.Name = "EndImage_btn";
            this.EndImage_btn.Size = new System.Drawing.Size(855, 27);
            this.EndImage_btn.TabIndex = 14;
            this.EndImage_btn.Text = "Click to set end image";
            this.EndImage_btn.UseVisualStyleBackColor = true;
            this.EndImage_btn.Click += new System.EventHandler(this.EndImage_btn_Click);
            // 
            // StartImage_btn
            // 
            this.StartImage_btn.Location = new System.Drawing.Point(164, 175);
            this.StartImage_btn.Name = "StartImage_btn";
            this.StartImage_btn.Size = new System.Drawing.Size(855, 27);
            this.StartImage_btn.TabIndex = 13;
            this.StartImage_btn.Text = "Click to set start image";
            this.StartImage_btn.UseVisualStyleBackColor = true;
            this.StartImage_btn.Click += new System.EventHandler(this.StartImage_btn_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 31);
            this.label5.TabIndex = 12;
            this.label5.Text = "END IMAGE";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 31);
            this.label4.TabIndex = 11;
            this.label4.Text = "START IMAGE";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Location = new System.Drawing.Point(0, 329);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 10);
            this.panel2.TabIndex = 12;
            this.panel2.Visible = false;
            // 
            // Scenario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1045, 344);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Scenario";
            this.Text = "Scenario";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TextBox textBoxPurpose;
        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CONFIRM_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button EndImage_btn;
        private System.Windows.Forms.Button StartImage_btn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
    }
}