using Student.Data.Models;
using Student.Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentDashboard.ViewModels
{
    public class StudentViewModel : ViewModelBaseClass
    {
        public Window CallingForm; // Used to close the form. Not a property, just a field.

        private int recordcount;
        public int RecordCount
        {
            get { return recordcount; }
            set
            {
                recordcount = value;
                RaisePropertyChanged("RecordCount");
            }
        }

        private Student.Data.Models.Student selectedperson;
        public Student.Data.Models.Student SelectedPerson
        {
            get { return selectedperson; }
            set
            {
                selectedperson = value;
                RaisePropertyChanged("SelectedPerson");
            }
        }


        public ObservableCollection<Student.Data.Models.Student> studentList { get; set; }
        public IStudentsRepository studentsRepository;

        public CommandMap _commands;
        public CommandMap Commands
        {
            get
            {
                return _commands;
            }
        }

        private EditMode _StudentListEditMode;
        public EditMode StudentListEditMode
        {
            get { return _StudentListEditMode; }
            set
            {
                if (_StudentListEditMode != value)
                {
                    _StudentListEditMode = value;
                    RaisePropertyChanged("TodoListEditMode");
                }
            }
        }

        public StudentViewModel()
        {
            studentList = new ObservableCollection<Student.Data.Models.Student>();
            studentsRepository = new StudentsRepository();

            _commands = new CommandMap();
            _commands.AddCommand("Add", x => Add(), x => CanAdd()/*!CanSave()*/);

            _commands.AddCommand("Save", x => Save(), x => CanSave());
            _commands.AddCommand("Cancel", x => Cancel(), x => CanSave());
            _commands.AddCommand("Delete", x => Delete(), x => CanAdd()/*!CanSave()*/);
            _commands.AddCommand("Close", x => Close(), x => CanAdd()/*!CanSave()*/);
            _commands.AddCommand("New", x => New(), x => CanNew()/*!CanSave()*/);
            LoadDataAsync().Wait();

            StudentListEditMode = EditMode.Update;
        }

        private async Task LoadDataAsync()
        {
            studentList.Clear();
            var query = await studentsRepository.GetStudentsAsync();
            foreach (Student.Data.Models.Student s in query)
            {
                studentList.Add(s);
            }

            RecordCount = studentList.Count;
            if (studentList.Count > 0)
            {
                SelectedPerson = studentList[0];
            }
        }

        //This goes in Initialization/constructor
        void Add()
        {
            var addedStudent = studentsRepository.AddStudentAsync(SelectedPerson).Result;
            studentList.Add(addedStudent);
            SelectedPerson = addedStudent;
            RecordCount = studentList.Count;
            StudentListEditMode = EditMode.Update;
        }
        private bool CanAdd()
        {
            return (true);
        }
        void Delete()
        {
            if (MessageBox.Show
              ("Delete selected row?",
              "Not undoable", MessageBoxButton.YesNo,
              MessageBoxImage.Question) == MessageBoxResult.Yes && studentList.Count > 0)
            {
                studentsRepository.DeleteStudent(SelectedPerson.ID);
                studentList.Remove(SelectedPerson);
                SelectedPerson = studentList[0];
                RecordCount = studentList.Count;
            }
        }

        void Save()
        {
            if (studentList.Count > 0)
            {
                _ = studentsRepository.UpdateStudentAsync(SelectedPerson).Result;
            }
            RecordCount = studentList.Count;
        }

        bool CanSave()
        {
            return true;
        }

        void New()
        {
            SelectedPerson = new Student.Data.Models.Student();
            StudentListEditMode = EditMode.Create;
        }
        private bool CanNew()
        {
            return (true);
        }

        void Cancel()
        {
            LoadDataAsync().Wait();
            StudentListEditMode = EditMode.Update;
        }

        void Close()
        {
            if (CallingForm == null)
            {
                MessageBox.Show("Callingform wasn't assigned in codebehind");
            }
            else
            {
                CallingForm.Close();
            }
        }
    }
}
