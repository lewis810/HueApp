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
using System.Xml.Linq;
using System.Xml;

namespace DropBox
{
    public partial class EditProject : Form
    {
        Data pData = new Data();

        private string myPath;
        private Button TempDeleteButton;
        private string deviceType;

        public struct LINK
        {
            public string fileName;
            public List<link_info_temp> link_data_temp;
        };

        public struct link_info_temp
        {
            public int btn_id;
            public Button bttn;
            public string DstFile;
            public Point image_xy;
            public float image_width;
            public float image_height;
            public string input_type;
        };

        List<LINK> links = new List<LINK>() { };
        LINK temp_link;

        public EditProject()
        {
            InitializeComponent();
        }

        public EditProject(string _myPath, string _deviceType)
        {
            InitializeComponent();

            myPath = _myPath;
            deviceType = _deviceType;
            temp_link.link_data_temp = new List<link_info_temp>();


            DirectoryInfo Info = new DirectoryInfo(myPath + "\\");

            if (Info.Exists)
            {
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

                    if (_file.Extension == ".png" || _file.Extension == ".jpg")             //여기서 extension 추가하기
                    {
                        //temp = _file.Name;
                        Button newButton = new Button();
                        newButton.Name = _file.Name;
                        newButton.Text = overName;
                        newButton.TextAlign = ContentAlignment.BottomCenter;
                        newButton.Size = new Size(128, 128);
                        newButton.Margin = new Padding(10, 10, 10, 10);
                        newButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.eachButton_Click);

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
            ReadLink();
        }

        //read
        private void ReadLink()
        {
            FileInfo Info = new FileInfo("XMLwork.xml");
            if (Info.Exists)
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load("XMLwork.xml");
                XmlNode nodeDeviceType = xmlDoc.DocumentElement.SelectSingleNode("/LinkTable");
                pData.SetDeviceType(nodeDeviceType.SelectSingleNode("DeviceInfo").InnerText);
                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/LinkTable/Link");

                string pFile_name;
                int pTag, pImage_width, pImage_height;
                string pDst_file, pInput_type;
                Point pImage_xy;

                foreach (XmlNode child_node in nodeList)
                {
                    pFile_name = child_node.Attributes["fileName"].Value.ToString();
                    foreach (XmlNode grand_child_node in child_node)
                    {
                        pTag = Convert.ToInt32(grand_child_node.SelectSingleNode("Tag").InnerText);
                        pDst_file = grand_child_node.SelectSingleNode("DstFile").InnerText;
                        pInput_type = grand_child_node.SelectSingleNode("InputType").InnerText;

                        if (pInput_type.CompareTo("Single_Touch") == 0 || pInput_type.CompareTo("Long_Touch") == 0)
                        {
                            pImage_xy = new Point(Convert.ToInt32(grand_child_node.SelectSingleNode("LinkX").InnerText), Convert.ToInt32(grand_child_node.SelectSingleNode("LinkY").InnerText));
                            pImage_width = Convert.ToInt32(grand_child_node.SelectSingleNode("LinkWidth").InnerText);
                            pImage_height = Convert.ToInt32(grand_child_node.SelectSingleNode("LinkHeight").InnerText);
                        }
                        else
                        {
                            pImage_xy = new Point(-1, -1);
                            pImage_width = -1;
                            pImage_height = -1;
                        }
                        pData.SetLink(pFile_name, pTag, pDst_file, pImage_xy, pImage_width, pImage_height, pInput_type);
                    }
                }
            }
            else
            {
                MessageBox.Show("no Info");
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



                    Edit_Image editProject = new Edit_Image(myPath, mPath, deviceType, pData);
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
