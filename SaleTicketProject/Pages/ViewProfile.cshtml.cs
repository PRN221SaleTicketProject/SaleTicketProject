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
        public IActionResult OnGet(short accountId)
        {
            Account = _account.GetById(accountId);
            if (Account == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Account == null)
            {
                return NotFound();
            }

            var existingAccount = _account.GetById(Account.Id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            existingAccount.Name = Account.Name;
            existingAccount.Email = Account.Email;
            existingAccount.Password = Account.Password;

            _account.Update(existingAccount);

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToPage(new { accountId = Account.Id });
        }

        public IActionResult OnPostViewHistoryTransaction()
        {
            if (Account == null)
            {
                return NotFound();
            }
            return RedirectToPage("ViewHistoryTransaction", new { accountId = Account.Id });
        }
    }
}
