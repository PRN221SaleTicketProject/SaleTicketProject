using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;

namespace SaleTicketProject.Pages
{
    public class PropertiesModel : PageModel
    {
        private readonly IEventCategory _eventRepository;

        public List<Event> Events = new List<Event>();
        public PropertiesModel(IEventCategory eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void OnGet()
        {
            Events = _eventRepository.GetAll().ToList();
        }
    }
}
