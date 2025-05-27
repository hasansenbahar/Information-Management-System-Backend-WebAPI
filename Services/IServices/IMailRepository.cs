using AutoMapper.Internal;
using MailKit.Net.Smtp;
using MailKit.Security;
using WebService.API.Helpers;

namespace WebService.API.Services.IServices
{
    public interface IMailRepository
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}