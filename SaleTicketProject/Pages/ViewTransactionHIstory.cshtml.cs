using BusinessObjects;
using BusinessObjects.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;

namespace SaleTicketProject.Pages
{
    public class ViewTransactionHIstoryModel : PageModel
    {
        private readonly IAccountRepository _account;
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;

        public ViewTransactionHIstoryModel(IAccountRepository account, ITransactionHistoryRepository transactionHistoryRepository)
        {
            _account = account;
            _transactionHistoryRepository = transactionHistoryRepository;
        }
        [BindProperty]
        public Account? Account { get; set; } = null;
        [BindProperty]
        public List<TransactionHistoryDto> TransactionHistory { get; set; }
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
            TransactionHistory = _transactionHistoryRepository.GetAllTransactionHistoryByAccountId(accountId);
            return Page();
        }
    }
}
