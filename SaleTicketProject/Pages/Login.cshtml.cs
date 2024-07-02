using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;

namespace SaleTicketProject.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountRepository _systemAccountService;
        public LoginModel(IAccountRepository systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }
        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string password { get; set; }

        public string message = string.Empty;
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData["Message"] = "You must enter an Email";
                return Page();
            }

            if (string.IsNullOrEmpty(password))
            {
                TempData["Message"] = "You must enter a Password";
                return Page();
            }

            var memberStaff = _systemAccountService.GetSystemAccountByEmailAndPassword(email, password);
            if (memberStaff == null)
            {
                TempData["Message"] = "Email or Password invalid...";
                return Page();
            }
            if(memberStaff.Status == 0)
            {
                TempData["Message"] = "This account has been banned!";
                return Page();
            }
            if(memberStaff.RoleId == 2)
            {
                return RedirectToPage("SponsorView", new { ID = memberStaff.Id });
            }
            else
            {
                return RedirectToPage("Index", new { ID = memberStaff.Id });
            }
        }
    }
}
