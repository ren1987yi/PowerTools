using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class MqttMessage
    {
        private string topic;
        private int qos;
        private byte[] data;

        public string Topic { get { return topic; } set => topic = value; }
        public int QoS { get { return qos; } set => qos = value; }
        
        public MqttMessage(string topic,byte[] data, int qos)
        {
            this.topic = topic;
            this.qos = qos;
            this.data = new byte[data.Length];
            data.CopyTo(this.data, 0);
        }

        public string GetStringData()
        {
            if(data == null || data.Length == 0)
            {
                return string.Empty;
            }
            else
            {
                return Encoding.UTF8.GetString(data);
            }
        }

    }
}
