using System.Net.Mail;
using System.Net;
using Network;

namespace PowerTools_Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            //Console.WriteLine("Hello, World!");


            Console.WriteLine("Start Test the Email");

            EMailTest.SendMail();

            EMailTest.RecvEmail();

            Console.WriteLine("End Test the Email");



            //await mqtt2.T1(null);
            //Console.WriteLine(1);


            //await mqtt2.T2();
            //Console.WriteLine(2);



            string broker = "2a5accc12feb4870b886423fe758fc7d.s1.eu.hivemq.cloud";
            //await MqttHelper.PublishMessage(broker, 8883, Guid.NewGuid().ToString(), "renyi", "123", "Csharp/mqtt", 1, "消息内容22222:" + DateTime.Now.ToString(), 5000);
            var client = await MqttHelper.ConnectAsync(broker, 8883, Guid.NewGuid().ToString(), "renyi", "123",5000,60000);

            for(var i = 0; i < 100; i++)
            {

                var v = DateTime.Now.Second;
                client.PublishMessageSync("Csharp/mqtt", 1, "消息内容22222:" + DateTime.Now.ToString() + "  " + i.ToString(), true);
            }


            

            Console.WriteLine(3);
            //         var client = new SmtpClient("smtp.163.com", 25)
            //         {
            //             Credentials = new NetworkCredential("ren1987yi", "DESMPZh2qiNjSQ5G"),
            //UseDefaultCredentials = false,
            //             Timeout = 5000,
            //             DeliveryMethod = SmtpDeliveryMethod.Network,
            //             EnableSsl =false,


            //         };


            //         var msg = new MailMessage("ren1987yi@163.com", "ren1987yi@163.com")
            //         {
            //             Subject = "test1",
            //             Body = "test" + DateTime.Now.ToString(),

            //         };
            //         client.Send(msg);





            //Network.EMailPowerUp.SendStringMessage(
            //    "ren1987yi@163.com"
            //    , "ren1987yi@163.com"
            //    , "test1"
            //    , "test" + DateTime.Now.ToString()
            //    , "smtp.163.com"
            //    , 25
            //    , "ren1987yi"
            //    , "DESMPZh2qiNjSQ5G"
            //    , 5000

            //    );

            //Network.EMailHelperPowerUp.ReciveMessage("pop.163.com"
            //    , 110
            //    , "ren1987yi"
            //    , "DESMPZh2qiNjSQ5G");


            //var success = Network.HttpRequestHelper.GetString("https://www.baidu.com", out var c);
            //Console.WriteLine(success.ToString() + " " + c);

        }
    }
}
