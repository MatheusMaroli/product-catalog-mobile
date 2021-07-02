using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseView : ContentPage
    {


        public static BindableProperty AppearingCommandProperty = BindableProperty.Create(nameof(AppearingCommand), typeof(ICommand), typeof(BaseView), null, BindingMode.TwoWay);

        public ICommand AppearingCommand
        {
            get => (ICommand)this.GetValue(AppearingCommandProperty);
            set => this.SetValue(AppearingCommandProperty, value);
        }

        public static BindableProperty DisappearingCommandProperty = BindableProperty.Create(nameof(DisappearingCommand), typeof(ICommand), typeof(BaseView), null, BindingMode.TwoWay);

        public ICommand DisappearingCommand
        {
            get => (ICommand)this.GetValue(DisappearingCommandProperty);
            set => this.SetValue(DisappearingCommandProperty, value);
        }

        public View Body
        {
            get => BodyContent.Content;
            set => BodyContent.Content = value;
        }

        public BaseView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            AppearingCommand?.Execute(null);

        }
        protected override void OnDisappearing()
        {
            DisappearingCommand?.Execute(null);

        }
    }
}