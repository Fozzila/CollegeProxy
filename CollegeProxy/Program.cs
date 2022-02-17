using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace CollegeProxy
{
    class Program
    {
        private static async Task OnResponseCaptureTrafficEventHandler(object sender, SessionEventArgs e) => await Task.Run(async () => {

  
        });

        private static async Task OnRequestCaptureTrafficEventHandler(object sender, SessionEventArgs e) => await Task.Run(async () =>
        {
            if (e.HttpClient.Request.Url.ToString().Contains("https://apc-api-production.collegeboard.org/fym/graphql"))
            {
                string payload = Encoding.UTF8.GetString(e.GetRequestBody().Result);
                payload = CollegeBoard.ReplaceComplete(payload);
                payload = CollegeBoard.ReplaceWatchTimes(payload);
                payload = CollegeBoard.ReplaceZeros(payload);
                e.SetRequestBodyString(payload);
            }
        });

        static void Main(string[] args)
        {
            ProxyManager proxyManager = new ProxyManager(OnRequestCaptureTrafficEventHandler, OnResponseCaptureTrafficEventHandler);

            Console.ReadLine();
            proxyManager.Stop();
        }
    }
}
