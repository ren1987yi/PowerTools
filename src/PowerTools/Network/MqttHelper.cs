using MQTTnet;
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
    public static class MqttHelper
    {

        public static async Task<MqttClient> ConnectAsync(string broker, int port, string clientid, string user, string password,int timeout,int keepalive)
        {
                var mqttClient = new MqttClientFactory().CreateMqttClient();
            try
            {


                var mqttClientOptions = new MqttClientTcpOptions()
                {
                    Server = broker,
                    ClientId = clientid,
                    UserName = user,
                    Password = password,
                    CleanSession = true,
                    DefaultCommunicationTimeout = TimeSpan.FromSeconds(timeout),
                    TlsOptions = new MqttClientTlsOptions() { UseTls = true }
                    ,
                    KeepAlivePeriod = TimeSpan.FromSeconds(keepalive)
                    ,
                    ProtocolVersion = MQTTnet.Core.Serializer.MqttProtocolVersion.V311
                };

                mqttClientOptions.Port = port;


                await mqttClient.ConnectAsync(mqttClientOptions);



                return new MqttClient(mqttClient);
            }catch 
            {
                mqttClient = null;
                return null;
            
            }



        }



        public static async Task PublishMessage(string broker,int port,string clientid, string user,string password,string topic,int qos,string message,int timeout )
        {
            //string broker = "2a5accc12feb4870b886423fe758fc7d.s1.eu.hivemq.cloud";

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

            var mqttClient = new MqttClientFactory().CreateMqttClient();

            var mqttClientOptions = new MqttClientTcpOptions()
            {
                Server = broker,
                ClientId = clientid,
                UserName = user,
                Password = password,
                CleanSession = true,
                DefaultCommunicationTimeout = TimeSpan.FromMilliseconds(timeout),
                TlsOptions = new MqttClientTlsOptions() { UseTls = true }
                ,
                KeepAlivePeriod = TimeSpan.FromSeconds(60)
                ,
                ProtocolVersion = MQTTnet.Core.Serializer.MqttProtocolVersion.V311



            };

            mqttClientOptions.Port = port;


            await mqttClient.ConnectAsync(mqttClientOptions);


          


            var appMsg = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(message),_qos, false);

            await mqttClient.PublishAsync(appMsg);

            await mqttClient.DisconnectAsync();

            //Console.WriteLine("MQTT application message is published.");
        }
    }
}
