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
    public partial class Survey : Form
    {
        List<EditProject.SurveyData> pSurvey_data;
        ScenarioData sData;
        string project_name;

        public Survey(List<EditProject.SurveyData> _pSurvey_data, string _project_name, ScenarioData _sData)
        {
            InitializeComponent();
            pSurvey_data = _pSurvey_data;
            project_name = _project_name;
            sData = _sData;

            for (int i = 0; i < sData.getSData().Count; i++)
            {
                cb_analysis_survey_selectScenario.Items.Add(sData.getSData()[i].title);
            }
            if (sData.getSData().Count == 0)
            {
                cb_analysis_survey_selectScenario.Items.Add("데이터없음");
            }
            cb_analysis_survey_selectScenario.SelectedIndex = 0;

            for (int i = 0; i < pSurvey_data.Count; i++)
            {
                cb_analysis_survey_selectTest.Items.Add(pSurvey_data[i].tag);
            }
            if (pSurvey_data.Count == 0)
            {
                cb_analysis_survey_selectTest.Items.Add("데이터없음");
            }
            cb_analysis_survey_selectTest.SelectedIndex = 0;

        }

        private void DrawSurvey(int index)
        {
            fpanel_analysis_survey.Controls.Clear();
            for (int i = 0; i < pSurvey_data[index].survey_info.Count; i++)
            {
                if (pSurvey_data[index].survey_info[i].question_type.CompareTo("resultTestQuestion_overTime") == 0)
                {
                    FlowLayoutPanel new_flowlayout = new FlowLayoutPanel();
                    new_flowlayout.FlowDirection = FlowDirection.LeftToRight;
                    fpanel_analysis_survey.Controls.Add(new_flowlayout);

                    PictureBox pBeforeImg = new PictureBox();
                    try
                    {
                        pBeforeImg.BackgroundImage = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + pSurvey_data[index].survey_info[i].beforeImg);
                    }
                    catch (FileNotFoundException fe)
                    {
                        pBeforeImg.BackColor = Color.Gray;
                        Label notFound = new Label();
                        notFound.Text = pSurvey_data[index].survey_info[i].beforeImg + "\nnot found";
                        pBeforeImg.Controls.Add(notFound);
                        notFound.Anchor = AnchorStyles.None | AnchorStyles.Left;
                    }
                    pBeforeImg.BackgroundImageLayout = ImageLayout.Stretch;
                    pBeforeImg.Size = new Size((int)(200 * 0.5625), 200);

                    PictureBox arrow = new PictureBox();
                    //arrow.BackgroundImage = Properties.Resources.arrow;
                    arrow.BackgroundImage = Image.FromFile(@"C:\Users\lewis\Documents\Visual Studio 2015\Projects\DistributionWork\DistributionWork\Resources\arrow.png");
                    arrow.Size = new Size(20, 20);
                    arrow.BackgroundImageLayout = ImageLayout.Stretch;


                    PictureBox pAfterImg = new PictureBox();
                    try
                    {
                        pAfterImg.BackgroundImage = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + pSurvey_data[index].survey_info[i].afterImg);
                    }
                    catch (FileNotFoundException fe)
                    {
                        pAfterImg.BackColor = Color.Gray;
                        Label notFound = new Label();
                        notFound.Text = pSurvey_data[index].survey_info[i].afterImg + "\nnot found";
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
                label_q.Text = "Q. " + pSurvey_data[index].survey_info[i].question;
                fpanel_analysis_survey.Controls.Add(label_q);
                label_q.Size = label_q.PreferredSize;

                Label label_a = new Label();

                label_a.Text = "A. " + pSurvey_data[index].survey_info[i].answer + "\n -----------------------------------------------------------------";
                label_a.ForeColor = Color.Red;
                fpanel_analysis_survey.Controls.Add(label_a);
                label_a.AutoSize = true;
            }
        }

        private void btn_analysis_show_survey_Click(object sender, EventArgs e)
        {
            bool exist = false;
            int index = 0;
            //여기서 인덱스 찾아서 넘겨야 할 듯
            for (int i = 0; i < pSurvey_data.Count; i++)
            {
                //비교 : tag, div, scenario_name              //태그가 모두 다르다는게 확정이면 div는 비교할 필요 없음
                if ((pSurvey_data[i].tag.CompareTo(cb_analysis_survey_selectTest.SelectedItem) == 0)
                    && (pSurvey_data[i].scenario_name.CompareTo(cb_analysis_survey_selectScenario.SelectedItem) == 0))
                {
                    index = i;
                    exist = true;
                    break;
                }
            }

            if (exist == true)
            {
                DrawSurvey(index);
            }
        }

        //private void cb_analysis_survey_selectGroup_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cb_analysis_survey_selectTest.Items.Clear();
        //    //피어목록보여주기
        //    if (cb_analysis_survey_selectGroup.SelectedIndex == 0)
        //    {
        //        for (int i = 0; i < pSurvey_data.Count; i++)
        //        {
        //            if (pSurvey_data[i].div.CompareTo("p") == 0)
        //            {
        //                cb_analysis_survey_selectTest.Items.Add(pSurvey_data[i].tag);
        //            }
        //        }
        //    }
        //    //유저
        //    else
        //    {
        //        for (int i = 0; i < pSurvey_data.Count; i++)
        //        {
        //            if (pSurvey_data[i].div.CompareTo("u") == 0)
        //            {
        //                cb_analysis_survey_selectTest.Items.Add(pSurvey_data[i].tag);
        //            }
        //        }
        //    }
        //    cb_analysis_survey_selectTest.Sorted = true;

        //    try
        //    {
        //        cb_analysis_survey_selectTest.SelectedIndex = 0;
        //    }
        //    catch (ArgumentOutOfRangeException ae)
        //    {
        //        cb_analysis_survey_selectTest.Items.Add("데이터없음");
        //        cb_analysis_survey_selectTest.SelectedIndex = 0;
        //    }
        //}
    }
}
