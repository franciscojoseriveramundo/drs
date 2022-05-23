using DRS.Data;
using DRS.Entities;
using DRS.PostgreSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.PostgreSQL
{
    public class EmailDA : IEmailDA
    {
        public bool CreateDestinatary(EmailReceipts emailReceipts, int isPrincipal, long EmailLog)
        {
            using (var drs = new DRSContext())
            {
                Emaillogdestinatary destinatary = new Emaillogdestinatary()
                {
                    Emaillogdestinataryemail = emailReceipts.EmailAddress,
                    Emaillogdestinatarytype = isPrincipal,
                    Emaillogid = EmailLog
                };

                drs.Add(destinatary);

                drs.SaveChanges();

                return true;
            }
        }

        //Crear el Log del Email
        public EmailLog CreateEmailLog(EmailLog emailLog)
        {
            using(var drs = new DRSContext())
            {
                Models.Emaillog emailLogI = new Models.Emaillog()
                {
                    Emaillayoutid = emailLog.EmailLayoutId,
                    Emaillogcreatedate = emailLog.EmailCreateDate,
                    Emailloghtml = emailLog.EmailHtml,
                    Emaillogsenddate = emailLog.EmailSendDate,
                    Emaillogstatusid = emailLog.EmailStatus,
                    Usersid = emailLog.UserId,
                    Emaillogsubject = emailLog.EmailSubject
                };

                drs.Add(emailLogI);

                drs.SaveChanges();

                emailLog.EmailLogId = drs.Emaillogs.Max(a => a.Emaillogid);

                return emailLog;
            }
        }

        //Metodo que obtiene el body del email a enviar
        public EmailLayout GetEmailById(int EmailId)
        {
            using (var drs = new DRSContext())
            {
                return (from a in drs.Emaillayouts
                        where a.Emaillayoutid == EmailId
                        select new EmailLayout
                        {
                            EmailId = a.Emaillayoutid,
                            EmailName = a.Emaillayoutname,
                            EmailBody = a.Emaillayoutbody
                        }).FirstOrDefault();
            }
        }

        public List<EmailReceipts> GetReceipts(int receipts)
        {
            using (var drs = new DRSContext())
            {
                return (from a in drs.Emaildestinatarydefaults
                        join b in drs.Emaildestinataryconfigurationgroups on a.Emaildestinatarydefaultid equals b.Emaildestinatarydefaultid
                        where b.Emaillayoutconfigurationgroupid == receipts
                        select new EmailReceipts
                        {
                            EmailAddress = a.Emaildestinatarydefaultemail
                        }).ToList();
            }
        }

        public EmailLog UpdateStatusEmailLog(EmailLog emailLog)
        {
            using (var drs = new DRSContext())
            {
                var updateEmailLog = drs.Emaillogs.Where(a => a.Emaillogid == emailLog.EmailLogId).FirstOrDefault();
                updateEmailLog.Emaillogstatusid = emailLog.EmailStatus;
                updateEmailLog.Emaillogsenddate = emailLog.EmailSendDate;

                drs.Update(updateEmailLog);

                drs.SaveChanges();

                return emailLog;
            }
        }
    }
}
