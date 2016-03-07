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
            this.Path_btn = new System.Windows.Forms.Button();
            this.fpanel_scenario_image = new System.Windows.Forms.FlowLayoutPanel();
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
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.CONFIRM_btn);
            this.panel1.Controls.Add(this.Path_btn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxTitle);
            this.panel1.Controls.Add(this.textBoxTime);
            this.panel1.Controls.Add(this.textBoxPurpose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1032, 306);
            this.panel1.TabIndex = 11;
            // 
            // Path_btn
            // 
            this.Path_btn.Location = new System.Drawing.Point(15, 175);
            this.Path_btn.Name = "Path_btn";
            this.Path_btn.Size = new System.Drawing.Size(1004, 27);
            this.Path_btn.TabIndex = 13;
            this.Path_btn.Text = "경로만들기";
            this.Path_btn.UseVisualStyleBackColor = true;
            this.Path_btn.Click += new System.EventHandler(this.Path_btn_Click);
            // 
            // fpanel_scenario_image
            // 
            this.fpanel_scenario_image.AutoScroll = true;
            this.fpanel_scenario_image.Location = new System.Drawing.Point(0, 306);
            this.fpanel_scenario_image.Name = "fpanel_scenario_image";
            this.fpanel_scenario_image.Size = new System.Drawing.Size(1032, 14);
            this.fpanel_scenario_image.TabIndex = 15;
            this.fpanel_scenario_image.Visible = false;
            this.fpanel_scenario_image.WrapContents = false;
            // 
            // Scenario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1032, 320);
            this.Controls.Add(this.fpanel_scenario_image);
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
        private System.Windows.Forms.Button Path_btn;
        private System.Windows.Forms.FlowLayoutPanel fpanel_scenario_image;
    }
}