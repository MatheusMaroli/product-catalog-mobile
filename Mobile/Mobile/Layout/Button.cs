using System;
using Xamarin.Forms;

namespace Mobile.Layout
{
    public class Button : Xamarin.Forms.Button
    {
        public static BindableProperty ColorButtonProperty = BindableProperty.Create(nameof(ColorButton), typeof(AppColor), typeof(Button),
             AppColor.bPrimary, BindingMode.TwoWay, propertyChanged: OnChangePaint);

        public static BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(AppSize), typeof(Button),
             AppSize.bMd, BindingMode.TwoWay, propertyChanged: OnChangeSize);

        public static BindableProperty IconProperty = BindableProperty.Create(nameof(Size), typeof(string), typeof(Button),
            string.Empty, BindingMode.TwoWay, propertyChanged: OnChangeIcon);


        private string _icon;
        public string  Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                SetIcon();
            }
        }
        private AppSize _size;
        public AppSize Size
        {
            get => _size;
            set
            {
                _size = value;
                Resize();
            }
        }

        private AppColor _colorButton;
        public AppColor ColorButton {
            get => _colorButton;
            set
            {
                _colorButton = value;
                Paint();
            }
        }

        public Button() : base()
        {
            PaintPrimary();
            BorderWidth = 1;
            CornerRadius = 2;
            Padding = new Thickness(10, 10, 10, 10);
        }        

        private static void OnChangePaint(BindableObject bindable, object oldValue, object newValue)
        {
            if ((AppSize)oldValue != (AppSize)newValue)
                ((Button)bindable).ColorButton = (AppColor)newValue;
        }
        private static void OnChangeSize(BindableObject bindable, object oldValue, object newValue)
        {
            if ((AppSize)oldValue != (AppSize)newValue)
                ((Button)bindable).Size = (AppSize)newValue;
        }


        private static void OnChangeIcon(BindableObject bindable, object oldValue, object newValue)
        {

            if ((string)oldValue != (string)newValue)
                ((Button)bindable).Icon = (string)newValue;
        }


        private void PaintSuccess()
        {
            TextColor = Color.White;
            BackgroundColor = Colors.ColorSuccess;
        }
        private void PaintPrimary()
        {
            TextColor = Color.White;
            BackgroundColor = Colors.ColorPrimary;
        }
        private void PaintInfo()
        {
            TextColor = Color.White;
            BackgroundColor = Colors.ColorInfo;
        }
        private void PaintDanger()
        {
            TextColor = Color.White;
            BackgroundColor = Colors.ColorDanger;

        }
        private void PaintWarning()
        {
            TextColor = Color.White;
            BackgroundColor = Colors.ColorWarning;

        }
        private void PaintDefault()
        {
            TextColor = Color.Black;
            BackgroundColor = Color.LightSlateGray;

        }
        public void Paint()
        {
            if (_colorButton == AppColor.bSuccess)
                PaintSuccess();
            else if (_colorButton == AppColor.bPrimary)
                PaintPrimary();
            else if (_colorButton == AppColor.bInfo)
                PaintInfo();
            else if (_colorButton == AppColor.bDanger)
                PaintDanger();
            else if (_colorButton == AppColor.bWarning)
                PaintWarning();
            else if (_colorButton == AppColor.bDefault)
                PaintDefault();
            else throw new System.Exception("BootstrapColor with invalid value");
        }


        public void Resize()
        {
            if (_size == AppSize.bSm)
                ResizeSm();
            else if (_size == AppSize.bMd)
                ResizeMd();
            else throw new System.Exception("ButtonBootstrap Size implement to Sm and Md");
        }

        private void ResizeSm()
        {
            HeightRequest = 40;
            FontSize = 12;
        }

        private void ResizeMd()
        {
            HeightRequest = 50;
            FontSize = 16;
        }

        public void  SetIcon()
        {
            ImageSource = ImageSource.FromFile(_icon); // FromStream(() => new MemoryStream(_productImagebyte));
        }


    }
}
