using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace DropBox
{
    public class LinkData
    {
        private string device_type;

        public struct LINK
        {
            public string file_name;
            public List<link_info> link_data;
        };

        public struct link_info
        {
            public int btn_id;
            public Button bttn;
            public string dst_file;
            public Point image_xy;
            public int image_width;
            public int image_height;
            public string input_type;
        };

        //원본 데이터를 담을 LINK 리스트
        List<LINK> links = new List<LINK>();

        //getters
        public List<LINK> GetLinks(){ return this.links;}
        public string GetDeviceType(){ return this.device_type;}

        //setters
        public void SetDeviceType(string new_device_type) { this.device_type = new_device_type; }
        public void SetLink(string new_file_name, int pTag, string pDstFile, Point pImage_xy, int pImage_width, int pImage_height, string pInput_type)
        {
            //원본 데이터에 ADD할 임시 LINK
            LINK temp_links = new LINK();
            //LINK->link_data에 대입할 임시 link_info 리스트
            List<link_info> list_temp_link_info = new List<link_info>();
            //임시 link_info 리스트에 추가할 내용
            link_info temp_link_info = new link_info();

            temp_links.file_name = new_file_name;
            temp_link_info.bttn = new Button();
            temp_link_info.btn_id = pTag;
            temp_link_info.dst_file = pDstFile;
            temp_link_info.image_xy = pImage_xy;
            temp_link_info.image_width = pImage_width;
            temp_link_info.image_height = pImage_height;
            temp_link_info.input_type = pInput_type;

            //이미 파일 이름이 links에 존재하면 해당 인덱스에 data만 추가
            for (int i = 0; i < links.Count; i++)
            {
                if (links.ElementAt(i).file_name.Equals(new_file_name))
                {
                    links.ElementAt(i).link_data.Add(temp_link_info);
                    //MessageBox.Show("Added : 이미 존재하는 파일명" + ", " + temp_link_info.btn_id + ", " + temp_link_info.DstFile + ", " + temp_link_info.image_height);
                    return;
                }
            }

            //존재하지 않으면 파일 이름 포함하여 데이터 전체를 새로운 링크로 추가
            //리스트형태의 임시 변수에 temp_link_info를 추가하고 이 변수를 temp_links의 link_data에 대입한다.
            list_temp_link_info.Add(temp_link_info);
            temp_links.link_data = list_temp_link_info;
            links.Add(temp_links);
            //임시 변수들 초기화
            //list_temp_link_info = null;
            //MessageBox.Show(temp_link_info.btn_id.ToString());
            //temp_links = null;

            //MessageBox.Show("Added : " + temp_links.fileName + ", " + temp_link_info.btn_id + ", " + temp_link_info.DstFile + ", " + temp_link_info.image_height);
            return;
        }

        public int AddLink(string image_name, int addIndex, int pTag, string pDstFile, Point pImage_xy, int pImage_width, int pImage_height, string pInput_type)
        {
            link_info plink_info = new link_info();

            plink_info.btn_id = pTag;
            plink_info.bttn = new Button();
            plink_info.dst_file = pDstFile;
            plink_info.image_xy = pImage_xy;
            plink_info.image_width = pImage_width;
            plink_info.image_height = pImage_height;
            plink_info.input_type = pInput_type;

            //해당 이미지에 처음 링크를 설정하는 것이라면 먼저 new LINK를 새로 추가한다.
            try {
                links.ElementAt(addIndex).link_data.Add(plink_info);
            }
            catch (ArgumentOutOfRangeException e)
            {
                //원본 데이터에 ADD할 임시 LINK
                LINK temp_links = new LINK();
                temp_links.file_name = image_name;
                //LINK->link_data에 대입할 임시 link_info 리스트
                List<link_info> list_temp_link_info = new List<link_info>();
                //임시 link_info 리스트에 추가할 내용
                link_info temp_link_info = new link_info();

                temp_link_info.bttn = new Button();
                temp_link_info.btn_id = pTag;
                temp_link_info.dst_file = pDstFile;
                temp_link_info.image_xy = pImage_xy;
                temp_link_info.image_width = pImage_width;
                temp_link_info.image_height = pImage_height;
                temp_link_info.input_type = pInput_type;

                list_temp_link_info.Add(temp_link_info);
                temp_links.link_data = list_temp_link_info;
                links.Add(temp_links);

                //MessageBox.Show(links.ElementAt(2).file_name + "  " + links.ElementAt(2).link_data.ElementAt(0).image_height);
            }

            MessageBox.Show(links[addIndex].link_data.Count.ToString());
            return links.ElementAt(addIndex).link_data.Count - 1;
        }

        
    }
}
