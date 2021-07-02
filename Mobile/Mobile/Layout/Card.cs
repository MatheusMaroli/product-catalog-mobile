using Xamarin.Forms;

namespace Mobile.Layout
{
    public class Card : Frame
    {
        public static BindableProperty ColorCardProperty = BindableProperty.Create(nameof(ColorCard), typeof(AppColor), typeof(Button),
        AppColor.bPrimary, BindingMode.TwoWay, propertyChanged: OnChangePaint);



        private AppColor _colorCard;
        public AppColor ColorCard
        {
            get => _colorCard;
            set
            {
                _colorCard = value;
                Paint();
            }
        }

        public Card() : base()
        {
            CornerRadius = 2;
            Padding = new Thickness(10, 5, 10, 5);
            PaintPrimary();
        }

        private void PaintSuccess()
        {
            
            BorderColor = Colors.ColorSuccess;
        }
        private void PaintPrimary()
        {
            BorderColor = Colors.ColorPrimary;
        }
        private void PaintInfo()
        {
            BorderColor = Colors.ColorInfo;
        }
        private void PaintDanger()
        {
            BorderColor = Colors.ColorDanger;

        }
        private void PaintWarning()
        {
            BorderColor = Colors.ColorWarning;

        }
        private void PaintDefault()
        {
            BorderColor = Colors.ColorBackground;

        }

        public void Paint()
        {
            if (_colorCard == AppColor.bSuccess)
                PaintSuccess();
            else if (_colorCard == AppColor.bPrimary)
                PaintPrimary();
            else if (_colorCard == AppColor.bInfo)
                PaintInfo();
            else if (_colorCard == AppColor.bDanger)
                PaintDanger();
            else if (_colorCard == AppColor.bWarning)
                PaintWarning();
            else if (_colorCard == AppColor.bDefault)
                PaintDefault();
            else throw new System.Exception("BootstrapColor with invalid value");
        }

        private static void OnChangePaint(BindableObject bindable, object oldValue, object newValue)
        {
            if ((AppSize)oldValue != (AppSize)newValue)
                ((Card)bindable).ColorCard = (AppColor)newValue;
        }

    }
}
