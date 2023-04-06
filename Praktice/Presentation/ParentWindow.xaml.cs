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

namespace Praktice.Presentation
{
    /// <summary>
    /// Логика взаимодействия для ParentWindow.xaml
    /// </summary>
    public partial class ParentWindow : Window
    {
        private readonly ParentWindowViewModel _parentWindowViewModel;

        public ParentWindow(Account account)
        {
            InitializeComponent();
            _parentWindowViewModel = (ParentWindowViewModel)DataContext;


            using (var context = new ApplicationDbContext())
            {
                _parentWindowViewModel.LoginnedParent = context.Parents
                    .FirstOrDefault(p => p.Account == account.Id);

                _parentWindowViewModel.FillPropertys();


                _parentWindowViewModel.FillDataGrid(1, ref AlgebraMarksDataGrid, _parentWindowViewModel.AlgebraMarks);
                _parentWindowViewModel.FillDataGrid(2, ref RussianLanguageMarksDataGrid, _parentWindowViewModel.RussianLanguageMarks);
                _parentWindowViewModel.FillDataGrid(3, ref LiteratureMarksDataGrid, _parentWindowViewModel.LiteratureMarks);
                _parentWindowViewModel.FillDataGrid(4, ref InformaticMarksDataGrid, _parentWindowViewModel.InformaticMarks);
                _parentWindowViewModel.FillDataGrid(5, ref PhysicsMarksDataGrid, _parentWindowViewModel.PhysicsMarks);
                _parentWindowViewModel.FillDataGrid(6, ref ChemistryMarksDataGrid, _parentWindowViewModel.ChemistryMarks);
                _parentWindowViewModel.FillDataGrid(7, ref BiologyMarksDataGrid, _parentWindowViewModel.BiologyMarks);
                _parentWindowViewModel.FillDataGrid(8, ref PhysicalCultureMarksDataGrid, _parentWindowViewModel.PhysicalCultureMarks);

                _parentWindowViewModel.CheckClub(ref MyClubTabItem);
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
