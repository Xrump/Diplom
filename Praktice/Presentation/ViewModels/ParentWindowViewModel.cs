using Microsoft.EntityFrameworkCore;
using Praktice.Domain.Entities;
using Praktice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Praktice.Presentation.ViewModels
{
    public class ParentWindowViewModel : ViewModelBase
    {
        private List<Pupil> _class = new List<Pupil>();
        private Pupil _loginnedPupil;
        private Teacher _classroomTeacher;
        private string _teacherName;
        private List<Club> _clubs;
        private List<Schedule> _schedules;
        private List<Marks> _algebraMarks = new List<Marks>();
        private List<Marks> _russianLanguageMarks = new List<Marks>();
        private List<Marks> _literatureMarks = new List<Marks>();
        private List<Marks> _informaticMarks = new List<Marks>();
        private List<Marks> _physicsMarks = new List<Marks>();
        private List<Marks> _chemistryMarks = new List<Marks>();
        private List<Marks> _biologyMarks = new List<Marks>();
        private List<Marks> _physicalCultureMarks = new List<Marks>();
        private Club _selectedClub;
        private Club _loginnedPupilClub;
        private TabItem _myClubTabItem;
        private Parent _loginnedParent;

        public List<Pupil> Class { get => _class; set => Set(ref _class, value, nameof(Class)); }
        public Parent LoginnedParent { get => _loginnedParent; set => Set(ref _loginnedParent , value,nameof(LoginnedParent)); }
        public Pupil Kid { get => _loginnedPupil; set => Set(ref _loginnedPupil, value, nameof(Kid)); }
        public Teacher ClassroomTeacher { get => _classroomTeacher; set => Set(ref _classroomTeacher, value, nameof(ClassroomTeacher)); }
        public string TeacherName { get => _teacherName; set => Set(ref _teacherName, value, nameof(TeacherName)); }
        public List<Club> Clubs { get => _clubs; set => Set(ref _clubs, value, nameof(Clubs)); }
        public List<Schedule> Schedules { get => _schedules; set => Set(ref _schedules, value, nameof(Schedules)); }
        public List<Marks> AlgebraMarks { get => _algebraMarks; set => Set(ref _algebraMarks, value, nameof(AlgebraMarks)); }
        public List<Marks> RussianLanguageMarks { get => _russianLanguageMarks; set => Set(ref _russianLanguageMarks, value, nameof(RussianLanguageMarks)); }
        public List<Marks> LiteratureMarks { get => _literatureMarks; set => Set(ref _literatureMarks, value, nameof(LiteratureMarks)); }
        public List<Marks> InformaticMarks { get => _informaticMarks; set => Set(ref _informaticMarks, value, nameof(InformaticMarks)); }
        public List<Marks> PhysicsMarks { get => _physicsMarks; set => Set(ref _physicsMarks, value, nameof(PhysicsMarks)); }
        public List<Marks> ChemistryMarks { get => _chemistryMarks; set => Set(ref _chemistryMarks, value, nameof(ChemistryMarks)); }
        public List<Marks> BiologyMarks { get => _biologyMarks; set => Set(ref _biologyMarks, value, nameof(BiologyMarks)); }
        public List<Marks> PhysicalCultureMarks { get => _physicalCultureMarks; set => Set(ref _physicalCultureMarks, value, nameof(PhysicalCultureMarks)); }
        public Club KidClub { get => _loginnedPupilClub; set => Set(ref _loginnedPupilClub, value, nameof(KidClub)); }
        public Club SelectedClub { get => _selectedClub; set => Set(ref _selectedClub, value, nameof(SelectedClub)); }
        public TabItem MyClubTabItem { get => _myClubTabItem; set => Set(ref _myClubTabItem, value, nameof(MyClubTabItem)); }


        public ParentWindowViewModel()
        {
            using (var context = new ApplicationDbContext())
            {
                Clubs = context.Clubs
                .Include(c => c.Pupils)
                .Include(c => c.LeaderNavigation)
                .ToList();
            }
        }
        public void FillPropertys()
        {
            using (var context = new ApplicationDbContext())
            {
                Kid = context.Pupils
                    .Include(p => p.ClassNavigation)
                    .ThenInclude(c => c.CuratorNavigation)
                    .FirstOrDefault(p => p.Mother == LoginnedParent.Id || p.Father == LoginnedParent.Id || p.Caretaker == LoginnedParent.Id);

                if (Kid.Club != null)
                    KidClub = context.Clubs
                        .FirstOrDefault(c => c.Id == Kid.Club);

                List<Pupil> pupils = context.Pupils
                    .ToList();

                foreach (var pupil in pupils)
                {
                    if (pupil.Class == Kid.Class)
                    {
                        if (pupil.Id != Kid.Id)
                            Class.Add(pupil);
                    }
                }

                ClassroomTeacher = Kid.ClassNavigation.CuratorNavigation;
                TeacherName = $"Классный руководитель: {ClassroomTeacher.FullName}";

                Schedules = context.Schedules
                    .Include(s => s.MondayNavigation)
                    .Include(s => s.TuesdayNavigation)
                    .Include(s => s.WednesdayNavigation)
                    .Include(s => s.ThursdayNavigation)
                    .Include(s => s.FridayNavigation)
                    .Include(s => s.SaturdayNavigation)
                    .Where(s => s.NumberClass == Kid.Class)
                    .ToList();


                AlgebraMarks = FillDisciplinesMarks(1);
                RussianLanguageMarks = FillDisciplinesMarks(2);
                LiteratureMarks = FillDisciplinesMarks(3);
                InformaticMarks = FillDisciplinesMarks(4);
                PhysicsMarks = FillDisciplinesMarks(5);
                ChemistryMarks = FillDisciplinesMarks(6);
                BiologyMarks = FillDisciplinesMarks(7);
                PhysicalCultureMarks = FillDisciplinesMarks(8);
            }
        }
        public void CheckClub(ref TabItem tabItem)
        {

            if (KidClub != null)
                tabItem.Visibility = Visibility.Visible;
            else
                tabItem.Visibility = Visibility.Hidden;

        }
        public void FillDataGrid(int disciplineId, ref DataGrid dataGrid, List<Marks> listForFilling)
        {
            dataGrid.ItemsSource = listForFilling;

            for (int i = 0; i < Kid.AcademicPerfomances.Where(ap => ap.Discipline == disciplineId).ToList().Count; i++)
            {
                DataGridTextColumn markTextColumn = new DataGridTextColumn();
                markTextColumn.Header = listForFilling[i].Header;
                markTextColumn.Binding = new Binding($"Mark[{i}]");
                dataGrid.Columns.Add(markTextColumn);
            }

            Marks marks = listForFilling[0];
            listForFilling.Clear();
            listForFilling.Add(marks);

            DataGridTextColumn averageMarkTextColumn = new DataGridTextColumn();
            averageMarkTextColumn.Header = "Средний балл";
            averageMarkTextColumn.Binding = new Binding($"AverageMark");
            dataGrid.Columns.Add(averageMarkTextColumn);

        }
        public List<Marks> FillDisciplinesMarks(int disciplineId)
        {
            List<decimal> marks = new List<decimal>();
            List<string> headers = new List<string>();
            List<Marks> fillingList = new List<Marks>();

            foreach (var item in Kid.AcademicPerfomances.Where(ap => ap.Discipline == disciplineId).OrderBy(ap => ap.Date).ToList())
            {
                headers.Add(item.Date.ToString().Substring(0, 10));
                marks.Add(Convert.ToDecimal(item.Mark));
            }

            for (int i = 0; i < Kid.AcademicPerfomances.Where(ap => ap.Discipline == disciplineId).OrderBy(ap => ap.Date).ToList().Count; i++)
                fillingList.Add(new Marks(headers[i], marks));

            return fillingList;
        }
    }
}
