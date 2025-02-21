﻿namespace BinanceSential.Auth.Core.Interfaces.IEmailServices;

public interface IEmailSender
{
  Task SendEmailAsync(string to, string from, string subject, string body);
}
