using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace PadMonsterInfo
{

    static class Methods
    {
        public static bool UpdateFileCached(string remoteUrl, string localUrl, int thresholdInHours = -1)
        {
            bool returnValue = false;
            DateTime threshold;
            if (thresholdInHours > -1)
            {
                threshold = DateTime.Now.AddHours(-thresholdInHours);
            }
            else
            {
                threshold = DateTime.MinValue;
            }
            try
            {
                if (!File.Exists(localUrl) || File.GetLastWriteTime(localUrl) < threshold)
                {
                    Console.WriteLine("we need to re-download " + localUrl);
                    using (System.Net.WebClient webClient = new System.Net.WebClient())
                    {
                        webClient.DownloadFile(remoteUrl, localUrl);
                        returnValue = true;
                    }
                }
                else
                {
                    returnValue = true;
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                returnValue = false;
            }
            return returnValue;
        }

        static List<Tuple<string, string>> downloads = null;
        static bool downloading = false;

        private async static Task downloadFileAsyncQueue(string remoteUrl, string localUrl)
        {
            if (downloads == null) downloads = new List<Tuple<string, string>>();
            if (!downloads.Contains(new Tuple<string, string>(remoteUrl, localUrl)))
            {
                downloads.Add(new Tuple<string, string>(remoteUrl, localUrl));
                while (downloading)
                {
                    await Task.Delay(100);
                }

                downloading = true;
                //Debug.WriteLine("begin downloading..." + localUrl);
                try
                {
                    using (System.Net.WebClient webClient = new System.Net.WebClient())
                    {
                        await webClient.DownloadFileTaskAsync(new Uri(remoteUrl), localUrl);
                    }
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e);
                    downloading = false;
                }
                //Debug.WriteLine("done downloading");
                downloading = false;
            }
            else
            {
                while(downloads.Count > 0)
                {
                    await Task.Delay(100);
                }
            }        
        }

        public async static Task UpdateFileCachedAsync(string remoteUrl, string localUrl, int thresholdInHours = -1)
        {
            DateTime threshold;
            if (thresholdInHours > -1)
            {
                threshold = DateTime.Now.AddHours(-thresholdInHours);
            }
            else
            {
                threshold = DateTime.MinValue;
            }
            try
            {
                if (!File.Exists(localUrl))
                {
                    //Console.WriteLine("we need to re-download " + localUrl);
                    await downloadFileAsyncQueue(remoteUrl, localUrl);
                }
                else if(File.GetLastWriteTime(localUrl) < threshold || new FileInfo(localUrl).Length == 0)
                {
                    await downloadFileAsyncQueue(remoteUrl, localUrl);
                }
                else
                {
                    //returnValue = true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //returnValue = false;
            }
        }
    }
}
