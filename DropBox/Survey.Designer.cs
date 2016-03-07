namespace DropBox
{
    partial class Survey
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
            this.panel_analysis_survey_main = new System.Windows.Forms.Panel();
            this.fpanel_analysis_survey = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_analysis_survey_left = new System.Windows.Forms.Panel();
            this.cb_analysis_survey_selectScenario = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btn_analysis_show_survey = new System.Windows.Forms.Button();
            this.cb_analysis_survey_selectTest = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel_analysis_survey_main.SuspendLayout();
            this.panel_analysis_survey_left.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_analysis_survey_main
            // 
            this.panel_analysis_survey_main.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel_analysis_survey_main.Controls.Add(this.fpanel_analysis_survey);
            this.panel_analysis_survey_main.Controls.Add(this.panel_analysis_survey_left);
            this.panel_analysis_survey_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_analysis_survey_main.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_survey_main.Name = "panel_analysis_survey_main";
            this.panel_analysis_survey_main.Size = new System.Drawing.Size(1583, 978);
            this.panel_analysis_survey_main.TabIndex = 7;
            // 
            // fpanel_analysis_survey
            // 
            this.fpanel_analysis_survey.AutoScroll = true;
            this.fpanel_analysis_survey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpanel_analysis_survey.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fpanel_analysis_survey.Location = new System.Drawing.Point(249, 0);
            this.fpanel_analysis_survey.Name = "fpanel_analysis_survey";
            this.fpanel_analysis_survey.Size = new System.Drawing.Size(1334, 978);
            this.fpanel_analysis_survey.TabIndex = 3;
            this.fpanel_analysis_survey.WrapContents = false;
            // 
            // panel_analysis_survey_left
            // 
            this.panel_analysis_survey_left.Controls.Add(this.cb_analysis_survey_selectScenario);
            this.panel_analysis_survey_left.Controls.Add(this.label14);
            this.panel_analysis_survey_left.Controls.Add(this.btn_analysis_show_survey);
            this.panel_analysis_survey_left.Controls.Add(this.cb_analysis_survey_selectTest);
            this.panel_analysis_survey_left.Controls.Add(this.label15);
            this.panel_analysis_survey_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_analysis_survey_left.Location = new System.Drawing.Point(0, 0);
            this.panel_analysis_survey_left.Name = "panel_analysis_survey_left";
            this.panel_analysis_survey_left.Size = new System.Drawing.Size(249, 978);
            this.panel_analysis_survey_left.TabIndex = 2;
            // 
            // cb_analysis_survey_selectScenario
            // 
            this.cb_analysis_survey_selectScenario.FormattingEnabled = true;
            this.cb_analysis_survey_selectScenario.Location = new System.Drawing.Point(32, 63);
            this.cb_analysis_survey_selectScenario.Name = "cb_analysis_survey_selectScenario";
            this.cb_analysis_survey_selectScenario.Size = new System.Drawing.Size(160, 26);
            this.cb_analysis_survey_selectScenario.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(29, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(116, 18);
            this.label14.TabIndex = 7;
            this.label14.Text = "시나리오선택";
            // 
            // btn_analysis_show_survey
            // 
            this.btn_analysis_show_survey.Location = new System.Drawing.Point(29, 528);
            this.btn_analysis_show_survey.Name = "btn_analysis_show_survey";
            this.btn_analysis_show_survey.Size = new System.Drawing.Size(163, 57);
            this.btn_analysis_show_survey.TabIndex = 6;
            this.btn_analysis_show_survey.Text = "SHOW";
            this.btn_analysis_show_survey.UseVisualStyleBackColor = true;
            this.btn_analysis_show_survey.Click += new System.EventHandler(this.btn_analysis_show_survey_Click);
            // 
            // cb_analysis_survey_selectTest
            // 
            this.cb_analysis_survey_selectTest.FormattingEnabled = true;
            this.cb_analysis_survey_selectTest.Location = new System.Drawing.Point(29, 264);
            this.cb_analysis_survey_selectTest.Name = "cb_analysis_survey_selectTest";
            this.cb_analysis_survey_selectTest.Size = new System.Drawing.Size(163, 26);
            this.cb_analysis_survey_selectTest.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(32, 225);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 18);
            this.label15.TabIndex = 0;
            this.label15.Text = "테스트선택";
            // 
            // Survey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1583, 978);
            this.Controls.Add(this.panel_analysis_survey_main);
            this.Name = "Survey";
            this.Text = "Survey";
            this.panel_analysis_survey_main.ResumeLayout(false);
            this.panel_analysis_survey_left.ResumeLayout(false);
            this.panel_analysis_survey_left.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_analysis_survey_main;
        private System.Windows.Forms.FlowLayoutPanel fpanel_analysis_survey;
        private System.Windows.Forms.Panel panel_analysis_survey_left;
        private System.Windows.Forms.ComboBox cb_analysis_survey_selectScenario;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btn_analysis_show_survey;
        private System.Windows.Forms.ComboBox cb_analysis_survey_selectTest;
        private System.Windows.Forms.Label label15;
    }
}