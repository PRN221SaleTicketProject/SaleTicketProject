using BusinessObjects;
using DataAccessLayers.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Repository;

namespace SaleTicketProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IEventCategory _eventRepository;

        [BindProperty]
        public Account Account { get; set; }
        public List<Event> Events = new List<Event>();
        public IndexModel(ILogger<IndexModel> logger, IAccountRepository accountRepository, IEventCategory eventRepository)
        {
            _accountRepository = accountRepository;
            _logger = logger;
            _eventRepository = eventRepository;
        }

        public void OnGet(int ID)
        {
            Account = _accountRepository.GetById(ID)!;
            Events = _eventRepository.GetAllInclude().ToList();
        }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public int AccountId { get; set; }

        public IActionResult OnPostProperty()
        {
            Account = _accountRepository.GetById(AccountId) ?? throw new Exception();
            return RedirectToPage("Property-details",new { Id = Id, accountId = Account.Id});
        }
        public IActionResult OnPostHome()
        {
            return RedirectToPage("Index",new {ID = Account.Id});
        }
        public IActionResult OnPostProperties()
        {
            return RedirectToPage("Properties", new { ID = Account.Id });
        }
        public IActionResult OnPostContact()
        {
            return RedirectToPage("Contact", new { ID = Account.Id });
        }
        public IActionResult OnPostProfile()
        {

            if (Account == null)
            {
                return NotFound();
            }
            return RedirectToPage("ViewProfile", new { accountId = Account.Id });
        }
    }
}
