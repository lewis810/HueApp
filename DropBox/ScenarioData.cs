using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropBox
{
    public class ScenarioData
    {
        public struct SCENARIO_DATA
        {
            public int tag;
            public string title;
            public string time;
            public string purpose;
            public string start_img;
            public string end_img;
        }

        List<SCENARIO_DATA> sData = new List<SCENARIO_DATA>();

        public void AddScenario(int sTag, string sTitle, string sPurpose, string sTime, string sStartImg, string sEndImg)
        {
            sData.Add(new SCENARIO_DATA() { tag = sTag, title = sTitle, purpose = sPurpose, time = sTime, start_img = sStartImg, end_img = sEndImg });
        }

        public List<SCENARIO_DATA> getSData()
        {
            return sData;
        }

        public int getSLastIndex()
        {
            int last_index = 0;
            for (int i = 0; i < sData.Count; i++)
            {
                if(last_index < sData[i].tag)
                {
                    last_index = sData[i].tag;
                }
            }
            return last_index;
        }

    }
}
