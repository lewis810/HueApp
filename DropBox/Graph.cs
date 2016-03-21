using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nevron;
using Nevron.Dom;
using Nevron.GraphicsCore;
using Nevron.Editors;
using Nevron.Chart;
using Nevron.Chart.Functions;
using Nevron.Chart.Windows;
using Nevron.Chart.WinForm;

namespace DropBox
{
    public partial class Graph : Form
    {
        ResultData rData;

        private NChart m_Chart;
        private NBarSeries m_Bar;
        private NLineSeries m_Line;
        private NFunctionCalculator m_FuncCalculator;

        private NHighLowSeries m_HighLow;


       /* public Graph(ResultData _rData)
        {

        }*/

        public Graph(ResultData _rData, string projectName, List<string> scenarioNames)
        {
            NLicense license = new NLicense("096ff936-7602-3c02-009c-5ea6176b006d");
            NLicenseManager.Instance.SetLicense(license);
            NLicenseManager.Instance.LockLicense = true;

            InitializeComponent();
            rData = _rData;


            listBox1.Items.Add(projectName);
            foreach(string temp in scenarioNames)
            {
                listBox1.Items.Add(temp);
            }

            if(rData == null)
            {
                MessageBox.Show("NULL!!!");
            }

            foreach (ResultData.ResultInfo temp in rData.getRData())
            {
                foreach(ResultData.ImgTime time in temp.pathInfo)
                {
                    listBox1.Items.Add(time.imgName + "  ||  " + time.timeImg);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            nChartControl1.Clear();

            

            //rData.getRData()[0].taskName 이런식으로 인덱스 줘서 사용하면 될 듯
            m_FuncCalculator = new NFunctionCalculator();

            nChartControl1.Legends.Clear();



            // set a chart title
            NLabel title = nChartControl1.Labels.AddHeader("Task x 소요 시간");
            title.TextStyle.FontStyle = new NFontStyle("Times New Roman", 18, FontStyle.Italic);
            title.ContentAlignment = ContentAlignment.BottomCenter;
            title.Location = new NPointL(new NLength(50, NRelativeUnit.ParentPercentage), new NLength(2, NRelativeUnit.ParentPercentage));

            // setup chart
            m_Chart = nChartControl1.Charts[0];
            m_Chart.BoundsMode = BoundsMode.Stretch;
            m_Chart.Location = new NPointL(new NLength(10, NRelativeUnit.ParentPercentage), new NLength(15, NRelativeUnit.ParentPercentage));
            m_Chart.Size = new NSizeL(new NLength(80, NRelativeUnit.ParentPercentage), new NLength(75, NRelativeUnit.ParentPercentage));



            //m_Chart.Axis(StandardAxis.Depth).Visible = false;


            // add a line series for the function
            m_Line = (NLineSeries)m_Chart.Series.Add(SeriesType.Line);
            m_Line.MarkerStyle.Visible = true;
            m_Line.MarkerStyle.BorderStyle.Color = Color.DarkGreen;
            m_Line.MarkerStyle.BorderStyle.Width = new NLength(2, NGraphicsUnit.Pixel);
            m_Line.MarkerStyle.Width = new NLength(1.2f, NRelativeUnit.ParentPercentage);
            m_Line.MarkerStyle.Height = new NLength(1.2f, NRelativeUnit.ParentPercentage);
            m_Line.MarkerStyle.PointShape = PointShape.Cylinder;
            m_Line.MarkerStyle.FillStyle = new NColorFillStyle(Color.Gold);
            m_Line.BorderStyle.Color = Color.DarkGreen;
            m_Line.BorderStyle.Width = new NLength(2, NGraphicsUnit.Pixel);
            m_Line.Legend.Mode = SeriesLegendMode.None;
            m_Line.DataLabelStyle.Format = "<value>";
            m_Line.Values.ValueFormatter = new NNumericValueFormatter("0.0");
            m_Line.ShadowStyle.Type = ShadowType.GaussianBlur;
            m_Line.ShadowStyle.Offset = new NPointL(2, 2);
            m_Line.ShadowStyle.Color = Color.FromArgb(120, 0, 0, 0);
            m_Line.ShadowStyle.FadeLength = new NLength(5);



            // add the bar series
            m_Bar = (NBarSeries)m_Chart.Series.Add(SeriesType.Bar);
            m_Bar.Name = "Bar1";
            m_Bar.Values.Name = "values";
            m_Bar.Values.ValueFormatter = new NNumericValueFormatter("0.0");
            m_Bar.MultiBarMode = MultiBarMode.Stacked;
            m_Bar.DataLabelStyle.Visible = true; // 바에 보이는 값
            //m_Bar.Legend.Mode = SeriesLegendMode.None; // 범례 안보이게 하는 거
            m_Bar.BarShape = BarShape.Cylinder;
            m_Bar.BorderStyle.Width = new NLength(0, NGraphicsUnit.Pixel);
            m_Bar.FillStyle = new NColorFillStyle(Color.DarkKhaki);
            m_Bar.ShadowStyle.Type = ShadowType.Solid;
            m_Bar.ShadowStyle.Offset = new NPointL(2, 2);
            m_Bar.ShadowStyle.Color = Color.FromArgb(80, 0, 0, 0);
            //m_Bar.Values.FillRandomRange(new Random(), 14, 0, 100);
            m_Bar.Values.Add(2.3);
            m_Bar.Values.Add(3.1);
            m_Bar.Values.Add(1.2);
            m_Bar.Values.Add(1.7);
            m_Bar.Values.Add(4.2);
            m_Bar.Values.Add(1.9);



            NLinearScaleConfigurator linearScale = new NLinearScaleConfigurator();
            //m_Chart.Axis(StandardAxis.PrimaryX).ScaleConfigurator = linearScale;
            m_Chart.Axis(StandardAxis.PrimaryX).ScrollBar.ResetButton.Visible = false;

            NNumericAxisPagingView numericPagingView = new NNumericAxisPagingView(new NRange1DD(0, 10));
            m_Chart.Axis(StandardAxis.PrimaryX).PagingView = numericPagingView;
            m_Chart.Axis(StandardAxis.PrimaryX).ScrollBar.Visible = true;
            nChartControl1.Controller.Tools.Add(new NAxisScrollTool());

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)m_Chart.Axis(StandardAxis.PrimaryX).ScaleConfigurator;
            scaleConfiguratorX.MajorTickMode = MajorTickMode.AutoMaxCount;

            scaleConfiguratorX.AutoLabels = false;
            scaleConfiguratorX.Labels.Add("7_main-02.png");
            scaleConfiguratorX.Labels.Add("10-06.png");
            scaleConfiguratorX.Labels.Add("10-05.png");
            scaleConfiguratorX.Labels.Add("7_main-02.png");
            scaleConfiguratorX.Labels.Add("7_main-11.png");
            scaleConfiguratorX.Labels.Add("scrap.jpg");


            // add range selection (needed for zoom in)
            //m_Chart.RangeSelections.Add(new NRangeSelection());


            //m_Bar.Values.Add(new NDataPoint(23.3, "main.png"));
            //m_Bar.AddDataPoint(new NDataPoint(23.3, "main.png"));


            /******************************************* 데이터 parse
             * 
             * 
             * 
             * 
             * 
             * 
             *
             *******************************************/


            //m_FuncCalculator.Arguments.Add(m_Line.Values);
            m_FuncCalculator.Arguments.Add(m_Bar.Values);
            m_FuncCalculator.Expression = "CUMSUM(values)";
            m_Line.Values = m_FuncCalculator.Calculate();

            m_Line.Values.ValueFormatter = new NNumericValueFormatter("0.0");


            // form controls
            /*m_FunctionCombo.Items.Add("Power");
            m_FunctionCombo.Items.Add("Cumulative");
            m_FunctionCombo.Items.Add("Exponential Average");
            m_FunctionCombo.SelectedIndex = 0;*/

            
            nChartControl1.Refresh();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            nChartControl1.Clear();
            nChartControl1.Legends.Clear();

            NLabel title = nChartControl1.Labels.AddHeader("2D High Low Chart");
            title.TextStyle.FontStyle = new NFontStyle("Times New Roman", 18, FontStyle.Italic);
            //title.TextStyle.FillStyle = new NColorFillStyle(GreyBlue);

            // configure the chart
            m_Chart = nChartControl1.Charts[0];
            m_Chart.Axis(StandardAxis.Depth).Visible = false;

            // add a High-Low series
            m_HighLow = (NHighLowSeries)m_Chart.Series.Add(SeriesType.HighLow);
            m_HighLow.Name = "High-Low Series";
            m_HighLow.HighFillStyle = new NColorFillStyle(Color.FromArgb(68, 90, 108));
            m_HighLow.HighBorderStyle = new NStrokeStyle(Color.FromArgb(254, 181, 25));
            //m_HighLow.LowFillStyle = new NColorFillStyle(DarkOrange);
            m_HighLow.DataLabelStyle.Visible = true;
            m_HighLow.DataLabelStyle.Format = " <high_value>\n<low_value>";
            m_HighLow.LowValues.ValueFormatter = new NNumericValueFormatter("0.#");
            m_HighLow.HighValues.ValueFormatter = new NNumericValueFormatter("0.#");


            m_HighLow.DropLines = true;

            m_HighLow.ClearDataPoints();
            for (int i = 0; i < 20; i++)
            {
                //m_HighLow.HighValues.Add(i);
                //m_HighLow.LowValues.Add(i);
            }


            NLinearScaleConfigurator linearScale = new NLinearScaleConfigurator();
            //m_Chart.Axis(StandardAxis.PrimaryX).ScaleConfigurator = linearScale;
            m_Chart.Axis(StandardAxis.PrimaryX).ScrollBar.ResetButton.Visible = false;

            NNumericAxisPagingView numericPagingView = new NNumericAxisPagingView(new NRange1DD(0, 10));
            m_Chart.Axis(StandardAxis.PrimaryX).PagingView = numericPagingView;
            m_Chart.Axis(StandardAxis.PrimaryX).ScrollBar.Visible = true;
            nChartControl1.Controller.Tools.Add(new NAxisScrollTool());

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)m_Chart.Axis(StandardAxis.PrimaryX).ScaleConfigurator;
            scaleConfiguratorX.MajorTickMode = MajorTickMode.AutoMaxCount;

            scaleConfiguratorX.AutoLabels = false;
            scaleConfiguratorX.Labels.Add("7_main-02.png");
            scaleConfiguratorX.Labels.Add("10-06.png");
            scaleConfiguratorX.Labels.Add("10-05.png");
            scaleConfiguratorX.Labels.Add("7_main-02.png");
            scaleConfiguratorX.Labels.Add("7_main-11.png");
            scaleConfiguratorX.Labels.Add("scrap.jpg");


            m_HighLow.HighValues.Add(3.4);
            m_HighLow.LowValues.Add(1.2);

            m_HighLow.HighValues.Add(2.5);
            m_HighLow.LowValues.Add(2.1);

            m_HighLow.HighValues.Add(1.9);
            m_HighLow.LowValues.Add(1.1);

            m_HighLow.HighValues.Add(4.2);
            m_HighLow.LowValues.Add(2.3);

            m_HighLow.HighValues.Add(3.1);
            m_HighLow.LowValues.Add(1.4);

            m_HighLow.HighValues.Add(5.2);
            m_HighLow.LowValues.Add(3.4);

            
            nChartControl1.Refresh();

        }
    }
}
