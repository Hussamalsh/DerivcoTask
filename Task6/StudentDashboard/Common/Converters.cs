using StudentDashboard.Models;
using System;
using System.Windows;
using System.Windows.Data;

namespace StudentDashboard.Common
{
    public class EditModeToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a boolean");

            EditMode mode = (EditMode)Enum.Parse(typeof(EditMode), parameter.ToString());
            if (((EditMode)value).Equals(mode))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
