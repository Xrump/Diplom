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

namespace Praktice.Presentation
{
    /// <summary>
    /// Логика взаимодействия для TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        private readonly TeacherWindowViewModel _teacherWindowViewModel;

        public TeacherWindow(Account account)
        {
            InitializeComponent();
            _teacherWindowViewModel = (TeacherWindowViewModel)DataContext;


            using (var context = new ApplicationDbContext())
            {
                _teacherWindowViewModel.LoginnedTeacher = context.Teachers
                    .FirstOrDefault(t => t.Account == account.Id);

                _teacherWindowViewModel.FillPropertys();

                _teacherWindowViewModel.FillDataGrid(1, ref AlgebraMarksDataGrid, _teacherWindowViewModel.AlgebraMarks);
                _teacherWindowViewModel.FillDataGrid(2, ref RussianLanguageMarksDataGrid, _teacherWindowViewModel.RussianLanguageMarks);
                _teacherWindowViewModel.FillDataGrid(3, ref LiteratureMarksDataGrid, _teacherWindowViewModel.LiteratureMarks);
                _teacherWindowViewModel.FillDataGrid(4, ref InformaticMarksDataGrid, _teacherWindowViewModel.InformaticMarks);
                _teacherWindowViewModel.FillDataGrid(5, ref PhysicsMarksDataGrid, _teacherWindowViewModel.PhysicsMarks);
                _teacherWindowViewModel.FillDataGrid(6, ref ChemistryMarksDataGrid, _teacherWindowViewModel.ChemistryMarks);
                _teacherWindowViewModel.FillDataGrid(7, ref BiologyMarksDataGrid, _teacherWindowViewModel.BiologyMarks);
                _teacherWindowViewModel.FillDataGrid(8, ref PhysicalCultureMarksDataGrid, _teacherWindowViewModel.PhysicalCultureMarks);

                _teacherWindowViewModel.CheckClub(ref MyClubTabItem);
            }
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }
}
