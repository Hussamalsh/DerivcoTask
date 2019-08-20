using StudentDashboard.Models;
using StudentDashboard.Services;
using StudentDashboard.Students;
using Unity;

namespace StudentDashboard
{
    public class MainWindowViewModel : ViewModelBaseClass
    {
        public object CurrentViewModel { get; set; }
        private StudentListViewModel studentListViewModel;

        public MainWindowViewModel()
        {
            studentListViewModel = ContainerHelper.Container.Resolve<StudentListViewModel>();
            CurrentViewModel = studentListViewModel;  // new StudentListViewModel();
        }
    }
}
