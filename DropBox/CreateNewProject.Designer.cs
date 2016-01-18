﻿namespace DropBox
{
    partial class CreateNewProject
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
            this.textBoxProjectName = new System.Windows.Forms.TextBox();
            this.lblCreateNewProject = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxProjectName
            // 
            this.textBoxProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProjectName.Location = new System.Drawing.Point(49, 75);
            this.textBoxProjectName.Name = "textBoxProjectName";
            this.textBoxProjectName.Size = new System.Drawing.Size(324, 29);
            this.textBoxProjectName.TabIndex = 0;
            this.textBoxProjectName.Enter += new System.EventHandler(this.textBoxProjectName_Enter);
            this.textBoxProjectName.Leave += new System.EventHandler(this.textBoxProjectName_Leave);
            // 
            // lblCreateNewProject
            // 
            this.lblCreateNewProject.AutoSize = true;
            this.lblCreateNewProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateNewProject.Location = new System.Drawing.Point(46, 32);
            this.lblCreateNewProject.Name = "lblCreateNewProject";
            this.lblCreateNewProject.Size = new System.Drawing.Size(137, 16);
            this.lblCreateNewProject.TabIndex = 1;
            this.lblCreateNewProject.Text = "Create new project";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Desktop/Web",
            "iPhone",
            "iPad",
            "Android",
            "Watch"});
            this.comboBox1.Location = new System.Drawing.Point(49, 117);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(324, 32);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Text = "What device you prototyping for?";
            // 
            // btCreate
            // 
            this.btCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCreate.Location = new System.Drawing.Point(253, 182);
            this.btCreate.Name = "btCreate";
            this.btCreate.Size = new System.Drawing.Size(120, 33);
            this.btCreate.TabIndex = 3;
            this.btCreate.Text = "Create project";
            this.btCreate.UseVisualStyleBackColor = true;
            this.btCreate.Click += new System.EventHandler(this.btCreate_Click);
            // 
            // CreateNewProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 250);
            this.Controls.Add(this.btCreate);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblCreateNewProject);
            this.Controls.Add(this.textBoxProjectName);
            this.Name = "CreateNewProject";
            this.Text = "NewProject";
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.TextBox textBoxProjectName;
        private System.Windows.Forms.Label lblCreateNewProject;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btCreate;
    }
}