using ConnectEduV2.Filters;
using ConnectEduV2.Models;
using ConnectEduV2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ConnectEduV2.Pages.Class
{
    public class ClassDetailModel : PageModel
    {
        private readonly IClassRepository _classRepository;
        private readonly IClassRegistrationRepository _classRegistrationRepository;
        private readonly IRegistrationStatusRepository _classRegistrationStatusRepository;
        private readonly IFeedbackRepository _feedbackRepository;

        public ClassDetailModel(IClassRepository classRepository, IClassRegistrationRepository classRegistrationRepository, IRegistrationStatusRepository classRegistrationStatusRepository, IFeedbackRepository feedbackRepository)
        {
            _classRepository = classRepository;
            _classRegistrationRepository = classRegistrationRepository;
            _classRegistrationStatusRepository = classRegistrationStatusRepository;
            _feedbackRepository = feedbackRepository;
        }

        public ConnectEduV2.Models.Class ClassDetail { get; set; }
        public List<ClassRegistration> classRegistrations { get; set; }
        public List<Feedback> feedbacks { get; set; }

        public IActionResult OnGet(int classID)
        {
            var includes = new string[] { "User", "Subject", "ClassStatus"};
            var includes2 = new string[] { "User", "RegistrationStatus" };
            var includes3 = new string[] { "User", "Registration", "Class" };
            var classdetail = _classRepository.GetSingleByCondition(c => c.Id == classID, includes);
            var regis = _classRegistrationRepository.GetMulti(o => o.ClassId==classID && o.RegistrationStatusId!= 3, includes2);
            feedbacks = _feedbackRepository.GetMulti(fb => fb.ClassId == classID, includes3).OrderByDescending(x=>x.Id).ToList();
            classRegistrations = regis.ToList();
            ClassDetail = classdetail;
            return Page();
        }

        public IActionResult OnGetEnroll(int? classID)
        {
            string? accJson = HttpContext.Session.GetString("User");

            if (accJson == null)
            {
                return RedirectToPage("/Login/Login");
            }
            else
            {
                User? acc = JsonConvert.DeserializeObject<User>(accJson);
                if (acc.RoleId == 2)
                {
                    var check = _classRegistrationRepository.GetMulti(check => check.UserId == acc.Id && check.ClassId == classID).OrderByDescending(c => c.Id).FirstOrDefault();
                    if (check == null)
                    {
                        ClassRegistration registration = new ClassRegistration();
                        registration.UserId = acc.Id;
                        registration.ClassId = classID;
                        registration.Date = DateTime.Now;
                        registration.RegistrationStatusId = 1;
                        registration.PaymentStatusId = 2;
                        _classRegistrationRepository.Add(registration);
                        _classRegistrationRepository.SaveChanges();
                    }
                    if (check != null && check.RegistrationStatusId == 3)
                    {
                        ClassRegistration registration = new ClassRegistration();
                        registration.UserId = acc.Id;
                        registration.ClassId = classID;
                        registration.Date = DateTime.Now;
                        registration.RegistrationStatusId = 1;
                        registration.PaymentStatusId = 2;
                        _classRegistrationRepository.Add(registration);
                        _classRegistrationRepository.SaveChanges();
                    }
                }
                return RedirectToPage("/Class/ClassRegistrationList");
            }

        }
    }
}
