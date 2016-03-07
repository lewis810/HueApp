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
            this.Analysis_btn = new System.Windows.Forms.Button();
            this.ProjectName_label = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Scenario_btn = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel_scenario = new System.Windows.Forms.Panel();
            this.DEL_btn = new System.Windows.Forms.Button();
            this.SAVE_btn = new System.Windows.Forms.Button();
            this.EDIT_btn = new System.Windows.Forms.Button();
            this.NEW_btn = new System.Windows.Forms.Button();
            this.imageListView_EditProject = new Manina.Windows.Forms.ImageListView();
            this.panel_analysis = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Dots_btn = new System.Windows.Forms.Button();
            this.Partition_btn = new System.Windows.Forms.Button();
            this.Graph_btn = new System.Windows.Forms.Button();
            this.Route_btn = new System.Windows.Forms.Button();
            this.Survey_btn = new System.Windows.Forms.Button();
            this.btn_analysis_dot_delete = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel_scenario.SuspendLayout();
            this.panel_analysis.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddImages
            // 
            this.btnAddImages.Location = new System.Drawing.Point(1037, 116);
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
            this.panel1.Controls.Add(this.Analysis_btn);
            this.panel1.Controls.Add(this.ProjectName_label);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Scenario_btn);
            this.panel1.Controls.Add(this.btnAddImages);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1528, 199);
            this.panel1.TabIndex = 1;
            // 
            // Analysis_btn
            // 
            this.Analysis_btn.Location = new System.Drawing.Point(412, 116);
            this.Analysis_btn.Name = "Analysis_btn";
            this.Analysis_btn.Size = new System.Drawing.Size(147, 58);
            this.Analysis_btn.TabIndex = 4;
            this.Analysis_btn.Text = "ANALYSIS";
            this.Analysis_btn.UseVisualStyleBackColor = true;
            this.Analysis_btn.Click += new System.EventHandler(this.Analysis_btn_Click);
            // 
            // ProjectName_label
            // 
            this.ProjectName_label.AutoSize = true;
            this.ProjectName_label.Font = new System.Drawing.Font("굴림", 15F);
            this.ProjectName_label.Location = new System.Drawing.Point(48, 36);
            this.ProjectName_label.Name = "ProjectName_label";
            this.ProjectName_label.Size = new System.Drawing.Size(96, 30);
            this.ProjectName_label.TabIndex = 3;
            this.ProjectName_label.Text = "label1";
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
            this.Scenario_btn.Location = new System.Drawing.Point(232, 116);
            this.Scenario_btn.Name = "Scenario_btn";
            this.Scenario_btn.Size = new System.Drawing.Size(162, 58);
            this.Scenario_btn.TabIndex = 1;
            this.Scenario_btn.Text = "SCENARIO";
            this.Scenario_btn.UseVisualStyleBackColor = true;
            this.Scenario_btn.Click += new System.EventHandler(this.Scenario_btn_Click);
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
            // panel_scenario
            // 
            this.panel_scenario.Controls.Add(this.DEL_btn);
            this.panel_scenario.Controls.Add(this.SAVE_btn);
            this.panel_scenario.Controls.Add(this.EDIT_btn);
            this.panel_scenario.Controls.Add(this.NEW_btn);
            this.panel_scenario.Controls.Add(this.listBox1);
            this.panel_scenario.Location = new System.Drawing.Point(3, 199);
            this.panel_scenario.Name = "panel_scenario";
            this.panel_scenario.Size = new System.Drawing.Size(1233, 656);
            this.panel_scenario.TabIndex = 0;
            // 
            // DEL_btn
            // 
            this.DEL_btn.Location = new System.Drawing.Point(782, 127);
            this.DEL_btn.Name = "DEL_btn";
            this.DEL_btn.Size = new System.Drawing.Size(107, 48);
            this.DEL_btn.TabIndex = 3;
            this.DEL_btn.Text = "DELETE";
            this.DEL_btn.UseVisualStyleBackColor = true;
            this.DEL_btn.Click += new System.EventHandler(this.DEL_btn_Click);
            // 
            // SAVE_btn
            // 
            this.SAVE_btn.Location = new System.Drawing.Point(780, 323);
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
            // imageListView_EditProject
            // 
            this.imageListView_EditProject.AllowDrag = true;
            this.imageListView_EditProject.AllowDrop = true;
            this.imageListView_EditProject.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.imageListView_EditProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageListView_EditProject.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.imageListView_EditProject.Location = new System.Drawing.Point(0, 199);
            this.imageListView_EditProject.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.imageListView_EditProject.Name = "imageListView_EditProject";
            this.imageListView_EditProject.PersistentCacheDirectory = "";
            this.imageListView_EditProject.PersistentCacheSize = ((long)(100));
            this.imageListView_EditProject.ShowCheckBoxes = true;
            this.imageListView_EditProject.Size = new System.Drawing.Size(1528, 839);
            this.imageListView_EditProject.TabIndex = 3;
            this.imageListView_EditProject.ItemClick += new Manina.Windows.Forms.ItemClickEventHandler(this.imageListView1_itemClick);
            this.imageListView_EditProject.ItemDoubleClick += new Manina.Windows.Forms.ItemDoubleClickEventHandler(this.imageListView1_itemDoubleClick);
            // 
            // panel_analysis
            // 
            this.panel_analysis.Controls.Add(this.flowLayoutPanel1);
            this.panel_analysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_analysis.Location = new System.Drawing.Point(0, 199);
            this.panel_analysis.Name = "panel_analysis";
            this.panel_analysis.Size = new System.Drawing.Size(1528, 839);
            this.panel_analysis.TabIndex = 4;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.Dots_btn);
            this.flowLayoutPanel1.Controls.Add(this.Partition_btn);
            this.flowLayoutPanel1.Controls.Add(this.Graph_btn);
            this.flowLayoutPanel1.Controls.Add(this.Route_btn);
            this.flowLayoutPanel1.Controls.Add(this.Survey_btn);
            this.flowLayoutPanel1.Controls.Add(this.btn_analysis_dot_delete);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1528, 47);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // Dots_btn
            // 
            this.Dots_btn.Location = new System.Drawing.Point(3, 3);
            this.Dots_btn.Name = "Dots_btn";
            this.Dots_btn.Size = new System.Drawing.Size(141, 46);
            this.Dots_btn.TabIndex = 0;
            this.Dots_btn.Text = "Dots";
            this.Dots_btn.UseVisualStyleBackColor = true;
            this.Dots_btn.Click += new System.EventHandler(this.Dots_btn_Click);
            // 
            // Partition_btn
            // 
            this.Partition_btn.Location = new System.Drawing.Point(150, 3);
            this.Partition_btn.Name = "Partition_btn";
            this.Partition_btn.Size = new System.Drawing.Size(141, 46);
            this.Partition_btn.TabIndex = 1;
            this.Partition_btn.Text = "Partition";
            this.Partition_btn.UseVisualStyleBackColor = true;
            this.Partition_btn.Click += new System.EventHandler(this.Partition_btn_Click);
            // 
            // Graph_btn
            // 
            this.Graph_btn.Location = new System.Drawing.Point(297, 3);
            this.Graph_btn.Name = "Graph_btn";
            this.Graph_btn.Size = new System.Drawing.Size(141, 46);
            this.Graph_btn.TabIndex = 2;
            this.Graph_btn.Text = "Graph";
            this.Graph_btn.UseVisualStyleBackColor = true;
            this.Graph_btn.Click += new System.EventHandler(this.Graph_btn_Click);
            // 
            // Route_btn
            // 
            this.Route_btn.Location = new System.Drawing.Point(444, 3);
            this.Route_btn.Name = "Route_btn";
            this.Route_btn.Size = new System.Drawing.Size(141, 46);
            this.Route_btn.TabIndex = 3;
            this.Route_btn.Text = "Route";
            this.Route_btn.UseVisualStyleBackColor = true;
            this.Route_btn.Click += new System.EventHandler(this.Route_btn_Click);
            // 
            // Survey_btn
            // 
            this.Survey_btn.Location = new System.Drawing.Point(591, 3);
            this.Survey_btn.Name = "Survey_btn";
            this.Survey_btn.Size = new System.Drawing.Size(141, 46);
            this.Survey_btn.TabIndex = 4;
            this.Survey_btn.Text = "Survey";
            this.Survey_btn.UseVisualStyleBackColor = true;
            this.Survey_btn.Click += new System.EventHandler(this.Survey_btn_Click);
            // 
            // btn_analysis_dot_delete
            // 
            this.btn_analysis_dot_delete.Location = new System.Drawing.Point(738, 3);
            this.btn_analysis_dot_delete.Name = "btn_analysis_dot_delete";
            this.btn_analysis_dot_delete.Size = new System.Drawing.Size(175, 50);
            this.btn_analysis_dot_delete.TabIndex = 1;
            this.btn_analysis_dot_delete.Text = "DELETE DATA";
            this.btn_analysis_dot_delete.UseVisualStyleBackColor = true;
            this.btn_analysis_dot_delete.Click += new System.EventHandler(this.btn_analysis_dot_delete_Click);
            // 
            // EditProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1528, 1038);
            this.Controls.Add(this.panel_analysis);
            this.Controls.Add(this.imageListView_EditProject);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_scenario);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "EditProject";
            this.Text = "EditProject";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditProject_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_scenario.ResumeLayout(false);
            this.panel_analysis.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddImages;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button Scenario_btn;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel_scenario;
        private System.Windows.Forms.Button EDIT_btn;
        private System.Windows.Forms.Button NEW_btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button SAVE_btn;
        private System.Windows.Forms.Button DEL_btn;
        private System.Windows.Forms.Label ProjectName_label;
        private Manina.Windows.Forms.ImageListView imageListView_EditProject;
        private System.Windows.Forms.Button Analysis_btn;
        private System.Windows.Forms.Panel panel_analysis;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button Dots_btn;
        private System.Windows.Forms.Button Partition_btn;
        private System.Windows.Forms.Button Graph_btn;
        private System.Windows.Forms.Button Route_btn;
        private System.Windows.Forms.Button Survey_btn;
        private System.Windows.Forms.Button btn_analysis_dot_delete;
    }
}