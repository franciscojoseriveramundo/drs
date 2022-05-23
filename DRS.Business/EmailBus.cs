using DRS.Data;
using DRS.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Business
{
    public class EmailBus
    {
        IEmailDA iEmailDA;
        private IConfiguration iConfiguration;

        public EmailBus(IEmailDA iEmailDA)
        {
            this.iEmailDA = iEmailDA;
            this.iConfiguration = ConectionDB.Configuration;
        }

        //public Response RegisterEmail(int EmailLayoutId, string EmailLayoutBodyEmail, string EmailLayoutDestinatary, string EmailLayoutSubject)
        //{
        //    return iEmailDA.RegisterEmail(EmailLayoutId, EmailLayoutBodyEmail, EmailLayoutDestinatary, EmailLayoutSubject);
        //}

        public EmailLayout GetLayout(int EmailLayout = 0)
        {
            return iEmailDA.GetEmailById(EmailLayout);
        }

        public Response SendEmail(EmailLayout email, List<EmailReceipts> recivers, string subject, string recipients, List<Attachment> attachments)
        {
            Response response = new Response();

            NetworkCredential credential = new NetworkCredential();
            credential.UserName = iConfiguration["GmailHost:SmtpUsername"];
            credential.Password = iConfiguration["GmailHost:SmtpPassword"];
            //credential.Domain = "smtp.gmail.com";

            SmtpClient mailClient = new SmtpClient();
            mailClient.Host = iConfiguration["GmailHost:SmtpHost"];
            mailClient.Port = Convert.ToInt32(iConfiguration["GmailHost:SmtpPort"]);
            mailClient.UseDefaultCredentials = false;
            //mailClient.Credentials = new NetworkCredential(iConfiguration["GmailHost:SmtpUsername"], iConfiguration["GmailHost:SmtpPassword"]);
            mailClient.Credentials = credential;
            mailClient.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(iConfiguration["GmailHost:SmtpRecipent"]);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = email.EmailBody;
            mail.Priority = MailPriority.High;
            mail.To.Add(recipients);

            if (recivers.Count > 0)
            {
                foreach (var item in recivers)
                {
                    MailAddress copy = new MailAddress(item.EmailAddress);
                    mail.CC.Add(copy);
                }

            }

            foreach(var item in attachments)
            {
                mail.Attachments.Add(item);
            }

            try
            {
                mailClient.Send(mail);

                response.Codigo = 1;
                response.Mensaje = "El correo ha sido enviado correctamente.";

                return response;
            }
            catch (Exception ex)
            {
                response.Codigo = 0;
                response.Mensaje = "La devolución se creo correctamente, pero el correo de notificación no se pudo enviar. Descripción: " + ex.Message;
            }

            return response;
        }
    }
}
