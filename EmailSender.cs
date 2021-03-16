using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class EmailSender : MonoBehaviour
{
    [SerializeField]
    private string email_ToSendTo = "email_receiver@a.com", email_Sender= "your_email_sender@gmail.com", password_email = "password";

    [HideInInspector]
    public string emailSubject;

    [HideInInspector]
    public string contentEmail;

    //Use this to add new line
    public void AddNewLine(string line)
    {
        contentEmail += line + " \n";
    }

    public void SendEmail(string RespAluno)
    {
        try
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(email_Sender);

            //If you want to send to loads of emails, pass a list to them here
            mail.To.Add(email_ToSendTo);

            mail.Subject = emailSubject;
            mail.Body = contentEmail;

            //This takes account that your sender is from gmail, incase you want other change server here
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            
            smtpServer.Credentials = new System.Net.NetworkCredential(email_Sender, password_email) as ICredentialsByHost;
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
            smtpServer.Send(mail);
            
            Debug.Log("Email Sent!");
        }
        catch
        {
            Debug.Log("Email Failed to send!");
        }
    }
}
