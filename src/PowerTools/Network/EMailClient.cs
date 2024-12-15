using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Network
{
    public class EMailClient
    {
        readonly EMailSetting _setting;

        readonly SmtpClient _client;

        public EMailClient(EMailSetting setting) { 
            _setting = setting;
            _client = new SmtpClient();

            _client.Timeout = _setting.Smtp.Timeout;
        }


        public void SendMessage(string[] tolist, string subject,string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("", _setting.FromAddress));

            foreach(var to in tolist)
            {

                message.To.Add(new MailboxAddress("", to));
            }

            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };
            _client.Connect(_setting.Smtp.Server, _setting.Smtp.Port, false);
            
            // Note: only needed if the SMTP server requires authentication
            _client.Authenticate(_setting.Smtp.Username, _setting.Smtp.Password);
            
            _client.Send(message);
            _client.Disconnect(true);
          
        }

    }
}
