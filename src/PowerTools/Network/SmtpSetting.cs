﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class SmtpSetting
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int Timeout { get; set; }
        public bool EnableSSL { get; set; }
    }
}
