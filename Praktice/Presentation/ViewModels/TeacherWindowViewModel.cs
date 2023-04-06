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
    public class TeacherWindowViewModel : ViewModelBase
    {
        private List<Pupil> _class = new List<Pupil>();
        private Teacher _classroomTeacher;
        private List<Club> _clubs;
        private List<Schedule> _sixASchedules;
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
        private Teacher _loginnedParent;
        private List<Schedule> _sevenBSchedules;
        private List<Schedule> _eightVSchedules;
        private List<Schedule> _nineGSchedules;

        public List<Pupil> Class { get => _class; set => Set(ref _class, value, nameof(Class)); }
        public Teacher LoginnedTeacher { get => _loginnedParent; set => Set(ref _loginnedParent, value, nameof(LoginnedTeacher)); }
        public Teacher ClassroomTeacher { get => _classroomTeacher; set => Set(ref _classroomTeacher, value, nameof(ClassroomTeacher)); }
        public List<Club> Clubs { get => _clubs; set => Set(ref _clubs, value, nameof(Clubs)); }
        public List<Schedule> SixASchedules { get => _sixASchedules; set => Set(ref _sixASchedules, value, nameof(SixASchedules)); }
        public List<Schedule> SevenBSchedules { get => _sevenBSchedules; set => Set(ref _sevenBSchedules , value,nameof(SevenBSchedules)); }
        public List<Schedule> EightVSchedules { get => _eightVSchedules; set => Set(ref _eightVSchedules , value,nameof(EightVSchedules)); }
        public List<Schedule> NineGSchedules { get => _nineGSchedules; set => Set(ref _nineGSchedules , value,nameof(NineGSchedules)); }
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


        public TeacherWindowViewModel()
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
                List<Pupil> pupils = context.Pupils
                    .ToList();

                foreach (var pupil in pupils)
                {
                    if (pupil.Class == new ApplicationDbContext().Classes.FirstOrDefault(c=>c.Curator==LoginnedTeacher.Id).Id)
                    {
                            Class.Add(pupil);
                    }
                }

                SixASchedules = FillScedule(1);
                SevenBSchedules = FillScedule(2);
                EightVSchedules = FillScedule(3);
                NineGSchedules = FillScedule(4);

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
        public List<Schedule> FillScedule(int classId)
        {
            return  new ApplicationDbContext().Schedules
                .Include(s => s.MondayNavigation)
                .Include(s => s.TuesdayNavigation)
                .Include(s => s.WednesdayNavigation)
                .Include(s => s.ThursdayNavigation)
                .Include(s => s.FridayNavigation)
                .Include(s => s.SaturdayNavigation)
                .Where(s => s.NumberClass == classId)
                .ToList();
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

            if (disciplineId == new ApplicationDbContext().Disciplines.FirstOrDefault(d => d.Teacher == LoginnedTeacher.Id).Id)
                dataGrid.IsReadOnly = false;

            DataGridTextColumn nameTextColumn = new DataGridTextColumn();
            nameTextColumn.Width = 250;
            nameTextColumn.Header = "Имя";
            nameTextColumn.Binding = new Binding($"Name");
            dataGrid.Columns.Add(nameTextColumn);

            int datesWithMarksCount = 0;
            List<string> dates = new List<string>();

            for (int i = 0; i <listForFilling.Count; i++)
            {
                if (!dates.Contains(listForFilling[i].Header))
                    dates.Add(listForFilling[i].Header);
            }

            string stringMem = "";

            for (int i = 0; i < dates.Count-1; i++)
            {
                for (int j = i+1; j < dates.Count; j++)
                {
                    if(Convert.ToDateTime(dates[i])>Convert.ToDateTime(dates[j]))
                    {
                        stringMem = dates[i];
                        dates[i] = dates[j];
                        dates[j] = stringMem;
                    }
                }
            }

            for (int i = 0; i < Class.Count; i++)
            {
                int intMem = 0;
                foreach (var academicPerfomance in Class[i].AcademicPerfomances.Where(ap => ap.Discipline == disciplineId).OrderBy(ap => ap.Date).ToList())
                {
                    intMem++;
                    if (intMem > datesWithMarksCount)
                        datesWithMarksCount = intMem;
                }
            }


            for (int i = 0; i <datesWithMarksCount ; i++)
            {
                DataGridTextColumn markTextColumn = new DataGridTextColumn();
                markTextColumn.Header = dates[i];
                markTextColumn.Binding = new Binding($"Mark[{i}]");
                dataGrid.Columns.Add(markTextColumn);
            }

            List<Marks> allPupilMarks = new List<Marks>();

            for (int i = 0; i < listForFilling.Count; i++)
            {
                if (i % 3 == 0)
                    allPupilMarks.Add(listForFilling[i]);
            }

            listForFilling.Clear();
            foreach (Marks mark in allPupilMarks)
                listForFilling.Add(mark);

            DataGridTextColumn averageMarkTextColumn = new DataGridTextColumn();
            averageMarkTextColumn.Header = "Средний балл";
            averageMarkTextColumn.Binding = new Binding($"AverageMark");
            dataGrid.Columns.Add(averageMarkTextColumn);

        }
        public List<Marks> FillDisciplinesMarks(int disciplineId)
        {
            List<Marks> fillingList = new List<Marks>();

            for (int i = 0; i < Class.Count; i++)
            {
                List<string> names = new List<string>();
                List<decimal> marks = new List<decimal>();
                List<string> headers = new List<string>();

                foreach (var academicPerfomance in Class[i].AcademicPerfomances.Where(ap => ap.Discipline == disciplineId).OrderBy(ap => ap.Date).ToList())
                {
                    names.Add(academicPerfomance.PupilNavigation.NormalFullName);
                    headers.Add(academicPerfomance.Date.ToString().Substring(0, 10));
                    marks.Add(Convert.ToDecimal(academicPerfomance.Mark));
                }

                for (int j = 0; j < Class[i].AcademicPerfomances.Where(ap => ap.Discipline == disciplineId).OrderBy(ap => ap.Date).ToList().Count; j++)
                    fillingList.Add(new Marks(names[j],headers[j], marks));
            }

            return fillingList;
        }
    }
}
