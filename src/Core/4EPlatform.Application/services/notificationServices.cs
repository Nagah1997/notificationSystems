using ControlPanel.Notifications.Protos;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using Grpc.Core;
using MailKit;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4EPlatform.Application.services
{
    public class notificationServices: notifiactionService.notifiactionServiceBase
    {
     
        private readonly IConfiguration _config;

        public notificationServices(IConfiguration config)
        {
          
            _config = config;
        }
        public override  async Task<ResponseConfirmationMessage> sendConfirmation(RequestConfirmationMessage request, ServerCallContext context)
        {
            var email = new MimeMessage();
     

            email.From.Add(
                new MailboxAddress(
                    "مركز اللغة المثاليه",
                    _config["EmailSettings:From"]));

            email.To.Add(new MailboxAddress("", request.Email));

            email.Subject = request.Subject;

            email.Body = new TextPart("html")
            {
                Text = request.Body
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            await smtp.ConnectAsync(
                _config["EmailSettings:SmtpServer"],
                int.Parse(_config["EmailSettings:Port"]!),
                true,
                context.CancellationToken);

            await smtp.AuthenticateAsync(
                _config["EmailSettings:Username"],
                _config["EmailSettings:Password"],
                context.CancellationToken);

            await smtp.SendAsync(email, context.CancellationToken);

            await smtp.DisconnectAsync(true, context.CancellationToken);

            return new ResponseConfirmationMessage
            {
                ConfirmationMessage = "Email sent successfully."
            };
        }
    }
    }
    

