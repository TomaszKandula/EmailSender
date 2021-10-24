﻿namespace EmailSenderService.Backend.SmtpService.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SmtpServer
    {
        public string Account { get; set; }
        
        public string Password { get; set; }
        
        public int Port { get; set; }
        
        public string Server { get; set; }

        public bool IsSSL { get; set; }
    }
}