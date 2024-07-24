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
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            _accountRepository = accountRepository;
            _eventCategory = eventCategory;
            _categoryRepository = categoryRepository;
            LoadData();
            bindcombo();
            _ticketRepository = ticketRepository;
        }

        private void LoadData()
        {
            this.dgData.ItemsSource = null;
            var accounts = _eventCategory.GetAllIncludeType().Where(a => a.Status == 1).Select(a => new {a.Id, a.Name, a.CategoryId,a.Category!.Type, a.TicketQuantity, a.Location, a.DateStart,a.DateEnd,a.Image });
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
                LoadData();
            }
            else
            {
                btnCreate.Visibility = Visibility.Visible;
                btnCreate.IsEnabled = true;
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
                _isCreateMode = true;
                dgData.ItemsSource = null;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txt.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txt.Text.Length > 255)
            {
                MessageBox.Show("equal or over 255? too long!");
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
            if (txtLocation.Text.Length > 188)
            {
                MessageBox.Show("your filling is not like a location or country the longest city name which is: Krung Thep Mahanakhon Amon Rattanakosin Mahinthara Yuthaya Mahadilok Phop Noppharat Ratchathani Burirom Udomratchaniwet Mahasathan Amon Piman Awatan Sathit Sakkathattiya Witsanukam Prasit which is BANGKOK!");
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
            if(txtImage.Text.Length > 255)
            {
                MessageBox.Show("link too long!");
                return;
            }
            var checkTime = @"^([1-9]|1[0-2])/([1-9]|[12][0-9]|3[01])/((19|20)\d\d)$";
            if (!Regex.IsMatch(txtDateStart.Text, checkTime) || !Regex.IsMatch(txtDateEnd.Text, checkTime))
            {
                MessageBox.Show("you must fill the format MM/DD/YYYY with the real time!");
                return;
            }
            if (!int.TryParse(txtTicketQuantity.Text, out int quantity))
            {
                MessageBox.Show("must number!");
                return;
            }
            if(int.Parse(txtTicketQuantity.Text) <= 0)
            {
                MessageBox.Show("must not smaller or equal 0!");
                return;
            }
            if (int.Parse(txtTicketQuantity.Text) > 9000000)
            {
                MessageBox.Show("quantity ticket cannot over 9 million!");
                return;
            }
            var checkExist = _categoryRepository.getByCateName(txtCategpryType.Text);
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
                CategoryId=checkExist.Id,
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
            var exist = _eventCategory.GetAll().FirstOrDefault(x => x.Name == newEvent.Name);
            if (exist == null)
            {
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
            else
            {
                MessageBox.Show($"Event existed!");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
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
            var checkTime = @"^([1-9]|1[0-2])/([1-9]|[12][0-9]|3[01])/((19|20)\d\d)$";
            if (!Regex.IsMatch(txtDateStart.Text, checkTime) || !Regex.IsMatch(txtDateEnd.Text, checkTime))
            {
                MessageBox.Show("you must fill the format MM/DD/YYYY with the real time!");
                return;
            }
            if (!int.TryParse(txtTicketQuantity.Text, out int quantity))
            {
                MessageBox.Show("must number!");
                return;
            }
            if (int.Parse(txtTicketQuantity.Text) <= 0)
            {
                MessageBox.Show("must not smaller or equal 0!");
                return;
            }
            var checkExist = _categoryRepository.getByCateName(txtCategpryType.Text); 
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
            if (DateTime.Parse(txtDateStart.Text) > DateTime.Parse(txtDateEnd.Text))
            {
                MessageBox.Show("Date End invalid");
                return;
            }
            if (txt.Text.IsNullOrEmpty())
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
            if (MessageBoxResult.Yes == MessageBox.Show("Do you want update ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                var UpdateEvent = _eventCategory.GetById(int.Parse(txtId.Text));
                if (UpdateEvent == null)
                {
                    MessageBox.Show($"Event Not Found!");
                    return;
                }
                var UpdateTicket = _ticketRepository.GetByEventId(UpdateEvent.Id);
                if (UpdateTicket == null)
                {
                    MessageBox.Show($"Ticket Not Found!");
                    return;
                }
                UpdateEvent.Name = txt.Text;
                UpdateEvent.CategoryId = checkExist.Id;
                UpdateEvent.TicketQuantity = int.Parse(txtTicketQuantity.Text);
                UpdateEvent.Location = txtLocation.Text;
                _eventCategory.Update(UpdateEvent);

                foreach (var newiUpdateTicket in UpdateTicket)
                {
                    newiUpdateTicket.Quantity = int.Parse(txtTicketQuantity.Text);
                    _ticketRepository.UpdateNewTicket(newiUpdateTicket);
                }
                MessageBox.Show("update completed!", "Exit", MessageBoxButton.OK);
                LoadData();
            }
            else
            {
                MessageBox.Show($"update failed!");
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var UpdateEvent = _eventCategory.GetById(int.Parse(txtId.Text));
            if (UpdateEvent == null)
            {
                MessageBox.Show($"Event Not Found!");
                return;
            }
            if (UpdateEvent.SponsorId != null)
            {
                MessageBox.Show("Can't delete due to having Sponsor");
                return;
            }
            if (MessageBoxResult.Yes == MessageBox.Show("Do you want delete ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                UpdateEvent.Status = 0;
                _eventCategory.Update(UpdateEvent);
                MessageBox.Show("delete completed!", "Exit", MessageBoxButton.OK);
                LoadData();
            }
            else
            {
                MessageBox.Show($"delete failed!");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            return;
        }

        public List<Category> cates { get; set; }
        private void bindcombo()
        {
            Prn221projectContext dc = new Prn221projectContext();
            var cate = dc.Categories.ToList();
            cates = cate;
            DataContext = cates;
        }

        private void cbCategoryType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCategoryType.SelectedItem is Category cate)
            {
                txtCategpryType.Text = cate.Type; // Assuming txtCategoryType is a TextBox or similar control
            }
        }
    }
}
