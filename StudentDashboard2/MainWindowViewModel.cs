using Student.Data.Models;
using StudentDashboard2.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDashboard2
{
    public class MainWindowViewModel : ViewModelBaseClass
    {
        public object CurrentViewModel { get; set; }
        public MainWindowViewModel()
        {
            CurrentViewModel = new StudentListViewModel();
        }
    }
}
