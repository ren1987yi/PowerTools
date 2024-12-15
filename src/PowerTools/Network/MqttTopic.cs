using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class MqttTopic
    {
        private string topic;
        private int qos;


        public string Topic { get { return topic; } set => topic = value; }
        public int QoS { get { return qos; } set => qos = value; }

        public MqttTopic(string topic, int qos)
        {
            this.topic = topic;
            this.qos = qos;
        }
    }
}
