using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;

namespace SaleTicketProject.Pages
{
    public class Property_detailsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventCategory _eventRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISolvedTicketRepository _solvedTicketRepository;

        public Property_detailsModel(ILogger<IndexModel> logger, ITicketRepository ticketRepository, IEventCategory eventRepository, IAccountRepository accountRepository, ICategoryRepository categoryRepository, ISolvedTicketRepository solvedTicketRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _solvedTicketRepository = solvedTicketRepository;
        }
        [BindProperty]
        public Event Event { get; set; }
        [BindProperty]
        public List<Ticket> Ticket { get; set; }
        [BindProperty]
        public Account Account { get; set; }
        [BindProperty]
        public Category Category { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        public IActionResult OnPostPropertyDetails()
        {
            _solvedTicketRepository.PurchaseTickets(Ticket, Account, Quantity);
            //return RedirectToPage(new {Id = Event.Id, accountId = Account.Id});
            return Page();
        }
        public void OnGet(int Id, int accountId)
        {
            Account = _accountRepository.GetById((int)accountId!);
            Event = _eventRepository.GetById(Id);
            Ticket = _ticketRepository.GetByEventId(Id);
            Category = _categoryRepository.GetById((int)Event.CategoryId!);


        }

    }
}
