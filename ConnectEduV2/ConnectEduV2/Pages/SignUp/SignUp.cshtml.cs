﻿using ConnectEduV2.Models;
using ConnectEduV2.Repositories;
using FluentValidation;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;
using System.ComponentModel.DataAnnotations;

namespace ConnectEduV2.Pages.SignUp
{
    public class SignUpModel : PageModel
    {
        private readonly ISchoolRepositoriy _schoolRepositoriy;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _validator;
        private readonly IWalletRepository _walletRepository;

        public SignUpModel(ISchoolRepositoriy schoolRepositoriy, IDepartmentRepository departmentRepository, IUserRepository userRepository, IValidator<User> validator, IWalletRepository walletRepository)
        {
            _schoolRepositoriy = schoolRepositoriy;
            _departmentRepository = departmentRepository;
            _userRepository = userRepository;
            _validator = validator;
            _walletRepository = walletRepository;
        }

        [BindProperty]
        [Required(ErrorMessage = "Please Enter Your Name")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please Enter Gmail!")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please Enter Your Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least 6 characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please Enter Your Phone Number")]
        [RegularExpression(@"^((\+84)|0)[3-9]\d{8}$", ErrorMessage = "Please enter a valid Vietnamese phone number")]
        public string Phone { get; set; }

        [BindProperty]
        public IFormFile AvatarForm { get; set; }
        public string Avatar { get; set; }

        [BindProperty]
        public int SchoolId { get; set; }

        [BindProperty]
        public int DepartmentId { get; set; }

        public List<Department> Departments { get; set; }
        public List<School> School { get; set; }

        public IActionResult OnGet()
        {
            Departments = (List<Department>)_departmentRepository.GetAll();
            School = _schoolRepositoriy.GetAll().ToList();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (AvatarForm != null && AvatarForm.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + AvatarForm.FileName;
                var filePath = Path.Combine("wwwroot/img/uploads", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    AvatarForm.CopyTo(stream);
                }

                Avatar = "/img/uploads/" + uniqueFileName;
            }
            else
            {
                Avatar = "/img/avatarmain.jpg";
            }

            if (ModelState.IsValid)
            {
                var checkEmail = _userRepository.GetSingleByCondition(us => us.Email.Equals(Email));
                if (checkEmail != null && checkEmail.StatusId == 2)
                {
                    int number = GenerateRandom5DigitNumber();
                    string emailBody = "Your single-use code is: " + number.ToString();
                    HttpContext.Session.SetInt32("RandomNumber", number);
                    HttpContext.Session.SetString("Email", checkEmail.Email.ToString());

                    SendEmail(Email, "Vui lòng xác thực email của bạn", emailBody);

                    TempData["mod"] = 1;
                    return RedirectToPage("/SignUp/VerifyRegister", "OnGet");
                }
                else
                {
                    if (checkEmail != null && checkEmail.StatusId == 1)
                    {
                        ViewData["ErrorEmail"] = "Email has been exist!";
                        Departments = (List<Department>)_departmentRepository.GetAll();
                        School = _schoolRepositoriy.GetAll().ToList();
                        return Page();
                    }
                    else
                    {
                        int number = GenerateRandom5DigitNumber();
                        string emailBody = "Your single-use code is: " + number.ToString();
                        HttpContext.Session.SetInt32("RandomNumber", number);

                        User user = new User
                        {
                            Email = Email,
                            Name = Name,
                            StatusId = 2,
                            Image = Avatar,
                            Password = Password,
                            Phone = Phone,
                            DepartmentId = DepartmentId,
                            SchoolId = SchoolId
                        };
                        _userRepository.Add(user);
                        _userRepository.SaveChanges();
                        HttpContext.Session.SetString("Email", user.Email.ToString());

                        SendEmail(Email, "Vui lòng xác thực email của bạn", emailBody);

                        TempData["mod"] = 1;
                        return RedirectToPage("/SignUp/VerifyRegister", "OnGet");
                    }
                }
            }
            else
            {
                Departments = (List<Department>)_departmentRepository.GetAll();
                School = _schoolRepositoriy.GetAll().ToList();
                return Page();
            }
        }

        public IActionResult OnGetSend()
        {
            string mail = HttpContext.Session.GetString("Email");
            int number = GenerateRandom5DigitNumber();
            string emailBody = "Your single-use code is: " + number.ToString();
            HttpContext.Session.SetInt32("RandomNumber", number);

            SendEmail(mail, "Vui lòng xác thực email của bạn", emailBody);

            TempData["mod"] = 1;
            return RedirectToPage("/SignUp/VerifyRegister", "OnGet");
        }

        private int GenerateRandom5DigitNumber()
        {
            Random rand = new Random();
            int min = 10000;
            int max = 99999;
            return rand.Next(min, max);
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("dinhd513@gmail.com"));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("dinhd513@gmail.com", "npgwpsgmsyqncsci");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
