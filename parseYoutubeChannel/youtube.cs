using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;

namespace parseYoutubeChannel
{
    public class youtube
    {
        private static string getSource(string url)
        {
            WebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            WebResponse wrs = wr.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(wrs.GetResponseStream(), Encoding.UTF8);

            string html = sr.ReadToEnd();
            return html;
        }

        public static string[] Channels(string name)
        {
            string url = "https://www.youtube.com/results?sp=EgIQAg%253D%253D&q=" + name;
            string html = getSource(url);
            string[] channels = html.Split(new string[] { "yt-lockup yt-lockup-tile yt-lockup-channel vve-check clearfix yt-uix-tile" }, StringSplitOptions.None);
            List<string> hrefs = new List<string>();
            for (int i = 1; i < channels.Length; i++)
            {
                string href = channels[i].Split(new string[] { "href=\"" }, StringSplitOptions.None)[1].Split(new string[] { "\"" }, StringSplitOptions.None)[0];
                if(href.Split('/')[1] == "user" || href.Split('/')[1] == "channel" && !href.Split('/')[2].StartsWith("UC"))
                hrefs.Add("https://www.youtube.com" + href);
            }

            return hrefs.ToArray();
        }

        public static string Subscribe(string url)
        {
            string html = getSource(url + "/about");
            string pars = html.Split(new string[] { "about-metadata-stats branded-page-box-padding" }, StringSplitOptions.None)[1].Split(new string[] { "about-stat" }, StringSplitOptions.None)[2].Split(new string[] { "<b>" }, StringSplitOptions.None)[1].Split(new string[] { "</b>" }, StringSplitOptions.None)[0];

            return pars;
        }

        public static string Pageview(string url)
        {
            string html = getSource(url + "/about");
            string pars = html.Split(new string[] { "about-metadata-stats branded-page-box-padding" }, StringSplitOptions.None)[1].Split(new string[] { "about-stat" }, StringSplitOptions.None)[3].Split(new string[] { "<b>" }, StringSplitOptions.None)[1].Split(new string[] { "</b>" }, StringSplitOptions.None)[0];

            return pars;
        }
    }
}
