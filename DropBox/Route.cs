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

namespace DropBox
{
    public partial class Route : Form
    {
        List<EditProject.RouteData> pRoute_data;
        ScenarioData sData = new ScenarioData();
        string project_name;

        public Route(List<EditProject.RouteData> _pRoute_data, string _project_name, ScenarioData _sData)
        {
            InitializeComponent();
            pRoute_data = _pRoute_data;
            project_name = _project_name;
            sData = _sData;

            for (int i = 0; i < sData.getSData().Count; i++)
            {
                cb_analysis_route_selectScenario.Items.Add(sData.getSData()[i].title);
            }

            if (sData.getSData().Count == 0)
            {
                cb_analysis_route_selectScenario.Items.Add("데이터없음");
            }
            cb_analysis_route_selectScenario.SelectedIndex = 0;

            for(int j = 0; j < pRoute_data.Count; j++)
            {
                cb_analysis_route_selectTest.Items.Add(pRoute_data[j].tag);
            }

            if (pRoute_data.Count == 0)
            {
                cb_analysis_route_selectTest.Items.Add("데이터없음");
            }
            cb_analysis_route_selectTest.SelectedIndex = 0;
        }

        public void DrawRoute(int index)
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


            for (int j = 0; j < pRoute_data[index].images.Count; j++)
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
                temp_pic.BackgroundImage = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + pRoute_data[index].images[j]);
            }
            catch (FileNotFoundException fe)
            {
                temp_pic.BackColor = Color.Gray;
                Label notFound = new Label();
                notFound.Text = pRoute_data[index].images[j] + "\nnot found";
                temp_pic.Controls.Add(notFound);
                notFound.Anchor = AnchorStyles.None | AnchorStyles.Left;
            }
            temp_pic.Parent = temp_fp2;
            temp_fp2.Controls.Add(temp_pic);
            temp_pic.Size = new Size((int)(temp_height * 0.5625), temp_height);     //나중에 해상도 받아와서 설정 다시
            temp_pic.BackgroundImageLayout = ImageLayout.Stretch;
            temp_pic.Text = pRoute_data[index].images[j];

            Label temp_label = new Label();
            temp_label.Text = pRoute_data[index].images[j];
            temp_fp2.Controls.Add(temp_label);
            temp_label.Size = temp_label.PreferredSize;
            temp_label.Width = temp_pic.Width;
            temp_label.TextAlign = ContentAlignment.MiddleCenter;

            Label temp_time = new Label();
            temp_time.Text = pRoute_data[index].visit_time[j].ToString();
            temp_fp2.Controls.Add(temp_time);
            temp_time.Size = temp_time.PreferredSize;
            temp_time.Width = temp_pic.Width;
            temp_time.TextAlign = ContentAlignment.MiddleCenter;

            temp_pic.BackColor = Color.Transparent;
            temp_label.BackColor = Color.Transparent;
            temp_time.BackColor = Color.Transparent;

            temp_fp2.Size = temp_fp2.PreferredSize;

            if (j != pRoute_data[index].images.Count - 1)
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

        public Color SetColor(int A, Color color)
        {
            return Color.FromArgb(A, color.R, color.G, color.B);
        }

        private void btn_analysis_show_route_Click(object sender, EventArgs e)
        {
            bool exist = false;
            int index = 0;
            //여기서 인덱스 찾아서 넘겨야 할 듯
            for (int i = 0; i < pRoute_data.Count; i++)
            {
                //비교 : tag, div, scenario_name              //태그가 모두 다르다는게 확정이면 div는 비교할 필요 없음
                if ((pRoute_data[i].tag.CompareTo(cb_analysis_route_selectTest.SelectedItem) == 0)
                    && (pRoute_data[i].scenario_name.CompareTo(cb_analysis_route_selectScenario.SelectedItem) == 0))
                {
                    index = i;
                    exist = true;
                    break;
                }
            }

            if (exist == true)
            {
                DrawRoute(index);
            }
        }

        //private void cb_analysis_route_selectGroup_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cb_analysis_route_selectTest.Items.Clear();
        //    //피어목록보여주기
        //    if (cb_analysis_route_selectGroup.SelectedIndex == 0)
        //    {
        //        for (int i = 0; i < pRoute_data.Count; i++)
        //        {
        //            if (pRoute_data[i].div.CompareTo("p") == 0)
        //            {
        //                cb_analysis_route_selectTest.Items.Add(pRoute_data[i].tag);
        //            }
        //        }
        //    }
        //    //유저
        //    else
        //    {
        //        for (int i = 0; i < pRoute_data.Count; i++)
        //        {
        //            if (pRoute_data[i].div.CompareTo("u") == 0)
        //            {
        //                cb_analysis_route_selectTest.Items.Add(pRoute_data[i].tag);
        //            }
        //        }
        //    }

        //    //데이터가 존재하지 않을 때. 
        //    if (cb_analysis_route_selectTest.Items.Count == 0)
        //    {
        //        cb_analysis_route_selectTest.Items.Add("데이터없음");
        //    }

        //    cb_analysis_route_selectTest.Sorted = true;
        //    cb_analysis_route_selectTest.SelectedIndex = 0;
        //}
    }
}
