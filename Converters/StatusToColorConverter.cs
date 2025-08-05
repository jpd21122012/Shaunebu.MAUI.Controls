using Shaunebu.Controls.Models;
using System.Globalization;

namespace Shaunebu.Controls.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is KanbanStatus status)
            {
                return status switch
                {
                    KanbanStatus.Todo => Color.FromArgb("#FF5722"),
                    KanbanStatus.InProgress => Color.FromArgb("#2196F3"),
                    KanbanStatus.Review => Color.FromArgb("#FFC107"),
                    KanbanStatus.Done => Color.FromArgb("#4CAF50"),
                    _ => Colors.Gray
                };
            }
            return Colors.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
