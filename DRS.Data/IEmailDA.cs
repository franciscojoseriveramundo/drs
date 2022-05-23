using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DRS.Entities;

namespace DRS.Data
{
    public interface IEmailDA
    {
        EmailLayout GetEmailById(int EmailId);
        List<EmailReceipts> GetReceipts(int receipts);
        EmailLog CreateEmailLog(EmailLog emailLog);
        EmailLog UpdateStatusEmailLog(EmailLog emailLog);
        bool CreateDestinatary(EmailReceipts emailReceipts, int isPrincipal, long EmailLog);
    }
}
