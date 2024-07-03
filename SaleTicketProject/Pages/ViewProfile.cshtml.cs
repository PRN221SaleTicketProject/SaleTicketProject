using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;

namespace SaleTicketProject.Pages
{
    public class ViewProfileModel : PageModel
    {
        private readonly IAccountRepository _account;

        public ViewProfileModel(IAccountRepository account)
        {
            _account = account;
        }
        [BindProperty]
        public Account? Account { get; set; } = null;
        [BindProperty]
        public int AccountId { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public IActionResult OnGet(int accountId)
        {
            Account = _account.GetById(accountId);
            if (Account == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPostUpdate()
        {
            var existingAccount = _account.GetById(AccountId);
            if (existingAccount == null)
            {
                return NotFound();
            }
            if(Password == null)
            {
                TempData["ErrorMessage"] = "Password is not null!";
                return RedirectToPage(new { accountId = existingAccount.Id });
            }
            existingAccount.Password = Password;
            _account.Update(existingAccount);
            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToPage(new { accountId = existingAccount.Id });
        }

        public IActionResult OnPostViewHistoryTransaction()
        {
            var existingAccount = _account.GetById(AccountId);
            if (existingAccount == null)
            {
                return NotFound();
            }
            return RedirectToPage("ViewTransactionHIstory", new { accountId = existingAccount.Id });
        }
    }
}
