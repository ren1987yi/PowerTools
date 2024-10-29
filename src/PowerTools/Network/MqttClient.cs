using MQTTnet.Core;
using MQTTnet.Core.Client;
using MQTTnet.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class MqttClient : IDisposable
    {
        readonly IMqttClient _client;
        public MqttClient(IMqttClient client) { 
        
            _client = client;
        }


        public void Close()
        {
            _client.DisconnectAsync().Wait();
        }

        public async Task CloseAsync()
        {
            await _client.DisconnectAsync();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            if(_client != null)
            {
                if (this._client.IsConnected)
                {
                    _client.DisconnectAsync();
                    
                }
            }
        }

        public async Task PublishMessageAsync(string topic,int qos,string message,bool retain)
        {
            var _qos = MqttQualityOfServiceLevel.AtMostOnce;
            switch (qos)
            {
                case 0:
                    _qos = MqttQualityOfServiceLevel.AtMostOnce;
                    break;
                case 1:
                    _qos = MqttQualityOfServiceLevel.AtLeastOnce;
                    break;
                case 2:
                    _qos = MqttQualityOfServiceLevel.ExactlyOnce;
                    break;
                default:
                    throw new ArgumentException();
                    break;
            }
            var appMsg = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(message), _qos, false);

            await _client.PublishAsync(appMsg);
        }

        public void PublishMessageSync(string topic, int qos, string message, bool retain)
        {
            var _qos = MqttQualityOfServiceLevel.AtMostOnce;
            switch (qos)
            {
                case 0:
                    _qos = MqttQualityOfServiceLevel.AtMostOnce;
                    break;
                case 1:
                    _qos = MqttQualityOfServiceLevel.AtLeastOnce;
                    break;
                case 2:
                    _qos = MqttQualityOfServiceLevel.ExactlyOnce;
                    break;
                default:
                    throw new ArgumentException();
                    break;
            }
            var appMsg = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(message), _qos, false);

            _client.PublishAsync(appMsg).Wait();
        }






    }
}
