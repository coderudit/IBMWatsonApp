using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IBMWatsonApp.Shared
{
    public class EmailService
    {
        public void SendMail(LabTest labTest)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(WatsonConstants.Useremail);
                mail.To.Add(labTest.Useremail);
                mail.Subject = "Lab test booking";
                mail.Body = "Hi, Your appointment for " + labTest.Testtype + " test on " + labTest.Testdate + " at " + labTest.Testtime + " has been booked." ;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(WatsonConstants.Useremail, WatsonConstants.Userpassword);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
