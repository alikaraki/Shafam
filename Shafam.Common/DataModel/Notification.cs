using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shafam.Common.DataModel
{
    public class Notification
    {
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
