using System;
using System.Collections.Generic;
using System.Text;

namespace RimuCloud.Persistence.DependencyInjection.Configurations
{
    public class DbConfig
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public string? DbName { get; set; }
        public int TimeoutSeconds { get; set; } = 180;
    }
}
