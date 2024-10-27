//using System.Net.Mail;
//using System.Net;

//namespace PowerTools_Test
//{
//    internal class Program11
//    {
//        static void Main2(string[] args)
//        {
            
//            Console.WriteLine("Hello, World!");

//            //         var client = new SmtpClient("smtp.163.com", 25)
//            //         {
//            //             Credentials = new NetworkCredential("ren1987yi", "DESMPZh2qiNjSQ5G"),
//            //UseDefaultCredentials = false,
//            //             Timeout = 5000,
//            //             DeliveryMethod = SmtpDeliveryMethod.Network,
//            //             EnableSsl =false,


//            //         };


//            //         var msg = new MailMessage("ren1987yi@163.com", "ren1987yi@163.com")
//            //         {
//            //             Subject = "test1",
//            //             Body = "test" + DateTime.Now.ToString(),

//            //         };
//            //         client.Send(msg);





//            //Network.EMailPowerUp.SendStringMessage(
//            //    "ren1987yi@163.com"
//            //    , "ren1987yi@163.com"
//            //    , "test1"
//            //    , "test" + DateTime.Now.ToString()
//            //    , "smtp.163.com"
//            //    , 25
//            //    , "ren1987yi"
//            //    , "DESMPZh2qiNjSQ5G"
//            //    , 5000

//            //    );

//            //Network.EMailHelperPowerUp.ReciveMessage("pop.163.com"
//            //    , 110
//            //    , "ren1987yi"
//            //    , "DESMPZh2qiNjSQ5G");


//            var success = Network.HttpRequestHelper.GetString("https://www.baidu.com", out var c);
//            Console.WriteLine(success.ToString() + " " + c);

//        }
//    }
//}
