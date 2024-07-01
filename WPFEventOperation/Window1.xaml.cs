using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.IdentityModel.Tokens;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessObjects;

namespace WPFEventOperation
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventCategory _eventCategory;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITicketRepository _ticketRepository;
        private bool _isCreateMode;

        public Window1(IAccountRepository accountRepository, IEventCategory eventCategory, ICategoryRepository categoryRepository, ITicketRepository ticketRepository)
        {
            InitializeComponent();
            _accountRepository = accountRepository;
            _eventCategory = eventCategory;
            _categoryRepository = categoryRepository;
            LoadData();
            _ticketRepository = ticketRepository;
        }

        private void LoadData()
        {
            this.dgData.ItemsSource = null;
            var accounts = _eventCategory.GetAll().Select(a => new { a.Name, a.CategoryId, a.TicketQuantity, a.Location, a.DateStart,a.DateEnd,a.Image });
            dgData.ItemsSource = accounts;
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnCcreateButton(object sender, RoutedEventArgs e)
        {
            if (_isCreateMode)
            {
                btnCreate.Visibility = Visibility.Collapsed;
                btnCreate.IsEnabled = false;
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                _isCreateMode = false;
            }
            else
            {
                btnCreate.Visibility = Visibility.Visible;
                btnCreate.IsEnabled = true;
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
                _isCreateMode = true;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txt.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtCategpryType.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtTicketQuantity.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtLocation.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtDateStart.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtDateEnd.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtImage.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (!txtImage.Text.StartsWith("https://"))
            {
                MessageBox.Show("must have link image");
                return;
            }

            var checkExist = _categoryRepository.GetById(int.Parse(txtCategpryType.Text));
            if (checkExist == null)
            {
                MessageBox.Show("Type Not Exist!");
                return;
            }

            if (DateTime.Parse(txtDateStart.Text) < DateTime.Now || DateTime.Parse(txtDateEnd.Text) < DateTime.Now)
            {
                MessageBox.Show("Event must be in future!");
                return;
            }
            if(DateTime.Parse(txtDateStart.Text) > DateTime.Parse(txtDateEnd.Text))
            {
                MessageBox.Show("Date End invalid");
                return;
            }
            var id = _eventCategory.GetAll().Count();
            Event newEvent = new Event()
            {
                Id = id + 1,
                Name=txt.Text,
                CategoryId=int.Parse(txtCategpryType.Text),
                TicketQuantity = int.Parse(txtTicketQuantity.Text),
                Location=txtLocation.Text,
                DateStart= DateOnly.Parse(txtDateStart.Text),
                DateEnd = DateOnly.Parse(txtDateEnd.Text),
                Image = txtImage.Text,
                Status = 1,
            };

            var ticketId = _ticketRepository.GetAll().Count();
            Ticket ticket = new Ticket()
            {
                Id = ticketId + 1,
                EventId = id + 1,
                Price = 20000,
                Quantity = int.Parse(txtTicketQuantity.Text),
                Status = 1,
            };
            if (MessageBoxResult.Yes == MessageBox.Show("Do you want create ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                _eventCategory.Add(newEvent);
                _ticketRepository.Add(ticket);
                MessageBox.Show("create completed!", "Exit", MessageBoxButton.OK);
                LoadData();
            }
            else
            {
                MessageBox.Show($"create failed!");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            return;
        }
    }
}
