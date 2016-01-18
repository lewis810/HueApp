using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DropBox
{
    public partial class CreateNewProject : Form
    {
        public CreateNewProject(main temp)
        {
            InitializeComponent();
            textBoxProjectName_SetText();
            form1 = temp;
        }

        protected void textBoxProjectName_SetText()
        {
            this.textBoxProjectName.Text = "  Your project name";
            textBoxProjectName.ForeColor = Color.Gray;
        }

        private void textBoxProjectName_Enter(object sender, EventArgs e)
        {
            if (textBoxProjectName.ForeColor == Color.Black)
                return;
            textBoxProjectName.Text = "";
            textBoxProjectName.ForeColor = Color.Black;
        }

        private void textBoxProjectName_Leave(object sender, EventArgs e)
        {
            if (textBoxProjectName.Text.Trim() == "")
                textBoxProjectName_SetText();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                comboBox1.Text = "What device are you prototyping for?";
            }
            else
            {
                comboBox1.Text = comboBox1.SelectedText;
            }
        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            //string mPath = @"C:\Prototype\project\" + textBoxProjectName.Text + "\\";
            string mPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + textBoxProjectName.Text + "\\";
            DirectoryInfo di = new DirectoryInfo(mPath);

            if (di.Exists == false)
            {
                di.Create();
            }
            else
            {
                MessageBox.Show("A project with this name already exists", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Image mImage = Image.FromFile((string)di.GetFiles()[0].Name);

            Button newButton = new Button();
            newButton.Name = textBoxProjectName.Text;
            newButton.Text = "New project\n" + textBoxProjectName.Text;
            newButton.TextAlign = ContentAlignment.BottomCenter;
            newButton.Size = new Size(128, 128);
            newButton.Margin = new Padding(10, 10, 10, 10);
            newButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.eachButton_Click);

            form1.flowLayoutPanel1.Controls.Add(newButton);
            this.Dispose();
        }

        private void eachButton_Click(object sender, MouseEventArgs e)
        {
            //MouseEventArgs me = (MouseEventArgs)e;
            

            switch (e.Button)
            {
                case MouseButtons.Left:

                    // 여기에 정의
                    // 이 주소를 통하여서 안에 있는 그림 파일 혹은 기타 파일 가져 오기
                    Button temp_btn_left = sender as Button;

                    string mPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + temp_btn_left.Name + "\\";
                    //string mPath = @"C:\Prototype\\project\\" + temp_btn.Name + "\\";

                    //MessageBox.Show(temp, "Warning",
                    //        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    EditProject editProject = new EditProject(mPath, getDevice(comboBox1.SelectedItem.ToString()));
                    editProject.Show();
                    //MessageBox.Show("Left click", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case MouseButtons.Right:

                    //MessageBox.Show("Right click", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    Button temp_btn_right = sender as Button;
                    ContextMenu cm = new ContextMenu();
                    cm.MenuItems.Add("Delete", new System.EventHandler(this.menuItem_delete_click));
                    cm.MenuItems.Add("Item 2");
                    temp_btn_right.ContextMenu = cm;
                    main.TempDeleteButton = temp_btn_right;

                    break;
            }

        }

        //device 리스트를 만들어서 체크해서 넘기는 것
        private string getDevice(string temp)
        {
            string[] device_list = {"iPhone4", "iPhone5", "iPhone6", "GalaxyS4", "GalaxyS5"};
            string device = string.Empty;
            
            for(int i = 0; i < device_list.Length; i++)
            {
                if(temp.Contains(device_list[i]) == true)
                {
                    device = device_list[i];
                    break;
                }
            }

            return device;
        }

        private void menuItem_delete_click(object sender, EventArgs e)
        {

            string mPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + main.TempDeleteButton.Name + "\\";

            //get control hovered with mouse
            //Button buttonToRemove = (this.GetChildAtPoint(this.PointToClient(Cursor.Position)) as Button);
            MessageBox.Show(main.TempDeleteButton.Text, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //if it's a Button, remove it from the form

            DirectoryInfo Info = new DirectoryInfo(mPath);

            Info.Delete(true);

            form1.flowLayoutPanel1.Controls.Remove(main.TempDeleteButton);

            //$$$$$$
        }

        main form1;
    }
}
