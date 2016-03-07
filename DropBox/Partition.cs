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
    public partial class Partition : Form
    {
        PictureBox pic;
        Graphics g;
        Brush br;
        Bitmap bitmap;
        LinkData pData = new LinkData();
        List<EditProject.TotalData> pTotal_data;
        List<string> image_names;
        List<int> count;
        List<PictureBox> pictures;
        float each_width, each_height;
        int w_count, h_count;
        double ratio;

        AnalysisData aData = new AnalysisData();
        ScenarioData sData = new ScenarioData();

        string project_name;

        public Partition(List<EditProject.TotalData> _pTotal_data, string _project_name, ScenarioData _sData)
        {
            InitializeComponent();
            pTotal_data = _pTotal_data;
            sData = _sData;
            project_name = _project_name;
            count = new List<int>();
            pictures = new List<PictureBox>();

            string cPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name;
            IEnumerable<string> imagenames = Directory.GetFiles(cPath, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".jpg") || s.EndsWith(".jpeg") || s.EndsWith(".png"));

            image_names = imagenames.Cast<string>().ToList();

            for (int i = 0; i < sData.getSData().Count; i++)
            {
                cb_analysis_partition_selectScenario.Items.Add(sData.getSData()[i].title);
            }
            if (sData.getSData().Count == 0)
            {
                cb_analysis_partition_selectScenario.Items.Add("데이터없음");
            }
            cb_analysis_partition_selectScenario.SelectedIndex = 0;


            for (int i = 0; i < image_names.Count; i++)                       //이거 시나리오 콤보박스 인덱스 change에서 사용
            {
                cb_analysis_partition_selectImage.Items.Add(Path.GetFileName(image_names[i]));
            }
            cb_analysis_partition_selectImage.Sorted = true;

            if (image_names.Count == 0)
            {
                cb_analysis_partition_selectImage.Items.Add("데이터없음");
            }
            cb_analysis_partition_selectImage.SelectedIndex = 0;
            cb_analysis_partition_selectTest.SelectedIndex = 0;

            //이미지 가로 세로 분할 횟수 초기화
            tb_analysis_partition_hori.Text = "10";     //가로
            tb_analysis_partition_verti.Text = "20";    //세로
        }

        public void DrawPartition(int _w_count, int _h_count, string selected, string test)
        {
            panel_analysis_partition_picture.Height = (int)(this.Height * 0.85);

            //backgroundimage width height를 해상도 받아온 것으로 해야함 - 뒤에 숫자.
            panel_analysis_partition_picture.Width = (int)(panel_analysis_partition_picture.Height * ((double)720 / (double)1280));
            bitmap = new Bitmap(panel_analysis_partition_picture.Width, panel_analysis_partition_picture.Height);

            panel_analysis_partition_picture.Location = new Point(((int)(this.Width / 2) - (panel_analysis_partition_picture.Width / 2)), 0);

            //초기화
            panel_analysis_partition_picture2.Controls.Clear();
            count.Clear();
            pictures.Clear();

            Image image;
            image = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + cb_analysis_partition_selectImage.SelectedItem);
            panel_analysis_partition_picture2.BackgroundImage = image;

            //화면분할할 때 각 칸의 가로세로 길이
            each_width = (float)panel_analysis_partition_picture.Width / (float)_w_count;
            each_height = (float)panel_analysis_partition_picture.Height / (float)_h_count;

            ratio = (double)panel_analysis_partition_picture.Width / (double)720;

            w_count = Convert.ToInt16(tb_analysis_partition_hori.Text);
            h_count = Convert.ToInt16(tb_analysis_partition_verti.Text);

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
            for (int i = 0; i < pTotal_data.Count; i++)
            {
                if (pTotal_data[i].image_name.CompareTo(selected) == 0)
                {
                    index = i;
                    break;
                }
            }

            int k = 0;
            if (test.CompareTo("모두보기") == 0)
            {
                try
                {
                    for (int u = 0; u < pTotal_data[index].event_data.Count; u++)
                    {
                        //선택한 이미지에 대한 정보만 읽어오기
                        if (pTotal_data[index].event_data[u].img.CompareTo(selected) == 0)
                        {
                            for (int i = 0; i < pictures.Count; i++)
                            {
                                if ((ratio * pTotal_data[index].event_data[u].xcoord >= pictures[i].Location.X) && (ratio * pTotal_data[index].event_data[u].xcoord <= pictures[i].Location.X + each_width))
                                {
                                    if ((ratio * pTotal_data[index].event_data[u].ycoord >= pictures[i].Location.Y) && (ratio * pTotal_data[index].event_data[u].ycoord <= pictures[i].Location.Y + each_height))
                                    {
                                        k++;
                                        count[i]++;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (ArgumentOutOfRangeException ae)
                {

                }
                
            }

            
            //switch (_div)
            //{
            //    case "모두보기":
            //        //각 포인트마다. 전체 박스를 돌면서 해당 박스에 도착하면 카운트를 올린다.
            //        for (int u = 0; u < pTotal_data[index].event_data.Count; u++)
            //        {
            //            //선택한 이미지에 대한 정보만 읽어오기
            //            if (pTotal_data[index].event_data[u].img.CompareTo(selected) == 0)
            //            {
            //                for (int i = 0; i < pictures.Count; i++)
            //                {
            //                    if ((ratio * pTotal_data[index].event_data[u].xcoord >= pictures[i].Location.X) && (ratio * pTotal_data[index].event_data[u].xcoord <= pictures[i].Location.X + each_width))
            //                    {
            //                        if ((ratio * pTotal_data[index].event_data[u].ycoord >= pictures[i].Location.Y) && (ratio * pTotal_data[index].event_data[u].ycoord <= pictures[i].Location.Y + each_height))
            //                        {
            //                            k++;
            //                            count[i]++;
            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        break;
            //    case "유저만":
            //        for (int u = 0; u < pTotal_data[index].event_data.Count; u++)
            //        {
            //            //선택한 이미지에 대한 정보만 읽어오기
            //            if (pTotal_data[index].event_data[u].img.CompareTo(selected) == 0 && pTotal_data[index].event_data[u].div.CompareTo("u") == 0)
            //            {
            //                for (int i = 0; i < pictures.Count; i++)
            //                {
            //                    if ((ratio * pTotal_data[index].event_data[u].xcoord >= pictures[i].Location.X) && (ratio * pTotal_data[index].event_data[u].xcoord <= pictures[i].Location.X + each_width))
            //                    {
            //                        if ((ratio * pTotal_data[index].event_data[u].ycoord >= pictures[i].Location.Y) && (ratio * pTotal_data[index].event_data[u].ycoord <= pictures[i].Location.Y + each_height))
            //                        {
            //                            k++;
            //                            count[i]++;
            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        break;
            //    case "피어만":
            //        for (int u = 0; u < pTotal_data[index].event_data.Count; u++)
            //        {
            //            //선택한 이미지에 대한 정보만 읽어오기
            //            if (pTotal_data[index].event_data[u].img.CompareTo(selected) == 0 && pTotal_data[index].event_data[u].div.CompareTo("p") == 0)
            //            {
            //                for (int i = 0; i < pictures.Count; i++)
            //                {
            //                    if ((ratio * pTotal_data[index].event_data[u].xcoord >= pictures[i].Location.X) && (ratio * pTotal_data[index].event_data[u].xcoord <= pictures[i].Location.X + each_width))
            //                    {
            //                        if ((ratio * pTotal_data[index].event_data[u].ycoord >= pictures[i].Location.Y) && (ratio * pTotal_data[index].event_data[u].ycoord <= pictures[i].Location.Y + each_height))
            //                        {
            //                            k++;
            //                            count[i]++;
            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        break;
            //    default: break;
            //}

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

        private void btn_analysis_show_partition_Click(object sender, EventArgs e)
        {
            DrawPartition(w_count, h_count, cb_analysis_partition_selectImage.SelectedItem.ToString(), cb_analysis_partition_selectTest.SelectedItem.ToString());
        }

        public Color SetColor(int A, Color color)
        {
            return Color.FromArgb(A, color.R, color.G, color.B);
        }
    }
}
