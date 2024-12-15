using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class MqttClientMessageReceivedEventArgs:EventArgs
    {
        public MqttMessage Message { get; private set; }
        public MqttClientMessageReceivedEventArgs(MqttMessage message)
        {
            Message = message;
        }
    }
}
