using MQTTnet.Core;
using MQTTnet.Core.Client;
using MQTTnet.Core.Packets;
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
        readonly MqttClientOptions _options;
        bool _autoConnect;

        //CancellationTokenSource _reconnectTimerToken = null;

        public event EventHandler<MqttClientMessageReceivedEventArgs> ClientMessageReceived;
        public event EventHandler ClientDisconnected;

        private List<MqttTopic> _subscribedTopics = new List<MqttTopic>();
        public List<MqttTopic> SubscribedTopics => _subscribedTopics; 
        public bool AutoConnect { get => _autoConnect; set => _autoConnect = value; }

        public MqttClient(IMqttClient client,MqttClientOptions options,bool autoConnect = true) { 
        
            _client = client;
            _options = options;
            _autoConnect = autoConnect;
            _client.Disconnected += Client_Disconnected;
            _client.ApplicationMessageReceived += Client_ApplicationMessageReceived;
        }

        private void Client_Disconnected(object? sender, EventArgs e)
        {

            if (ClientDisconnected != null)
            {
                ClientDisconnected.Invoke(this, EventArgs.Empty);
            }

            //if (_autoConnect)
            //{
            //    Reconnect();
            //}
            //else
            //{
            //    if(ClientDisconnected != null)
            //    {
            //        ClientDisconnected.Invoke(this, EventArgs.Empty);
            //    }
            //}
            //throw new NotImplementedException();
        }

        private void Client_ApplicationMessageReceived(object? sender, MqttApplicationMessageReceivedEventArgs e)
        {
            //throw new NotImplementedException();
            var msg = new MqttMessage(e.ApplicationMessage.Topic, e.ApplicationMessage.Payload,(int)e.ApplicationMessage.QualityOfServiceLevel);
            if(ClientMessageReceived != null)
            {
                ClientMessageReceived.Invoke(this,new MqttClientMessageReceivedEventArgs(msg));
            }
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
                _client.ApplicationMessageReceived -= Client_ApplicationMessageReceived;
                _client.Disconnected -= Client_Disconnected;


                if (this._client.IsConnected)
                {
                    _client.DisconnectAsync();
                    
                }
            }
        }

        public async Task<IEnumerable<MqttTopic>> SubscribeAsync(string topic,int qos)
        {
            var results = await _client.SubscribeAsync(new TopicFilter[] { new TopicFilter(topic, (MqttQualityOfServiceLevel)qos) });

            var okresults = results.Where(r => r.ReturnCode != MqttSubscribeReturnCode.Failure).Select(r=>r.TopicFilter);

            var oktopics = okresults.Select(c => new MqttTopic(c.Topic, (int)c.QualityOfServiceLevel));
            _subscribedTopics.AddRange(oktopics);
            _subscribedTopics.DistinctBy(c => c.Topic);
            return oktopics;

        }

        public async Task PublishMessageAsync(string topic,int qos,string message,bool retain)
        {
            if (!_client.IsConnected)
            {
                return;
            }
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
            var appMsg = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(message), _qos, retain);

            await _client.PublishAsync(appMsg);
        }

        public void PublishMessageSync(string topic, int qos, string message, bool retain)
        {
            if (!_client.IsConnected)
            {
                return;
            }
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
            var appMsg = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(message), _qos, retain);

            _client.PublishAsync(appMsg).Wait();
        }


        /*
        private async Task Reconnect()
        {
            if(_reconnectTimerToken != null)
            {
                return;
            }



            _reconnectTimerToken = new CancellationTokenSource();

            var period = 5000;
            var timeoutcount = 0;
            using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(period));
            try
            {
                while(await timer.WaitForNextTickAsync(_reconnectTimerToken.Token))
                {

                    if (_client.IsConnected)
                    {
                        _reconnectTimerToken.Dispose();
                        _reconnectTimerToken = null;
                        timeoutcount = 0;
                        break;
                    }
                    else
                    {
                        timeoutcount++;
                        if(timeoutcount > 5)
                        {
                            _reconnectTimerToken.Dispose();
                            _reconnectTimerToken = null;
                            break;
                        }

                        await _client.ConnectAsync(_options);
                    }


                }

                if(timeoutcount > 0)
                {
                    if (ClientDisconnected != null)
                    {
                        ClientDisconnected.Invoke(this, EventArgs.Empty);
                    }
                }



            }catch (Exception ex)
            {
                _reconnectTimerToken.Dispose();
                _reconnectTimerToken = null;
            }



        }
        */

    }
}
