﻿//using System;
//using uPLibrary.Networking.M2Mqtt;
//using uPLibrary.Networking.M2Mqtt.Messages;

//namespace PowerTools_Test
//{
//    internal class Program22
//    {
//        static MqttClient ConnectMQTT(string broker, int port, string clientId, string username, string password)
//        {
//            MqttClient client = new MqttClient(broker, port, true, MqttSslProtocols.None, null, null);

//            client.Connect(clientId, username, password);
//            if (client.IsConnected)
//            {
//                Console.WriteLine("Connected to MQTT Broker");
//            }
//            else
//            {
//                Console.WriteLine("Failed to connect");
//            }
//            return client;
//        }

//        static void Publish(MqttClient client, string topic)
//        {
//            int msg_count = 0;
//            while (true)
//            {
//                System.Threading.Thread.Sleep(1 * 1000);
//                string msg = "messages: " + msg_count.ToString();
//                client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(msg));
//                Console.WriteLine("Send `{0}` to topic `{1}`", msg, topic);
//                msg_count++;
//            }
//        }

//        static void Subscribe(MqttClient client, string topic)
//        {
//            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
//            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
//        }
//        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
//        {
//            string payload = System.Text.Encoding.Default.GetString(e.Message);
//            Console.WriteLine("Received `{0}` from `{1}` topic", payload, e.Topic.ToString());
//        }

//        static void Main22(string[] args)
//        {
//            string broker = "2a5accc12feb4870b886423fe758fc7d.s1.eu.hivemq.cloud";
//            int port = 8883;
//            string topic = "Csharp/mqtt";
//            string clientId = Guid.NewGuid().ToString();
//            string username = "renyi";
//            string password = "123";
//            MqttClient client = ConnectMQTT(broker, port, clientId, username, password);
//            //Subscribe(client, topic);
//            Publish(client, topic);
//        }
//    }
//}