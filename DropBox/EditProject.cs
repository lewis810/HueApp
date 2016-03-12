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
using System.Reflection;
using Manina.Windows.Forms;
using Ionic.Zip;

namespace DropBox
{
    public partial class EditProject : Form
    {
        LinkData pData = new LinkData();
        ScenarioData sData = new ScenarioData();
        AnalysisData aData = new AnalysisData();
        ResultData rData = new ResultData();

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
        List<string> sName;

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
            pData.SetDownload(false);       //edit image에서 edit project로 넘어오면 download를 다시 하지 않게 함

            //_main = temp;

            SetupButton();
            //ReadLink();
            //ReadScenario();
            //Analysis_Setup();

            for(int i = 0; i < sData.getSData().Count; i++)
            {
                listBox1.Items.Add(sData.getSData()[i].title);
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

        //from main
        public EditProject(string _myPath, string _id, string _project_name)
        {
            
            InitializeComponent();

            _main = new main();             // 2/17 이거만 추가했음
            myPath = _myPath;
            user_id = _id;
            pData.SetProjectName(_project_name);
            pData.SetUserId(_id);
            pData.SetDownload(true);        //main에서부터 올 경우 다운로드 하도록 함

            SetupButton();
            ReadLink();
            ReadScenario();
            Analysis_Setup();


            for(int i = 0; i < sData.getSData().Count; i++)
            {
                Console.WriteLine("받아온거 : " + sData.getSData()[i].title);
                for (int j = 0; j < sData.getSData()[i].paths.Count; j++)
                {
                    Console.WriteLine("받아온거 : " + sData.getSData()[i].paths[j].path);
                }
            }

            Assembly assembly = Assembly.GetAssembly(typeof(ImageListView));
            //imageListView_EditProject.Width = this.Width;
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
            
            //imageListView_EditProject.Location = new Point(0, imageListView_EditProject.Location.Y);
            //imageListView_EditProject.Width = this.Width;
            //MessageBox.Show(imageListView_EditProject.Location.X.ToString() + "//" + imageListView_EditProject.Location.Y.ToString());

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

            //flowLayoutPanel_home.Visible = false;


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
                        //Button newButton = new Button();
                        //newButton.Name = _file.Name;

                        ////image_list에 이미지 이름 다 넣기
                        //image_list.Add(_file.Name);

                        //newButton.Text = overName;
                        //newButton.TextAlign = ContentAlignment.BottomCenter;
                        //newButton.Size = new Size(128, 128);
                        //newButton.Margin = new Padding(10, 10, 10, 10);
                        //newButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.eachButton_Click);
                        ////newButton.Parent = flowLayoutPanel_home;

                        //Image img;
                        //using (var bmpTemp = new Bitmap(_file.DirectoryName + "\\" + _file.Name))
                        //{
                        //    img = new Bitmap(bmpTemp);
                        //}

                        //using (Graphics gr = Graphics.FromImage(img))
                        //{
                        //    gr.SmoothingMode = SmoothingMode.HighQuality;
                        //    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        //    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        //    gr.DrawImage(img, new Rectangle(0, 0, 64, 64));
                        //}

                        //PictureBox newPicture = new PictureBox();
                        //newPicture.Image = img;
                        //newPicture.Size = new Size(90, 80);
                        //newPicture.Location = new Point(20, 10);
                        //newPicture.SizeMode = PictureBoxSizeMode.StretchImage;

                        //newButton.Controls.Add(newPicture);
                        //flowLayoutPanel_home.Controls.Add(newButton);
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
                    sPath = new List<ScenarioData.PATH_DATA>();
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
                    Console.WriteLine("추가");
                    sData.AddScenario(sTag, sTitle, sPurpose, sTitle, sLevel, sPath);
                    
                    listBox1.Items.Add(sTitle);

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

                    pData.GetLinks().Add(new LinkData.LINK() { file_name = fileName, link_data = new List<LinkData.link_info>() });

                    if (!System.IO.Directory.Exists(myPath))
                    {
                        System.IO.Directory.CreateDirectory(myPath);
                    }

                    try
                    {
                        System.IO.File.Copy(temp, destFile);
                    }catch(IOException ie)
                    {
                        MessageBox.Show("파일이름이 중복되거나 이미 파일이 있습니다.");
                    }
                    

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

                    //Button newButton = new Button();
                    //newButton.Name = fileName;
                    //newButton.Text = _overName;
                    //newButton.TextAlign = ContentAlignment.BottomCenter;
                    //newButton.Size = new Size(128, 128);
                    //newButton.Margin = new Padding(10, 10, 10, 10);
                    ////newButton.BackgroundImage = tempImage;
                    ////newButton.BackgroundImageLayout = ImageLayout.Stretch;

                    //PictureBox newPicture = new PictureBox();
                    //newPicture.Image = img;
                    //newPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                    //newPicture.Size = new Size(90, 80);
                    //newPicture.Location = new Point(20, 10);

                    //newButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.eachButton_Click);

                    //newButton.Controls.Add(newPicture);
                    //newPicture.Anchor = AnchorStyles.None;
                    //flowLayoutPanel_home.Controls.Add(newButton);
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

            //this.flowLayoutPanel_home.Controls.Remove(TempDeleteButton);
        }

        private void Scenario_btn_Click(object sender, EventArgs e)
        {
            panel_scenario.Visible = true;
            imageListView_EditProject.Visible = false;
            //flowLayoutPanel_home.Visible = false;
            panel_analysis.Visible = false;
            btnAddImages.Visible = false;

            //시나리오 페이지 띄우기
            //Data.cs에 시나리오 부분 필요.
            //총 소요시간, 목적, 이름, 시작.png, 도착.png
        }

        private void Project_btn_Click(object sender, EventArgs e)
        {
            //flowLayoutPanel_home.Visible = true;
            panel_scenario.Visible = false;
            panel_analysis.Visible = false;
            imageListView_EditProject.Visible = true;
            btnAddImages.Visible = true;
        }

        private void Analysis_btn_Click(object sender, EventArgs e)
        {
            panel_analysis.Visible = true;
            imageListView_EditProject.Visible = false;
            //flowLayoutPanel_home.Visible = false;
            panel_scenario.Visible = false;
            btnAddImages.Visible = false;

            string front = panel_analysis.Controls[0].Name;

            //원래 열려있던 창 다시 열기
            //현재 해당 창이 다시 열리긴 하는데 초기화 되어버려서 보던 자료가 뜨지 않음.
            if (front.CompareTo("Dots") == 0 || front.CompareTo("Partition") == 0 ||
                front.CompareTo("Route") == 0 || front.CompareTo("Survey") == 0)
            {
                panel_analysis.Controls[0].Show();
            }
            else
            {
                Dots dots = new Dots(total_data, pData.GetProjectName(), sData);
                dots.TopLevel = false;
                dots.AutoScroll = true;
                this.panel_analysis.Controls.Add(dots);
                dots.FormBorderStyle = FormBorderStyle.None;
                dots.Dock = DockStyle.Fill;
                dots.Show();
                dots.BringToFront();
            }
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

            for (int a = 0; a < sData.getSData().Count; a++)
            {
                for (int b = 0; b < sData.getSData()[a].paths.Count; b++)
                {
                    Console.WriteLine(a.ToString() + " : " + sData.getSData()[a].paths[b].path);
                }
            }

            ////xml 만들기
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
                if (File.Exists(myPath + difo.Name + ".zip"))  //프로젝트 이름
                {
                    File.Delete(myPath + difo.Name + ".zip");
                }

                string[] filenames = Directory.GetFiles(myPath, "*.*");

                bool zipped = false;
                using (ZipFile zip = new ZipFile(myPath))
                {
                    zip.AddFiles(filenames, false, "");
                    zip.Save(string.Format("{0}{1}.zip", myPath, difo.Name));
                    zipped = true;
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
            //_main.refresh();
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
            sName = new List<string>();

            for(int i = 0; i < listBox1.Items.Count; i++)
            {
                Console.WriteLine(listBox1.Items[i].ToString());
                sName.Add(listBox1.Items[i].ToString());
            }

            string[] download_filenames = Directory.GetFiles(@"C:\Users\" + Environment.UserName + "\\Nudge", "*.xml");
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\" + Environment.UserName + "\\Nudge");

            if (di.Exists == false)
            {
                di.Create();
            }

            if (pData.GetDownload() == true)
            {
                //파일 다운로스 시작
                FileDownloader fd = new FileDownloader(download_filenames, sData, pData.GetProjectName(), user_id);
            }

            XmlRead();        //다운로드 되어있는 Xml 읽어와서 데이터 저장
        }

        private void XmlRead()
        {
            //Xml파일들이 저장되는 폴더 경로
            string mPath = @"C:\Users\" + Environment.UserName + "\\Nudge";
            DirectoryInfo dInfo = new DirectoryInfo(mPath);
            List<ResultData.ImgTime> riData = new List<ResultData.ImgTime>();

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

                    riData = new List<ResultData.ImgTime>();

                    //xml 이름에서 underbar 위치 찾아내서 각 정보 불러올 때 사용
                    for (int k = 0; k < filenames[i].Length; k++)
                    {
                        if (filenames[i][k].CompareTo('_') == 0)
                        {
                            under_bar_index.Add(k);
                        }
                    }

                    //peer // user 구분
                    string temp_scenario_tag = filenames[i].Substring(0, under_bar_index[0]);
                    string temp_scenario_name = filenames[i].Substring(under_bar_index[1] + 1, (under_bar_index[2] - under_bar_index[1]) - 1);
                    string temp_devicie_id = filenames[i].Substring(under_bar_index[2] + 1, (under_bar_index[3] - under_bar_index[2]) - 1);
                    string temp_project_name = filenames[i].Substring(under_bar_index[0] + 1, (under_bar_index[1] - under_bar_index[0]) - 1);
                    string temp_test_type = filenames[i].Substring(under_bar_index[3] + 1, (under_bar_index[4] - under_bar_index[3]) - 1);

                    Console.WriteLine("시작");
                    Console.WriteLine(temp_scenario_tag);
                    Console.WriteLine(temp_scenario_name);
                    Console.WriteLine(temp_devicie_id);
                    Console.WriteLine(temp_project_name);
                    Console.WriteLine(temp_test_type);

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
                        Console.WriteLine("add" + scenario_add.ToString());
                        Console.WriteLine(pData.GetProjectName());
                        Console.WriteLine(scenario_name);

                        //위에서 중복된 정보가 아니더라도 tag, scenario_name, project_name 중 하나라도 다르면 입력 x
                        if (scenario_add == false && temp_project_name.CompareTo(pData.GetProjectName()) == 0)
                        {
                            for(int p = 0; p < sData.getSData().Count; p++)
                            {
                                if(temp_scenario_name.CompareTo(sData.getSData()[p].title) == 0)
                                {
                                    route_data.Add(new RouteData() { tag = Convert.ToInt32(temp_scenario_tag), creation = date_time, scenario_name = temp_scenario_name, device_id = temp_devicie_id, images = new List<string>(), visit_time = new List<double>() });
                                    break;
                                }
                            }
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
                                    total_data[j].event_data.Add(new EventData() { xcoord = x_cor, ycoord = y_cor, event_info = pEvent, img = pImg, timeEntire = pTimeEntire, timeImg = pTimeImg });
                                    data_in = true;
                                    break;
                                }
                            }

                            //total data안에 없으면 새로운 정보 입력
                            if (data_in == false)
                            {
                                total_data.Add(new TotalData() { image_name = pImg, event_data = new List<EventData>() });
                                total_data[total_data.Count - 1].event_data.Add(new EventData() { xcoord = x_cor, ycoord = y_cor, event_info = pEvent, img = pImg, timeEntire = pTimeEntire, timeImg = pTimeImg });
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

                        Console.WriteLine("count : " + route_data[route_data.Count - 1].images.Count.ToString());
                        for (int k = 0; k < route_data[route_data.Count - 1].images.Count; k++)
                        {
                            
                            riData.Add(new ResultData.ImgTime() { imgName = route_data[route_data.Count - 1].images[k], timeImg = route_data[route_data.Count - 1].visit_time[k].ToString() });
                        }

                        bool isMin = false;
                        for(int j = 0; j < sName.Count; j++)
                        {
                            Console.WriteLine("비교1 : " + sName[j] + "==" + temp_scenario_name);
                            Console.WriteLine("비교2 : " + sData.getSData()[j].paths.Count.ToString() + "==" + riData.Count.ToString());
                            if (sName[j].CompareTo(temp_scenario_name) == 0 && sData.getSData()[j].paths.Count == riData.Count)
                            {
                                isMin = true;
                                for(int h = 0; h < riData.Count; h++)
                                {
                                    Console.WriteLine("비교3 : " + riData[h].imgName + "==" + sData.getSData()[j].paths[h].path);
                                    if(riData[h].imgName.CompareTo(sData.getSData()[j].paths[h].path) == 0)
                                    {

                                    }
                                    else
                                    {
                                        isMin = false;
                                        break;
                                    }
                                }
                                if (isMin == false) break;
                            }
                        }

                        Console.WriteLine("입력한 것");
                        Console.WriteLine(temp_scenario_name);
                        rData.AddResultInfo(pData.GetProjectName(), temp_scenario_name, riData, isMin, Convert.ToInt16(temp_scenario_tag));

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

                        if (scenario_add == false && temp_project_name.CompareTo(pData.GetProjectName()) == 0)
                        {
                            for (int p = 0; p < sData.getSData().Count; p++)
                            {
                                if (temp_scenario_name.CompareTo(sData.getSData()[p].title) == 0)
                                {
                                    survey_data.Add(new SurveyData() { tag = Convert.ToInt32(temp_scenario_tag), creation = date_time, scenario_name = temp_scenario_name, device_id = temp_devicie_id, survey_info = new List<SurveyInternalInfo>() });
                                    break;
                                }
                            }
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

            for (int p = 0; p < rData.getRData().Count; p++)
            {
                Console.WriteLine("projectName : " + rData.getRData()[p].projectName);
                Console.WriteLine("taskName : " + rData.getRData()[p].taskName);
                Console.WriteLine("isMin : " + rData.getRData()[p].isMin);
                Console.WriteLine("idx : " + rData.getRData()[p].idx);
                for(int u = 0; u < rData.getRData()[p].pathInfo.Count; u++)
                {
                    Console.WriteLine("imgName : " + rData.getRData()[p].pathInfo[u].imgName);
                    Console.WriteLine("timeImg : " + rData.getRData()[p].pathInfo[u].timeImg);
                }
            }
        }

        private void Dots_btn_Click(object sender, EventArgs e)
        {
            Dots dots = new Dots(total_data, pData.GetProjectName(), sData);
            dots.TopLevel = false;
            dots.AutoScroll = true;
            this.panel_analysis.Controls.Add(dots);
            dots.FormBorderStyle = FormBorderStyle.None;
            dots.Dock = DockStyle.Fill;
            dots.Show();
            dots.BringToFront();
        }
        
        private void Partition_btn_Click(object sender, EventArgs e)
        {
            Partition partition = new Partition(total_data, pData.GetProjectName(), sData);
            partition.TopLevel = false;
            partition.AutoScroll = true;
            this.panel_analysis.Controls.Add(partition);
            partition.FormBorderStyle = FormBorderStyle.None;
            partition.Dock = DockStyle.Fill;
            partition.Show();
            partition.BringToFront();
        }

        private void Route_btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show(route_data.Count.ToString());
            Route route = new Route(route_data, pData.GetProjectName(), sData);
            route.TopLevel = false;
            route.AutoScroll = true;
            this.panel_analysis.Controls.Add(route);
            route.FormBorderStyle = FormBorderStyle.None;
            route.Dock = DockStyle.Fill;
            route.Show();
            route.BringToFront();
        }

        private void Survey_btn_Click(object sender, EventArgs e)
        {
            Survey survey = new Survey(survey_data, pData.GetProjectName(), sData);
            survey.TopLevel = false;
            survey.AutoScroll = true;
            this.panel_analysis.Controls.Add(survey);
            survey.FormBorderStyle = FormBorderStyle.None;
            survey.Dock = DockStyle.Fill;
            survey.Show();
            survey.BringToFront();
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


            //Delete delete = new Delete(route_data, survey_data, filenames, pData.GetProjectName(),
            //    cb_analysis_route_selectGroup, cb_analysis_route_selectTest, cb_analysis_survey_selectGroup, cb_analysis_survey_selectTest,
            //    cb_analysis_dots_selectGroup, cb_analysis_dots_selectTest, cb_analysis_dots_selectImage,
            //    cb_analysis_partition_selectGroup, cb_analysis_partition_selectTest, cb_analysis_partition_selectImage);
            //delete.Show();
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
        }
        //survey//

        private void Graph_btn_Click(object sender, EventArgs e)
        {
            Graph graph = new Graph(rData, pData.GetProjectName(), sName);
            graph.TopLevel = false;
            graph.AutoScroll = true;
            this.panel_analysis.Controls.Add(graph);
            graph.FormBorderStyle = FormBorderStyle.None;
            graph.Dock = DockStyle.Fill;
            graph.Show();
            graph.BringToFront();
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
        }



        public List<RouteData> GetRouteData()
        {
            return this.route_data;
        }
        
    }
}
