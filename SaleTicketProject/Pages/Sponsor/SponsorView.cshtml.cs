using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;

namespace SaleTicketProject.Pages.Sponsor
{
    public class SponsorViewModel : PageModel
    {
        private readonly IEventCategory _eventRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ISolvedTicketRepository _solvedTicketRepository;

        public List<Event> Events = new List<Event>();
        public SponsorViewModel(IEventCategory eventRepository, IAccountRepository accountRepository, ITicketRepository ticketRepository, ISolvedTicketRepository solvedTicketRepository)
        {
            _accountRepository = accountRepository;
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _solvedTicketRepository = solvedTicketRepository;
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
        [BindProperty]
        public int AccountId { get; set; }

        public IActionResult OnPostProperty()
        {
            Account = _accountRepository.GetById(AccountId) ?? throw new Exception();
            return RedirectToPage("Property-details", new { Id = Id, accountId = Account.Id });
        }
        public IActionResult OnPostHome()
        {
            Account = Account;
            return RedirectToPage("Index", new { ID = Account!.Id });
        }

        public IActionResult OnPostProperties()
        {
            Account = Account;
            return RedirectToPage(new { ID = Account!.Id });
        }

        public IActionResult OnPostSponsor()
        {
            var newEvent = _eventRepository.GetById(Id) ?? throw new Exception();
            Account = _accountRepository.GetById(AccountId) ?? throw new Exception();
            newEvent.SponsorId = Account.Id;
            _eventRepository.Update(newEvent);
            return RedirectToPage(new { ID = Account.Id });
        }

        public IActionResult OnPostNotSponsor()
        {
            var newEvent = _eventRepository.GetById(Id) ?? throw new Exception();
            Account = _accountRepository.GetById(AccountId) ?? throw new Exception();
            var ticket = _ticketRepository.GetByEventId(newEvent.Id);
            Boolean checkSolvedTicket = false;
            foreach (var checkticket  in ticket)
            {
                checkSolvedTicket = _solvedTicketRepository.CheckSolvedTicket(checkticket.Id);
            }
            if (checkSolvedTicket is false)
            {
                newEvent.SponsorId = null;
                _eventRepository.Update(newEvent);
                return RedirectToPage(new { ID = Account.Id });
            }
            else
            {
                TempData["Message"] = "This event's tickets have been bought, cannot take back!";
                return RedirectToPage(new { ID = Account.Id });
            }    
 
        }
    }
}
