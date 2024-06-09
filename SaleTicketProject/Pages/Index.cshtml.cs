using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;

namespace SaleTicketProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAccountRepository _accountRepository;

        public IndexModel(ILogger<IndexModel> logger, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public void OnGet()
        {
            var test = _accountRepository.GetAll();
        }
    }
}
