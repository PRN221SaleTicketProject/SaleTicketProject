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
        private readonly IUnitOfWork _unitOfWork;

        public List<Account> Accounts = new List<Account>();
        public List<Account> Accounts2 = new List<Account>();
        public IndexModel(ILogger<IndexModel> logger, IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            Accounts = _unitOfWork.AccountDAO.GetAll().ToList();
            Accounts2 = _accountRepository.GetAllName();
        }
    }
}
