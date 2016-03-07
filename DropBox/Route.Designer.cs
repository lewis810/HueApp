namespace DropBox
{
    partial class Route
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
            this.panel_analysis_route_main = new System.Windows.Forms.Panel();
            this.fpanel_analysis_route = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_analysis_route_left = new System.Windows.Forms.Panel();
            this.cb_analysis_route_selectScenario = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_analysis_show_route = new System.Windows.Forms.Button();
            this.cb_analysis_route_selectTest = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel_analysis_route_main.SuspendLayout();
            this.panel_analysis_route_left.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_analysis_route_main
            // 
            this.panel_analysis_route_main.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel_analysis_route_main.Controls.Add(this.fpanel_analysis_route);
            this.panel_analysis_route_main.Controls.Add(this.panel_analysis_route_left);
            this.panel_analysis_route_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_analysis_route_main.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_route_main.Name = "panel_analysis_route_main";
            this.panel_analysis_route_main.Size = new System.Drawing.Size(1320, 812);
            this.panel_analysis_route_main.TabIndex = 6;
            // 
            // fpanel_analysis_route
            // 
            this.fpanel_analysis_route.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpanel_analysis_route.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fpanel_analysis_route.Location = new System.Drawing.Point(249, 0);
            this.fpanel_analysis_route.Name = "fpanel_analysis_route";
            this.fpanel_analysis_route.Size = new System.Drawing.Size(1071, 812);
            this.fpanel_analysis_route.TabIndex = 3;
            // 
            // panel_analysis_route_left
            // 
            this.panel_analysis_route_left.Controls.Add(this.cb_analysis_route_selectScenario);
            this.panel_analysis_route_left.Controls.Add(this.label10);
            this.panel_analysis_route_left.Controls.Add(this.btn_analysis_show_route);
            this.panel_analysis_route_left.Controls.Add(this.cb_analysis_route_selectTest);
            this.panel_analysis_route_left.Controls.Add(this.label13);
            this.panel_analysis_route_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_analysis_route_left.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_route_left.Name = "panel_analysis_route_left";
            this.panel_analysis_route_left.Size = new System.Drawing.Size(249, 812);
            this.panel_analysis_route_left.TabIndex = 2;
            // 
            // cb_analysis_route_selectScenario
            // 
            this.cb_analysis_route_selectScenario.FormattingEnabled = true;
            this.cb_analysis_route_selectScenario.Location = new System.Drawing.Point(32, 63);
            this.cb_analysis_route_selectScenario.Name = "cb_analysis_route_selectScenario";
            this.cb_analysis_route_selectScenario.Size = new System.Drawing.Size(160, 26);
            this.cb_analysis_route_selectScenario.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 18);
            this.label10.TabIndex = 7;
            this.label10.Text = "시나리오선택";
            // 
            // btn_analysis_show_route
            // 
            this.btn_analysis_show_route.Location = new System.Drawing.Point(29, 528);
            this.btn_analysis_show_route.Name = "btn_analysis_show_route";
            this.btn_analysis_show_route.Size = new System.Drawing.Size(163, 57);
            this.btn_analysis_show_route.TabIndex = 6;
            this.btn_analysis_show_route.Text = "SHOW";
            this.btn_analysis_show_route.UseVisualStyleBackColor = true;
            this.btn_analysis_show_route.Click += new System.EventHandler(this.btn_analysis_show_route_Click);
            // 
            // cb_analysis_route_selectTest
            // 
            this.cb_analysis_route_selectTest.FormattingEnabled = true;
            this.cb_analysis_route_selectTest.Location = new System.Drawing.Point(29, 264);
            this.cb_analysis_route_selectTest.Name = "cb_analysis_route_selectTest";
            this.cb_analysis_route_selectTest.Size = new System.Drawing.Size(163, 26);
            this.cb_analysis_route_selectTest.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(32, 225);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 18);
            this.label13.TabIndex = 0;
            this.label13.Text = "테스트선택";
            // 
            // Route
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 812);
            this.Controls.Add(this.panel_analysis_route_main);
            this.Name = "Route";
            this.Text = "Route";
            this.panel_analysis_route_main.ResumeLayout(false);
            this.panel_analysis_route_left.ResumeLayout(false);
            this.panel_analysis_route_left.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_analysis_route_main;
        private System.Windows.Forms.FlowLayoutPanel fpanel_analysis_route;
        private System.Windows.Forms.Panel panel_analysis_route_left;
        private System.Windows.Forms.ComboBox cb_analysis_route_selectScenario;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_analysis_show_route;
        public System.Windows.Forms.ComboBox cb_analysis_route_selectTest;
        private System.Windows.Forms.Label label13;
    }
}