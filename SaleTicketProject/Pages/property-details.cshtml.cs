using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
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
        [BindProperty]
        public int Id { get; set; }
        public IActionResult OnPostPropertyDetails()
        {
            if(Quantity == null || Quantity == 0)
            {
                TempData["ErrorMessage"] = "Quantity cannot null or 0!";
                return RedirectToPage(new { Id = Event.Id, accountId = Account.Id });
            }
            Account = _accountRepository.GetById((int)Account.Id!);
            Event = _eventRepository.GetById(Event.Id);
            Ticket = _ticketRepository.GetByEventId(Event.Id);
            Category = _categoryRepository.GetById((int)Event.CategoryId!);

            foreach(var ticket in Ticket)
            {
                if (ticket.Quantity == 0)
                {
                    TempData["ErrorMessage"] = "Ticket Sold Out!";
                    return RedirectToPage(new { Id = Event.Id, accountId = Account.Id });
                }
                if (ticket.Quantity < Quantity)
                {
                    TempData["WarningMessage"] = "Not Enough Ticket for you to buy!";
                    return RedirectToPage(new { Id = Event.Id, accountId = Account.Id });
                }
            }

            double? totalQUantity = 0;
            foreach (var ticket in Ticket)
            {
                totalQUantity = (double?)(Quantity * ticket.Price);
            }
            if(Account!.Wallet < totalQUantity)
            {
                TempData["WarningMessage"] = "Your wallet is not enough to buy tickets!";
                return RedirectToPage(new { Id = Event.Id, accountId = Account.Id });
            }
            _solvedTicketRepository.PurchaseTickets(Ticket, Account, Quantity);
            TempData["SuccessMessage"] = "Buy tickets successfully!";
            return RedirectToPage(new { Id = Event.Id, accountId = Account.Id });
        }
        public void OnGet(int Id, int accountId)
        {
            Account = _accountRepository.GetById((int)accountId!);
            Event = _eventRepository.GetById(Id);
            Ticket = _ticketRepository.GetByEventId(Id);
            Category = _categoryRepository.GetById((int)Event.CategoryId!);
        }
        public IActionResult OnPostHome()
        {
            Account = _accountRepository.GetById((int)Account.Id!);
            return RedirectToPage("/Index",new { ID = Account.Id });
        }

        public IActionResult OnPostProperties()
        {
            Account = _accountRepository.GetById((int)Account.Id!);
            return RedirectToPage("/Properties", new { ID = Account!.Id });
        }

        public IActionResult OnPostContact()
        {
            return RedirectToPage("Contact", new { ID = Account.Id });
        }
    }
}
