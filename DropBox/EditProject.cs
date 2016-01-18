using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DropBox
{
    public partial class EditProject : Form
    {
        private string myPath;
        private Button TempDeleteButton;
        private string deviceType;

        public EditProject()
        {
            InitializeComponent();
        }

        public EditProject(string _myPath, string _deviceType)
        {
            InitializeComponent();

            myPath = _myPath;
            deviceType = _deviceType;

            DirectoryInfo Info = new DirectoryInfo(myPath + "\\");

            if (Info.Exists)
            {
                String temp;
                foreach (System.IO.FileInfo _file in Info.GetFiles())
                {
                    string fileName = _file.Name.Substring(0, _file.Name.LastIndexOf('.'));
                    string fileExt = _file.Name.Substring(_file.Name.LastIndexOf('.'));
                    string overName = String.Empty;

                    if (fileName.Length > 10)
                    {
                        overName = fileName.Substring(0, 8) + "....." + fileExt;
                    }
                    else
                    {
                        overName = fileName + fileExt;
                    }

                    if (_file.Extension == ".png" || _file.Extension == ".jpg")
                    {
                        //temp = _file.Name;
                        Button newButton = new Button();
                        newButton.Name = _file.Name;
                        newButton.Text = overName;
                        newButton.TextAlign = ContentAlignment.BottomCenter;
                        newButton.Size = new Size(128, 128);
                        newButton.Margin = new Padding(10, 10, 10, 10);
                        newButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.eachButton_Click);

                        //MessageBox.Show(" Directory Name : " + _file.DirectoryName + "\n file Name : " + _file.Name);

                        Image img;
                        using (var bmpTemp = new Bitmap(_file.DirectoryName + "\\" + _file.Name))
                        {
                            img = new Bitmap(bmpTemp);
                        }

                        using (Graphics gr = Graphics.FromImage(img))
                        {
                            gr.SmoothingMode = SmoothingMode.HighQuality;
                            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            gr.DrawImage(img, new Rectangle(0, 0, 64, 64));
                        }

                        PictureBox newPicture = new PictureBox();
                        newPicture.Image = img;
                        newPicture.Size = new Size(90, 80);
                        newPicture.Location = new Point(20, 10);
                        newPicture.SizeMode = PictureBoxSizeMode.StretchImage;


                        newButton.Controls.Add(newPicture);
                        flowLayoutPanel1.Controls.Add(newButton);
                    }
                }
            }
        }


        private void btnAddImages_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string destFile = "";
            DialogResult dr = this.openFileDialog.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string temp in openFileDialog.FileNames)
                {
                    fileName = System.IO.Path.GetFileName(temp);
                    destFile = System.IO.Path.Combine(myPath, fileName);

                    if (!System.IO.Directory.Exists(myPath))
                    {
                        System.IO.Directory.CreateDirectory(myPath);
                    }
                    System.IO.File.Copy(temp, destFile);

                    Image img;
                    using (var bmpTemp = new Bitmap(destFile))
                    {
                        img = new Bitmap(bmpTemp);
                    }


                    //Bitmap tempImage = new Bitmap(destFile);
                    using (Graphics gr = Graphics.FromImage(img))
                    {
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        gr.DrawImage(img, new Rectangle(0, 0, 64, 64));
                    }

                    string _fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
                    string _fileExt = fileName.Substring(fileName.LastIndexOf('.'));
                    string _overName = String.Empty;

                    if (_fileName.Length > 10)
                    {
                        _overName = _fileName.Substring(0, 8) + "....." + _fileExt;
                    }
                    else
                    {
                        _overName = _fileName + _fileExt;
                    }

                    Button newButton = new Button();
                    newButton.Name = fileName;
                    newButton.Text = _overName;
                    newButton.TextAlign = ContentAlignment.BottomCenter;
                    newButton.Size = new Size(128, 128);
                    newButton.Margin = new Padding(10, 10, 10, 10);
                    //newButton.BackgroundImage = tempImage;
                    //newButton.BackgroundImageLayout = ImageLayout.Stretch;

                    PictureBox newPicture = new PictureBox();
                    newPicture.Image = img;
                    newPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                    newPicture.Size = new Size(90, 80);
                    newPicture.Location = new Point(20, 10);


                    newButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.eachButton_Click);

                    newButton.Controls.Add(newPicture);
                    newPicture.Anchor = AnchorStyles.None;
                    flowLayoutPanel1.Controls.Add(newButton);
                }
            }
        }

        /*private void eachButton_Click(object sender, EventArgs e)
        {
            // 영민이형이 한거 불러오기

            Button temp_btn = sender as Button;

            string mPath = myPath + "\\" + temp_btn.Name;



            Edit_Image editProject = new Edit_Image(myPath, mPath);
            editProject.Show();
        }*/




        private void eachButton_Click(object sender, MouseEventArgs e)
        {
            //MouseEventArgs me = (MouseEventArgs)e;

            switch (e.Button)
            {
                case MouseButtons.Left:

                    Button temp_btn = sender as Button;

                    string mPath = myPath + "\\" + temp_btn.Name;



                    Edit_Image editProject = new Edit_Image(myPath, mPath, deviceType);
                    editProject.Show();
                    break;
                case MouseButtons.Right:
                    //MessageBox.Show("Right click", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    Button temp_btn_right = sender as Button;
                    ContextMenu cm = new ContextMenu();
                    cm.MenuItems.Add("Delete", new System.EventHandler(this.menuItem_delete_click));
                    cm.MenuItems.Add("Item 2");
                    temp_btn_right.ContextMenu = cm;
                    TempDeleteButton = temp_btn_right;

                    break;
            }

        }

        private void menuItem_delete_click(object sender, EventArgs e)
        {

            string mPath = myPath + TempDeleteButton.Name;

            //get control hovered with mouse
            //Button buttonToRemove = (this.GetChildAtPoint(this.PointToClient(Cursor.Position)) as Button);

            //if it's a Button, remove it from the form

            if (File.Exists(mPath))
            {
                MessageBox.Show(mPath, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                File.Delete(mPath);
            }

            /*if (Info.Exists)
            {
                foreach (FileInfo file in Info.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in Info.GetDirectories())
                {
                    dir.Delete(true);
                }
            }*/

            //Info.Delete();

            this.flowLayoutPanel1.Controls.Remove(TempDeleteButton);



            //$$$$$$
        }
    }
}
