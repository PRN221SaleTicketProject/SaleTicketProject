using DataAccessLayers.UnitOfWork;
using Repositories.IRepository;
using Repositories.Repository;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFEventOperation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventCategory _eventCategory;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITicketRepository _ticketRepository;

        public MainWindow(IAccountRepository accountRepository, IEventCategory eventCategory, ICategoryRepository categoryRepository, ITicketRepository ticketRepository)
        {
            InitializeComponent();
            _accountRepository = accountRepository;
            _eventCategory = eventCategory;
            _categoryRepository = categoryRepository;
            _ticketRepository = ticketRepository;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text == "")
            {
                MessageBox.Show("ban phai nhap email");
                tbEmail.Focus();
                return;
            }
            if (tbPassword.Password == "")
            {
                MessageBox.Show("Ban phai nhap Mat khau");
                tbPassword.Focus();
                return;
            }

            var admin = _accountRepository.GetSystemAccountByEmailAndPassword(tbEmail.Text, tbPassword.Password);
            if (admin == null)
            {
                MessageBox.Show("Email or Pass invalid...");
                tbEmail.Focus();
                return;
            }
            if (admin.RoleId == 1 || admin.RoleId == 2)
            {
                MessageBox.Show("your account are not allowed");
                tbEmail.Focus();
                return;
            }

            else
            {
                Window1 page1 = new Window1(_accountRepository,_eventCategory , _categoryRepository, _ticketRepository);
                page1.Show();
                MessageBox.Show("OKE ADMIN");
                this.Close();
            }
        }
    }
}