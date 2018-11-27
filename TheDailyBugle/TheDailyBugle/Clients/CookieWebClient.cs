using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TheDailyBugle.Clients
{
    public class CookieWebClient : WebClient
    {

        public CookieContainer m_container = new CookieContainer();
        public WebProxy proxy = null;

        public CookieWebClient()
        {
            this.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";
            this.Headers[HttpRequestHeader.Referer] = "https://www.gocomics.com/comics/a-to-z";

            this.Headers[HttpRequestHeader.Accept] = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            this.Headers[HttpRequestHeader.AcceptLanguage] = "en-GB,en-US;q=0.8,en;q=0.6";
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            try
            {
                ServicePointManager.DefaultConnectionLimit = 1000000;
                WebRequest request = base.GetWebRequest(address);
                request.Proxy = proxy;

                HttpWebRequest webRequest = request as HttpWebRequest;
                webRequest.Pipelined = true;
                webRequest.KeepAlive = true;
                if (webRequest != null)
                {
                    webRequest.CookieContainer = m_container;
                }

                return request;
            }
            catch
            {
                return null;
            }
        }
    }
}
