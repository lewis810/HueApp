namespace DropBox
{
    partial class EditProject
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
            this.btnAddImages = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.Scenario_btn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SAVE_btn = new System.Windows.Forms.Button();
            this.EDIT_btn = new System.Windows.Forms.Button();
            this.NEW_btn = new System.Windows.Forms.Button();
            this.DEL_btn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddImages
            // 
            this.btnAddImages.Location = new System.Drawing.Point(1036, 116);
            this.btnAddImages.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnAddImages.Name = "btnAddImages";
            this.btnAddImages.Size = new System.Drawing.Size(168, 58);
            this.btnAddImages.TabIndex = 0;
            this.btnAddImages.Text = "+   Add  images";
            this.btnAddImages.UseVisualStyleBackColor = true;
            this.btnAddImages.Click += new System.EventHandler(this.btnAddImages_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Scenario_btn);
            this.panel1.Controls.Add(this.btnAddImages);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1237, 199);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(48, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 58);
            this.button1.TabIndex = 2;
            this.button1.Text = "PROJECT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Project_btn_Click);
            // 
            // Scenario_btn
            // 
            this.Scenario_btn.Location = new System.Drawing.Point(231, 116);
            this.Scenario_btn.Name = "Scenario_btn";
            this.Scenario_btn.Size = new System.Drawing.Size(161, 58);
            this.Scenario_btn.TabIndex = 1;
            this.Scenario_btn.Text = "SCENARIO";
            this.Scenario_btn.UseVisualStyleBackColor = true;
            this.Scenario_btn.Click += new System.EventHandler(this.Scenario_btn_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 208);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1237, 659);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DereferenceLinks = false;
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.ShowReadOnly = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(762, 364);
            this.listBox1.TabIndex = 0;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DEL_btn);
            this.panel2.Controls.Add(this.SAVE_btn);
            this.panel2.Controls.Add(this.EDIT_btn);
            this.panel2.Controls.Add(this.NEW_btn);
            this.panel2.Controls.Add(this.listBox1);
            this.panel2.Location = new System.Drawing.Point(3, 199);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1234, 656);
            this.panel2.TabIndex = 0;
            // 
            // SAVE_btn
            // 
            this.SAVE_btn.Location = new System.Drawing.Point(780, 322);
            this.SAVE_btn.Name = "SAVE_btn";
            this.SAVE_btn.Size = new System.Drawing.Size(107, 54);
            this.SAVE_btn.TabIndex = 0;
            this.SAVE_btn.Text = "SAVE";
            this.SAVE_btn.UseVisualStyleBackColor = true;
            this.SAVE_btn.Click += new System.EventHandler(this.SAVE_btn_Click);
            // 
            // EDIT_btn
            // 
            this.EDIT_btn.Location = new System.Drawing.Point(780, 69);
            this.EDIT_btn.Name = "EDIT_btn";
            this.EDIT_btn.Size = new System.Drawing.Size(107, 51);
            this.EDIT_btn.TabIndex = 2;
            this.EDIT_btn.Text = "EDIT";
            this.EDIT_btn.UseVisualStyleBackColor = true;
            this.EDIT_btn.Click += new System.EventHandler(this.EDIT_btn_Click);
            // 
            // NEW_btn
            // 
            this.NEW_btn.Location = new System.Drawing.Point(780, 12);
            this.NEW_btn.Name = "NEW_btn";
            this.NEW_btn.Size = new System.Drawing.Size(107, 51);
            this.NEW_btn.TabIndex = 1;
            this.NEW_btn.Text = "NEW";
            this.NEW_btn.UseVisualStyleBackColor = true;
            this.NEW_btn.Click += new System.EventHandler(this.NEW_btn_Click);
            // 
            // DEL_btn
            // 
            this.DEL_btn.Location = new System.Drawing.Point(781, 127);
            this.DEL_btn.Name = "DEL_btn";
            this.DEL_btn.Size = new System.Drawing.Size(106, 48);
            this.DEL_btn.TabIndex = 3;
            this.DEL_btn.Text = "DELETE";
            this.DEL_btn.UseVisualStyleBackColor = true;
            this.DEL_btn.Click += new System.EventHandler(this.DEL_btn_Click);
            // 
            // EditProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 867);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "EditProject";
            this.Text = "EditProject";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddImages;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button Scenario_btn;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button EDIT_btn;
        private System.Windows.Forms.Button NEW_btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button SAVE_btn;
        private System.Windows.Forms.Button DEL_btn;
    }
}