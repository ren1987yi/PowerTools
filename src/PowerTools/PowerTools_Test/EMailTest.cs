using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Network;
namespace PowerTools_Test
{
    internal static class EMailTest
    {
        public static void SendMail()
        {


            Network.EMailHelperPowerUp.SendStringMessage(
                "ren1987yi@163.com"
                , "ren1987yi@163.com"
                , "test1"
                , "test" + DateTime.Now.ToString()
                , "smtp.163.com"
                , 25
                , "ren1987yi"
                , "DESMPZh2qiNjSQ5G"
                , 60000

                );


            Network.EMailHelperPowerUp.SendStringMessage(
               "ren1987yi222@163.com"
               , "ren1987yi@163.com"
               , "test1"
               , "test" + DateTime.Now.ToString()
               , "smtp.163.com"
               , 25
               , "ren1987yi"
               , "DESMPZh2qiNjSQ5G"
               , 60000

               );
        }

        public static void SendMail4()
        {


            Network.EMailHelperPowerUp.SendStringMessage(
                "13916872674@139.com"
                , "13916872674@139.com"
                , "test1"
                , "test" + DateTime.Now.ToString()
                , "smtp.139.com"
                , 25
                , "13916872674@139.com"
                , "f6683d45644e02aa8300"
                , 6000

                );
        }

        public static void RecvEmail()
        {
            Network.EMailHelperPowerUp.ReciveMessage("pop.163.com"
                , 110
                , "ren1987yi"
                , "YNQJe38gaeUT6eVM");

        }


        public static void SendMail2()
        {

            

            var client = new SmtpClient("smtp.163.com", 25)
            {
                Credentials = new NetworkCredential("ren1987yi@163.com", "DESMPZh2qiNjSQ5G"),
                UseDefaultCredentials = false,
                Timeout = 60000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = false,


            };


            var msg = new MailMessage("ren1987yi@163.com", "ren1987yi@163.com")
            {
                Subject = "test1",
                Body = "mail2 test" + DateTime.Now.ToString(),

            };
            client.Send(msg);


        }

        public static void SendMail3()
        {

            //f6683d45644e02aa8300

            var client = new SmtpClient("smtp.139.com", 25)
            {
                Credentials = new NetworkCredential("13916872674@139.com", "f6683d45644e02aa8300"),
                UseDefaultCredentials = false,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = false,


            };


            var msg = new MailMessage("13916872674@139.com", "13916872674@139.com")
            {
                Subject = "test1",
                Body = "test" + DateTime.Now.ToString(),

            };
            client.Send(msg);

            //fshbksqujddibghj
        }

        public static void SendMail7()
        {

            //f6683d45644e02aa8300

            var client = new SmtpClient("smtp.qq.com", 465)
            {
                //Credentials = new NetworkCredential("442737665", "fshbksqujddibghj"),
                Credentials = new NetworkCredential("442737665", "Cwx20060901"),
                UseDefaultCredentials = false,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,


            };


            var msg = new MailMessage("442737665@qq.com", "442737665@qq.com")
            {
                Subject = "test1",
                Body = "test" + DateTime.Now.ToString(),

            };
            client.Send(msg);

            //fshbksqujddibghj
        }
    }
}
