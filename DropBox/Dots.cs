using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DropBox
{
    public partial class Dots : Form
    {
        PictureBox pic;
        Graphics gr;
        Brush br;
        Bitmap bitmap;
        LinkData pData = new LinkData();
        List<EditProject.TotalData> pTotal_data;
        List<string> image_names;

        AnalysisData aData = new AnalysisData();
        ScenarioData sData = new ScenarioData();

        string project_name;

        Regex rgbInputR;
        Regex rgbInputG;
        Regex rgbInputB;

        Match R, G, B;
        int r;
        int g;
        int b;


        string colorX;

        [DllImport("gdi32")]
        private static extern int GetPixel(IntPtr hdc, int x, int y);
        [DllImport("User32")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);

        private static readonly IntPtr DesktopDC = GetWindowDC(IntPtr.Zero);

        public static System.Drawing.Color GetPixelAtCursor()
        {
            System.Drawing.Point p = Cursor.Position;
            return System.Drawing.Color.FromArgb(GetPixel(DesktopDC, p.X, p.Y));
        }

        public Dots(List<EditProject.TotalData> _pTotal_data, string _project_name, ScenarioData _sData)
        {
            InitializeComponent();
            pTotal_data = _pTotal_data;
            sData = _sData;
            project_name = _project_name;

            string cPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name;
            IEnumerable<string> imagenames = Directory.GetFiles(cPath, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".jpg") || s.EndsWith(".jpeg") || s.EndsWith(".png"));

            image_names = imagenames.Cast<string>().ToList();

            for(int i = 0; i < sData.getSData().Count; i++)
            {
                cb_analysis_dots_selectScenario.Items.Add(sData.getSData()[i].title);
            }
            if (sData.getSData().Count == 0)
            {
                cb_analysis_dots_selectScenario.Items.Add("데이터없음");
            }
            cb_analysis_dots_selectScenario.SelectedIndex = 0;


            for (int i = 0; i < image_names.Count; i++)                       //이거 시나리오 콤보박스 인덱스 change에서 사용
            {
                cb_analysis_dots_selectImage.Items.Add(Path.GetFileName(image_names[i]));
            }
            cb_analysis_dots_selectImage.Sorted = true;

            if (image_names.Count == 0)
            {
                cb_analysis_dots_selectImage.Items.Add("데이터없음");
            }
            cb_analysis_dots_selectImage.SelectedIndex = 0;

            cb_analysis_dots_selectTest.SelectedIndex = 0;
        }

        public void DrawDots(string selected, string test)
        {
            panel_analysis_picture.Height = (int)(this.Height * 0.85);

            //backgroundimage width height를 해상도 받아온 것으로 해야함 - 뒤에 숫자.
            panel_analysis_picture.Width = (int)(panel_analysis_picture.Height * ((double)720 / (double)1280));
            bitmap = new Bitmap(panel_analysis_picture.Width, panel_analysis_picture.Height);

            panel_analysis_picture.Location = new Point(((int)(this.Width / 2) - (panel_analysis_picture.Width / 2)), 0);


            //기존에 설정되어 있던 컨트롤 다 지우기
            panel_analysis_picture_dot.Controls.Remove(pic);
            //panel_analysis_picture_dot.BackColor = Color.White;
            pic = new PictureBox();
            pic.Parent = panel_analysis_picture_dot;
            panel_analysis_picture_dot.Controls.Add(pic);
            pic.Location = new Point(0, 0);

            //Image img = Bitmap.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + selected);
            //MessageBox.Show(panel_analysis_picture_dot.Width.ToString() + "//" + img.Width.ToString());
            //float ratio = (float)panel_analysis_picture_dot.Width / (float)img.Width;

            //panel_analysis_picture_dot.BackgroundImage = Bitmap.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + selected);
            //panel_analysis_picture_dot.BackgroundImageLayout = ImageLayout.Stretch;

            //Image img_temp = AlterTransparency(Bitmap.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + selected), 80);

            pic.BackgroundImage = Bitmap.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + selected);
            pic.BackgroundImageLayout = ImageLayout.Stretch;
            pic.Size = panel_analysis_picture.Size;
            //pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_mousemove);
            //pic.BackColor = Color.White;
            //pic.BackgroundImage = img_temp;
            //pic.BackgroundImageLayout = ImageLayout.Stretch;
            //gr = panel_analysis_picture_dot.CreateGraphics();
            gr = pic.CreateGraphics();
            //gr = Graphics.FromHwnd(panel_analysis_picture_dot.Handle);
            gr = Graphics.FromImage(pic.BackgroundImage);

            br = new SolidBrush(SetColor(30, Color.Red));

            int index = 0;
            for (int i = 0; i < pTotal_data.Count; i++)
            {
                if (pTotal_data[i].image_name.CompareTo(selected) == 0)
                {
                    index = i;
                    break;
                }
            }

            if(test.CompareTo("모두보기") == 0)
            {
                //MessageBox.Show("창 크기: " + panel_analysis_picture_dot.Width.ToString() + ", " + panel_analysis_picture_dot.Height.ToString());
                try
                {
                    for (int j = 0; j < pTotal_data[index].event_data.Count; j++)
                    {
                        if (pTotal_data[index].image_name.CompareTo(selected) == 0)
                        {
                            //gr.FillPie(br, new Rectangle(new Point(((int)((pTotal_data[index].event_data[j].xcoord*ratio) - 15)), (int)((pTotal_data[index].event_data[j].ycoord * ratio) - 15)), new Size(30, 30)), 0, 360);
                            gr.FillPie(br, new Rectangle(new Point(((int)(pTotal_data[index].event_data[j].xcoord) - 25), (int)(pTotal_data[index].event_data[j].ycoord) - 25), new Size(50, 50)), 0, 360);
                            //MessageBox.Show(((pTotal_data[index].event_data[j].xcoord * ratio) - 15).ToString() + ", "+ ((pTotal_data[index].event_data[j].ycoord * ratio) - 15).ToString());
                        }
                    }
                }
                catch (ArgumentOutOfRangeException ae)
                {

                }
            }

            //전체 데이터중에 클릭들이 가지고 있는 이미지 이름과 현재 이미지 이름이 같은거만 그리기
            //ALL / USER / PEER
            //switch (_div)
            //{
            //    case "모두보기":
            //        //MessageBox.Show("창 크기: " + panel_analysis_picture_dot.Width.ToString() + ", " + panel_analysis_picture_dot.Height.ToString());
            //        for (int j = 0; j < pTotal_data[index].event_data.Count; j++)
            //        {
            //            if (pTotal_data[index].image_name.CompareTo(selected) == 0)
            //            {
            //                //gr.FillPie(br, new Rectangle(new Point(((int)((pTotal_data[index].event_data[j].xcoord*ratio) - 15)), (int)((pTotal_data[index].event_data[j].ycoord * ratio) - 15)), new Size(30, 30)), 0, 360);
            //                gr.FillPie(br, new Rectangle(new Point(((int)(pTotal_data[index].event_data[j].xcoord) - 25), (int)(pTotal_data[index].event_data[j].ycoord) - 25), new Size(50, 50)), 0, 360);
            //                //MessageBox.Show(((pTotal_data[index].event_data[j].xcoord * ratio) - 15).ToString() + ", "+ ((pTotal_data[index].event_data[j].ycoord * ratio) - 15).ToString());
            //            }
            //        }
            //        break;
            //    case "유저만":
            //        for (int j = 0; j < pTotal_data[index].event_data.Count; j++)
            //        {
            //            if (pTotal_data[index].image_name.CompareTo(selected) == 0 && pTotal_data[index].event_data[j].div.CompareTo("u") == 0)
            //            {
            //                gr.FillPie(br, new Rectangle(new Point(pTotal_data[index].event_data[j].xcoord - 25, pTotal_data[index].event_data[j].ycoord - 25), new Size(50, 50)), 0, 360);
            //            }
            //        }
            //        break;
            //    case "피어만":
            //        //p가 없을 때 null exception 뜸
            //        for (int j = 0; j < pTotal_data[index].event_data.Count; j++)
            //        {
            //            if (pTotal_data[index].image_name.CompareTo(selected) == 0 && pTotal_data[index].event_data[j].div.CompareTo("p") == 0)
            //            {
            //                gr.FillPie(br, new Rectangle(new Point(pTotal_data[index].event_data[j].xcoord - 25, pTotal_data[index].event_data[j].ycoord - 25), new Size(50, 50)), 0, 360);
            //            }
            //        }
            //        break;
            //    default: break;
            //}
        }
        
        public static Bitmap AlterTransparency(Image image, Byte Alpha)
        {
            Bitmap Original = new Bitmap(image);
            Bitmap TransparentImage = new Bitmap(image.Width, image.Height);

            Color c = Color.Black;
            Color v = Color.Black;

            int av = 0;

            for(int i = 0; i < image.Width; i++)
            {
                for(int y = 0; y < image.Height; y++)
                {
                    c = Original.GetPixel(i, y);
                    v = Color.FromArgb(Alpha, c.R, c.G, c.B);
                    TransparentImage.SetPixel(i, y, v);
                }
            }

            return TransparentImage;
        }

        private void panel_analysis_picture_dot_MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine("1" + GetPixelAtCursor().A.ToString() + "//" + GetPixelAtCursor().R.ToString() + "//" + GetPixelAtCursor().G.ToString() + "//" + GetPixelAtCursor().B.ToString());

        }

        private void panel_analysis_picture_MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine("2" + GetPixelAtCursor().A.ToString() + "//" + GetPixelAtCursor().R.ToString() + "//" + GetPixelAtCursor().G.ToString() + "//" + GetPixelAtCursor().B.ToString());

        }

        private void pic_mousemove(object sender, MouseEventArgs e)
        {
            Console.WriteLine("3" + GetPixelAtCursor().A.ToString() + "//" + GetPixelAtCursor().R.ToString() + "//" + GetPixelAtCursor().G.ToString() + "//" + GetPixelAtCursor().B.ToString());

        }

        public Color SetColor(int A, Color color)
        {
            return Color.FromArgb(A, color.R, color.G, color.B);
        }

        private void btn_analysis_show_dot_Click_1(object sender, EventArgs e)
        {
            DrawDots(cb_analysis_dots_selectImage.SelectedItem.ToString(), cb_analysis_dots_selectTest.SelectedItem.ToString());
        }
    }
}
