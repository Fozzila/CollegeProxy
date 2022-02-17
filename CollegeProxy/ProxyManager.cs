using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.Models;
using Titanium.Web.Proxy.EventArguments;
namespace CollegeProxy
{
    public class ProxyManager
    {
        public int Port => 18882;
        public string Host => "http://localhost:"; 

        private static ProxyServer proxyServer;

        public ProxyManager(AsyncEventHandler<SessionEventArgs> requestTraffic, AsyncEventHandler<SessionEventArgs> responseTraffic)
        {
            proxyServer = new ProxyServer(true, true, false);
            var explicitEndPoint = new ExplicitProxyEndPoint(System.Net.IPAddress.Any, 18882, true);

            proxyServer.AddEndPoint(explicitEndPoint);
            proxyServer.Start();
            proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
            proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);
            proxyServer.BeforeRequest += requestTraffic;
            proxyServer.BeforeResponse += responseTraffic;
        }

        public void Stop()
        {
            proxyServer.Stop();
        }
    }
}
