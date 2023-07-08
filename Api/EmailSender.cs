using System.Net.Mail;
using MailKit.Net.Smtp;
using MimeKit;
using Serilog;
using Serilog.Events;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace Api;

public static class EmailSender
{
    public static void SendEmail(string message, string mailTo)
    {
        var Mailmessage = new MimeMessage();
        Mailmessage.From.Add(new MailboxAddress("Моя компания",
            "a.galimz@mail.ru")); //отправитель сообщения поменять адрес
        Mailmessage.To.Add(new MailboxAddress("Моя компания", mailTo)); //адресат сообщения поменять адрес
        Mailmessage.Subject = "Сообщение от FinacialAnalysis"; //тема сообщения 
        Mailmessage.Body = new BodyBuilder() { HtmlBody = $"<div style=\"color: black;\">{message}</div>" }
            .ToMessageBody(); //тело сообщения (так же в формате HTML)

        using var client = new SmtpClient();
        client.Connect("smtp.mail.ru", 465, true);
        client.Authenticate("a.galimz@mail.ru", "QZMg0eAFK50yUw5QPFgM");
        client.Send(Mailmessage);
        client.Disconnect(true);
    }
}