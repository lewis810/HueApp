using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Collections.Generic;


namespace DropBox
{
    public class FileDownloader
    {
        string[] filenames;
        string scenario_name;
        string project_name;
        List<int> under_bar_index;


        public FileDownloader(string[] _filenames, string _scenario_name, string _project_name)
        {
            Console.WriteLine("다운로드 세션 시작");

            filenames = _filenames;
            scenario_name = _scenario_name;
            project_name = _project_name;
            under_bar_index = new List<int>();

            string mPath = @"C:\Users\" + Environment.UserName + "\\Nudge\\";
            DirectoryInfo di = new DirectoryInfo(mPath);

            if (di.Exists == false)
            {
                di.Create();
            }

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://kyjk3@kyjk3.cafe24.com/uploads/");
            request.Credentials = new NetworkCredential("kyjk3", "sogong123");
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            //encoding 한글깨짐방지
            StreamReader streamReader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.Default, true);

            List<string> fileNames = new List<string>();
            string fileName = streamReader.ReadLine();

            while (fileName != null)
            {
                //여기서 xml의 이름에 대한 조건을 걸어서 필요한 xml만 리스트에 넣기 -> 이 정보만 다운
                //시나리오 이름(받아온 거) 디바이스 정보(새로 추출)를 가지고 조건 걸기
                //시나리오 정보로 먼저 비교해서 반복문 로드 줄이기

                Console.WriteLine(fileName);
                if (fileName.Contains(scenario_name) && fileName.Contains(project_name))
                {
                    Console.WriteLine("있음1");

                    //디바이스정보 추출
                    under_bar_index.Clear();
                    for (int k = 0, h = 0; k < fileName.Length; k++)
                    {
                        if (fileName[k].CompareTo('_') == 0)
                        {
                            under_bar_index.Add(k);
                            h++;
                        }
                    }
                    string device_id = fileName.Substring(under_bar_index[3] + 1, (under_bar_index[4] - under_bar_index[3]) - 1);
                    string scenario_tag = fileName.Substring(0, under_bar_index[0]);

                    //같은 시나리오 데이터 중에 해당 디바이스 정보가 있으면 다운 x
                    bool exist = false;
                    for (int i = 0; i < filenames.Length; i++)
                    {
                        //번호 //device_id //
                        if (filenames[i].Contains(device_id) && Path.GetFileName(filenames[i]).Substring(0, under_bar_index[0]).CompareTo(scenario_tag) == 0)
                        {
                            Console.WriteLine("이미 다운로드 되어있음");
                            exist = true;
                        }
                    }

                    if (exist == false)
                    {
                        //같은 시나리오 이름에 디바이스 정보가 없는 데이터
                        Console.WriteLine("다운로드 목록에 추가.");
                        fileNames.Add(fileName);
                    }

                }
                //fileNames.Add(fileName);
                fileName = streamReader.ReadLine();
            }

            request = null;
            streamReader = null;

            string remoteUri = "http://kyjk3.cafe24.com/uploads/";
            string myStringWebResource = null;
            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
            myWebClient = new System.Net.WebClient();

            for (int i = 0; i < fileNames.Count; i++)
            {
                Console.WriteLine("in " + fileNames[i]);

                if (fileNames[i].Contains(".xml"))
                {
                    // Concatenate the domain with the Web resource filename.
                    myStringWebResource = remoteUri + fileNames[i];
                    Uri uri = new Uri(myStringWebResource);
                    Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileNames[i], myStringWebResource);
                    // Download the Web resource and save it into the current filesystem folder.
                    // 특정 폴더로 파일 저장
                    myWebClient.DownloadFileAsync(uri, mPath + fileNames[i]);
                    while (myWebClient.IsBusy)
                    {

                    }
                    Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileNames[i], myStringWebResource);
                    Console.WriteLine("\nDownloaded file saved in the following file system folder:\n\t" + Application.StartupPath);
                }
            }


            Console.WriteLine("다운로드 세션 종료");
        }
    }
}