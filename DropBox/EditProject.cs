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
using System.Text.RegularExpressions;

namespace DropBox
{
    public partial class EditProject : Form
    {
        LinkData pData = new LinkData();
        ScenarioData sData = new ScenarioData();

        private string myPath;
        private Button TempDeleteButton;
        private string deviceType;

        public struct SCENARIO
        {
            public int tag;
            public string title;
            public string purpose;
            public string start_img;
            public string end_img;
            public string time;
        }

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

        List<SCENARIO> scenarios = new List<SCENARIO>();
        List<LINK> links = new List<LINK>() { };
        List<string> image_list = new List<string>();
        LINK temp_link;
        int last_index = 0;
        public ListBox listBox;

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
            panel2.Visible = false;
            listBox = this.listBox1;

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
                    if (Regex.IsMatch(_file.Extension, "jpg", RegexOptions.IgnoreCase) ||
                        Regex.IsMatch(_file.Extension, "png", RegexOptions.IgnoreCase))             //여기서 extension 추가하기
                    {
                        //temp = _file.Name;
                        Button newButton = new Button();
                        newButton.Name = _file.Name;

                        //image_list에 이미지 이름 다 넣기
                        image_list.Add(_file.Name);

                        newButton.Text = overName;
                        newButton.TextAlign = ContentAlignment.BottomCenter;
                        newButton.Size = new Size(128, 128);
                        newButton.Margin = new Padding(10, 10, 10, 10);
                        newButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.eachButton_Click);
                        newButton.Parent = flowLayoutPanel1;

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
            ReadScenario();
        }

        //read
        private void ReadLink()
        {
            FileInfo Info = new FileInfo(myPath + "\\" + "link.xml");
            if (Info.Exists)
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(myPath + "\\" + "link.xml");
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


            //링크정보가 없는 이미지에 대해서도 미리 리스트에 공간을 확보 해두기(첫 버튼 생성시 outofbound exception이 나기 때문)
            int check = 0;
            for (int i = 0; i < image_list.Count; i++)
            {
                check = 0;
                for(int j = 0; j < pData.GetLinks().Count; j++)
                {
                    //i번째의 이미지 이름이 링크정보가 만들어진 데이터에 존재하지 않는다면 추가하기
                    if(pData.GetLinks()[j].file_name.CompareTo(image_list[i]) == 0)
                    {
                        check = 1;
                        break;
                    }
                }
                if(check == 0)
                {
                    //추가하기
                    pData.GetLinks().Add(new LinkData.LINK() { file_name = image_list[i], link_data = new List<LinkData.link_info>() });
                }
            }
        }

        private void ReadScenario()
        {
            //Scenrio.xml 읽어와서 정보 읽어오기
            //데이터 저장
            FileInfo Info = new FileInfo(myPath + "\\" + "scenario.xml");
            if (Info.Exists)
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(myPath + "\\" + "scenario.xml");

                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/ScenarioTable/Scenario");

                string sTitle, sPurpose, sTime, sStart_img, sEnd_img;
                int sTag = 0;

                foreach (XmlNode child_node in nodeList)
                {
                    sTag = Convert.ToInt32(child_node.SelectSingleNode("Tag").InnerText);
                    sTitle = child_node.SelectSingleNode("Title").InnerText;
                    sPurpose = child_node.SelectSingleNode("Purpose").InnerText;
                    sTime = child_node.SelectSingleNode("Time").InnerText;
                    sStart_img = child_node.SelectSingleNode("StartImage").InnerText;
                    sEnd_img = child_node.SelectSingleNode("EndImage").InnerText;

                    sData.AddScenario(sTag, sTitle, sPurpose, sTitle, sStart_img, sEnd_img);
                    listBox1.Items.Add(sTitle);

                    if (last_index > sTag)
                    {
                        last_index = sTag;
                    }
                }
            }
            else
            {
                MessageBox.Show("no Info");
            }
            MessageBox.Show(this.listBox1.Items.Count.ToString());
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
                    this.Close();
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
        }

        private void Scenario_btn_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            flowLayoutPanel1.Visible = false;
            btnAddImages.Visible = false;

            //시나리오 페이지 띄우기
            //Data.cs에 시나리오 부분 필요.
            //총 소요시간, 목적, 이름, 시작.png, 도착.png
        }

        private void Project_btn_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = true;
            panel2.Visible = false;
            btnAddImages.Visible = true;
        }

        private void EDIT_btn_Click(object sender, EventArgs e)
        {
            //리스트박스에서 선택된 항목 수정하기
            if(listBox1.SelectedItem == null)
            {
                MessageBox.Show("please select an item");
            }
            else
            {
                Scenario scenario = new Scenario(1, myPath, sData, listBox1.SelectedIndex, listBox1);
                scenario.Show();
            }
        }

        private void NEW_btn_Click(object sender, EventArgs e)
        {
            //시나리오 등록하는 창 새로 띄워서 추가할 수 있도록
            Scenario scenario = new Scenario(0, myPath, sData, 0, listBox1);
            scenario.Show();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            //리스트박스에서 선택된 항목 보여주기 수정x
            Scenario scenario = new Scenario(2, myPath, sData, listBox1.SelectedIndex, listBox1);
            scenario.Show();
        }

        private void SAVE_btn_Click(object sender, EventArgs e)
        {
            //xml 만들기
            DirectoryInfo difo = new DirectoryInfo(myPath);
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.NewLineOnAttributes = true;
            using (XmlWriter xmlWriter = XmlWriter.Create(myPath + "\\" + "scenario.xml", xmlWriterSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("ScenarioTable");
                //xmlWriter.WriteStartElement("ProjectName");
                //xmlWriter.WriteString(pData.GetDeviceType());               //project name
                //xmlWriter.WriteEndElement();

                CreateNode(xmlWriter);

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
                MessageBox.Show("create");

                //기존의 ZIP파일 지우고 다시 생성하기
                //if (File.Exists(mPath + difo.Name + ".zip"))  //프로젝트 이름
                //{
                //    MessageBox.Show("기존 파일 삭제");
                //    File.Delete(mPath + difo.Name + ".zip");
                //}

                ////이미 프로그램 상에서 이미지 파일을 사용하고 있기 때문에 사용중이라는 에러가 뜨는데 실제로는 ZIP파일이 생성이 됨. 에러메시지 차단.
                //try
                //{
                //    ZipFile.CreateFromDirectory(mPath, mPath + difo.Name + ".zip");
                //}
                //catch (IOException IOE)
                //{
                //    MessageBox.Show("IOException");
                //}
            }
        }

        private void CreateNode(XmlWriter writer)
        {
            //editProject에서 받아온 정보(create_links)에서 전체 링크 정보를 저장, 파일명으로 링크 구분
            int i = 0, j = 0;
            //해당 인덱스에 링크정보가 하나도 없으면 저장하지 않는다.
            if (sData.getSData().Count != 0)
            {
                for (j = 0; j < sData.getSData().Count; j++)
                {
                    writer.WriteStartElement("Scenario");

                    writer.WriteStartElement("Tag");
                    writer.WriteString(sData.getSData()[j].tag.ToString());
                    writer.WriteEndElement();
                    writer.WriteStartElement("Title");
                    writer.WriteString(sData.getSData()[j].title);
                    writer.WriteEndElement();
                    writer.WriteStartElement("Purpose");
                    writer.WriteString(sData.getSData()[j].purpose);
                    writer.WriteEndElement();
                    writer.WriteStartElement("Time");
                    writer.WriteString(sData.getSData()[j].time);
                    writer.WriteEndElement();
                    writer.WriteStartElement("StartImage");
                    writer.WriteString(sData.getSData()[j].start_img);
                    writer.WriteEndElement();
                    writer.WriteStartElement("EndImage");
                    writer.WriteString(sData.getSData()[j].end_img);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
            }
        }

        private void DEL_btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Deleted!");
            sData.getSData().RemoveAt(listBox1.SelectedIndex);
            this.listBox1.Items.Remove(listBox1.SelectedItem);
        }

        
    }
}
