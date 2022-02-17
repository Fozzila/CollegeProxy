using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace CollegeProxy
{
    class CollegeBoard
    {
        public static string ReplaceZeros(string text)
        {
            string res = "";
            res = text.Replace("0,", "1,");
            res = res.Replace("[0,", "[1,");
            res = res.Replace(",0]", ",1]");
            return res;
        }
        public static string ReplaceComplete(string text)
        {
            return text.Replace("STARTED", "COMPLETE");
        }
        public static string ReplaceWatchTimes(string text)
        {
            var jobj = (JObject)JsonConvert.DeserializeObject(text);
            var vars = (JObject)jobj["variables"];
            vars["watchedPercentage"] = "1.00";
            vars["playTimePercentage"] = "1.00";
            jobj["variables"] = vars;
            string reparsed = JsonConvert.SerializeObject(jobj);

            return reparsed;
        }
    }
}
