﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nemiro.OAuth;
using Nemiro.OAuth.LoginForms;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace DropBox
{
    public partial class main : Form
    {
        private string CurrentPath = "/";
        public static Button TempDeleteButton;

        public main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //this.GetAccessToken();

            if (String.IsNullOrEmpty(Properties.Settings.Default.AccessToken))
            {
                this.GetAccessToken();
            }
            else
            {
                this.GetFiles();
            }

        }

        private void GetAccessToken()
        {
            var login = new DropboxLogin("bfgjti9lkskri9h", "l4tuda9vhrsq6wv");
            login.Owner = this;
            login.ShowDialog();

            if (login.IsSuccessfully)
            {
                Properties.Settings.Default.AccessToken = login.AccessToken.Value;
                Properties.Settings.Default.Save();
                this.GetFiles();                                //초기 로그인 하고 나서 정보 받아오기
            }
            else
            {
                MessageBox.Show("error...");
            }
        }

        private void GetFiles()
        {
            OAuthUtility.GetAsync
            (
                "https://api.dropboxapi.com/1/metadata/auto/",
                new HttpParameterCollection
                {
                    {"path", this.CurrentPath },
                    {"access_token", Properties.Settings.Default.AccessToken }
                },
                callback: GetFiles_Result
            );
        }

        private void GetFiles_Result(RequestResult result)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<RequestResult>(GetFiles_Result), result);
                return;
            }

            if (result.StatusCode == 200)
            {
                //listBox1.Items.Clear();
                //listBox1.DisplayMember = "path";

                init();

                //foreach (UniValue file in result["contents"])
                //{
                //    //MessageBox.Show(file["path"].ToString());
                //    //listBox1.Items.Add(file);
                //}

                if (this.CurrentPath != "/")
                {
                    listBox1.Items.Insert(0, UniValue.ParseJson("{path: '..'}"));
                }
            }
            else
            {
                MessageBox.Show("error...GetFile");
            }
        }

        public void init()
        {
            string mPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE";
            DirectoryInfo Info = new DirectoryInfo(mPath);

            if (Info.Exists)
            {
                DirectoryInfo[] CInfo = Info.GetDirectories();

                // file 들이 있는지 확인


                foreach (DirectoryInfo eachInfo in CInfo)
                {
                    Button newButton = new Button();
                    newButton.Name = eachInfo.Name;
                    newButton.Text = "New project\n" + eachInfo.Name;
                    newButton.TextAlign = ContentAlignment.BottomCenter;
                    newButton.Size = new Size(128, 128);
                    newButton.Margin = new Padding(10, 10, 10, 10);
                    newButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.eachButton_Click);

                    if (eachInfo.GetFiles().Length > 0)
                    {
                        Image img;
                        using (var bmpTemp = new Bitmap(eachInfo.FullName + "\\" + eachInfo.GetFiles()[0]))
                        {
                            img = new Bitmap(bmpTemp);
                        }


                        // 이것을 썸네일로
                        //Bitmap tempImage = new Bitmap(eachInfo.FullName + "\\" + eachInfo.GetFiles()[0]);

                        using (Graphics gr = Graphics.FromImage(img))
                        {
                            gr.SmoothingMode = SmoothingMode.HighQuality;
                            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            gr.DrawImage(img, new Rectangle(0, 0, 64, 64));
                        }

                        PictureBox newPicture = new PictureBox();
                        newPicture.Image = img;
                        newPicture.Size = new Size(90, 80);
                        newPicture.Location = new Point(20, 10);
                        newPicture.SizeMode = PictureBoxSizeMode.StretchImage;

                        newButton.Controls.Add(newPicture);
                    }
                    flowLayoutPanel1.Controls.Add(newButton);
                }
            }
        }

        private void eachButton_Click(object sender, MouseEventArgs e)
        {
            //MouseEventArgs me = (MouseEventArgs)e;

            switch (e.Button)
            {
                case MouseButtons.Left:

                    // 여기에 정의
                    // 이 주소를 통하여서 안에 있는 그림 파일 혹은 기타 파일 가져 오기
                    Button temp_btn_left = sender as Button;

                    string mPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + temp_btn_left.Name + "\\";
                    //string mPath = @"C:\Prototype\\project\\" + temp_btn.Name + "\\";

                    //MessageBox.Show(temp, "Warning",
                    //        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    EditProject editProject = new EditProject(mPath);
                    editProject.Show();
                    //MessageBox.Show("Left click", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case MouseButtons.Right:
                    //MessageBox.Show("Right click", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    Button temp_btn_right = sender as Button;
                    ContextMenu cm = new ContextMenu();
                    cm.MenuItems.Add("Delete", new System.EventHandler(this.menuItem_delete_click));
                    cm.MenuItems.Add("Item 2");
                    temp_btn_right.ContextMenu = cm;
                    TempDeleteButton = temp_btn_right;

                    break;
            }

        }

        private void menuItem_delete_click(object sender, EventArgs e)
        {

            string mPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + TempDeleteButton.Name + "\\";

            //get control hovered with mouse
            //Button buttonToRemove = (this.GetChildAtPoint(this.PointToClient(Cursor.Position)) as Button);
            MessageBox.Show(TempDeleteButton.Text, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //if it's a Button, remove it from the form

            DirectoryInfo Info = new DirectoryInfo(mPath);

            /*if (Info.Exists)
            {
                foreach (FileInfo file in Info.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in Info.GetDirectories())
                {
                    dir.Delete(true);
                }
            }*/

            Info.Delete(true);

            flowLayoutPanel1.Controls.Remove(TempDeleteButton);



            //$$$$$$
        }



        private void button1_Click(object sender, EventArgs e)
        {
            OAuthUtility.PostAsync
            (
                "https://api.dropboxapi.com/1/fileops/create_folder",
                new HttpParameterCollection
                {
                    {"access_token", Properties.Settings.Default.AccessToken},
                    {"root", "auto"},
                    {"path", Path.Combine(this.CurrentPath, textBox1.Text).Replace("\\", "/")}
                },
                callback: CreateFolder_Result
            );
        }

        private void CreateFolder_Result(RequestResult result)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<RequestResult>(CreateFolder_Result), result);
                return;
            }

            if (result.StatusCode == 200)
            {
                this.GetFiles();
            }
            else
            {
                if (result["error"].HasValue)
                {
                    MessageBox.Show(result["error"].ToString());
                }
                else
                {
                    MessageBox.Show(result.ToString());
                }
            }
        }

        private void listBox1_doubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem.ToString() == null) { return; }

            UniValue file = (UniValue)listBox1.SelectedItem;

            if (file["path"] == "..")
            {
                if (this.CurrentPath != "/")
                {
                    this.CurrentPath = Path.GetDirectoryName(this.CurrentPath).Replace("\\", "/");
                }
            }

            else if (Regex.IsMatch(file["path"].ToString(), "jpg", RegexOptions.IgnoreCase) ||
                    Regex.IsMatch(file["path"].ToString(), "png", RegexOptions.IgnoreCase))
            {
                //MessageBox.Show(file["path"].ToString() + "\n" + Path.GetDirectoryName(file["path"].ToString()).Replace("\\", "/"));
                //MessageBox.Show("C:\\Users\\" + Environment.UserName + "\\Dropbox");

                Edit_Image edit_image = new Edit_Image("C:\\Users\\" + Environment.UserName + "\\Dropbox" + Path.GetDirectoryName(file["path"].ToString())
                                                    , "C:\\Users\\" + Environment.UserName + "\\Dropbox\\" + file["path"].ToString());
                edit_image.Show();
            }

            else
            {
                if (file["is_dir"] == 1)
                {
                    this.CurrentPath = file["path"].ToString();
                }

                else
                {
                    saveFileDialog1.FileName = Path.GetFileName(file["path"].ToString());

                    //MessageBox.Show(file["path"].ToString());

                    if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }

                    var web = new WebClient();
                    web.DownloadProgressChanged += DownloadProgressChanged;
                    web.DownloadFileAsync(new Uri(String.Format("https://content.dropboxapi.com/1/files/auto{0}?access_token={1}", file["path"], Properties.Settings.Default.AccessToken)), saveFileDialog1.FileName);
                }
            }
            this.GetFiles();
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) { return; }

            OAuthUtility.PutAsync
            (
                "https://content.dropboxapi.com/1/files_put/auto/",
                new HttpParameterCollection
                {
                    {"access_token", Properties.Settings.Default.AccessToken },
                    {"path", Path.Combine(this.CurrentPath, Path.GetFileName(openFileDialog1.FileName)).Replace("\\", "/")},
                    {"overwrite", "true" },
                    {"autorename", "true" },
                    {openFileDialog1.OpenFile() }

                },
                callback: Upload_Result

            );
        }

        private void Upload_Result(RequestResult result)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<RequestResult>(Upload_Result), result);
                return;
            }

            if (result.StatusCode == 200)
            {
                this.GetFiles();
            }
            else
            {
                if (result["error"].HasValue)
                {
                    MessageBox.Show(result["error"].ToString());
                }
                else
                {
                    MessageBox.Show(result.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AccessToken = null;
            Properties.Settings.Default.Save();

            Application.Restart();
        }

        private void btCreateProject_Click(object sender, EventArgs e)
        {
            CreateNewProject mCreateNewProject = new CreateNewProject(this);
            if (!mCreateNewProject.Visible)
                mCreateNewProject.Show();
        }
    }
}