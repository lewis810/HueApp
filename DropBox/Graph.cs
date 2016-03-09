using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DropBox
{
    public partial class Graph : Form
    {
        ResultData rData;

        public Graph(ResultData _rData)
        {
            InitializeComponent();
            rData = _rData;

            //rData.getRData()[0].taskName 이런식으로 인덱스 줘서 사용하면 될 듯
        }
    }
}
