

using MQTTnet;
using MQTTnet.Core;
using MQTTnet.Core.Client;
using MQTTnet.Core.Packets;
using MQTTnet.Core.Protocol;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerTools_Test
{
    internal static class mqtt2
    {
        public static async Task T1(string[] args)
        {
            string broker = "2a5accc12feb4870b886423fe758fc7d.s1.eu.hivemq.cloud";
            //            int port = 8883;
            //            string topic = "Csharp/mqtt";
            //            string clientId = Guid.NewGuid().ToString();
            //            string username = "renyi";
            //            string password = "123";
            //            MqttClient client = ConnectMQTT(broker, port, clientId, username, password);


            var mqttClient = new MqttClientFactory().CreateMqttClient();

            var mqttClientOptions = new MqttClientTcpOptions()
            {
                Server = broker,
                ClientId= Guid.NewGuid().ToString(),
            UserName = "renyi",
                Password = "123",
                CleanSession = true,
                DefaultCommunicationTimeout = TimeSpan.FromSeconds(5),
                TlsOptions = new MqttClientTlsOptions() { UseTls =  true}   
                ,KeepAlivePeriod = TimeSpan.FromSeconds(60)
                ,ProtocolVersion = MQTTnet.Core.Serializer.MqttProtocolVersion.V311

                

            };
            mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
            mqttClientOptions.Port = 8883;


                await mqttClient.ConnectAsync(mqttClientOptions);

            var appMsg = new MqttApplicationMessage("Csharp/mqtt", Encoding.UTF8.GetBytes("消息内容" + DateTime.Now.ToString()), MqttQualityOfServiceLevel.AtLeastOnce, false);

            await mqttClient.PublishAsync(appMsg);
                Console.WriteLine("MQTT application message is published.");
            await mqttClient.SubscribeAsync(new TopicFilter[] { new TopicFilter("mqtt2", MqttQualityOfServiceLevel.ExactlyOnce)  });
            Console.WriteLine("MQTT application message is subscribed.");
            Console.WriteLine("press the key will be exit");
            Console.ReadLine();


            await mqttClient.DisconnectAsync();

            
        }

        private static void MqttClient_ApplicationMessageReceived(object? sender, MqttApplicationMessageReceivedEventArgs e)
        {
            //throw new NotImplementedException();
            var topic = e.ApplicationMessage.Topic;
            var body = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            Console.WriteLine($"{topic}: {body}");
        }

        public static async Task T2()
        {
            string broker = "2a5accc12feb4870b886423fe758fc7d.s1.eu.hivemq.cloud";
            await MqttHelper.PublishMessage(broker, 8883, Guid.NewGuid().ToString(), "renyi", "123", "Csharp/mqtt", 1, "消息内容22222:" + DateTime.Now.ToString(), 60000);
        }
    }
}
