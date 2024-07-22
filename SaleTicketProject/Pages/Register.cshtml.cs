using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;

namespace SaleTicketProject.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountRepository _systemAccountService;
        public RegisterModel(IAccountRepository systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }
        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string name { get; set; }
        [BindProperty]
        public string address { get; set; }
        [BindProperty]
        public string phone { get; set; }
        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public string confirm { get; set; }

        public string message = string.Empty;
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData["Message"] = "You must enter an email";
                return Page();
            }

            if (string.IsNullOrEmpty(password))
            {
                TempData["Message"] = "You must enter a password";
                return Page();
            }
            if (string.IsNullOrEmpty(phone))
            {
                TempData["Message"] = "You must enter an phone";
                return Page();
            }

            if (string.IsNullOrEmpty(address))
            {
                TempData["Message"] = "You must enter a address";
                return Page();
            }
            if (string.IsNullOrEmpty(name))
            {
                TempData["Message"] = "You must enter an name";
                return Page();
            }

            if (string.IsNullOrEmpty(confirm))
            {
                TempData["Message"] = "You must enter a confirm";
                return Page();
            }

            if (confirm != password)
            {
                TempData["Message"] = "Confirm password is not correct";
                return Page();
            }
            var count = _systemAccountService.GetAll().Count();

            var newAccount = new Account()
            {
                Id = count + 1,
                Name = name,
                Address = address,
                Phone = phone,
                Email = email,
                Password = password,
                RoleId = 1,
                Status = 1,
                Wallet=0
            };

            var memberStaff = _systemAccountService.Add(newAccount);
            TempData["Message"] = "Register Successfuly!";
            return RedirectToPage("Login");

        }
    }
}
