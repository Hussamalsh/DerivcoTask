using StudentDashboard.Common;
using StudentDashboard.Models;
using StudentDashboard.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StudentDashboard.Students
{
    public class StudentListViewModel : ViewModelBaseClass
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

        private Student selectedperson;
        public Student SelectedPerson
        {
            get
            {
                return selectedperson;
            }
            set
            {
                if (value != null && value.RowKey != null)
                {
                    selectedperson = studentService.GetStudent(value.RowKey).Result;
                }
                else {
                    selectedperson = value;
                }
                RaisePropertyChanged("SelectedPerson");
            }
        }


        public ObservableCollection<Student> studentList { get; set; }
        
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
                    RaisePropertyChanged("StudentListEditMode");
                }
            }
        }

        public IStudentService studentService;
        
        public StudentListViewModel(IStudentService studentService)
        {
            this.studentService = studentService;//new StudentService(new DefaultHttpClientAccessor());

            studentList = new ObservableCollection<Student>();

            _commands = new CommandMap();
            _commands.AddCommand("Add", x => Add(), x => CanAdd()/*!CanSave()*/);

            _commands.AddCommand("Save", x => Save(), x => CanSave());
            _commands.AddCommand("Cancel", x => Cancel(), x => CanSave());
            _commands.AddCommand("Delete", x => Delete(), x => CanAdd()/*!CanSave()*/);
            _commands.AddCommand("Close", x => Close(), x => CanAdd()/*!CanSave()*/);
            _commands.AddCommand("New", x => New(), x => CanNew()/*!CanSave()*/);
            LoadDataAsync();

            StudentListEditMode = EditMode.Update;
        }

        private void LoadDataAsync()
        {
            studentList.Clear();
            var query = studentService.GetStudents().Result;
            foreach (Student s in query)
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
            if (SelectedPerson != null)
            {
                var addedStudent = studentService.AddStudent(SelectedPerson).Result;
                studentList.Add(addedStudent);
                SelectedPerson = addedStudent;
                RecordCount = studentList.Count;
                StudentListEditMode = EditMode.Update;
            }
            else
            {
                MessageBox.Show("Couldn't add the student", "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Question);
            }
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
                studentService.deleteStudent(SelectedPerson.RowKey).ConfigureAwait(false);
                Student item = this.studentList.FirstOrDefault(x => x.RowKey == selectedperson.RowKey);
                int index = this.studentList.IndexOf(item);

                studentList.RemoveAt(index);

                SelectedPerson = studentList[0];
                RecordCount = studentList.Count;
            }
        }

        void Save()
        {
            if (studentList.Count > 0)
            {
               _ = studentService.UpdateStudent(selectedperson).Result;
                Student item = this.studentList.FirstOrDefault(x => x.RowKey == selectedperson.RowKey);
                int index = this.studentList.IndexOf(item);
                this.studentList[index] = selectedperson;
            }
            RecordCount = studentList.Count;
        }

        bool CanSave()
        {
            return true;
        }

        void New()
        {
            SelectedPerson = new Student();
            StudentListEditMode = EditMode.Create;
        }
        private bool CanNew()
        {
            return (true);
        }

        void Cancel()
        {
            LoadDataAsync();
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
