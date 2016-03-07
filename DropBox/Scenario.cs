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
using System.Drawing.Drawing2D;

namespace DropBox
{
    public partial class Scenario : Form
    {
        int show_type = 0;    // 0 : new, 1 : edit, 2 : show

        const int NEW = 0;
        const int EDIT = 1;
        const int SHOW = 2;

        //EditProject editProject = new EditProject();

        ScenarioData sData;
        Control[] btn_link;
        string mPath;
        int edit_index;
        int select_image;       // 0 = start image, 1 = end image
        ListBox listBox;
        int temp_height;
        List<ScenarioData.PATH_DATA> sPath;
        FlowLayoutPanel fpanel_path, fpanel_path2;

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
            temp_height = panel1.Height;
            sPath = new List<ScenarioData.PATH_DATA>();
            Setup();

            //EDIT과 SHOW의 경우 기존 정보 불러오기
            if (show_type == EDIT || show_type == SHOW)
            {
                GetInfo();
            }
        }

        private void GetInfo()
        {
            string temp = string.Empty;

            //sPath = sData.getSData()[edit_index].paths;
            for(int i = 0; i < sData.getSData()[edit_index].paths.Count; i++)
            {
                sPath.Add(new ScenarioData.PATH_DATA() { tag = sData.getSData()[edit_index].paths[i].tag, path = sData.getSData()[edit_index].paths[i].path, time = sData.getSData()[edit_index].paths[i].time });
                //temp = temp + " - " + sData.getSData()[edit_index].paths[i].path;
            }

            //edit box에 데이터 미리 불러오기
            textBoxTitle.Text = sData.getSData()[edit_index].title;
            textBoxPurpose.Text = sData.getSData()[edit_index].purpose;
            //textBoxTime.Text = sData.getSData()[edit_index].time;
            //Path_btn.Text = temp;
            if(show_type == SHOW)
            {
                textBoxTitle.ReadOnly = true;
                textBoxPurpose.ReadOnly = true;
                textBoxTime.ReadOnly = true;
                Path_btn.Enabled = false;

                Label temp_label = new Label();
                temp_label.Text = "경로";
                temp_label.Location = new Point(0, CONFIRM_btn.Location.Y + CONFIRM_btn.Height);
                panel1.Controls.Add(temp_label);

                fpanel_path = new FlowLayoutPanel();
                fpanel_path.FlowDirection = FlowDirection.LeftToRight;
                fpanel_path.Size = new Size(this.Width - 25, (int)(temp_height * 1.2));
                fpanel_path.Location = new Point(0, temp_label.Location.Y + temp_label.Height);
                fpanel_path.BackColor = Color.Red;
                this.Height = this.Height + (int)(temp_height);
                fpanel_path.WrapContents = false;
                fpanel_path.AutoScroll = true;
                panel1.Controls.Add(fpanel_path);

                
                Button path_pic;
                
                for (int i = 0; i < sPath.Count; i++)
                {
                    path_pic = new Button();
                    path_pic.Size = new Size((int)(temp_height * 0.5625), temp_height);
                    path_pic.BackgroundImage = Image.FromFile(mPath + sPath[i].path);
                    path_pic.BackgroundImageLayout = ImageLayout.Stretch;
                    path_pic.Click += new EventHandler(this.picture_click);
                    path_pic.FlatStyle = FlatStyle.Flat;
                    path_pic.FlatAppearance.BorderSize = 0;

                    fpanel_path.Controls.Add(path_pic);
                }

            }
        }

        private void picture_click(object sender, EventArgs e)
        {
            Button path_btn = sender as Button;

            //MessageBox.Show(path_btn.TabIndex.ToString());

            //if(e.Button == MouseButtons.Left)
            //{
                //MessageBox.Show("머라도 떠라");
            Button button1 = new Button();
            Control ctl = GetControlByName(path_btn);
            MessageBox.Show(path_btn.Name);

            //Label label1 = this.Controls.Get

            string var = "";

            if (PopupInput(button1, 4, 75, ref var) == System.Windows.Forms.DialogResult.OK)
            {
                //MessageBox.Show(var);
                //MessageBox.Show(ctl.Name);
                ((Label)ctl).Text = var;

                for(int i = 0; i < sPath.Count; i++)
                {
                    if(sPath[i].tag == path_btn.TabIndex)
                    {
                        sPath.RemoveAt(i);
                        sPath.Insert(i, new ScenarioData.PATH_DATA() { tag = path_btn.TabIndex, path = path_btn.Name, time = var});
                    }
                }
            }
        }

        Control GetControlByName(Button clicked_btn)
        {
            foreach (Control c in this.fpanel_path.Controls)
            {
                if (c.Name == clicked_btn.Name)
                {
                    foreach (Control ct in c.Controls)
                    {
                        if(ct is Label && ct.TabIndex == clicked_btn.TabIndex)
                        {
                            return ct;
                        }
                    }
                }
            }

            return null;
        }

        public DialogResult PopupInput(Control ctrl, int border, int length, ref string output)
        {


            //handle alignment
            Point ctrlpt = Control.MousePosition;
            //Point ctrlpt = this.PointToScreen(ctrl.Location);
            //ctrlpt.Y += 24;
            //ctrlpt.X += 4;

            TextBox input = new TextBox { Height = 20, Width = length, Top = border / 2, Left = border / 2 };
            input.BorderStyle = BorderStyle.FixedSingle;
            //######## SetColor to your preference
            input.BackColor = Color.Azure;

            Button btnok = new Button { DialogResult = System.Windows.Forms.DialogResult.OK, Top = 25 };
            Button btncn = new Button { DialogResult = System.Windows.Forms.DialogResult.Cancel, Top = 25 };

            btnok.Text = "O";
            btncn.Text = "X";
            btnok.BackColor = Color.Red;
            btncn.BackColor = Color.Red;
            btnok.Location = new Point(btncn.Location.X + btncn.Width, btncn.Location.Y);

            Form frm = new Form { ControlBox = false, AcceptButton = btnok, CancelButton = btncn, StartPosition = FormStartPosition.Manual, Location = ctrlpt };
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //######## SetColor to your preference
            frm.BackColor = Color.Navy;

            RectangleF rec = new RectangleF(0, 0, (length + border) * 2, (20 + border) * 2);
            GraphicsPath GP = new GraphicsPath(); //GetRoundedRect(rec, 4.0F);
            float diameter = 8.0F;
            SizeF sizef = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(rec.Location, sizef);
            GP.AddArc(arc, 180, 90);
            arc.X = rec.Right - diameter;
            GP.AddArc(arc, 270, 90);
            arc.Y = rec.Bottom - diameter;
            GP.AddArc(arc, 0, 90);
            arc.X = rec.Left;
            GP.AddArc(arc, 90, 90);
            GP.CloseFigure();

            frm.Region = new Region(GP);
            frm.Controls.AddRange(new Control[] { input, btncn, btnok });
            DialogResult rst = frm.ShowDialog();
            output = input.Text;
            return rst;
        }

        private void Setup()
        {
            //panel에 버튼 만들기

            //프로젝트 내에 있는 파일이름 가져오기
            DirectoryInfo dinfo = new DirectoryInfo(mPath);

            string[] extensions = new[] { ".jpg", ".tiff", ".bmp", ".png" };

            FileInfo[] files =
                dinfo.EnumerateFiles()
                     .Where(f => extensions.Contains(f.Extension.ToLower()))
                     .ToArray();

            btn_link = new Control[files.Length];

            //fpanel_scenario_image.Size = new Size(this.Width, temp_height);

            for (int i = 0; i < files.Length; i++)
            {
                //링크연결을 위한 이미지
                btn_link[i] = new Button();
                btn_link[i].Size = new Size((int)(temp_height * 0.5625), temp_height);
                btn_link[i].Tag = i.ToString();

                btn_link[i].Click += new EventHandler(SelectImage);
                btn_link[i].Name = files[i].Name;
                btn_link[i].Text = i.ToString();
                btn_link[i].ForeColor = Color.Lime;
                btn_link[i].Font = new Font(btn_link[i].Font.Name, 10, FontStyle.Bold);
                btn_link[i].BackgroundImage = Image.FromFile(mPath + files[i].Name);
                btn_link[i].BackgroundImageLayout = ImageLayout.Stretch;

                fpanel_scenario_image.Controls.Add(btn_link[i]);                         //패널에 버튼을 추가
            }
        }

        private void SelectImage(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            Button btn = sender as Button;

            //fpanel_scenario_image.Visible = false;
            //this.Height = panel1.Height;
            if (ctl != null)
            {
                fpanel_path2 = new FlowLayoutPanel();
                fpanel_path2.FlowDirection = FlowDirection.TopDown;


                //여기서 에러나면 맨 처음은 0으로 설정
                try
                {
                    sPath.Add(new ScenarioData.PATH_DATA() { tag = sPath[sPath.Count - 1].tag + 1, path = btn.Name, time = "-1" });
                }
                catch(ArgumentOutOfRangeException ae)
                {
                    sPath.Add(new ScenarioData.PATH_DATA() { tag = 1, path = btn.Name, time = "-1" });
                }

                Button path_pic = new Button();
                path_pic.Size = new Size((int)(temp_height * 0.5625), temp_height);
                path_pic.BackgroundImage = Image.FromFile(mPath + sPath[sPath.Count-1].path);
                path_pic.BackgroundImageLayout = ImageLayout.Stretch;
                path_pic.Click += new EventHandler(this.picture_click);
                path_pic.FlatStyle = FlatStyle.Flat;
                path_pic.FlatAppearance.BorderSize = 0;
                path_pic.Name = "path_pic" + sPath[sPath.Count - 1].tag.ToString();
                path_pic.TabIndex = sPath[sPath.Count - 1].tag;

                fpanel_path2.Controls.Add(path_pic);

                //label
                Label path_name, path_time;
                path_name = new Label();
                path_name.Text = sPath[sPath.Count - 1].path;
                fpanel_path2.Controls.Add(path_name);

                path_time = new Label();
                path_time.Text = sPath[sPath.Count - 1].time;
                path_time.TabIndex = sPath[sPath.Count - 1].tag;
                path_time.Name = "path_time" + sPath[sPath.Count - 1].tag.ToString();
                fpanel_path2.Controls.Add(path_time);

                fpanel_path.Width = this.Width - 25;

                fpanel_path2.WrapContents = false;
                fpanel_path2.Size = fpanel_path2.PreferredSize;
                fpanel_path2.Height = (int)(fpanel_path2.Height * 1.2);
                fpanel_path2.Name = "path_pic" + sPath[sPath.Count - 1].tag.ToString();
                fpanel_path2.TabIndex = sPath[sPath.Count - 1].tag;
                fpanel_path.Controls.Add(fpanel_path2);
            }
            //완료 버튼 누르면 종료하는 것으로.
        }

        private void Path_btn_Click(object sender, EventArgs e)
        {
            panel1.Controls.Remove(fpanel_path);
            fpanel_scenario_image.Height = 0;

            //모든 이미지 다 보여주고 선택하기
            this.Height = panel1.Height;
            fpanel_scenario_image.Visible = true;
            fpanel_scenario_image.Size = new Size(this.Width -25 ,(int)(temp_height * 1.2));
            this.Height = panel1.Height + (int)(temp_height * 1.8);
            fpanel_scenario_image.BackColor = Color.Aqua;

            Label temp_label = new Label();
            temp_label.Text = "경로";
            temp_label.Location = new Point(0, fpanel_scenario_image.Location.Y + fpanel_scenario_image.Height);
            panel1.Controls.Add(temp_label);

            fpanel_path = new FlowLayoutPanel();
            fpanel_path.FlowDirection = FlowDirection.LeftToRight;
            fpanel_path.Size = new Size(this.Width - 25, fpanel_scenario_image.Height);
            fpanel_path.Location = new Point(0, temp_label.Location.Y + temp_label.Height);
            fpanel_path.BackColor = Color.Red;
            this.Height = this.Height + (int)(temp_height);
            fpanel_path.WrapContents = false;
            fpanel_path.AutoScroll = true;
            panel1.Controls.Add(fpanel_path);

            Button path_pic;
            Label path_name, path_time;
            for(int i = 0; i < sPath.Count; i++)
            {
                fpanel_path2 = new FlowLayoutPanel();
                fpanel_path2.FlowDirection = FlowDirection.TopDown;

                path_pic = new Button();
                path_pic.Size = new Size((int)(temp_height * 0.5625), temp_height);
                path_pic.BackgroundImage = Image.FromFile(mPath + sPath[i].path);
                path_pic.BackgroundImageLayout = ImageLayout.Stretch;
                path_pic.TabIndex = sPath[i].tag;
                path_pic.Click += new EventHandler(this.picture_click);
                path_pic.FlatStyle = FlatStyle.Flat;
                path_pic.FlatAppearance.BorderSize = 0;
                path_pic.Name = "path_pic" + sPath[i].tag.ToString();
                fpanel_path2.Controls.Add(path_pic);

                path_name = new Label();
                path_name.Text = sPath[i].path;
                fpanel_path2.Controls.Add(path_name);

                path_time = new Label();
                path_time.Text = sPath[i].time;
                path_time.TabIndex = sPath[i].tag;
                path_time.Name = "path_time" + sPath[i].tag.ToString();
                fpanel_path2.Controls.Add(path_time);

                fpanel_path2.WrapContents = false;
                fpanel_path2.Size = fpanel_path2.PreferredSize;
                fpanel_path2.Height = (int)(fpanel_path2.Height * 1.2);
                fpanel_path2.Name = "path_pic" + sPath[i].tag.ToString();
                fpanel_path2.TabIndex = sPath[i].tag;

                fpanel_path.Controls.Add(fpanel_path2);
            }
        }

        private void CONFIRM_btn_Click(object sender, EventArgs e)
        {
            this.Height = panel1.Height;
            fpanel_scenario_image.Visible = false;

            //저장한 거 어레이에 두었다가 한 번에 저장하면서 xml파일 만들기

            //pTemp_data.id = 어레이의 카운트 값을 입력
            string pTitle = textBoxTitle.Text;
            string pPurpose = textBoxPurpose.Text;
            string pTime = textBoxTime.Text;
            string pStart_img = Path_btn.Text;

            //시간은 초단위로만 받기
            try
            {
                if (pTime.CompareTo("") == 0)
                {
                    pTime = "0";
                }
                int temp = Convert.ToInt32(pTime);

            } catch(FormatException fe)
            {
                MessageBox.Show("숫자만 입력해주세요");
            }

            if (pTitle.Contains("_"))
            {
                MessageBox.Show("제목에 '_' 기호를 포함시킬 수 없습니다.");
            }

            //시나리오 이름에 언더바 있으면 안 됨!!!!

            //팝업창 띄워서 내용 확인해서 오케이 누르면 아래 진행

            if (show_type == NEW)
            {
                for (int i = 0; i < sPath.Count; i++)
                {
                    Console.WriteLine(sPath[i].path);
                }
                sData.AddScenario((sData.getSLastIndex() + 1), pTitle, pPurpose, pTime, "1", sPath);
                MessageBox.Show("등록되었습니다.");
                listBox.Items.Add(pTitle);
                this.Close();
            }
            else if(show_type == EDIT)
            {
                sData.getSData()[edit_index] = new ScenarioData.SCENARIO_DATA() { tag = sData.getSData()[edit_index].tag, title = pTitle, purpose = pPurpose, time = pTime, level = "2", paths = sPath};
                MessageBox.Show("수정되었습니다.");
                listBox.Items.RemoveAt(edit_index);
                listBox.Items.Insert(edit_index, pTitle);
                this.Close();
            }
        }

    }
}
