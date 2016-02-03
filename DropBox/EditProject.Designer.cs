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
            this.ProjectName_label = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Scenario_btn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DEL_btn = new System.Windows.Forms.Button();
            this.SAVE_btn = new System.Windows.Forms.Button();
            this.EDIT_btn = new System.Windows.Forms.Button();
            this.NEW_btn = new System.Windows.Forms.Button();
            this.imageListView_EditProject = new Manina.Windows.Forms.ImageListView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddImages
            // 
            this.btnAddImages.Location = new System.Drawing.Point(622, 84);
            this.btnAddImages.Name = "btnAddImages";
            this.btnAddImages.Size = new System.Drawing.Size(101, 42);
            this.btnAddImages.TabIndex = 0;
            this.btnAddImages.Text = "+   Add  images";
            this.btnAddImages.UseVisualStyleBackColor = true;
            this.btnAddImages.Click += new System.EventHandler(this.btnAddImages_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.ProjectName_label);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Scenario_btn);
            this.panel1.Controls.Add(this.btnAddImages);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1179, 144);
            this.panel1.TabIndex = 1;
            // 
            // ProjectName_label
            // 
            this.ProjectName_label.AutoSize = true;
            this.ProjectName_label.Font = new System.Drawing.Font("굴림", 15F);
            this.ProjectName_label.Location = new System.Drawing.Point(29, 26);
            this.ProjectName_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ProjectName_label.Name = "ProjectName_label";
            this.ProjectName_label.Size = new System.Drawing.Size(57, 20);
            this.ProjectName_label.TabIndex = 3;
            this.ProjectName_label.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(29, 84);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 42);
            this.button1.TabIndex = 2;
            this.button1.Text = "PROJECT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Project_btn_Click);
            // 
            // Scenario_btn
            // 
            this.Scenario_btn.Location = new System.Drawing.Point(139, 84);
            this.Scenario_btn.Margin = new System.Windows.Forms.Padding(2);
            this.Scenario_btn.Name = "Scenario_btn";
            this.Scenario_btn.Size = new System.Drawing.Size(97, 42);
            this.Scenario_btn.TabIndex = 1;
            this.Scenario_btn.Text = "SCENARIO";
            this.Scenario_btn.UseVisualStyleBackColor = true;
            this.Scenario_btn.Click += new System.EventHandler(this.Scenario_btn_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 150);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(739, 476);
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
            this.listBox1.Location = new System.Drawing.Point(7, 9);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(459, 264);
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
            this.panel2.Location = new System.Drawing.Point(2, 144);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(740, 474);
            this.panel2.TabIndex = 0;
            // 
            // DEL_btn
            // 
            this.DEL_btn.Location = new System.Drawing.Point(469, 92);
            this.DEL_btn.Margin = new System.Windows.Forms.Padding(2);
            this.DEL_btn.Name = "DEL_btn";
            this.DEL_btn.Size = new System.Drawing.Size(64, 35);
            this.DEL_btn.TabIndex = 3;
            this.DEL_btn.Text = "DELETE";
            this.DEL_btn.UseVisualStyleBackColor = true;
            this.DEL_btn.Click += new System.EventHandler(this.DEL_btn_Click);
            // 
            // SAVE_btn
            // 
            this.SAVE_btn.Location = new System.Drawing.Point(468, 233);
            this.SAVE_btn.Margin = new System.Windows.Forms.Padding(2);
            this.SAVE_btn.Name = "SAVE_btn";
            this.SAVE_btn.Size = new System.Drawing.Size(64, 39);
            this.SAVE_btn.TabIndex = 0;
            this.SAVE_btn.Text = "SAVE";
            this.SAVE_btn.UseVisualStyleBackColor = true;
            this.SAVE_btn.Click += new System.EventHandler(this.SAVE_btn_Click);
            // 
            // EDIT_btn
            // 
            this.EDIT_btn.Location = new System.Drawing.Point(468, 50);
            this.EDIT_btn.Margin = new System.Windows.Forms.Padding(2);
            this.EDIT_btn.Name = "EDIT_btn";
            this.EDIT_btn.Size = new System.Drawing.Size(64, 37);
            this.EDIT_btn.TabIndex = 2;
            this.EDIT_btn.Text = "EDIT";
            this.EDIT_btn.UseVisualStyleBackColor = true;
            this.EDIT_btn.Click += new System.EventHandler(this.EDIT_btn_Click);
            // 
            // NEW_btn
            // 
            this.NEW_btn.Location = new System.Drawing.Point(468, 9);
            this.NEW_btn.Margin = new System.Windows.Forms.Padding(2);
            this.NEW_btn.Name = "NEW_btn";
            this.NEW_btn.Size = new System.Drawing.Size(64, 37);
            this.NEW_btn.TabIndex = 1;
            this.NEW_btn.Text = "NEW";
            this.NEW_btn.UseVisualStyleBackColor = true;
            this.NEW_btn.Click += new System.EventHandler(this.NEW_btn_Click);
            // 
            // imageListView_EditProject
            // 
            this.imageListView_EditProject.AllowDrag = true;
            this.imageListView_EditProject.AllowDrop = true;
            this.imageListView_EditProject.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.imageListView_EditProject.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.imageListView_EditProject.Location = new System.Drawing.Point(745, 150);
            this.imageListView_EditProject.Name = "imageListView_EditProject";
            this.imageListView_EditProject.PersistentCacheDirectory = "";
            this.imageListView_EditProject.PersistentCacheSize = ((long)(100));
            this.imageListView_EditProject.ShowCheckBoxes = true;
            this.imageListView_EditProject.Size = new System.Drawing.Size(422, 468);
            this.imageListView_EditProject.TabIndex = 3;
            this.imageListView_EditProject.ItemClick += new Manina.Windows.Forms.ItemClickEventHandler(this.imageListView1_itemClick);
            this.imageListView_EditProject.ItemDoubleClick += new Manina.Windows.Forms.ItemDoubleClickEventHandler(this.imageListView1_itemDoubleClick);
            // 
            // EditProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 626);
            this.Controls.Add(this.imageListView_EditProject);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Name = "EditProject";
            this.Text = "EditProject";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Label ProjectName_label;
        private Manina.Windows.Forms.ImageListView imageListView_EditProject;
    }
}