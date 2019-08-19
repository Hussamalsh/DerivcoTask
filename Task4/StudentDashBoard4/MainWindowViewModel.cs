using StudentDashBoard4;
using StudentDashBoard4.Students;

namespace StudentDashBoard4
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
