using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class EmailLog
    {
        public long EmailLogId { get; set; }
        public int EmailLayoutId { get; set; }
        public string EmailHtml { get; set; }
        public DateTime EmailCreateDate { get; set; }
        public int EmailStatus { get; set; }
        public string EmailSubject { get; set; }
        public DateTime? EmailSendDate{ get; set; }
        public long UserId { get; set; }
    }
}
