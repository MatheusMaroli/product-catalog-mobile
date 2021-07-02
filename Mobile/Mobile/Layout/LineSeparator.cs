using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mobile.Layout
{
    public class LineSeparator : Frame
    {
        public static BindableProperty ColorSeparatorProperty = BindableProperty.Create(nameof(ColorSeparator), typeof(Color), typeof(LineSeparator),
        Colors.ColorPrimary, BindingMode.TwoWay, propertyChanged: OnChangeColor);

        private Color _colorSeparator;
        public Color ColorSeparator
        {
            get => _colorSeparator;
            set
            {
                _colorSeparator = value;
                Paint();
            }
        }

        private static void OnChangeColor(BindableObject bindable, object oldValue, object newValue)
        {
            ((LineSeparator)bindable).ColorSeparator = (Color)newValue;
        }

        public LineSeparator() : base()
        {
            Padding = 0;
            HeightRequest = 1;
        }

        private void Paint()
        {
            BackgroundColor = _colorSeparator;
        }
    }
}
