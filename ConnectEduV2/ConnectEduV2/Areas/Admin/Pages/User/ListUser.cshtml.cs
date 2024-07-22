using ConnectEduV2.Models;
using ConnectEduV2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using X.PagedList;

namespace ConnectEduV2.Areas.Admin.Pages.User
{
    public class ListUserModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public ListUserModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string? SearchKeyword { get; set; }
        public IPagedList<ConnectEduV2.Models.User> Users { get; set; }

        public int TotalUsers { get; set; }

        public void OnGet(int pageNumber = 1, int pageSize = 10, string searchKeyword = "")
        {
            var includes = new string[] { "Status" };
            var users = _userRepository.GetMulti(
                          users => string.IsNullOrEmpty(searchKeyword)
                          || users.Name.Contains(searchKeyword)
                          || users.Email.Contains(searchKeyword)
                          || users.Phone.Contains(searchKeyword),
                         includes: includes).OrderByDescending(x=>x.Id).ToPagedList(pageNumber, pageSize);
            SearchKeyword = searchKeyword; // Lưu từ khóa tìm kiếm vào ViewBag
            Users = users;
            TotalUsers = _userRepository.getTotalUsers();
        }
    }
}
