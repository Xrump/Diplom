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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Praktice.Presentation.ViewModels.PupilWindowViewModel;

namespace Praktice.Presentation
{
    /// <summary>
    /// Логика взаимодействия для PupilWindow.xaml
    /// </summary>
    public partial class PupilWindow : Window
    {
        private readonly PupilWindowViewModel _pupilWindowViewModel;

        public PupilWindow(Account account)
        {
            InitializeComponent();
            _pupilWindowViewModel = (PupilWindowViewModel)DataContext;


            using (var context=new ApplicationDbContext())
            {
                _pupilWindowViewModel.LoginnedPupil = context.Pupils
                    .Include(p => p.ClassNavigation)
                    .ThenInclude(c => c.CuratorNavigation)
                    .FirstOrDefault(p => p.Account == account.Id);

                _pupilWindowViewModel.FillPropertys();
                _pupilWindowViewModel.FillAnnoucements();

                _pupilWindowViewModel.FillDataGrid(1,ref AlgebraMarksDataGrid,_pupilWindowViewModel.AlgebraMarks);
                _pupilWindowViewModel.FillDataGrid(2,ref RussianLanguageMarksDataGrid, _pupilWindowViewModel.RussianLanguageMarks);
                _pupilWindowViewModel.FillDataGrid(3,ref LiteratureMarksDataGrid, _pupilWindowViewModel.LiteratureMarks);
                _pupilWindowViewModel.FillDataGrid(4,ref InformaticMarksDataGrid, _pupilWindowViewModel.InformaticMarks);
                _pupilWindowViewModel.FillDataGrid(5,ref PhysicsMarksDataGrid, _pupilWindowViewModel.PhysicsMarks);
                _pupilWindowViewModel.FillDataGrid(6,ref ChemistryMarksDataGrid, _pupilWindowViewModel.ChemistryMarks);
                _pupilWindowViewModel.FillDataGrid(7,ref BiologyMarksDataGrid, _pupilWindowViewModel.BiologyMarks);
                _pupilWindowViewModel.FillDataGrid(8,ref PhysicalCultureMarksDataGrid, _pupilWindowViewModel.PhysicalCultureMarks);

                _pupilWindowViewModel.CheckClub(ref MyClubTabItem);
            }
        }

        private void JoinClubButton_Click(object sender, RoutedEventArgs e)
        {
            _pupilWindowViewModel.JoinClub();
            _pupilWindowViewModel.CheckClub(ref MyClubTabItem);
        }

        private void LeaveClubButton_Click(object sender, RoutedEventArgs e)
        {
            _pupilWindowViewModel.LeaveClub();
            _pupilWindowViewModel.CheckClub(ref MyClubTabItem);
            ClubsTabControl.SelectedIndex = 0;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
