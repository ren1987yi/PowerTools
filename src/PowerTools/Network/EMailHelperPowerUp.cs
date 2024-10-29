using System;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Net.Pop3;


namespace Network
{
    public static class EMailHelperPowerUp
    {
        public static void SendStringMessage(
          string from
          , string to
          , string subject
          , string body
          , string server
          , int port
          , string user
          , string password
          , int timeout = 5000
          )
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("", from));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Timeout = timeout;
                client.Connect(server, port, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(user,password);

                client.Send(message);
                client.Disconnect(true);
            }

        }

        public static void SendStringMessage(
             string from
             , string to
             , string subject
             , string body
             , SmtpSetting setting
         )
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("", from));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Timeout = setting.Timeout;
                client.Connect(setting.Server, setting.Port, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(setting.Username, setting.Password);

                client.Send(message);
                client.Disconnect(true);
            }

        }


        public static void ReciveMessage(string server,int port,string user,string password)
        {
            using (var client = new Pop3Client())
            {
                client.Connect(server, port, false);

                client.Authenticate(user, password);

               

                for (int i = 0; i < client.Count; i++)
                {
                    var message = client.GetMessage(i);
                    Console.WriteLine("Subject: {0}", message.Subject);
                }

                client.Disconnect(true);
            }
        }
    }
}
