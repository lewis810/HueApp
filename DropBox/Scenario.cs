using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml;
using System.IO.Compression;


namespace DropBox
{
    public partial class Scenario : Form
    {
        int show_type = 0;    // 0 : new, 1 : edit, 2 : show

        const int NEW = 0;
        const int EDIT = 1;
        const int SHOW = 2;

        //EditProject editProject = new EditProject();

        struct image_info
        {
            public string str_image;
            public string str_name;
        };

        List<image_info> imageInfo = new List<image_info>();
        ScenarioData sData;
        Control[] btn_link;
        string mPath;
        int edit_index;
        int select_image;       // 0 = start image, 1 = end image
        ListBox listBox;

        public Scenario()
        {
            InitializeComponent();
        }

        public Scenario(int _show_type, string _path, ScenarioData _Data, int _edit_index, ListBox _listBox)
        {
            InitializeComponent();
            sData = _Data;

            show_type = _show_type;
            mPath = _path;
            edit_index = _edit_index;
            listBox = _listBox;

            Setup();

            //EDIT과 SHOW의 경우 기존 정보 불러오기
            if (show_type == EDIT || show_type == SHOW)
            {
                GetInfo();
            }
        }

        private void GetInfo()
        {
            //edit box에 데이터 미리 불러오기
            textBoxTitle.Text = sData.getSData()[edit_index].title;
            textBoxPurpose.Text = sData.getSData()[edit_index].purpose;
            textBoxTime.Text = sData.getSData()[edit_index].time;
            StartImage_btn.Text = sData.getSData()[edit_index].start_img;
            EndImage_btn.Text = sData.getSData()[edit_index].end_img;
            if(show_type == SHOW)
            {
                textBoxTitle.ReadOnly = true;
                textBoxPurpose.ReadOnly = true;
                textBoxTime.ReadOnly = true;
                StartImage_btn.Enabled = false;
                EndImage_btn.Enabled = false;
            }

        }

        private void Setup()
        {
            //panel에 버튼 만들기

            //디렉토리 주소를 받아와서 저장
            string path_folder;
            path_folder = mPath; //지용이가 넘겨준 주소값

            //int k = 0;
            //반복문을 통해 디렉토리 내부의 이미지 파일 경로 얻어오기
            foreach (var path in Directory.GetFiles(path_folder))
            {
                //이미지 파일만 불러오도록 필터링
                if (Regex.IsMatch(path.ToString(), "jpg", RegexOptions.IgnoreCase) ||
                    Regex.IsMatch(path.ToString(), "png", RegexOptions.IgnoreCase))
                {
                    imageInfo.Add(new image_info() { str_image = path.ToString(), str_name = Path.GetFileName(path) });
                    //if (Path.GetFileName(path).CompareTo(image_name) == 0)
                    //{
                    //    image_tag = k; //여기서 태그정보가 무조건 나와야함.
                    //}
                    //k++;
                }
            }

            btn_link = new Control[imageInfo.Count];

            //프로젝트 내에 있는 파일이름 가져오기
            DirectoryInfo dinfo = new DirectoryInfo(mPath);

            string[] extensions = new[] { ".jpg", ".tiff", ".bmp", ".png" };

            FileInfo[] files =
                dinfo.EnumerateFiles()
                     .Where(f => extensions.Contains(f.Extension.ToLower()))
                     .ToArray();

            for (int i = 0; i < files.Length; i++)
            {
                //링크연결을 위한 이미지
                btn_link[i] = new Button();
                btn_link[i].Parent = this;
                btn_link[i].Location = new Point(10 + i * 110, 10);
                btn_link[i].Size = new Size(100, 120);
                panel2.Controls.Add(btn_link[i]);                         //패널에 버튼을 추가
                btn_link[i].Tag = i.ToString();

                btn_link[i].Click += new EventHandler(SelectImage);
                btn_link[i].Name = files[i].Name;
                btn_link[i].Text = i.ToString();
                btn_link[i].ForeColor = Color.Lime;
                btn_link[i].Font = new Font(btn_link[i].Font.Name, 10, FontStyle.Bold);
                btn_link[i].BackgroundImage = Image.FromFile(imageInfo[i].str_image);
                btn_link[i].BackgroundImageLayout = ImageLayout.Stretch;

            }
        }

        private void SelectImage(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            Button btn = sender as Button;

            panel2.Visible = false;

            if(ctl != null)
            {
                if(select_image == 0)
                {
                    StartImage_btn.Text = btn.Name;
                }
                else
                {
                    EndImage_btn.Text = btn.Name;
                }
            }

        }

        private void StartImage_btn_Click(object sender, EventArgs e)
        {
            //모든 이미지 다 보여주고 선택하기
            panel2.Visible = true;
            select_image = 0;
        }

        private void EndImage_btn_Click(object sender, EventArgs e)
        {
            //모든 이미지 다 보여주고 선택하기
            panel2.Visible = true;
            select_image = 1;
        }

        private void CONFIRM_btn_Click(object sender, EventArgs e)
        {
            //저장한 거 어레이에 두었다가 한 번에 저장하면서 xml파일 만들기
            
            //pTemp_data.id = 어레이의 카운트 값을 입력
            string pTitle = textBoxTitle.Text;
            string pPurpose = textBoxPurpose.Text;
            string pTime = textBoxTime.Text;
            string pStart_img = StartImage_btn.Text;
            string pEnd_img = EndImage_btn.Text;

            if (show_type == NEW)
            {
                sData.AddScenario((sData.getSLastIndex() + 1), pTitle, pPurpose, pTime, pStart_img, pEnd_img);
                //MessageBox.Show("Added");
                listBox.Items.Add(pTitle);
                //editProject.RefreshListbox(true, pTitle, -1);
                this.Close();
            }
            else if(show_type == EDIT)
            {
                sData.getSData()[edit_index] = new ScenarioData.SCENARIO_DATA() { tag = sData.getSData()[edit_index].tag, title = pTitle, purpose = pPurpose, time = pTime, start_img = pStart_img, end_img = pEnd_img};
                //MessageBox.Show("Edited");
                listBox.Items.RemoveAt(edit_index);
                listBox.Items.Insert(edit_index, pTitle);
                //editProject.RefreshListbox(true, pTitle, edit_index);
                this.Close();
            }
        }

    }
}
