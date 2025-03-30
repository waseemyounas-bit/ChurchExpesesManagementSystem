using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Services;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace Church_App.Pages.ManagementSystem
{
    public class SendMailModel : PageModel
    {
        [BindProperty]
        public EmailDto Email { get; set; }

        [BindProperty]
        public List<string> SelectedRecipients { get; set; }

        public bool MessageSent { get; set; } = false;

        public List<UserDto> AllRecipients { get; set; }= new List<UserDto>();  

        private readonly EmailSettings _emailSettings;

        private readonly IMemberService _memberService;
        public SendMailModel(IOptions<EmailSettings> emailOptions, IMemberService memberService)
        {
            _emailSettings = emailOptions.Value;
            _memberService = memberService;
        }
        public void OnGet()
        {
            LoadRecipients();
            Email = new EmailDto();
        }

        public IActionResult OnPost()
        {
            LoadRecipients();

            if (SelectedRecipients == null || !SelectedRecipients.Any())
            {
                ModelState.AddModelError("SelectedRecipients", "Please select at least one recipient.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                foreach (var email in SelectedRecipients)
                {
                    SendEmail(email, Email.Subject, Email.Body);
                }

                MessageSent = true;
                ModelState.Clear();
                Email = new EmailDto();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error sending email: {ex.Message}");
            }

            return Page();
        }

        private void SendEmail(string to, string subject, string body)
        {
            var smtpClient = new SmtpClient(_emailSettings.Host)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = _emailSettings.EnableSsl,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(to);
            smtpClient.Send(mailMessage);
        }


        private void LoadRecipients()
        {
            AllRecipients = new List<UserDto>();
            var results= _memberService.GetAllMembers();
            foreach (var item in results)
            {
                AllRecipients.Add(new UserDto { Name = item.FName + " " + item.LName, Email = item.Email });
            }
        
        }

      
       
    }
}
