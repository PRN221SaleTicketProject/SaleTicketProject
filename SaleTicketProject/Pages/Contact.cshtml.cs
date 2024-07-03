using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;

namespace SaleTicketProject.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        [BindProperty]
        public Account Account { get; set; }
        [BindProperty]
        public int Id { get; set; }
        public ContactModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void OnGet(int ID)
        {
            Account = _accountRepository.GetById(ID)!;
        }

        public IActionResult OnPostHome()
        {
            return RedirectToPage("Index", new { ID = Account.Id });
        }
        public IActionResult OnPostProperties()
        {
            return RedirectToPage("Properties", new { ID = Account.Id });
        }
        public IActionResult OnPostContact()
        {
            return RedirectToPage("Contact", new { ID = Account.Id });
        }
    }
}
