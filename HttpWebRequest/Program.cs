using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace HttpWebRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 2; i++)
            {
                openURL("http://tools.ietf.org/rfc/rfc" + i + ".txt");
                System.Threading.Thread.Sleep(3000);
            }
        }

        static void openURL(string strURL)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36";
            request.Accept = "*/*";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string s = readStream.ReadToEnd();
            s = s.Replace("\n", "\r\n");
            response.Close();
            readStream.Close();
            string filename = strURL.Replace(@"http://tools.ietf.org/rfc/", "");
            if (!Directory.Exists(@"C:\RFC"))
            {
                Directory.CreateDirectory(@"C:\RFC");
            }

            using (StreamWriter outfile = new StreamWriter(@"C:\RFC\" + filename))
            {
                outfile.Write(s.ToString());
            }


        }
    }

}
