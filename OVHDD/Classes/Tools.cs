using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OVHDD.Classes
{
    public class Tools
    {
        public static string ChangeOVHDNS(string host, string username, string password)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.ovh.com/nic/update?system=dyndns&hostname=" + host);
            string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            request.Headers.Add("Authorization", "Basic " + encoded);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream streamResponse = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            string responseString = streamRead.ReadToEnd();
            streamResponse.Close();
            streamRead.Close();
            response.Close();

            return responseString;
        }

    }
}
