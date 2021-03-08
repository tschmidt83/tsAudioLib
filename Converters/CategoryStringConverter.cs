using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using tsAudioLib.Categories;

namespace tsAudioLib.Converters
{
    /// <summary>
    /// Converts a category type to a string. The way back is not used because of localization.
    /// </summary>
    [ValueConversion(typeof(CategoryType), typeof(string))]
    public class CategoryStringConverter : IValueConverter
    {
        /// <summary>
        /// Convert category to string
        /// </summary>
        /// <param name="value">Input category</param>
        /// <param name="targetType">Target type of conversion, only string implemented right now</param>
        /// <param name="parameter">Converter parameter, will not be used</param>
        /// <param name="culture">Not used, but might be used for localization</param>
        /// <returns>Converted string</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CategoryType cat = (CategoryType)value;
            string s = string.Empty;

            switch(cat)
            {
                case CategoryType.Sample: s = "Sample"; break;
                case CategoryType.Effect: s = "Effekt"; break;
                case CategoryType.Atmo: s = "Atmo"; break;
                case CategoryType.Music: s = "Musik"; break;
                default: break;
            }

            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
