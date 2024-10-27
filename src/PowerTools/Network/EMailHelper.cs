using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public static class EMailHelper
    {
        public static void SendStringMessage(
            string from
            ,string to
            ,string subject
            ,string body
            ,string server
            ,int port
            ,string user
            ,string password
            ,int timeout=5000
            )
        {
            var client = new SmtpClient(server, port)
            {
                Credentials = new NetworkCredential(user, password),
                UseDefaultCredentials = false,
                Timeout = timeout,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = false,
            };


            var msg = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body

            };
            client.Send(msg);
           
        }

        public static void SendStringMessage(
          string from
          , string to
          , string subject
          , string body
          ,SmtpSetting setting
          )
        {
            var client = new SmtpClient(setting.Server, setting.Port)
            {
                Credentials = new NetworkCredential(setting.Username, setting.Password),
                UseDefaultCredentials = false,
                Timeout = setting.Timeout,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = false,
            };


            var msg = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body

            };
            client.Send(msg);

        }
    }
}
