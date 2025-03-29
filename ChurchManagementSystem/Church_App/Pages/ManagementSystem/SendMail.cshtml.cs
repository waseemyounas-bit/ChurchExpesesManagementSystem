using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public List<UserDto> AllRecipients { get; set; }

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
            var smtpClient = new SmtpClient("smtp.your-email.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("your-email@example.com", "your-email-password"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("your-email@example.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(to);
            smtpClient.Send(mailMessage);
        }

        private void LoadRecipients()
        {
            // Simulated data - replace with DB call to Members & Visitors table
            AllRecipients = new List<UserDto>
        {
            new UserDto { Name = "John Doe", Email = "john@example.com" },
            new UserDto { Name = "Mary Smith", Email = "mary@example.com" },
            new UserDto { Name = "Visitor Joe", Email = "joe.visitor@example.com" }
        };
        }

        public class EmailDto
        {
            [Required]
            public string Subject { get; set; }

            [Required]
            public string Body { get; set; }
        }

        public class UserDto
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }
}
