﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace Prestart.ValueConverters
{
    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color labelBckColor = Color.Transparent;
            if (value != null)
            {
                string label = value.ToString();
                switch (label)
                {
                    case "LOW":
                        labelBckColor = Color.Green;
                        break;
                    case "MEDIUM":
                        labelBckColor = Color.Yellow;
                        break;
                    case "HIGH":
                        labelBckColor = Color.Red;
                        break;
                    case "EXTREME":
                        labelBckColor = Color.Black;
                        break;
                }
            }

            return labelBckColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
