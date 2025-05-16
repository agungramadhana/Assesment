using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentShared.Contract
{
    public class RabbitMqSettingModel
    {
        public string Host { get; set; } = string.Empty;
        public string VirtualHost { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
