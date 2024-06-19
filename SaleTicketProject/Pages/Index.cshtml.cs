using BusinessObjects;
using DataAccessLayers.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;

namespace SaleTicketProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IEventCategory _eventRepository;

        public Account Account = new Account();
        public List<Account> Accounts2 = new List<Account>();
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
            Events = _eventRepository.GetAll().ToList();
        }
    }
}
