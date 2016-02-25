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
using System.IO.Compression;
using System.Reflection;
using Manina.Windows.Forms;

namespace DropBox
{
    public partial class EditProject : Form
    {
        LinkData pData = new LinkData();
        ScenarioData sData = new ScenarioData();

        private string myPath;
        private Button TempDeleteButton;
        private ContextMenu cm;

        main _main;

        public struct SCENARIO
        {
            public int tag;
            public string title;
            public string purpose;
            public string level;
            public string time;
        }

        public struct LINK
        {
            public string fileName;
            public List<link_info_temp> link_data_temp;
        }

        public struct link_info_temp
        {
            public int btn_id;
            public Button bttn;
            public string DstFile;
            public Point image_xy;
            public float image_width;
            public float image_height;
            public string input_type;
        }

        List<SCENARIO> scenarios = new List<SCENARIO>();
        List<LINK> links = new List<LINK>() { };
        List<string> image_list = new List<string>();
        LINK temp_link;
        int last_index = 0;
        public ListBox listBox;
        string user_id;

        /// <summary>
        /// Data for analysis
        /// </summary>
        public struct TotalData
        {
            public string image_name;
            public List<EventData> event_data;  //해당 xml의 모든 데이터들이 들어갈 리스트
        }

        public struct EventData
        {
            public string div;
            public int xcoord;
            public int ycoord;
            public string event_info;
            public string img;
            public double timeEntire;
            public double timeImg;
        }

        public struct RouteData
        {
            public int tag;
            public string div;
            public DateTime creation;
            public string scenario_name;
            public string device_id;
            public List<string> images;
            public List<double> visit_time;
        }

        public struct SurveyData
        {
            public int tag;
            public string div;
            public DateTime creation;
            public string scenario_name;
            public string device_id;
            public List<SurveyInternalInfo> survey_info;
        }

        public struct SurveyInternalInfo
        {
            public string question_type;
            public string question;
            public string answer;
            public string beforeImg;
            public string afterImg;
        }

        List<TotalData> total_data;

        float width, height;
        int w_count, h_count;
        float each_width, each_height;
        List<PictureBox> pictures;
        List<int> count;
        Image mainImage;
        PictureBox pic;
        double ratio;
        Graphics g;
        Panel panel_dots;
        Bitmap bitmap;
        SolidBrush br;
        List<int> under_bar_index;
        List<RouteData> route_data;
        List<SurveyData> survey_data;

        string scenario_name;
        string[] filenames;
        string delete_file = "test.txt";

        //data for analysis


        public EditProject()
        {
            InitializeComponent();

            Assembly assembly = Assembly.GetAssembly(typeof(ImageListView));

            imageListView_EditProject.SetRenderer(new ImageListViewRenderers.DefaultRenderer());
            imageListView_EditProject.SortColumn = 0;
            imageListView_EditProject.SortOrder = Manina.Windows.Forms.SortOrder.AscendingNatural;

            string cacheDir = Path.Combine(
                Path.GetDirectoryName(new Uri(assembly.GetName().CodeBase).LocalPath),
                "Cache"
                );
            if (!Directory.Exists(cacheDir))
                Directory.CreateDirectory(cacheDir);
            imageListView_EditProject.PersistentCacheDirectory = cacheDir;
            imageListView_EditProject.Columns.Add(ColumnType.Name);
            imageListView_EditProject.Columns.Add(ColumnType.Dimensions);
            imageListView_EditProject.Columns.Add(ColumnType.FileSize);
            imageListView_EditProject.Columns.Add(ColumnType.FolderName);
        }

        public EditProject(string _myPath, LinkData _pData, ScenarioData _sData, string _user_id)
        {
            InitializeComponent();

            _main = new main();
            myPath = _myPath;
            pData = _pData;
            sData = _sData;
            user_id = _user_id;

            //_main = temp;

            SetupButton();

            for(int i = 0; i < sData.getSData().Count; i++)
            {
                listBox1.Items.Add(sData.getSData()[i].title);
                cb_analysis_dots_selectScenario.Items.Add(sData.getSData()[i].title);
                cb_analysis_partition_selectScenario.Items.Add(sData.getSData()[i].title);
                cb_analysis_route_selectScenario.Items.Add(sData.getSData()[i].title);
                cb_analysis_survey_selectScenario.Items.Add(sData.getSData()[i].title);
            }

            Assembly assembly = Assembly.GetAssembly(typeof(ImageListView));

            imageListView_EditProject.SetRenderer(new ImageListViewRenderers.DefaultRenderer());
            imageListView_EditProject.SortColumn = 0;
            imageListView_EditProject.SortOrder = Manina.Windows.Forms.SortOrder.AscendingNatural;

            string cacheDir = Path.Combine(
                Path.GetDirectoryName(new Uri(assembly.GetName().CodeBase).LocalPath),
                "Cache"
                );
            if (!Directory.Exists(cacheDir))
                Directory.CreateDirectory(cacheDir);
            imageListView_EditProject.PersistentCacheDirectory = cacheDir;
            imageListView_EditProject.Columns.Add(ColumnType.Name);
            imageListView_EditProject.Columns.Add(ColumnType.Dimensions);
            imageListView_EditProject.Columns.Add(ColumnType.FileSize);
            imageListView_EditProject.Columns.Add(ColumnType.FolderName);

            cm = new ContextMenu();
            cm.MenuItems.Add("Delete", new System.EventHandler(this.imageListView_menuItem_delete_click));
        }

        public EditProject(string _myPath, string _id, string _project_name)
        {
            InitializeComponent();

            _main = new main();             // 2/17 이거만 추가했음
            myPath = _myPath;
            user_id = _id;
            pData.SetProjectName(_project_name);

            SetupButton();
            ReadLink();
            ReadScenario();

            panel_analysis_dot_main.Visible = true;
            panel_analysis_partition_main.Visible = false;
            panel_analysis_route_main.Visible = false;
            panel_analysis_survey_main.Visible = false;

            Assembly assembly = Assembly.GetAssembly(typeof(ImageListView));

            imageListView_EditProject.SetRenderer(new ImageListViewRenderers.DefaultRenderer());
            imageListView_EditProject.SortColumn = 0;
            imageListView_EditProject.SortOrder = Manina.Windows.Forms.SortOrder.AscendingNatural;

            string cacheDir = Path.Combine(
                Path.GetDirectoryName(new Uri(assembly.GetName().CodeBase).LocalPath),
                "Cache"
                );
            if (!Directory.Exists(cacheDir))
                Directory.CreateDirectory(cacheDir);
            imageListView_EditProject.PersistentCacheDirectory = cacheDir;
            imageListView_EditProject.Columns.Add(ColumnType.Name);
            imageListView_EditProject.Columns.Add(ColumnType.Dimensions);
            imageListView_EditProject.Columns.Add(ColumnType.FileSize);
            imageListView_EditProject.Columns.Add(ColumnType.FolderName);

            cm = new ContextMenu();
            cm.MenuItems.Add("Delete", new System.EventHandler(this.imageListView_menuItem_delete_click));
        }


        private void SetupButton()
        {
            temp_link.link_data_temp = new List<link_info_temp>();
            panel_scenario.Visible = false;
            panel_analysis.Visible = false;
            listBox = this.listBox1;

            DirectoryInfo Info = new DirectoryInfo(myPath + "\\");

            //프로젝트 이름
            ProjectName_label.Text = Info.Name;

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

                        imageListView_EditProject.Items.Add(_file.FullName);

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
                        newButton.Parent = flowLayoutPanel_home;

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
                        flowLayoutPanel_home.Controls.Add(newButton);
                    }
                }
            }
        }

        //read
        private void ReadLink()
        {
            FileInfo Info = new FileInfo(myPath + "\\" + "link.xml");
            if (Info.Exists)
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(myPath + "\\" + "link.xml");
                XmlNode nodeDevice = xmlDoc.DocumentElement.SelectSingleNode("/LinkTable");
                pData.SetDeviceType(nodeDevice.SelectSingleNode("DeviceInfo").InnerText);
                pData.SetDeviceResolution(nodeDevice.SelectSingleNode("DeviceResolution").InnerText);
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
                //MessageBox.Show("no Info");
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

                string sTitle, sPurpose, sTime, sLevel;
                int sTag = 0;
                List<ScenarioData.PATH_DATA> sPath = new List<ScenarioData.PATH_DATA>();
                string pPath, pPathTime;

                foreach (XmlNode child_node in nodeList)
                {
                    sPath.Clear();
                    sTag = Convert.ToInt32(child_node.SelectSingleNode("Tag").InnerText);
                    sTitle = child_node.SelectSingleNode("Title").InnerText;
                    sPurpose = child_node.SelectSingleNode("Purpose").InnerText;
                    sLevel = child_node.SelectSingleNode("Level").InnerText;

                    XmlNode new_node = child_node.SelectSingleNode("Path");

                    for(int j = 0; j < Convert.ToInt16(new_node.Attributes["count"].Value.ToString()); j++)
                    {
                        XmlNode temp = new_node.SelectSingleNode("Route" + (j+1).ToString());
                        pPath = temp.Attributes["img"].Value.ToString();
                        pPathTime = temp.InnerText;
                        sPath.Add(new ScenarioData.PATH_DATA() { tag = j, path = pPath, time = pPathTime });
                    }
                    sTime = child_node.SelectSingleNode("TotalTime").InnerText;
                    sData.AddScenario(sTag, sTitle, sPurpose, sTitle, sLevel, sPath);
                    listBox1.Items.Add(sTitle);

                    //시나리오 정보 읽어오면서 목록 콤보박스에 저장
                    cb_analysis_dots_selectScenario.Items.Add(sTitle);
                    cb_analysis_partition_selectScenario.Items.Add(sTitle);
                    cb_analysis_route_selectScenario.Items.Add(sTitle);
                    cb_analysis_survey_selectScenario.Items.Add(sTitle);

                    if (last_index > sTag)
                    {
                        last_index = sTag;
                    }
                }
            }
            else
            {
                //MessageBox.Show("no Info");
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


                    imageListView_EditProject.Items.Add(destFile);


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
                    flowLayoutPanel_home.Controls.Add(newButton);
                }
            }
        }

        private void eachButton_Click(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:

                    Button temp_btn = sender as Button;

                    string mPath = myPath + "\\" + temp_btn.Name;


                    Edit_Image editProject = new Edit_Image(myPath, mPath, pData, sData, user_id);
                    this.Dispose();
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

            this.flowLayoutPanel_home.Controls.Remove(TempDeleteButton);
        }

        private void Scenario_btn_Click(object sender, EventArgs e)
        {
            panel_scenario.Visible = true;
            flowLayoutPanel_home.Visible = false;
            panel_analysis.Visible = false;
            btnAddImages.Visible = false;

            //시나리오 페이지 띄우기
            //Data.cs에 시나리오 부분 필요.
            //총 소요시간, 목적, 이름, 시작.png, 도착.png
        }

        private void Project_btn_Click(object sender, EventArgs e)
        {
            flowLayoutPanel_home.Visible = true;
            panel_scenario.Visible = false;
            panel_analysis.Visible = false;
            btnAddImages.Visible = true;
        }

        private void Analysis_btn_Click(object sender, EventArgs e)
        {
            panel_analysis.Visible = true;
            flowLayoutPanel_home.Visible = false;
            panel_scenario.Visible = false;
            btnAddImages.Visible = false;

            Analysis_Setup();
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
            if(listBox1.SelectedItem != null)
            {
                //리스트박스에서 선택된 항목 보여주기 수정x
                Scenario scenario = new Scenario(2, myPath, sData, listBox1.SelectedIndex, listBox1);
                scenario.Show();
            }
            
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
                //MessageBox.Show(pData.GetDeviceType());
                //GetDeviceType(pData.GetDeviceType());

                CreateNode(xmlWriter);

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
                MessageBox.Show("create");

                //기존의 ZIP파일 지우고 다시 생성하기
                if (File.Exists(myPath + difo.Name + ".zip"))  //프로젝트 이름
                {
                    File.Delete(myPath + difo.Name + ".zip");
                }

                //이미 프로그램 상에서 이미지 파일을 사용하고 있기 때문에 사용중이라는 에러가 뜨는데 실제로는 ZIP파일이 생성이 됨. 에러메시지 차단.
                try
                {
                    ZipFile.CreateFromDirectory(myPath, myPath + difo.Name + ".zip");
                }
                catch (IOException IOE)
                {
                }
            }
        }

        private void CreateNode(XmlWriter writer)
        {
            //editProject에서 받아온 정보(create_links)에서 전체 링크 정보를 저장, 파일명으로 링크 구분
            int i = 0, j = 0, totalTime = 0;
            //해당 인덱스에 링크정보가 하나도 없으면 저장하지 않는다.
            if (sData.getSData().Count != 0)
            {
                for (j = 0; j < sData.getSData().Count; j++)
                {
                    totalTime = 0;
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
                    writer.WriteStartElement("Level");
                    writer.WriteString(sData.getSData()[j].level);
                    writer.WriteEndElement();

                    writer.WriteStartElement("Path");
                    writer.WriteStartAttribute("count");
                    writer.WriteString(sData.getSData()[j].paths.Count.ToString());

                    //경로 xml 저장하는 부분!
                    for (int k = 0; k < sData.getSData()[j].paths.Count; k++)
                    {
                        writer.WriteStartElement("Route" + (k+1).ToString());
                        writer.WriteStartAttribute("img");
                        writer.WriteString(sData.getSData()[j].paths[k].path);
                        writer.WriteEndAttribute();
                        writer.WriteString(sData.getSData()[j].paths[k].time);
                        writer.WriteEndElement();
                        totalTime += Convert.ToInt16(sData.getSData()[j].paths[k].time);
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("TotalTime");
                    writer.WriteString(totalTime.ToString());
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
            }
        }

        private void DEL_btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Deleted!");
            //시나리오 삭제할 때 인덱스값을.. 찾아서 지우기?
            sData.getSData().RemoveAt(listBox1.SelectedIndex);
            this.listBox1.Items.Remove(listBox1.SelectedItem);
        }


        private void imageListView1_itemDoubleClick(object sender, ItemClickEventArgs e)
        {
            ImageListViewItem item = imageListView_EditProject.SelectedItems[0];

            //string mPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + item.FolderName + "\\";

            //MessageBox.Show(item.FileName);

            Edit_Image editImage = new Edit_Image(myPath, item.FileName, pData, sData, user_id);
            this.Dispose();
            editImage.Show();

        }

        private void imageListView1_itemClick(object sender, ItemClickEventArgs e)
        {
            if ((e.Buttons & MouseButtons.Right) != MouseButtons.None)
            {
                
                imageListView_EditProject.ContextMenu = cm;
            }
        }

        private void imageListView_menuItem_delete_click(object sender, EventArgs e)
        {
            foreach (ImageListViewItem item in imageListView_EditProject.SelectedItems)
            {
                if (File.Exists(item.FileName))
                {
                    
                    //MessageBox.Show(item.FileName, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    File.Delete(item.FileName);
                    imageListView_EditProject.Items.Remove(item);
                    
                }
            }
        }

        private void EditProject_FormClosed(object sender, FormClosedEventArgs e)
        {
            _main.refresh();
        }

        private void Analysis_Setup()
        {
            //처음 눌렀을 때 초기화 해주고 그 다음부터는 바로 보여주는 곳으로.

            total_data = new List<TotalData>();
            pictures = new List<PictureBox>();
            count = new List<int>();
            under_bar_index = new List<int>();
            route_data = new List<RouteData>();
            survey_data = new List<SurveyData>();

            scenario_name = "글 수정하기";           //combobox_scenario 에서 인덱스 0번으로 받아오기.

            width = panel_analysis_picture.Width;
            height = panel_analysis_picture.Height;

            

            //나중에 리사이즈 할 때도 적용해야함
            panel_analysis_dot_main.Size = panel_analysis.Size;
            panel_analysis_partition_main.Size = panel_analysis.Size;
            panel_analysis_route_main.Size = panel_analysis.Size;
            panel_analysis_survey_main.Size = panel_analysis.Size;

            panel_analysis_picture.Height = (int)(panel_analysis.Height * 0.85);

            //backgroundimage width height를 해상도 받아온 것으로 해야함 - 뒤에 숫자.
            panel_analysis_picture.Width = (int)(panel_analysis_picture.Height * ((double)720 / (double)1280));
            bitmap = new Bitmap(panel_analysis_picture.Width, panel_analysis_picture.Height);

            panel_analysis_partition_picture.Size = panel_analysis_picture.Size; //여기 나중에 수정

            g = Graphics.FromImage(bitmap);

            filenames = Directory.GetFiles(@"C:\Users\" + Environment.UserName + "\\Nudge", "*.xml");

            //파일 다운로스 시작
            //FileDownloader fd = new FileDownloader(filenames, scenario_name, project_name);

            ratio = (double)panel_analysis_picture.Width / (double)720;

            //이미지 가로 세로 분할 횟수 초기화
            tb_analysis_partition_hori.Text = "10";     //가로
            tb_analysis_partition_verti.Text = "20";    //세로

            XmlRead();        //다운로드 되어있는 Xml 읽어와서 데이터 저장
        }

        private void XmlRead()
        {
            //Xml파일들이 저장되는 폴더 경로
            string mPath = @"C:\Users\" + Environment.UserName + "\\Nudge";
            DirectoryInfo dInfo = new DirectoryInfo(mPath);

            string div_temp;
            DateTime date_time;

            if (dInfo.Exists)
            {
                //다운로드 되고 나서 다시 xml 파일 정보 불러오기
                filenames = Directory.GetFiles(@"C:\Users\" + Environment.UserName + "\\Nudge", "*.xml");

                for (int i = 0; i < filenames.Length; i++)
                {
                    //경로에서 파일 이름만 추출
                    filenames[i] = Path.GetFileName(filenames[i]);
                }

                ////xml name
                ////number
                //Console.WriteLine(filenames[0][0]);
                ////user or peer
                //Console.WriteLine(filenames[0].Substring(under_bar_index[0] + 1, (under_bar_index[1] - under_bar_index[0]) - 1));
                ////project
                //Console.WriteLine(filenames[0].Substring(under_bar_index[1] + 1, (under_bar_index[2] - under_bar_index[1]) - 1));
                ////scenario
                //Console.WriteLine(filenames[0].Substring(under_bar_index[2] + 1, (under_bar_index[3] - under_bar_index[2]) - 1));
                ////device id
                //Console.WriteLine(filenames[0].Substring(under_bar_index[3] + 1, (under_bar_index[4] - under_bar_index[3]) - 1));
                ////touch data or survey result
                //Console.WriteLine(filenames[0].Substring(under_bar_index[4] + 1, (under_bar_index[5] - under_bar_index[4]) - 1));
                ////designer name
                //Console.WriteLine(filenames[0].Substring(under_bar_index[5] + 1, (filenames[0].Length - under_bar_index[5]) - 5));

                for (int i = 0; i < filenames.Length; i++)
                {
                    //생성일자
                    date_time = File.GetCreationTime(mPath + "\\" + filenames[i]);

                    //시나리오 추출 &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    under_bar_index.Clear();

                    //xml 이름에서 underbar 위치 찾아내서 각 정보 불러올 때 사용
                    for (int k = 0; k < filenames[i].Length; k++)
                    {
                        if (filenames[i][k].CompareTo('_') == 0)
                        {
                            under_bar_index.Add(k);
                        }
                    }

                    //peer // user 구분
                    div_temp = filenames[i].Substring(under_bar_index[0] + 1, (under_bar_index[1] - under_bar_index[0]) - 1);
                    string temp_scenario_tag = filenames[i][0].ToString();
                    string temp_scenario_name = filenames[i].Substring(under_bar_index[2] + 1, (under_bar_index[3] - under_bar_index[2]) - 1);
                    string temp_devicie_id = filenames[i].Substring(under_bar_index[3] + 1, (under_bar_index[4] - under_bar_index[3]) - 1);
                    string temp_project_name = filenames[i].Substring(under_bar_index[1] + 1, (under_bar_index[2] - under_bar_index[1]) - 1);
                    string temp_test_type = filenames[i].Substring(under_bar_index[4] + 1, (under_bar_index[5] - under_bar_index[4]) - 1);

                    if (temp_test_type.CompareTo("userData") == 0)
                    {
                        bool scenario_add = false;

                        for (int k = 0; k < route_data.Count; k++)
                        {
                            //번호, 시나리오 이름, 디바이스 아이디 같으면 이미 존재 add = true; 
                            if (route_data[k].tag == Convert.ToInt32(temp_scenario_tag)
                                && route_data[k].scenario_name.CompareTo(temp_scenario_name) == 0
                                && route_data[k].device_id.CompareTo(temp_devicie_id) == 0)
                            {
                                scenario_add = true;
                                break;
                            }
                        }

                        //위에서 중복된 정보가 아니더라도 tag, scenario_name, project_name 중 하나라도 다르면 입력 x
                        if (scenario_add == false
                            && temp_project_name.CompareTo(pData.GetProjectName()) == 0
                            && temp_scenario_name.CompareTo(scenario_name) == 0)
                        {
                            route_data.Add(new RouteData() { tag = Convert.ToInt32(temp_scenario_tag), div = div_temp, creation = date_time, scenario_name = temp_scenario_name, device_id = temp_devicie_id, images = new List<string>(), visit_time = new List<double>() });
                        }
                        else continue;
                        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

                        XmlDocument xmlDoc = new XmlDocument();
                        int x_cor = 0, y_cor = 0;
                        double pTimeEntire = 0, pTimeImg = 0;
                        string pEvent = string.Empty, pImg = string.Empty;
                        bool data_in = false;

                        double real1 = 0, real2 = 0;

                        xmlDoc.Load(mPath + "\\" + filenames[i]);
                        XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/project/InputInfo");
                        //각 파일의 생성일자를 받아와서 이것을 기준으로 보여주는 순서 설정

                        foreach (XmlNode child_node in nodeList)
                        {

                            data_in = false;
                            x_cor = Convert.ToInt32(child_node.SelectSingleNode("xcoord").InnerText);
                            y_cor = Convert.ToInt32(child_node.SelectSingleNode("ycoord").InnerText);
                            pEvent = child_node.SelectSingleNode("event").InnerText;
                            pImg = child_node.SelectSingleNode("img").InnerText;
                            pTimeEntire = Convert.ToDouble(child_node.SelectSingleNode("timeEntire").InnerText);
                            pTimeImg = Convert.ToDouble(child_node.SelectSingleNode("timeImg").InnerText);

                            //이미지 이름 확인해서 해당 인덱스의 리스트에 넣기
                            for (int j = 0; j < total_data.Count; j++)
                            {
                                if (total_data[j].image_name.Equals(pImg))
                                {
                                    total_data[j].event_data.Add(new EventData() { div = div_temp, xcoord = x_cor, ycoord = y_cor, event_info = pEvent, img = pImg, timeEntire = pTimeEntire, timeImg = pTimeImg });
                                    data_in = true;
                                    break;
                                }
                            }

                            //total data안에 없으면 새로운 정보 입력
                            if (data_in == false)
                            {
                                total_data.Add(new TotalData() { image_name = pImg, event_data = new List<EventData>() });
                                total_data[total_data.Count - 1].event_data.Add(new EventData() { div = div_temp, xcoord = x_cor, ycoord = y_cor, event_info = pEvent, img = pImg, timeEntire = pTimeEntire, timeImg = pTimeImg });
                            }

                            //시나리오 정보 입력

                            //2번 이미지에서 3번 이미지로 넘어갈 경우, real1은 2->3의 시간을, real2는 1->2의 시간을 가지고 있는다.
                            //두 시간을 빼면 2번 이미지에 머물렀던 시간을 구할 수 있다.
                            real1 = pTimeEntire - pTimeImg;
                            try
                            {
                                if (route_data[route_data.Count - 1].images.ElementAt(route_data[route_data.Count - 1].images.Count - 1).CompareTo(pImg) != 0)
                                {
                                    route_data[route_data.Count - 1].images.Add(pImg);
                                    route_data[route_data.Count - 1].visit_time.Add(real1 - real2);
                                    real2 = real1;
                                }
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                //리스트가 비어있을 경우 처리
                                Console.WriteLine("초기값 입력");
                                route_data[route_data.Count - 1].images.Add(pImg);
                            }
                            continue;
                        }
                        //마지막은 더 이상 이미지의 전환이 없기 때문에 해당 이미지에 머문 시간을 입력한다.
                        route_data[route_data.Count - 1].visit_time.Add(pTimeImg);
                    }

                    //설문조사 데이터에 대한 것
                    else
                    {
                        bool scenario_add = false;

                        for (int k = 0; k < survey_data.Count; k++)
                        {
                            //번호, 시나리오 이름, 디바이스 아이디 같으면 이미 존재 add = true; 
                            if (survey_data[k].tag == Convert.ToInt32(temp_scenario_tag)
                                && survey_data[k].scenario_name.CompareTo(temp_scenario_name) == 0
                                && survey_data[k].device_id.CompareTo(temp_devicie_id) == 0)
                            {
                                scenario_add = true;
                                break;
                            }
                        }

                        //위에서 중복된 정보가 아니더라도 tag, scenario_name, project_name 중 하나라도 다르면 입력 x
                        if (scenario_add == false
                            && temp_project_name.CompareTo(pData.GetProjectName()) == 0
                            && temp_scenario_name.CompareTo(scenario_name) == 0)
                        {
                            survey_data.Add(new SurveyData() { tag = Convert.ToInt32(temp_scenario_tag), div = div_temp, creation = date_time, scenario_name = temp_scenario_name, device_id = temp_devicie_id, survey_info = new List<SurveyInternalInfo>()});
                        }
                        else continue;
                        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

                        XmlDocument xmlDoc = new XmlDocument();
                        string temp_question_type;
                        string temp_question, temp_answer;
                        string temp_beforeImg, temp_afterImg;

                        xmlDoc.Load(mPath + "\\" + filenames[i]);
                        XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/survey/SurveyInfo");

                        foreach (XmlNode child_node in nodeList)
                        {
                            temp_question_type = string.Empty;
                            temp_question = string.Empty;
                            temp_answer = string.Empty;
                            temp_beforeImg = string.Empty;
                            temp_afterImg = string.Empty;

                            temp_question_type = child_node.SelectSingleNode("questionType").InnerText;
                            temp_question = child_node.SelectSingleNode("question").InnerText;
                            temp_answer = child_node.SelectSingleNode("answer").InnerText;

                            if (temp_question_type.CompareTo("resultTestQuestion_overTime") == 0)
                            {
                                temp_beforeImg = child_node.SelectSingleNode("beforeImg").InnerText;
                                temp_afterImg = child_node.SelectSingleNode("afterImg").InnerText;
                            }
                            //general question에 대한 것
                            else
                            {

                            }
                            survey_data[survey_data.Count - 1].survey_info.Add(new SurveyInternalInfo() { question_type = temp_question_type, question = temp_question, answer = temp_answer, afterImg = temp_afterImg, beforeImg = temp_beforeImg });
                        }
                    }
                }
            }
            else
            { //MessageBox.Show("no Info");
            }

            //해당 프로젝트에 있는 모든 사진들의 이름을 combobox에 넣기
            //TODO 여기서 project폴더가 실제로 존재하지 않으면 에러 뜸. 예외처리필요.
            string cPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + pData.GetProjectName();
            IEnumerable<string> imagenames = Directory.GetFiles(cPath, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".jpg") || s.EndsWith(".jpeg") || s.EndsWith(".png"));

            List<string> image_names = new List<string>();
            image_names = imagenames.Cast<string>().ToList();

            
            //해당 프로젝트에 있는 모든 사진들의 이름을 combobox에 넣기 끝

            //시나리오, 테스트, 이미지, 실험군 초기화
            //콤보박스의 0번째로 정하면서 해당 이미지 배경으로 설정하고 dots, shading 호출
            //시나리오
            //for (int i = 0; i < sData.getSData().Count; i++)
            //{
            //    comboBox_analysis_selectScenario.Items.Add(sData.getSData()[i].title);
            //}

            //시나리오
            cb_analysis_dots_selectScenario.SelectedIndex = 0;         //이거도 add다하고나서 사용
            cb_analysis_partition_selectScenario.SelectedIndex = 0;
            cb_analysis_route_selectScenario.SelectedIndex = 0;
            cb_analysis_survey_selectScenario.SelectedIndex = 0;

            //이미지
            for (int i = 0; i < image_names.Count; i++)                       //이거 시나리오 콤보박스 인덱스 change에서 사용
            {
                cb_analysis_dots_selectImage.Items.Add(Path.GetFileName(image_names[i]));
                cb_analysis_partition_selectImage.Items.Add(Path.GetFileName(image_names[i]));
            }
            cb_analysis_dots_selectImage.Sorted = true;
            cb_analysis_partition_selectImage.Sorted = true;

            cb_analysis_dots_selectImage.SelectedIndex = 0;
            cb_analysis_partition_selectImage.SelectedIndex = 0;

            //실험군
            cb_analysis_dots_selectGroup.SelectedIndex = 0;
            cb_analysis_partition_selectGroup.SelectedIndex = 0;
            cb_analysis_route_selectGroup.SelectedIndex = 0;
            cb_analysis_survey_selectGroup.SelectedIndex = 0;

            Image image;                                                          //상동
            image = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + pData.GetProjectName() + "\\" + cb_analysis_dots_selectImage.SelectedItem);
            panel_analysis_picture.BackgroundImage = image;
            panel_analysis_partition_picture.BackgroundImage = image;

            panel_analysis_picture.BackgroundImageLayout = ImageLayout.Stretch;
            panel_analysis_partition_picture.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void comboBox_analysis_selectScenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //해당 시나리오와 같은 시나리오 정보만 불러오기
            for(int i = 0; i < route_data.Count; i++)
            {
                if (route_data[i].scenario_name.CompareTo(cb_analysis_dots_selectScenario.SelectedItem) == 0)
                {
                    cb_analysis_dots_selectTest.Items.Add(route_data[i].tag);
                    cb_analysis_partition_selectTest.Items.Add(route_data[i].tag);
                }
            }
            cb_analysis_dots_selectTest.SelectedIndex = 0;
            cb_analysis_partition_selectTest.SelectedIndex = 0;
        }

        //dots//
        public void Dots(string selected, string _div)
        {
            panel_analysis_picture.Location = new Point(((int)(panel_analysis.Width / 2) - (panel_analysis_picture.Width / 2)), 0);

            //기존에 설정되어 있던 컨트롤 다 지우기
            panel_analysis_picture_dot.Controls.Remove(pic);

            pic = new PictureBox();
            pic.Parent = panel_analysis_picture_dot;
            panel_analysis_picture_dot.Controls.Add(pic);
            pic.Location = new Point(0, 0);

            pic.BackgroundImage = Bitmap.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + pData.GetProjectName() + "\\" + selected);
            pic.BackgroundImageLayout = ImageLayout.Stretch;
            pic.Size = panel_analysis_picture.Size;

            g = pic.CreateGraphics();
            g = Graphics.FromImage(pic.BackgroundImage);

            br = new SolidBrush(SetColor(30, Color.Red));

            int index = 0;
            for (int i = 0; i < total_data.Count; i++)
            {
                if (total_data[i].image_name.CompareTo(selected) == 0)
                {
                    index = i;
                    break;
                }
            }

            //전체 데이터중에 클릭들이 가지고 있는 이미지 이름과 현재 이미지 이름이 같은거만 그리기
            //ALL / USER / PEER
            switch (_div)
            {
                case "모두보기":
                    for (int j = 0; j < total_data[index].event_data.Count; j++)
                    {
                        if (total_data[index].image_name.CompareTo(selected) == 0)
                        {
                            g.FillPie(br, new Rectangle(new Point(total_data[index].event_data[j].xcoord - 25, total_data[index].event_data[j].ycoord - 25), new Size(50, 50)), 0, 360);
                        }
                    }
                    break;
                case "유저만":
                    for (int j = 0; j < total_data[index].event_data.Count; j++)
                    {
                        if (total_data[index].image_name.CompareTo(selected) == 0 && total_data[index].event_data[j].div.CompareTo("u") == 0)
                        {
                            g.FillPie(br, new Rectangle(new Point(total_data[index].event_data[j].xcoord - 25, total_data[index].event_data[j].ycoord - 25), new Size(50, 50)), 0, 360);
                        }
                    }
                    break;
                case "피어만":
                    //p가 없을 때 null exception 뜸
                    for (int j = 0; j < total_data[index].event_data.Count; j++)
                    {
                        if (total_data[index].image_name.CompareTo(selected) == 0 && total_data[index].event_data[j].div.CompareTo("p") == 0)
                        {
                            g.FillPie(br, new Rectangle(new Point(total_data[index].event_data[j].xcoord - 25, total_data[index].event_data[j].ycoord - 25), new Size(50, 50)), 0, 360);
                        }
                    }
                    break;
                default: break;
            }
        }

        private void Dots_btn_Click(object sender, EventArgs e)
        {
            panel_analysis_dot_main.Visible = true;
            panel_analysis_partition_main.Visible = false;
            panel_analysis_route_main.Visible = false;
            panel_analysis_survey_main.Visible = false;

            //cb_analysis_dots_selectScenario.SelectedIndex = cb_analysis_partition_selectScenario.SelectedIndex;
            //cb_analysis_dots_selectTest.SelectedIndex = cb_analysis_partition_selectTest.SelectedIndex;
            //cb_analysis_dots_selectImage.SelectedIndex = cb_analysis_partition_selectImage.SelectedIndex;
            //cb_analysis_dots_selectGroup.SelectedIndex = cb_analysis_partition_selectGroup.SelectedIndex;
            //Dots(cb_analysis_dots_selectImage.SelectedItem.ToString(), cb_analysis_dots_selectGroup.SelectedItem.ToString());
        }

        private void btn_analysis_show_dot_Click(object sender, EventArgs e)
        {
            Dots(cb_analysis_dots_selectImage.SelectedItem.ToString(), cb_analysis_dots_selectGroup.SelectedItem.ToString());
        }

        private void btn_analysis_dot_delete_Click(object sender, EventArgs e)
        {
            //string dPath = @"C:\Users\" + Environment.UserName + "\\Nudge\\" + delete_file;

            //if (File.Exists(dPath))
            //{
            //    //데이터를 삭제하시면 복구가 불가능합니다. 그래도 진행하시겠습니까? 메시지 띄우기
            //    MessageBox.Show("삭제 : " + delete_file);

            //    File.Delete(dPath);

            //    //삭제하기전에 RouteData 수정하기
            //}

            
            Delete delete = new Delete(route_data, survey_data, filenames, pData.GetProjectName(), 
                cb_analysis_route_selectGroup, cb_analysis_route_selectTest, cb_analysis_survey_selectGroup, cb_analysis_survey_selectTest,
                cb_analysis_dots_selectGroup, cb_analysis_dots_selectTest, cb_analysis_dots_selectImage,
                cb_analysis_partition_selectGroup, cb_analysis_partition_selectTest, cb_analysis_partition_selectImage);
            delete.Show();
        }
        //dots//

        //partition//
        public void Partition(int _w_count, int _h_count, string selected, string _div)
        {
            panel_analysis_partition_picture.Location = new Point(((int)(panel_analysis.Width / 2) - (panel_analysis_partition_picture.Width / 2)), 0);
            //초기화
            panel_analysis_partition_picture2.Controls.Clear();
            count.Clear();
            pictures.Clear();

            Image image;
            image = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + pData.GetProjectName() + "\\" + cb_analysis_partition_selectImage.SelectedItem);
            panel_analysis_partition_picture2.BackgroundImage = image;

            //화면분할할 때 각 칸의 가로세로 길이
            each_width = (float)panel_analysis_partition_picture.Width / (float)_w_count;
            each_height = (float)panel_analysis_partition_picture.Height / (float)_h_count;

            for (int i = 0; i < w_count; i++)
            {
                for (int j = 0; j < h_count; j++)
                {
                    panel_analysis_partition_picture2.BackgroundImageLayout = ImageLayout.Stretch;
                    PictureBox pBox = new PictureBox();
                    pBox.Parent = panel_analysis_partition_picture2;
                    panel_analysis_partition_picture2.Controls.Add(pBox);
                    pBox.Location = new Point((int)(each_width * i), (int)(each_height * j));
                    pBox.BackColor = SetColor(50, Color.Black);
                    pBox.Size = new Size((int)each_width, (int)each_height);
                    pictures.Add(pBox);

                    //네모칸의 횟수만큼 0 카운트 추가
                    count.Add(0);
                }
            }

            //combobox에서 정해준 이미지의 인덱스 찾기
            int index = 0;
            for (int i = 0; i < total_data.Count; i++)
            {
                if (total_data[i].image_name.CompareTo(selected) == 0)
                {
                    index = i;
                    break;
                }
            }

            int k = 0;
            switch (_div)
            {
                case "모두보기":
                    //각 포인트마다. 전체 박스를 돌면서 해당 박스에 도착하면 카운트를 올린다.
                    for (int u = 0; u < total_data[index].event_data.Count; u++)
                    {
                        //선택한 이미지에 대한 정보만 읽어오기
                        if (total_data[index].event_data[u].img.CompareTo(selected) == 0)
                        {
                            for (int i = 0; i < pictures.Count; i++)
                            {
                                if ((ratio * total_data[index].event_data[u].xcoord >= pictures[i].Location.X) && (ratio * total_data[index].event_data[u].xcoord <= pictures[i].Location.X + each_width))
                                {
                                    if ((ratio * total_data[index].event_data[u].ycoord >= pictures[i].Location.Y) && (ratio * total_data[index].event_data[u].ycoord <= pictures[i].Location.Y + each_height))
                                    {
                                        k++;
                                        count[i]++;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "유저만":
                    for (int u = 0; u < total_data[index].event_data.Count; u++)
                    {
                        //선택한 이미지에 대한 정보만 읽어오기
                        if (total_data[index].event_data[u].img.CompareTo(selected) == 0 && total_data[index].event_data[u].div.CompareTo("u") == 0)
                        {
                            for (int i = 0; i < pictures.Count; i++)
                            {
                                if ((ratio * total_data[index].event_data[u].xcoord >= pictures[i].Location.X) && (ratio * total_data[index].event_data[u].xcoord <= pictures[i].Location.X + each_width))
                                {
                                    if ((ratio * total_data[index].event_data[u].ycoord >= pictures[i].Location.Y) && (ratio * total_data[index].event_data[u].ycoord <= pictures[i].Location.Y + each_height))
                                    {
                                        k++;
                                        count[i]++;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "피어만":
                    for (int u = 0; u < total_data[index].event_data.Count; u++)
                    {
                        //선택한 이미지에 대한 정보만 읽어오기
                        if (total_data[index].event_data[u].img.CompareTo(selected) == 0 && total_data[index].event_data[u].div.CompareTo("p") == 0)
                        {
                            for (int i = 0; i < pictures.Count; i++)
                            {
                                if ((ratio * total_data[index].event_data[u].xcoord >= pictures[i].Location.X) && (ratio * total_data[index].event_data[u].xcoord <= pictures[i].Location.X + each_width))
                                {
                                    if ((ratio * total_data[index].event_data[u].ycoord >= pictures[i].Location.Y) && (ratio * total_data[index].event_data[u].ycoord <= pictures[i].Location.Y + each_height))
                                    {
                                        k++;
                                        count[i]++;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                default: break;
            }

            //횟수의 범위를 정해서 다른 색으로 구분
            for (int i = 0; i < pictures.Count; i++)
            {
                if (count[i] > 0 && count[i] < 3)
                {
                    pictures[i].BackColor = SetColor(50, Color.Red);
                }
                else if (count[i] >= 3 && count[i] < 5)
                {
                    pictures[i].BackColor = SetColor(50, Color.Blue);
                }
                else if (count[i] >= 5)
                {
                    pictures[i].BackColor = SetColor(50, Color.Green);
                }
            }
        }

        private void Partition_btn_Click(object sender, EventArgs e)
        {
            panel_analysis_partition_main.Visible = true;
            panel_analysis_dot_main.Visible = false;
            panel_analysis_route_main.Visible = false;
            panel_analysis_survey_main.Visible = false;

            //cb_analysis_partition_selectScenario.SelectedIndex = cb_analysis_dots_selectScenario.SelectedIndex;
            //cb_analysis_partition_selectTest.SelectedIndex = cb_analysis_dots_selectTest.SelectedIndex;
            //cb_analysis_partition_selectImage.SelectedIndex = cb_analysis_dots_selectImage.SelectedIndex;
            //cb_analysis_partition_selectGroup.SelectedIndex = cb_analysis_dots_selectGroup.SelectedIndex;
            //Shading(w_count, h_count, cb_analysis_partition_selectImage.SelectedItem.ToString(), cb_analysis_partition_selectGroup.SelectedItem.ToString());
        }

        private void btn_analysis_show_partition_Click(object sender, EventArgs e)
        {
            w_count = Convert.ToInt16(tb_analysis_partition_hori.Text);
            h_count = Convert.ToInt16(tb_analysis_partition_verti.Text);

            

            Partition(w_count, h_count, cb_analysis_partition_selectImage.SelectedItem.ToString(), cb_analysis_partition_selectGroup.SelectedItem.ToString());
        }
        //partition//
       
        //route//
        public void Route(int index)
        {
            //해당 패널의 모든 컨트롤을 클리어하고
            fpanel_analysis_route.Controls.Clear();

            //라우트 데이터의 특정 인덱스에 있는 이미지 파일들과 정보를 불러와서 저장한다.
            //새로운 flow panel을 만들고 처음 만들 시 오른쪽 방향, 그 다음은 왼쪽 방향으로 가는 패널을 만든다.
            //다음으로 넘어가는 조건은, 현재 아이템이 꽉 찼을 때.
            FlowLayoutPanel temp_fp = new FlowLayoutPanel();
            temp_fp.FlowDirection = FlowDirection.LeftToRight;
            fpanel_analysis_route.Controls.Add(temp_fp);
            int fp_height = (int)(fpanel_analysis_route.Height * 0.3);
            temp_fp.Height = fp_height;
            temp_fp.BackColor = SetColor(50, Color.Red);

            
            //for(int i = 0; i < route_data[index].images.Count; i++)
            //{
            //    PictureBox temp_pic = new PictureBox();

            //}

            PictureBox temp_pic = new PictureBox();
            FlowLayoutPanel temp_fp2 = new FlowLayoutPanel();
           

            for (int j = 0; j < route_data[index].images.Count; j++)
            {
                //temp fp가 전체width - left panel 한 것보다 오버가 될 경우 다시 초기화 해서 새로운 fp생성 
                if ((panel_analysis_route_main.Width - panel_analysis_route_left.Width) > (temp_fp.Width + 530)) //나중에 해상도 받아와서 설정 다시
                {
                    RouteAddImage(temp_fp, temp_fp2, temp_pic, index, j);
                }
                
                //flowlayoutpanel이 parent의 길이를 초과할 경우
                else
                {
                    temp_fp = new FlowLayoutPanel();
                    temp_fp.FlowDirection = FlowDirection.LeftToRight;
                    fpanel_analysis_route.Controls.Add(temp_fp);
                    RouteAddImage(temp_fp, temp_fp2, temp_pic, index, j);
                }

            }

        }

        private void RouteAddImage(FlowLayoutPanel temp_fp, FlowLayoutPanel temp_fp2, PictureBox temp_pic, int index, int j)
        {
            temp_fp2 = new FlowLayoutPanel();
            temp_fp.Controls.Add(temp_fp2);
            temp_fp2.Parent = temp_fp;
            temp_fp2.FlowDirection = FlowDirection.TopDown;

            int temp_height = (int)(temp_fp.Height * 0.85);

            temp_pic = new PictureBox();
            try
            {
                temp_pic.BackgroundImage = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + pData.GetProjectName() + "\\" + route_data[index].images[j]);
            }
            catch (FileNotFoundException fe)
            {
                temp_pic.BackColor = Color.Gray;
                Label notFound = new Label();
                notFound.Text = route_data[index].images[j] + "\nnot found";
                temp_pic.Controls.Add(notFound);
                notFound.Anchor = AnchorStyles.None | AnchorStyles.Left;
            }
            temp_pic.Parent = temp_fp2;
            temp_fp2.Controls.Add(temp_pic);
            temp_pic.Size = new Size((int)(temp_height * 0.5625), temp_height);     //나중에 해상도 받아와서 설정 다시
            temp_pic.BackgroundImageLayout = ImageLayout.Stretch;
            temp_pic.Text = route_data[index].images[j];

            Label temp_label = new Label();
            temp_label.Text = route_data[index].images[j];
            temp_fp2.Controls.Add(temp_label);
            temp_label.Size = temp_label.PreferredSize;
            temp_label.Width = temp_pic.Width;
            temp_label.TextAlign = ContentAlignment.MiddleCenter;
            
            Label temp_time = new Label();
            temp_time.Text = route_data[index].visit_time[j].ToString();
            temp_fp2.Controls.Add(temp_time);
            temp_time.Size = temp_time.PreferredSize;
            temp_time.Width = temp_pic.Width;
            temp_time.TextAlign = ContentAlignment.MiddleCenter;

            temp_pic.BackColor = Color.Transparent;
            temp_label.BackColor = Color.Transparent;
            temp_time.BackColor = Color.Transparent;

            temp_fp2.Size = temp_fp2.PreferredSize;

            if (j != route_data[index].images.Count - 1)
            {
                Console.WriteLine("화살표");
                PictureBox arrow = new PictureBox();
                arrow.Parent = temp_fp;
                temp_fp.Controls.Add(arrow);
                arrow.Size = new Size(30, 30);
                arrow.Anchor = AnchorStyles.None;

                //arrow.BackgroundImage = new Bitmap(Properties.Resources.arrow);
                arrow.BackgroundImage = Image.FromFile(@"C:\Users\lewis\Documents\Visual Studio 2015\Projects\DistributionWork\DistributionWork\Resources\arrow.png");
                arrow.BackgroundImageLayout = ImageLayout.Stretch;
            }
            temp_fp.Size = temp_fp.PreferredSize;
        }

        private void Route_btn_Click(object sender, EventArgs e)
        {
            panel_analysis_route_main.Visible = true;
            panel_analysis_partition_main.Visible = false;
            panel_analysis_dot_main.Visible = false;
            panel_analysis_survey_main.Visible = false;
        }

        private void btn_analysis_show_route_Click(object sender, EventArgs e)
        {
            bool exist = false;
            int index = 0;
            //여기서 인덱스 찾아서 넘겨야 할 듯
            for (int i = 0; i < route_data.Count; i++)
            {
                //비교 : tag, div, scenario_name              //태그가 모두 다르다는게 확정이면 div는 비교할 필요 없음
                if ((route_data[i].tag.CompareTo(cb_analysis_route_selectTest.SelectedItem) == 0)
                    && (route_data[i].scenario_name.CompareTo(cb_analysis_route_selectScenario.SelectedItem) == 0))
                {
                    index = i;
                    exist = true;
                    break;
                }
            }

            if (exist == true)
            {
                Route(index);
            }
        }

        private void cb_analysis_route_selectGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_analysis_route_selectTest.Items.Clear();
            //피어목록보여주기
            if (cb_analysis_route_selectGroup.SelectedIndex == 0)
            {
                for (int i = 0; i < route_data.Count; i++)
                {
                    if (route_data[i].div.CompareTo("p") == 0)
                    {
                        cb_analysis_route_selectTest.Items.Add(route_data[i].tag);
                    }
                }
            }
            //유저
            else
            {
                for (int i = 0; i < route_data.Count; i++)
                {
                    if (route_data[i].div.CompareTo("u") == 0)
                    {
                        cb_analysis_route_selectTest.Items.Add(route_data[i].tag);
                    }
                }
            }

            //데이터가 존재하지 않을 때. 
            if(cb_analysis_route_selectTest.Items.Count == 0)
            {
                cb_analysis_route_selectTest.Items.Add("데이터없음");
            }

            cb_analysis_route_selectTest.Sorted = true;
            cb_analysis_route_selectTest.SelectedIndex = 0;
         
        }
        //route//

        //survey//
        private void Survey(int index)
        {
            fpanel_analysis_survey.Controls.Clear();
            for(int i = 0; i < survey_data[index].survey_info.Count; i++)
            {
                if (survey_data[index].survey_info[i].question_type.CompareTo("resultTestQuestion_overTime") == 0)
                {
                    FlowLayoutPanel new_flowlayout = new FlowLayoutPanel();
                    new_flowlayout.FlowDirection = FlowDirection.LeftToRight;
                    fpanel_analysis_survey.Controls.Add(new_flowlayout);

                    PictureBox pBeforeImg = new PictureBox();
                    try
                    {
                        pBeforeImg.BackgroundImage = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + pData.GetProjectName() + "\\" + survey_data[index].survey_info[i].beforeImg);
                    }
                    catch (FileNotFoundException fe)
                    {
                        pBeforeImg.BackColor = Color.Gray;
                        Label notFound = new Label();
                        notFound.Text = survey_data[index].survey_info[i].beforeImg + "\nnot found";
                        pBeforeImg.Controls.Add(notFound);
                        notFound.Anchor = AnchorStyles.None | AnchorStyles.Left;
                    }
                    pBeforeImg.BackgroundImageLayout = ImageLayout.Stretch;
                    pBeforeImg.Size = new Size((int)(200*0.5625), 200);

                    PictureBox arrow = new PictureBox();
                    //arrow.BackgroundImage = Properties.Resources.arrow;
                    arrow.BackgroundImage = Image.FromFile(@"C:\Users\lewis\Documents\Visual Studio 2015\Projects\DistributionWork\DistributionWork\Resources\arrow.png");
                    arrow.Size = new Size(20, 20);
                    arrow.BackgroundImageLayout = ImageLayout.Stretch;
                    

                    PictureBox pAfterImg = new PictureBox();
                    try
                    {
                        pAfterImg.BackgroundImage = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + pData.GetProjectName() + "\\" + survey_data[index].survey_info[i].afterImg);
                    }
                    catch (FileNotFoundException fe)
                    {
                        pAfterImg.BackColor = Color.Gray;
                        Label notFound = new Label();
                        notFound.Text = survey_data[index].survey_info[i].afterImg + "\nnot found";
                        pAfterImg.Controls.Add(notFound);
                        notFound.Anchor = AnchorStyles.None | AnchorStyles.Left;
                    }
                    pAfterImg.BackgroundImageLayout = ImageLayout.Stretch;
                    pAfterImg.Size = new Size((int)(200 * 0.5625), 200);

                    new_flowlayout.Controls.Add(pBeforeImg);
                    new_flowlayout.Controls.Add(arrow);
                    new_flowlayout.Controls.Add(pAfterImg);

                    new_flowlayout.Size = new_flowlayout.PreferredSize;

                    //가운데 화살표 위치 정할 때 상단 여백 주기
                    arrow.Margin = new Padding(0, (int)(new_flowlayout.Height / 2 - arrow.Height / 2), 0, 0);
                }
                //general question
                else
                {

                }
                Label label_q = new Label();
                label_q.Text = "Q. " + survey_data[index].survey_info[i].question;
                fpanel_analysis_survey.Controls.Add(label_q);
                label_q.Size = label_q.PreferredSize;

                Label label_a = new Label();

                label_a.Text = "A. " + survey_data[index].survey_info[i].answer + "\n -----------------------------------------------------------------";
                label_a.ForeColor = Color.Red;
                fpanel_analysis_survey.Controls.Add(label_a);
                label_a.AutoSize = true;
            }
        }

        private void cb_analysis_survey_selectGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_analysis_survey_selectTest.Items.Clear();
            //피어목록보여주기
            if (cb_analysis_survey_selectGroup.SelectedIndex == 0)
            {
                for (int i = 0; i < survey_data.Count; i++)
                {
                    if (survey_data[i].div.CompareTo("p") == 0)
                    {
                        cb_analysis_survey_selectTest.Items.Add(survey_data[i].tag);
                    }
                }
            }
            //유저
            else
            {
                for (int i = 0; i < survey_data.Count; i++)
                {
                    if (survey_data[i].div.CompareTo("u") == 0)
                    {
                        cb_analysis_survey_selectTest.Items.Add(survey_data[i].tag);
                    }
                }
            }
            cb_analysis_survey_selectTest.Sorted = true;

            try
            {
                cb_analysis_survey_selectTest.SelectedIndex = 0;
            }catch(ArgumentOutOfRangeException ae)
            {
                cb_analysis_survey_selectTest.Items.Add("데이터없음");
                cb_analysis_survey_selectTest.SelectedIndex = 0;
            }

        }

        private void btn_analysis_show_survey_Click(object sender, EventArgs e)
        {
            bool exist = false;
            int index = 0;
            //여기서 인덱스 찾아서 넘겨야 할 듯
            for (int i = 0; i < survey_data.Count; i++)
            {
                //비교 : tag, div, scenario_name              //태그가 모두 다르다는게 확정이면 div는 비교할 필요 없음
                if ((survey_data[i].tag.CompareTo(cb_analysis_survey_selectTest.SelectedItem) == 0)
                    && (survey_data[i].scenario_name.CompareTo(cb_analysis_survey_selectScenario.SelectedItem) == 0))
                {
                    index = i;
                    exist = true;
                    break;
                }
            }

            if (exist == true)
            {
                Survey(index);
            }
        }

        private void Survey_btn_Click(object sender, EventArgs e)
        {
            panel_analysis_survey_main.Visible = true;
            panel_analysis_route_main.Visible = false;
            panel_analysis_partition_main.Visible = false;
            panel_analysis_dot_main.Visible = false;
        }

        public void SetSurveyData(List<SurveyData> _survey_data)
        {
            this.survey_data = _survey_data;
            //cb_analysis_route_selectTest.Items.Clear();

            //for(int i = 0; i < route_data.Count; i++)
            //{
            //    //cb를 파라미터로 보내서 수정해야하나.. 
            //    cb_analysis_route_selectTest.Items.Add(route_data[i].tag);
            //    MessageBox.Show(route_data[i].tag.ToString());
            //}
            //MessageBox.Show("완료");

            cb_analysis_survey_selectGroup.SelectedIndex = 0;
        }
        //survey//

        //배경색 + opacity
        public Color SetColor(int A, Color color)
        {
            return Color.FromArgb(A, color.R, color.G, color.B);
        }

        private void Graph_btn_Click(object sender, EventArgs e)
        {

        }

        public void SetRouteData(List<RouteData> _route_data)
        {
            this.route_data = _route_data;
            //cb_analysis_route_selectTest.Items.Clear();

            //for(int i = 0; i < route_data.Count; i++)
            //{
            //    //cb를 파라미터로 보내서 수정해야하나.. 
            //    cb_analysis_route_selectTest.Items.Add(route_data[i].tag);
            //    MessageBox.Show(route_data[i].tag.ToString());
            //}
            //MessageBox.Show("완료");

            cb_analysis_route_selectGroup.SelectedIndex = 0;
        }

       

        public List<RouteData> GetRouteData()
        {
            return this.route_data;
        }
        
    }
}
