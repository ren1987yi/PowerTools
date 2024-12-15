using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotServerHost
{
    public class GodotSession:WsSession
    {
        readonly Action<byte[], long, long> _actionRecive;
        public GodotSession(WsServer server, Action<byte[],long,long> actionRecive) : base(server)
        {
            _actionRecive = actionRecive;
        }

        public override void OnWsConnected(HttpRequest request)
        {


            Console.WriteLine($"Chat WebSocket session with Id {Id} connected!");

            // Send invite message
            string message = "Hello from WebSocket chat! Please send a message or '!' to disconnect the client!";
            SendTextAsync(message);
        }

        public override void OnWsDisconnected()
        {
            Console.WriteLine($"Chat WebSocket session with Id {Id} disconnected!");
        }

        public override void OnWsReceived(byte[] buffer, long offset, long size)
        {
            //string message = System.Text.Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            //Console.WriteLine("Incoming: " + message);
            //Log.Info("Incoming: " + message);

            if (_actionRecive != null) { 
                _actionRecive.Invoke(buffer,offset, size);
            
            }
            // Multicast message to all connected sessions
            //((WsServer)Server).MulticastText(message);

            // If the buffer starts with '!' the disconnect the current session
            //if (message == "!")
            //    Close(0);
        }
    }
}
