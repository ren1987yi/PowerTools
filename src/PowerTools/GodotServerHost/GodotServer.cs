using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NetCoreServer;
using Newtonsoft.Json;

namespace GodotServerHost
{
    public class GodotServer: WsServer
    {
        //readonly Func<WsSession> initSession;
        readonly Action<byte[], long, long> _actionRecive;
        public GodotServer(IPAddress address, int port, Action<byte[], long, long> actionRecive ) : base(address, port)
        {
            _actionRecive = actionRecive;
        }

        protected override TcpSession CreateSession()
        {
            //return new GodotSession(this); 

            return new GodotSession(this, _actionRecive);


            //if (initSession != null)
            //{

            //    return initSession.Invoke();
            //}
            //else
            //{
            //    throw new NotImplementedException();
            //}

        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat WebSocket server caught an error with code {error}");
        }


        public static GodotServer Builder(string root, int port, string router, Action<byte[], long, long> actionRecive)
        {
            var server = new GodotServer(IPAddress.Any, port, actionRecive);

            server.AddStaticContent(root, "", "*.*", null, null);
            return server;
        }

        public void SendPackage(SendPackageType type, object data, string target = "*")
        {
            var d = new
            {
                Type = type,
                Target = target,
                Data = data
            };
            var j = JsonConvert.SerializeObject(d);
            this.MulticastText(j);
        }


        public void UpdateStatus(string text)
        {
            this.MulticastText(text);
        }
    }
}
