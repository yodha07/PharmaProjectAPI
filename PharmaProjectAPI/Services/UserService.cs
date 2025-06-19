using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;
using System.Net;
using System.Net.Mail;

namespace PharmaProjectAPI.Services
{
    public class UserService : IUserRepo
    {
        private readonly ApplicationDbContext db;

        private readonly IMapper mapper;

        private readonly MailSettings mail;

        public UserService(ApplicationDbContext db, IMapper mapper, MailSettings mail)
        {
            this.mapper = mapper;
            this.db = db;
            this.mail = mail;
        }

        public async Task Register(User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public bool UserExists(Register reg)
        {
            return db.Users.Any(u => u.Username == reg.Username || u.Email == reg.Email);
        }

        public async Task<string> Login(Login login)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Username == login.Username);

            if (user == null)
                return "User not found";

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(login.PasswordHash, user.PasswordHash);

            if (!isPasswordCorrect)
                return "Invalid password";

            return "Login successful";
        }

        public List<UsersDTO> GetUser()
        {
            var userList = db.Users.ToList();

            var data = mapper.Map<List<UsersDTO>>(userList);

            return data;
        }
        
        public void DeleteUserById(int userId)
        {
            var user = db.Users.Find(userId);
            if(user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient(mail.Host, mail.Port)
                {
                    Credentials = new NetworkCredential(mail.UserName, mail.Password),
                    EnableSsl = mail.UseSSL
                };

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(mail.EmailId, mail.DisplayName);
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}
