using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentShared.Contract
{
    public enum EventPaymentEnum
    {
        [Description("Pending")]
        Pending,
        [Description("Paid")]
        Paid,
        [Description("Canceled")]
        Canceled
    }
}
