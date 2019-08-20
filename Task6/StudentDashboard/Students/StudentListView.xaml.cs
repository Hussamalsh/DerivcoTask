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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentDashboard.Students
{
    /// <summary>
    /// Interaction logic for StudentListView.xaml
    /// </summary>
    public partial class StudentListView : UserControl
    {
        public StudentListView()
        {
            InitializeComponent();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "RowKey" || e.Column.Header.ToString() == "PartitionKey"
                || e.Column.Header.ToString() == "Timestamp" || e.Column.Header.ToString() == "ETag")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] headings = { "Hidden", "First", "Last", "Street Address", "City", "ST", "ZIP code", "Tel" };
            for (int I = 0; I < headings.Length; I++)
            {
                grid.Columns[I].Header = headings[I];
            }
        }
    }
}
