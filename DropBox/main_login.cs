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
    public partial class main_login : Form
    {
        public main_login(int w, int h)
        {
            InitializeComponent();
            
            //parent를 뒷 배경의 오브젝트로 두게 되면 배경을 transparent한 효과가 나타난다.
            panel_center.Parent = pictureBox_back;
            //로고를 센터로
            panel_center.Location = new Point((int)(w / 2) - (int)(panel_center.Width / 2), (int)(h / 2) - (int)(panel_center.Height / 2));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //main _main = new main(1);
            //_main.Show();
            //this.Close();
            Application.Restart();
        }
    }
}
