using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentPayment.Domain
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
