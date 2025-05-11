using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentEvent.Domain.Enums
{
    public enum EventCategoryEnum
    {
        [Description("Tribune")]
        Tribune,
        [Description("Festival")]
        Festival,
        [Description("VIP")]
        VIP,
        [Description("VVIP")]
        VVIP,
    }
}
