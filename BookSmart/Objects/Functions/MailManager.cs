using System;
using System.Net;
using System.Net.Mail;

namespace BookSmart.Objects.Functions {
    public class MailManager {

        public static String SystemEmail = "booksmart.cheerkoot@gmail.com";

        public static SmtpClient getEmailClient() {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(SystemEmail, "byuw cjjv ahhk nuvc");
            smtp.EnableSsl = true;
            return smtp;
        }

        public static MailMessage WriteEmail(string receiverEmail, string subject, string body) {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(SystemEmail, "BookSmart");
            message.To.Add(receiverEmail);
            message.Subject = subject;
            message.Body = body;
            return message;
        }

        public static bool IsValidEmail(string email) {
            try {
                return new MailAddress(email).Address == email;
            } catch {
                return false;
            }
        }
    }
}