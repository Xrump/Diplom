using Praktice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktice.Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public bool Authorization(Account account)
        {
            switch (account.Role)
            {
                default:
                    return false;
                    break;
                //Teacher
                case 1:
                    TeacherWindow teacherWindow = new TeacherWindow(account);
                    teacherWindow.Show();
                    return true;
                    break;
                //Administration
                case 2:
                    return true;
                    break;
                //Pupil
                case 3:
                    PupilWindow pupilWindow = new PupilWindow(account);
                    pupilWindow.Show();
                    return true;
                    break;
                //Parents
                case 4:
                    ParentWindow parentWindow = new ParentWindow(account);
                    parentWindow.Show();
                    return true;
                    break;
            }
        }
    }
}
