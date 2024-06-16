using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SaleTicketProject.Pages
{
    public class Property_detailsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public  Property_detailsModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
