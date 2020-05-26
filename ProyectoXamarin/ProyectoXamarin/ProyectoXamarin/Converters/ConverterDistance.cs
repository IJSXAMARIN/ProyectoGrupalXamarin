using ProyectoXamarin.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProyectoXamarin.Converters
{
    public class ConverterDistance : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int distance = (int)value;


            if(distance == -1) {
                return "";
            } 

            if(distance > 1000) 
            {

                return "Debe estar a menos de 1Km para poder visitarlo.";
            }

            if(distance <= 1000)
            {
                return "Visitado!!";
            }

            return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
