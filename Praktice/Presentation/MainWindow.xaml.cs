using Microsoft.EntityFrameworkCore;
using Praktice.Domain.Entities;
using Praktice.Infrastructure.Persistence;
using Praktice.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Praktice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _mainWindowViewModel;


        public MainWindow()
        {
            InitializeComponent();
            _mainWindowViewModel = (MainWindowViewModel)DataContext;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ApplicationDbContext())
            {
                List<Account> accountsList = context.Accounts
                    .Include(a => a.Pupils)
                    .ThenInclude(p => p.ClassNavigation)
                    .Include(a => a.Teachers)
                    .Include(a => a.Parents)
                    .Include(a=>a.Administrations)
                    .ToList();

                Account account = new Account();
                account.Login = LoginTextBox.Text;
                account.Password = PasswordPBox.Password;

                foreach (var acc in accountsList)
                {
                    if (acc.Login == account.Login && acc.Password == account.Password)
                        if (_mainWindowViewModel.Authorization(acc) == true)
                            this.Close();
                }
            }
        }
    }
}
