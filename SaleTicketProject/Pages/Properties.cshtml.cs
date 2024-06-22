using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Repository;

namespace SaleTicketProject.Pages
{
    public class PropertiesModel : PageModel
    {
        private readonly IEventCategory _eventRepository;
        private readonly IAccountRepository _accountRepository;

        public List<Event> Events = new List<Event>();
        public PropertiesModel(IEventCategory eventRepository, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _eventRepository = eventRepository;
        }

        [BindProperty]
        public Account Account { get; set; }
        public void OnGet(int ID)
        {
            Account = _accountRepository.GetById(ID)!;
            Events = _eventRepository.GetAll().ToList();
        }

        [BindProperty]
        public int Id { get; set; }

        public IActionResult OnPostProperty()
        {
            return RedirectToPage("Property-details", new { Id = Id , accountId = Account.Id });
        }
        public IActionResult OnPostHome()
        {
            Account = Account;
            return RedirectToPage("Index",new { ID = Account!.Id });
        }

        public IActionResult OnPostProperties()
        {
            Account = Account;
            return RedirectToPage(new { ID = Account!.Id });
        }
    }
}

