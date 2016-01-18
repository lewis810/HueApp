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
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml;

namespace DropBox
{

    public partial class Edit_Image : Form
    {
        struct link_info
        {
            public int btn_id;
            public Button bttn;
            public string from;
            public string to;
            public Point image_xy;
            public float image_width;
            public float image_height;
        };

        struct image_info
        {
            public int Tag;
            public string str_image;
            public string str_name;
        };

        string image_name;
        int image_tag;
        string work_message;
        int btn_id_to_add = -1;

        List<link_info> link_data = new List<link_info>();
        link_info link_temp;
        List<string> str_work = new List<string>();             //화면 우측에 출력되는 링크정보

        List<image_info> imageInfo = new List<image_info>();

        Control[] btn_link;
        Button TempDeleteButton;
        ContextMenu cm;

        //drawing
        int myShape;
        Point myPointStart;
        Point myPointEnd;
        Pen myPen;
        Pen myPenTemp;
        int sizeX;
        int sizeY;
        Boolean myPress;
        Graphics g;
        Rectangle rect;
        //drawing

        Image mainImage, imageTemp;
        System.Drawing.Image img;
        float org_width = 0, org_height = 0, resize_ratio = 0;

        private string mPath;
        private string mfilePath;

        public Edit_Image() { InitializeComponent(); }

        public Edit_Image(string _mPath, string _filePath)
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            pictureBox1.Parent = panel1;
            mPath = _mPath;
            mfilePath = _filePath;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Init for drawing
            this.myShape = 3;
            this.myPointStart = new Point(0, 0);
            this.myPointEnd = new Point(0, 0);
            this.myPen = new Pen(Color.Lime, 2);
            this.myPenTemp = new Pen(Color.Lime, 2);
            this.sizeX = 0;
            this.sizeY = 0;
            this.myPress = false;

            //clear
            this.link_temp = default(link_info);
            this.link_data.Clear();
            btn_id_to_add = 0;

            cm = new ContextMenu();
            cm.MenuItems.Add("Delete", new System.EventHandler(this.menuItem_delete_click));

            //경로 받아오기 - 링크 정보를 추가 수정할 사진 한 장에 대한 경로
            string img_path = mfilePath;//@"C:\Users\lewis\Documents\Visual Studio 2015\Projects\workspace\workspace\pic\main.png";
            //경로 메인 이미지에 입력
            mainImage = Image.FromFile(img_path);
            image_name = Path.GetFileName(img_path);
            link_temp.from = image_name;

            img = System.Drawing.Image.FromFile(mfilePath);
            org_width = img.Width;
            org_height = img.Height;

            //Edit_Image 처음 호출 했을 때 panel1의 사이즈 정하기
            if (img != null && (img.Height > img.Width))
            {
                panel1.Height = (int)(this.Height * 0.9);
                float WidthOverHeight = (float)img.Width / (float)img.Height;    //이미지의 가로세로 비율
                panel1.Width = (int)(WidthOverHeight * (this.Height * 0.9));
                //MessageBox.Show(img.Width.ToString() + "/" + img.Height.ToString() + "*" + this.Height.ToString() + "=" + panel1.Width.ToString() + ", " + WidthOverHeight.ToString());
                panel1.Location = new Point(((this.Width / 2) - (panel1.Width / 2)), 0);

                resize_ratio = (float)panel1.Width / (float)img.Width;
            }
            
            //메인이미지를 pictureBox1의 배경으로 설정
            pictureBox1.BackgroundImage = mainImage;
            groupBox1_Load();
            read_link();
        }

        private void groupBox1_Load()
        {
            //디렉토리 주소를 받아와서 저장
            string path_folder;
            path_folder = mPath; //지용이가 넘겨준 주소값

            int k = 0;
            //반복문을 통해 디렉토리 내부의 이미지 파일 경로 얻어오기
            foreach (var path in Directory.GetFiles(path_folder))
            {
                //이미지 파일만 불러오도록 필터링
                if (Regex.IsMatch(path.ToString(), "jpg", RegexOptions.IgnoreCase) ||
                    Regex.IsMatch(path.ToString(), "png", RegexOptions.IgnoreCase))
                {
                    imageInfo.Add(new image_info() { Tag = k, str_image = path.ToString(), str_name = Path.GetFileName(path) });
                    if (Path.GetFileName(path).CompareTo(image_name) == 0)
                    {
                        image_tag = k; //여기서 태그정보가 무조건 나와야함.
                    }
                    k++;
                }
            }

            btn_link = new Control[imageInfo.Count];

            //프로젝트 내에 있는 파일이름 가져오기
            var imageFiles = new DirectoryInfo(mPath).GetFiles("*.png");

            for (int i = 0; i < imageInfo.Count; i++)
            {
                //링크연결을 위한 이미지
                btn_link[i] = new Button();
                btn_link[i].Parent = this;
                btn_link[i].Location = new Point(10 + i * 110, 10);
                btn_link[i].Size = new Size(100, 120);
                panel_image_link.Controls.Add(btn_link[i]);                         //패널에 버튼을 추가
                btn_link[i].Tag = i.ToString();

                btn_link[i].Click += new EventHandler(btt_link_click);
                btn_link[i].Name = imageFiles[i].Name;
                btn_link[i].Text = i.ToString();
                btn_link[i].ForeColor = Color.Lime;
                btn_link[i].Font = new Font(btn_link[i].Font.Name, 10, FontStyle.Bold);
                btn_link[i].BackgroundImage = Image.FromFile(imageInfo.ElementAt(i).str_image);
                btn_link[i].BackgroundImageLayout = ImageLayout.Stretch;

            }
        }

        //네모박스 그린 이 후 링크연동 시 이미지 파일을 클릭할 때 동작하는 버튼
        public void btt_link_click(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            Button btn = sender as Button;

            link_alloc.Visible = false;

            if (ctl != null)
            {
                link_temp.from = image_name;
                link_temp.to = btn.Name;
                link_temp.bttn = new Button();
                
                //각 버튼의 아이디 부여하기
                link_temp.btn_id = btn_id_to_add++;    

                link_temp.bttn.Parent = pictureBox1;
                link_temp.bttn.TabIndex = link_data.Count;
                link_temp.bttn.MouseUp += new MouseEventHandler(btt_in_picture);
                link_temp.bttn.Location = link_temp.image_xy;
                link_temp.bttn.Size = new Size((int)link_temp.image_width, (int)link_temp.image_height);
                this.pictureBox1.Controls.Add(link_temp.bttn);
                link_temp.bttn.FlatStyle = FlatStyle.Flat;
                link_temp.bttn.BackColor = Color.Transparent;
                link_temp.bttn.FlatAppearance.BorderColor = Color.Lime;
                //link_temp.bttn.FlatAppearance.BorderSize = 2;
                link_temp.bttn.FlatAppearance.MouseDownBackColor = Color.Transparent;
                link_temp.bttn.FlatAppearance.MouseOverBackColor = Color.Transparent;
                link_temp.bttn.ContextMenu = cm;

                //이미지 원본의 사이즈 비율에 맞게 크기 및 좌표 조정
                link_temp.image_width = (int)(link_temp.image_width / resize_ratio);
                link_temp.image_height = (int)(link_temp.image_height / resize_ratio);
                link_temp.image_xy = new Point((int)(link_temp.image_xy.X / resize_ratio), (int)(link_temp.image_xy.Y / resize_ratio));
                link_data.Add(link_temp);

                //*******************
                if (File.Exists(mPath + "link.xml") == false)
                {
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.Indent = true;
                    xmlWriterSettings.NewLineOnAttributes = true;
                    using (XmlWriter xmlWriter = XmlWriter.Create(mPath + "link.xml", xmlWriterSettings))
                    {
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("LinkTable");
                        xmlWriter.WriteStartElement("DeviceInfo");
                        xmlWriter.WriteString("IOS");
                        xmlWriter.WriteEndElement();

                        createNode(image_name, link_temp.btn_id.ToString(), link_temp.from
                            , link_temp.to, 
                            ((double)(link_temp.image_xy.X) / resize_ratio).ToString(),
                            ((double)(link_temp.image_xy.Y) / resize_ratio).ToString(), 
                            (link_temp.image_width / resize_ratio).ToString(), 
                            (link_temp.image_height / resize_ratio).ToString(), 
                            xmlWriter);

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();
                        xmlWriter.Flush();
                        xmlWriter.Close();
                        MessageBox.Show("XML File created ! ");
                    }

                }
                else
                {
                    appendNode(image_name, link_temp.btn_id.ToString(), link_temp.from
                            , link_temp.to,
                            link_temp.image_xy.X.ToString(),
                            link_temp.image_xy.Y.ToString(),
                            link_temp.image_width.ToString(),
                            link_temp.image_height.ToString());
                }
                //*******************
                work_message =
                            "저장되었습니다."
                        + "\nStarting Index : " + link_data.ElementAt(link_data.Count - 1).from
                        + "\nDestination Index : " + link_data.ElementAt(link_data.Count - 1).to
                        + "\nXY coordination : " + link_data.ElementAt(link_data.Count - 1).image_xy.X + ", " + link_data.ElementAt(link_data.Count - 1).image_xy.Y
                        + "\nRectangle Width : " + link_data.ElementAt(link_data.Count - 1).image_width
                        + "\nRectangle Height : " + link_data.ElementAt(link_data.Count - 1).image_height
                        + "\n------------------------\n";
                str_work.Add(work_message);

                //MessageBox.Show(link_data.ElementAt(link_data.Count - 1).to);

                //이후에 버튼을 눌러서 수정하는 작업 여기서 구현

                this.label1.Text = "";

                //우측에 출력되는 메시지 관련
                for (int j = str_work.Count; j > 0; j--)
                {
                    this.label1.Text += str_work.ElementAt(j - 1);
                }
                panel_image_link.Visible = false;
            }
            this.Invalidate();
        }

        private void createNode(string pFileName, string pTag, string pSrcIdx, string pDstIdx,
            string pLinkX, string pLinkY, string pLinkWidth, string pLinkHeight, XmlWriter writer)
        {
            writer.WriteStartElement("LinkInfo");
            writer.WriteStartElement("FileName");
            writer.WriteString(pFileName);
            writer.WriteEndElement();
            writer.WriteStartElement("Tag");
            writer.WriteString(pTag);
            writer.WriteEndElement();
            writer.WriteStartElement("SrcIdx");
            writer.WriteString(pSrcIdx);
            writer.WriteEndElement();
            writer.WriteStartElement("DstIdx");
            writer.WriteString(pDstIdx);
            writer.WriteEndElement();
            writer.WriteStartElement("LinkX");
            writer.WriteString(pLinkX);
            writer.WriteEndElement();
            writer.WriteStartElement("LinkY");
            writer.WriteString(pLinkY);
            writer.WriteEndElement();
            writer.WriteStartElement("LinkWidth");
            writer.WriteString(pLinkWidth);
            writer.WriteEndElement();
            writer.WriteStartElement("LinkHeight");
            writer.WriteString(pLinkHeight);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        private void appendNode(string pFileName, string pTag, string pSrcIdx, string pDstIdx,
           string pLinkX, string pLinkY, string pLinkWidth, string pLinkHeight)
        {
            XDocument xDocument = XDocument.Load(mPath + "link.xml");
            XElement root = xDocument.Element("LinkTable");
            IEnumerable<XElement> rows = root.Descendants("LinkInfo");
            XElement firstRow = rows.First();
            firstRow.AddBeforeSelf(
               new XElement("LinkInfo",
               new XElement("FileName", pFileName),
               new XElement("Tag", pTag),
               new XElement("SrcIdx", pSrcIdx),
               new XElement("DstIdx", pDstIdx),
               new XElement("LinkX", pLinkX),
               new XElement("LinkY", pLinkY),
               new XElement("LinkWidth", pLinkWidth),
               new XElement("LinkHeight", pLinkHeight)
               ));
            xDocument.Save(mPath + "link.xml");
            MessageBox.Show("XML File appended ! ");
        }

        //이미지 내부에 구현되어 있는 버튼에 대한.
        public void btt_in_picture(object sender, MouseEventArgs e)
        {
            Control ctl = sender as Control;
            Button btn = sender as Button;

            if (e.Button == MouseButtons.Right)
            {
                TempDeleteButton = btn;
                TempDeleteButton.TabIndex = btn.TabIndex;
            }
            else
            {
                panel_image_link.Visible = false;
                if (ctl != null)
                {
                    for (int i = 0; i < link_data.Count; i++)
                    {
                        if (getIndex(ctl.TabIndex) == link_data.ElementAt(i).btn_id)
                        {
                            work_message =
                                        "Starting Index : " + link_data.ElementAt(i).from
                                    + "\nDestination Index : " + link_data.ElementAt(i).to
                                    + "\nXY coordination : " + link_data.ElementAt(i).image_xy.X + ", " + link_data.ElementAt(i).image_xy.Y
                                    + "\nRectangle Width : " + link_data.ElementAt(i).image_width
                                    + "\nRectangle Height : " + link_data.ElementAt(i).image_height
                                    + "\n------------------------\n";
                            str_work.Add(work_message);
                            //이후에 버튼을 눌러서 수정하는 작업 여기서 구현
                        }
                    }
                    this.label1.Text = string.Empty;

                    for (int j = str_work.Count; j > 0; j--)
                    {
                        this.label1.Text += str_work.ElementAt(j - 1);
                    }
                }
            }
            
        }

        private void menuItem_delete_click(object sender, EventArgs e)
        {
            //get control hovered with mouse
            Button buttonToRemove = (this.GetChildAtPoint(this.PointToClient(Cursor.Position)) as Button);
            //MessageBox.Show(TempDeleteButton.Text, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //if it's a Button, remove it from the form
            //MessageBox.Show(getIndex(TempDeleteButton.TabIndex).ToString());
            delete_link(image_name, getIndex(TempDeleteButton.TabIndex).ToString());
            pictureBox1.Controls.Remove(TempDeleteButton);

            //$$$$$$
            this.Invalidate();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            this.myPress = true; //마우스가 눌러짐
            this.link_alloc.Visible = false;
            this.panel_image_link.Visible = false;
            this.myPointStart.X = e.X; //마우스가 눌러진 X 좌표
            this.myPointStart.Y = e.Y; //마우스가 눌러진 Y 좌표
            this.g = Graphics.FromHwnd(pictureBox1.Handle);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (myShape == 3)      //사각형
            {
                if (this.myPress)
                {
                    g.DrawImage(mainImage, 0, 0, this.panel1.Width, this.panel1.Height);

                    this.sizeX = Math.Abs(e.X - this.myPointStart.X);
                    this.sizeY = Math.Abs(e.Y - this.myPointStart.Y);
                    if (this.sizeX == 0){ this.sizeX = 1;}
                    if (this.sizeY == 0){ this.sizeY = 1;}

                    if (e.X < this.myPointStart.X && e.Y < this.myPointStart.Y)         //우측하단 시작
                    {
                        this.rect = new Rectangle(e.X, e.Y, this.sizeX, this.sizeY);
                    }
                    else if (e.X > this.myPointStart.X && e.Y < this.myPointStart.Y)    //좌측하단 시작
                    {
                        this.rect = new Rectangle(this.myPointStart.X, e.Y, this.sizeX, this.sizeY);
                    }
                    else if (e.X < this.myPointStart.X && e.Y > this.myPointStart.Y)    //우측상단 시작
                    {
                        this.rect = new Rectangle(e.X, this.myPointStart.Y, this.sizeX, this.sizeY);
                    }
                    else                                                                //좌측상단 시작
                    {
                        this.rect = new Rectangle(this.myPointStart.X, this.myPointStart.Y, this.sizeX, this.sizeY);
                    }
                    g.DrawRectangle(this.myPen, this.rect);
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.myShape == 3)
            {
                this.sizeX = Math.Abs(e.X - this.myPointStart.X);
                this.sizeY = Math.Abs(e.Y - this.myPointStart.Y);
                if (this.sizeX == 0)
                {
                    this.sizeX = 1;
                }
                if (this.sizeY == 0)
                {
                    this.sizeY = 1;
                }
                this.rect = new Rectangle(this.myPointStart.X, this.myPointStart.Y, this.sizeX, this.sizeY);

                link_temp.image_width = this.sizeX;
                link_temp.image_height = this.sizeY;

                //그냥 클릭한 것에 대해서는 반응하지 않도록
                if (Math.Abs(this.myPointStart.X - e.X) > 5 && Math.Abs(this.myPointStart.Y - e.Y) > 5)
                {
                    this.link_alloc.Visible = true;
                    this.link_alloc.BringToFront();
                    //사각형의 어느 꼭짓점에서 시작하는지에 따라 그려지는 과정이 다름
                    if (e.X < this.myPointStart.X && e.Y < this.myPointStart.Y)         //우측하단 시작
                    {
                        this.link_alloc.Location = new Point(this.rect.X + (this.Width / 2) - (panel1.Width / 2), this.rect.Y);
                        link_temp.image_xy = new Point(e.X, e.Y);
                    }
                    else if (e.X > this.myPointStart.X && e.Y < this.myPointStart.Y)    //좌측하단 시작
                    {
                        this.link_alloc.Location = new Point(this.rect.X + this.sizeX + (this.Width / 2) - (panel1.Width / 2), this.rect.Y);
                        link_temp.image_xy = new Point(this.rect.X, e.Y);
                    }
                    else if (e.X < this.myPointStart.X && e.Y > this.myPointStart.Y)    //우측상단 시작
                    {
                        this.link_alloc.Location = new Point(this.rect.X + (this.Width / 2) - (panel1.Width / 2), this.rect.Y + this.sizeY);
                        link_temp.image_xy = new Point(e.X, this.rect.Y);
                    }
                    else                                                                //좌측상단 시작
                    {
                        this.link_alloc.Location = new Point(this.rect.X + this.sizeX + (this.Width / 2) - (panel1.Width / 2), this.rect.Y + this.sizeY);
                        link_temp.image_xy = new Point(this.rect.X, this.rect.Y);
                    }
                }
                else
                {
                    this.Invalidate();
                }
            }
            this.myPress = false;
        }

        //링크연동버튼
        private void button5_Click(object sender, EventArgs e)
        {
            this.panel_image_link.Visible = true;
            this.panel_image_link.BringToFront();
            this.link_alloc.Visible = false;
        }

        
        private void Form1_Resize(object sender, EventArgs e)
        {
            //세로가 더 긴 경우
            if (img != null && (img.Height > img.Width))
            {
                //원본 이미지 파일의 가로 세로 비율을 통해 새로운 resolution에서 같은 비율 적용
                panel1.Height = (int)(this.Height * 0.9);                           //화면 아래로 잘려나가는 것을 방지하기 위함
                double WidthOverHeight = (double)img.Width / (double)img.Height;    //이미지의 가로세로 비율
                panel1.Width = (int)(WidthOverHeight * (this.Height * 0.9));
                //MessageBox.Show(img.Width.ToString() + "/" + img.Height.ToString() + "*" + this.Height.ToString() + "=" + panel1.Width.ToString() + ", " + WidthOverHeight.ToString());
                panel1.Location = new Point(((this.Width / 2) - (panel1.Width / 2)), 0);

                //resize시 원본대비 변경된 사이즈의 비율
                resize_ratio =  (float)panel1.Width / (float)img.Width;

                for (int i = 0; i < link_data.Count; i++)
                {
                    link_data.ElementAt(i).bttn.Location = new Point((int)(link_data.ElementAt(i).image_xy.X * resize_ratio), (int)(link_data.ElementAt(i).image_xy.Y * resize_ratio));
                    link_data.ElementAt(i).bttn.Width = (int)(link_data.ElementAt(i).image_width * resize_ratio);
                    link_data.ElementAt(i).bttn.Height = (int)(link_data.ElementAt(i).image_height * resize_ratio);
                }
            }

            link_alloc.Visible = false;
            panel_image_link.Visible = false;
        }

        //read
        private void read_link()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(mPath + "link.xml");
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/LinkTable/LinkInfo");
            string pFileName = "", pTag = "", pSrcIdx = "", pDstIdx = "";
            string pLinkX = "", pLinkY = "", pLinkWidth = "", pLinkHeight = "";
            int i = 0;
            foreach (XmlNode node in nodeList)
            {
                pFileName = node.SelectSingleNode("FileName").InnerText;
                pTag = node.SelectSingleNode("Tag").InnerText;
                pSrcIdx = node.SelectSingleNode("SrcIdx").InnerText;
                pDstIdx = node.SelectSingleNode("DstIdx").InnerText;
                pLinkX = node.SelectSingleNode("LinkX").InnerText;
                pLinkY = node.SelectSingleNode("LinkY").InnerText;
                pLinkWidth = node.SelectSingleNode("LinkWidth").InnerText;
                pLinkHeight = node.SelectSingleNode("LinkHeight").InnerText;
                //MessageBox.Show("FileName : " + pFileName + "\n"
                //    + "Tag : " + pTag + "\n"
                //    + "SrcIdx : " + pSrcIdx + "\n"
                //    + "DstIdx : " + pDstIdx + "\n"
                //    + "LinkX : " + pLinkX + "\n"
                //    + "LinkY : " + pLinkY + "\n"
                //    + "LInkWidth : " + pLinkWidth + "\n"
                //    + "LinkHeight : " + pLinkHeight + "\n");
                link_temp.from = pSrcIdx;
                link_temp.to = pDstIdx;
                link_temp.image_xy = new Point(Convert.ToInt32(pLinkX), Convert.ToInt32(pLinkY));
                link_temp.image_width = Convert.ToInt32(pLinkWidth);
                link_temp.image_height = Convert.ToInt32(pLinkHeight);
                link_temp.btn_id = Convert.ToInt32(pTag);
                if(btn_id_to_add < link_temp.btn_id)
                {
                    btn_id_to_add = link_temp.btn_id;
                }

                if (link_temp.from.CompareTo(image_name) == 0)
                {
                    //이미지의 오리지널 데이터
                    //MessageBox.Show("temp X : " + link_temp.image_xy.X.ToString() + "\ntemp Y : " + link_temp.image_xy.Y.ToString()
                    //                + "\ntemp width : " + link_temp.image_width.ToString() + "\ntemp height : " + link_temp.image_height.ToString());
                    link_temp.bttn = new Button();
                    link_data.Add(link_temp);
                    this.pictureBox1.Controls.Add(link_data.ElementAt(i).bttn);
                    this.link_data.ElementAt(i).bttn.Parent = this.pictureBox1;
                    link_data.ElementAt(i).bttn.MouseUp += new MouseEventHandler(btt_in_picture);
                    link_data.ElementAt(i).bttn.Visible = true;
                    link_data.ElementAt(i).bttn.FlatStyle = FlatStyle.Flat;
                    link_data.ElementAt(i).bttn.BackColor = Color.Transparent;
                    link_data.ElementAt(i).bttn.FlatAppearance.BorderColor = Color.Lime;
                    link_data.ElementAt(i).bttn.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    link_data.ElementAt(i).bttn.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    link_data.ElementAt(i).bttn.Location = new Point((int)(link_temp.image_xy.X * resize_ratio), (int)(link_temp.image_xy.Y * resize_ratio));
                    link_data.ElementAt(i).bttn.Width = (int)(link_temp.image_width * resize_ratio);
                    link_data.ElementAt(i).bttn.Height = (int)(link_temp.image_height * resize_ratio);
                    link_data.ElementAt(i).bttn.BringToFront();
                    //비율이 조정된 데이터
                    //MessageBox.Show("btn X : " + link_data.ElementAt(i).bttn.Location.X.ToString()
                    //                + "\nbtn Y : " + link_data.ElementAt(i).bttn.Location.Y.ToString()
                    //                + "\nbtn width : " + link_data.ElementAt(i).bttn.Width
                    //                + "\nbtn height : " + link_data.ElementAt(i).bttn.Height);

                    //contextMenu 설정
                    link_data.ElementAt(i).bttn.ContextMenu = cm;

                    //원본 width * 변경 x 좌표 / 변경 width = 원본 x 좌표 
                    //Console.WriteLine("원본 x 좌표 : " + (((double)img.Width * (double)link_data.ElementAt(i).image_xy.X) / (double)panel1.Width).ToString());
                    //MessageBox.Show(link_data.ElementAt(i).bttn.Location.X.ToString() + ", " + link_data.ElementAt(i).image_xy.X.ToString());
                    i++;
                }
            }
            btn_id_to_add += 1;   //다음 버튼 생성 시 부여될 id
        }

        //delete
        private void delete_link(string filename, string tag)
        {
            string pfileName = filename;
            string pTag = tag;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(mPath + "link.xml");
            XmlElement el = (XmlElement)xmlDoc.SelectSingleNode("/LinkTable/LinkInfo[FileName='" + pfileName + "' and Tag=" + pTag + "]");
            if (el == null)
                MessageBox.Show("el null");
            if (el != null)
            {
                //MessageBox.Show("el info : " + el.InnerText);
                el.ParentNode.RemoveChild(el);
            }
            xmlDoc.Save(mPath + "link.xml");
        }

        private int getIndex(int temp)
        {
            return link_data.ElementAt(temp).btn_id;
        }
        
    }
}
